using Glitch.CodeAnalysis.Builders;
using Glitch.Functional.Collections;
using Glitch.Functional.Extensions;
using Glitch.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Glitch.CodeAnalysis
{
    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "This is intentionally an instance wrapper around a static class")]
    [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "Intentionally being suppressed at the class level")]
    public static partial class CSharpSyntax
    {
        public static TypeSyntax TypeName(Type type) => ParseTypeName(type.Signature());

        public static BaseTypeDeclarationSyntax TypeDeclaration(Type type)
        {
            var modifiers = new TokenListBuilder();

            modifiers
                .AddIf(type.IsPublic, SyntaxKind.PublicKeyword)
                .AddIf(type.IsNestedPrivate || type.IsNestedFamANDAssem, SyntaxKind.PrivateKeyword)
                .AddIf(type.IsNestedFamily || type.IsNestedFamANDAssem || type.IsNestedFamORAssem, SyntaxKind.ProtectedKeyword)
                .AddIf(type.IsNestedFamily || type.IsNestedFamORAssem, SyntaxKind.InternalKeyword);

            modifiers.AddIf(type.IsAbstract, SyntaxKind.AbstractKeyword)
                     .AddIf(type.IsSealed, SyntaxKind.SealedKeyword);

            // TODO Static classes
            // TODO Attributes
            var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
            var fields = type.GetFields(flags);

            if (type.IsEnum)
            {
                return EnumDeclaration(type.Name)
                    .WithModifiers(modifiers.ToTokenList())
                    .WithMembers(
                        SeparatedList(
                            Token(SyntaxKind.CommaToken)
                                .WithTrailingTrivia(CarriageReturnLineFeed),
                            fields.Select(f => EnumMemberDeclaration(f.Name))));
            }

            var properties = type.GetProperties(flags).Select(PropertyDeclaration);
            var constructors = type.GetConstructors(flags).Select(ConstructorDeclaration);
            var methods = type.GetMethods(flags).Where(m => !m.IsSpecialName).Select(MethodDeclaration);

            IEnumerable<MemberDeclarationSyntax> members = 
                [
                    ..fields.Select(FieldDeclaration),
                    ..constructors,
                    ..properties,
                    ..methods,
                ];


            TypeDeclarationSyntax declaration = type.IsValueType ? StructDeclaration(type.Name) : ClassDeclaration(type.Name);

            return declaration
                .WithModifiers(modifiers.ToTokenList())
                .WithMembers(List(members));
        }

        public static FieldDeclarationSyntax FieldDeclaration(FieldInfo field)
        {
            var modifiers = new TokenListBuilder();

            modifiers
                .AddIf(field.IsPublic, SyntaxKind.PublicKeyword)
                .AddIf(field.IsPrivate || field.IsFamilyAndAssembly, SyntaxKind.PrivateKeyword)
                .AddIf(field.IsFamily || field.IsFamilyAndAssembly || field.IsFamilyOrAssembly, SyntaxKind.ProtectedKeyword)
                .AddIf(field.IsAssembly || field.IsFamilyOrAssembly, SyntaxKind.InternalKeyword)
                .AddIf(field.IsStatic, SyntaxKind.StaticKeyword)
                .AddIf(field.IsInitOnly, SyntaxKind.ReadOnlyKeyword); // TODO Constants

            return FieldDeclaration(
                VariableDeclaration(
                    TypeName(field.FieldType),
                    field.Name))
                .WithModifiers(modifiers.ToTokenList());
        }

        public static PropertyDeclarationSyntax PropertyDeclaration(PropertyInfo property)
        {
            var getMethod = Option.Maybe(property.GetMethod);
            var setMethod = Option.Maybe(property.SetMethod);

            var method = getMethod.Or(setMethod)
                .IfNone(_ => throw new ArgumentException("Property must provide accessors"));

            var modifiers = GetMethodModifiers(method);

            // TODO Indexers

            var setterModifiers = getMethod.Zip(setMethod)
                .Select(e => new
                {
                    Get = new
                    {
                        Method = e.Item1,
                        Access = e.Item1.Attributes & MethodAttributes.MemberAccessMask,
                    },
                    Set = new
                    {
                        Method = e.Item2,
                        Access = e.Item2.Attributes & MethodAttributes.MemberAccessMask,
                    }
                })
                .Where(e => e.Get.Access > e.Set.Access)
                .Select(e => GetAccessModifiers(e.Set.Method))
                .Select(e => e.ToTokenList());

            var getAccessor = getMethod.Select(_ => AccessorDeclaration(SyntaxKind.GetAccessorDeclaration));
            var setAccessor = setMethod.Select(_ => AccessorDeclaration(SyntaxKind.SetAccessorDeclaration))
                                       .Select(s => setterModifiers.Select(m => s.WithModifiers(m)).IfNone(s));

            var accessors = Sequence.Of(getAccessor, setAccessor)
                .Somes()
                .Select(a => a.WithSemicolonToken(
                    Token(SyntaxKind.SemicolonToken)
                        .WithTrailingTrivia(Space)))
                .Select(a => a.WithKeyword(a.Keyword)
                    .WithLeadingTrivia(Space));

            return PropertyDeclaration(TypeName(property.PropertyType), property.Name)
                .WithModifiers(modifiers.ToTokenList())
                .WithAccessorList(
                    AccessorList(
                        List(accessors)));
        }

        public static ConstructorDeclarationSyntax ConstructorDeclaration(ConstructorInfo constructor)
        {
            ArgumentNullException.ThrowIfNull(constructor.DeclaringType);

            var modifiers = constructor.IsStatic
                          ? new TokenListBuilder().Add(SyntaxKind.StaticKeyword)
                          : GetAccessModifiers(constructor);

            var identifier = Identifier(constructor.DeclaringType.Name);

            var parameters = constructor.GetParameters()
                .Select(p => new
                {
                    Name = Identifier(p.Name ?? $"p{p.Position}"),
                    Type = ParseTypeName(p.ParameterType.Signature()),
                    // TODO Default values
                });

            // TODO Generics
            return ConstructorDeclaration(identifier)
                .WithModifiers(modifiers.ToTokenList())
                .WithParameterList(
                    ParameterList(
                        SeparatedList(Token(SyntaxKind.CommaToken), // TODO Parameter modifiers
                            parameters.Select(p =>
                            Parameter(p.Type, p.Name)))))
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        }

        public static MethodDeclarationSyntax MethodDeclaration(MethodInfo method)
        {
            var modifiers = GetMethodModifiers(method);

            // TODO Override
            var returnType = ParseTypeName(method.ReturnType.Signature());

            var parameters = method.GetParameters()
                .Select(p => new
                {
                    Name = Identifier(p.Name ?? $"p{p.Position}"),
                    Type = ParseTypeName(p.ParameterType.Signature()),
                    // TODO Default values
                    // TODO Parameter modifiers
                })
                .Select(p => Parameter(
                    p.Type.WithTrailingTrivia(Space),
                    p.Name));

            var parameterList = parameters.Any() ? SeparatedList(Token(SyntaxKind.CommaToken).WithTrailingTrivia(Space), parameters) : SeparatedList<ParameterSyntax>();

            // TODO Generics
            return MethodDeclaration(returnType, method.Name)
                .WithModifiers(modifiers.ToTokenList())
                .WithParameterList(
                    ParameterList(parameterList))
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        }

        private static TokenListBuilder GetMethodModifiers(MethodInfo method)
        {
            var modifiers = GetAccessModifiers(method)
                .AddIf(method.IsStatic, SyntaxKind.StaticKeyword)
                .AddIf(method.GetCustomAttribute<AsyncStateMachineAttribute>() != null, SyntaxKind.AsyncKeyword);

            if (method.IsAbstract)
            {
                modifiers.Add(SyntaxKind.AbstractKeyword);
            }
            else if (method.IsVirtual)
            {
                modifiers.Add(SyntaxKind.VirtualKeyword);
            }

            modifiers.AddIf(method.IsFinal, SyntaxKind.SealedKeyword);

            return modifiers;
        }

        private static TokenListBuilder GetAccessModifiers(MethodBase method)
        {
            var modifiers = new TokenListBuilder();

            modifiers
                .AddIf(method.IsPublic, SyntaxKind.PublicKeyword)
                .AddIf(method.IsFamilyOrAssembly || method.IsFamily || method.IsFamilyOrAssembly, SyntaxKind.ProtectedKeyword)
                .AddIf(method.IsFamilyOrAssembly || method.IsAssembly, SyntaxKind.InternalKeyword)
                .AddIf(method.IsFamilyAndAssembly || method.IsPrivate, SyntaxKind.PrivateKeyword)
                .AddIf(method.IsPrivate, SyntaxKind.PrivateKeyword);

            return modifiers;
        }
    }
}
