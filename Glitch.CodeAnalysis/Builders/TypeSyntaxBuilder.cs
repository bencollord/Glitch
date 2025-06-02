using Glitch.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Glitch.CodeAnalysis.Builders
{
    using static CSharpSyntax;
    
    public class TypeSyntaxBuilder
    {
        private SyntaxToken identifier;
        private TokenListBuilder modifiers = [];
        private TypeDeclarationKind declarationKind;
        private List<PropertySyntaxBuilder> properties = [];
        private List<MethodSyntaxBuilder> methods = [];

        private Func<SyntaxToken, TypeDeclarationSyntax> declarationFactory;

        public TypeSyntaxBuilder(string name)
        {
            identifier = SyntaxFactory.Identifier(name);
            Class();
        }

        public TypeSyntaxBuilder Static()
        {
            if (!modifiers.Any(t => t.IsKind(SyntaxKind.StaticKeyword)))
            {
                modifiers.Add(Token(SyntaxKind.StaticKeyword));
            }

            return this;
        }

        [MemberNotNull(nameof(declarationFactory))]
        public TypeSyntaxBuilder Class()
        {
            declarationKind = TypeDeclarationKind.Class;
            declarationFactory = ClassDeclaration;
            return this;
        }

        [MemberNotNull(nameof(declarationFactory))]
        public TypeSyntaxBuilder Interface()
        {
            declarationKind = TypeDeclarationKind.Interface;
            declarationFactory = InterfaceDeclaration;
            return this;
        }

        [MemberNotNull(nameof(declarationFactory))]
        public TypeSyntaxBuilder Struct()
        {
            declarationKind = TypeDeclarationKind.Struct;
            declarationFactory = StructDeclaration;
            return this;
        }

        [MemberNotNull(nameof(declarationFactory))]
        public TypeSyntaxBuilder Record()
        {
            declarationKind = TypeDeclarationKind.Record;
            declarationFactory = RecordDeclaration;
            return this;
        }

        public TypeSyntaxBuilder WithDeclarationKind(TypeDeclarationKind kind)
        {
            return kind switch
            {
                TypeDeclarationKind.Class     => Class(),
                TypeDeclarationKind.Interface => Interface(),
                TypeDeclarationKind.Record    => Record(),
                TypeDeclarationKind.Struct    => Struct(),
                _ => throw new ArgumentOutOfRangeException(nameof(kind)),
            };
        }

        public TypeSyntaxBuilder Public() => SetAccessModifiers(SyntaxKind.PublicKeyword);
        public TypeSyntaxBuilder Internal() => SetAccessModifiers(SyntaxKind.InternalKeyword);
        public TypeSyntaxBuilder ProtectedInternal() => SetAccessModifiers(SyntaxKind.ProtectedKeyword, SyntaxKind.InternalKeyword);
        public TypeSyntaxBuilder Protected() => SetAccessModifiers(SyntaxKind.ProtectedKeyword);
        public TypeSyntaxBuilder PrivateProtected() => SetAccessModifiers(SyntaxKind.PrivateKeyword, SyntaxKind.ProtectedKeyword);
        public TypeSyntaxBuilder Private() => SetAccessModifiers(SyntaxKind.PrivateKeyword);

        public TypeSyntaxBuilder WithAccessModifier(AccessModifier accessModifier)
        {
            return accessModifier switch
            {
                AccessModifier.Public            => Public(),
                AccessModifier.Internal          => Internal(),
                AccessModifier.ProtectedInternal => ProtectedInternal(),
                AccessModifier.PrivateProtected  => PrivateProtected(),
                AccessModifier.Private           => Private(),
                _ => throw new ArgumentOutOfRangeException(nameof(accessModifier))
            };
        }

        public TypeSyntaxBuilder AddProperty(PropertySyntaxBuilder property)
        {
            properties.Add(property);
            return this;
        }

        public TypeSyntaxBuilder AddProperty(PropertyDeclarationSyntax property)
            => AddProperty(new PropertySyntaxBuilder(property));

        public PropertySyntaxBuilder Property<T>(string name)
            => Property(typeof(T), name);

        public PropertySyntaxBuilder Property(Type type, string name)
        {
            var property = new PropertySyntaxBuilder(TypeName(type), name);
            AddProperty(property);
            return property;
        }

        public TypeSyntaxBuilder Property<T>(string name, Action<PropertySyntaxBuilder> builder)
            => Property(typeof(T), name, builder);

        public TypeSyntaxBuilder Property(Type type, string name, Action<PropertySyntaxBuilder> builder)
        {
            var property = Property(type, name);
            builder(property);
            return AddProperty(property);
        }

        public TypeDeclarationSyntax Build()
        {
            return declarationFactory(identifier)
                .WithModifiers(modifiers.Sort(new ModifierComparer()).ToTokenList())
                .WithMembers(List(BuildMembers()));
        }

        private IEnumerable<MemberDeclarationSyntax> BuildMembers() => properties.Select(p => p.Build());

        private TypeSyntaxBuilder SetAccessModifiers(params SyntaxKind[] kinds)
        {
            var accessModifiers = kinds.Select(Token);

            Debug.Assert(accessModifiers.All(t => t.IsAccessModifier()));
            
            modifiers.Remove(t => t.IsAccessModifier())
                     .AddRange(accessModifiers);

            return this;
        }
    }
}
