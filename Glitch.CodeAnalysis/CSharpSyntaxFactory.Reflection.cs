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
    [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "Intentionally being suppressed at the classe level")]
    public partial class CSharpSyntaxFactory
    {
        public TypeSyntax TypeName(Type type) => ParseTypeName(type.Signature());

        public BaseTypeDeclarationSyntax TypeDeclaration(Type type)
        {
            var modifiers = new ModifierListBuilder(this)
                .WithTrailingTrivia(Space);

            modifiers
                .AddIf(type.IsPublic, SyntaxKind.PublicKeyword)
                .AddIf(type.IsNestedPrivate, SyntaxKind.PrivateKeyword)
                .AddIf(type.IsNestedFamily, SyntaxKind.ProtectedKeyword)
                .AddIf(type.IsNestedFamANDAssem, SyntaxKind.PrivateKeyword, SyntaxKind.ProtectedKeyword)
                .AddIf(type.IsNestedFamORAssem, SyntaxKind.ProtectedKeyword, SyntaxKind.InternalKeyword)
                .AddIf(type.IsNestedFamily, SyntaxKind.InternalKeyword);

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
                            fields.Select(f => EnumMemberDeclaration(f.Name)),
                            Token(SyntaxKind.CommaToken)
                                .WithTrailingTrivia(CarriageReturnLineFeed)));
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

        public FieldDeclarationSyntax FieldDeclaration(FieldInfo field)
        {
            var modifiers = new ModifierListBuilder(this)
                .WithTrailingTrivia(Space);

            modifiers.AddIf(field.IsPublic, SyntaxKind.PublicKeyword)
                .AddIf(field.IsAssembly, SyntaxKind.InternalKeyword)
                .AddIf(field.IsFamilyOrAssembly, SyntaxKind.ProtectedKeyword, SyntaxKind.InternalKeyword)
                .AddIf(field.IsFamilyAndAssembly, SyntaxKind.PrivateKeyword, SyntaxKind.ProtectedKeyword)
                .AddIf(field.IsFamily, SyntaxKind.ProtectedKeyword)
                .AddIf(field.IsPrivate, SyntaxKind.PrivateKeyword)
                .AddIf(field.IsStatic, SyntaxKind.StaticKeyword)
                .AddIf(field.IsInitOnly, SyntaxKind.ReadOnlyKeyword); // TODO Constants

            return FieldDeclaration(
                VariableDeclaration(
                    TypeName(field.FieldType),
                    field.Name))
                .WithModifiers(modifiers.ToTokenList());
        }

        public PropertyDeclarationSyntax PropertyDeclaration(PropertyInfo property)
        {
            throw new NotImplementedException();
        }

        public ConstructorDeclarationSyntax ConstructorDeclaration(ConstructorInfo constructor)
        {
            throw new NotImplementedException();
        }

        public MethodDeclarationSyntax MethodDeclaration(MethodInfo method)
        {
            var modifiers = new ModifierListBuilder(this);

            modifiers.AddIf(method.IsPublic, SyntaxKind.PublicKeyword)
                .AddIf(method.IsAssembly, SyntaxKind.InternalKeyword)
                .AddIf(method.IsFamilyOrAssembly, SyntaxKind.ProtectedKeyword, SyntaxKind.InternalKeyword)
                .AddIf(method.IsFamilyAndAssembly, SyntaxKind.PrivateKeyword, SyntaxKind.ProtectedKeyword)
                .AddIf(method.IsFamily, SyntaxKind.ProtectedKeyword)
                .AddIf(method.IsPrivate, SyntaxKind.PrivateKeyword);
            
            modifiers.AddIf(method.GetCustomAttribute<AsyncStateMachineAttribute>() != null, SyntaxKind.AsyncKeyword);

            if (method.IsAbstract)
            {
                modifiers.Add(SyntaxKind.AbstractKeyword);
            }
            else if (method.IsVirtual)
            {
                modifiers.Add(SyntaxKind.VirtualKeyword);
            }

            modifiers.AddIf(method.IsFinal, SyntaxKind.SealedKeyword);

            // TODO Override
            var returnType = ParseTypeName(method.ReturnType.Signature());

            var parameters = method.GetParameters()
                .Select(p => new
                {
                    Name = Identifier(p.Name ?? $"p{p.Position}"),
                    Type = ParseTypeName(p.ParameterType.Signature()),
                    // TODO Default values
                });

            // TODO Generics
            return MethodDeclaration(returnType.WithTrailingTrivia(Space), method.Name)
                .WithModifiers(modifiers.WithTrailingTrivia(Space).ToTokenList())
                .WithParameterList(
                    ParameterList(
                        SeparatedList(parameters.Select(p =>
                            Parameter(
                                p.Type.WithTrailingTrivia(Space), // TODO Parameter modifiers
                                p.Name)),
                            Token(SyntaxKind.CommaToken).WithTrailingTrivia(Space))))
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        }

        private class ModifierListBuilder
        {
            private readonly CSharpSyntaxFactory F;
            private List<SyntaxToken> modifiers = [];

            internal ModifierListBuilder(CSharpSyntaxFactory factory)
            {
                F = factory;
            }

            internal ModifierListBuilder Add(SyntaxKind modifier) => Add(F.Token(modifier));

            internal ModifierListBuilder Add(SyntaxToken modifier) => AddIf(true, modifier);

            internal ModifierListBuilder AddIf(bool condition, params SyntaxKind[] modifiers)
                => AddRangeIf(condition, modifiers.Select(F.Token));

            internal ModifierListBuilder AddIf(bool condition, params SyntaxToken[] modifiers)
                => AddRangeIf(condition, modifiers);

            internal ModifierListBuilder AddRangeIf(bool condition, IEnumerable<SyntaxToken> modifiers)
            {
                if (condition)
                {
                    this.modifiers.AddRange(modifiers);
                }

                return this;
            }

            internal ModifierListBuilder WithTrailingTrivia(SyntaxTrivia trivia)
            {
                modifiers = modifiers.ConvertAll(e => e.WithTrailingTrivia(trivia));
                return this;
            }

            internal SyntaxTokenList ToTokenList() => F.TokenList(modifiers);
        }
    }
}
