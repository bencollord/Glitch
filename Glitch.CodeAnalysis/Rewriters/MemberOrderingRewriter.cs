using Glitch.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Glitch.CodeAnalysis.Rewriters
{
    public class MemberOrderingRewriter : CSharpSyntaxRewriter
    {
        public override SyntaxNode? VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            return VisitTypeDeclaration(node);
        }

        public override SyntaxNode? VisitStructDeclaration(StructDeclarationSyntax node)
        {
            return VisitTypeDeclaration(node);
        }

        private SyntaxNode? VisitTypeDeclaration(TypeDeclarationSyntax node)
        {
            var fields = node.Members
                .OfType<FieldDeclarationSyntax>()
                .OrderBy(GetAccessModifierOrdinal)
                .ThenByDescending(f => f.Modifiers.Any(m => m.IsKind(SyntaxKind.ConstKeyword)))
                .ThenByDescending(f => f.Modifiers.Any(m => m.IsKind(SyntaxKind.StaticKeyword)))
                .ThenByDescending(f => f.Modifiers.Any(m => m.IsKind(SyntaxKind.ReadOnlyKeyword)));

            var constructors = node.Members
                .OfType<ConstructorDeclarationSyntax>()
                .OrderBy(GetAccessModifierOrdinal)
                .ThenBy(c => c.ParameterList.Parameters.Count);

            var properties = node.Members
                .OfType<PropertyDeclarationSyntax>()
                .OrderBy(GetAccessModifierOrdinal)
                .ThenBy(p => p.ExplicitInterfaceSpecifier != null);

            var indexers = node.Members
                .OfType<IndexerDeclarationSyntax>()
                .OrderBy(GetAccessModifierOrdinal)
                .ThenBy(i => i.ExplicitInterfaceSpecifier != null)
                .ThenBy(i => i.ParameterList.Parameters.Count);

            var events = node.Members
                .OfType<EventDeclarationSyntax>()
                .OrderBy(GetAccessModifierOrdinal)
                .ThenBy(e => e.ExplicitInterfaceSpecifier != null);

            var methods = node.Members
                .OfType<MethodDeclarationSyntax>()
                .GroupBy(m => m.Identifier.Text)
                .Select(g => g.OrderBy(GetAccessModifierOrdinal)
                              .ThenBy(m => m.ExplicitInterfaceSpecifier != null)
                              .ThenBy(m => m.ParameterList.Parameters.Count))
                .Flatten();

            var operators = node.Members
                .OfType<OperatorDeclarationSyntax>()
                .GroupBy(o => o.OperatorToken)
                .Select(g => g.OrderBy(GetAccessModifierOrdinal))
                .Flatten();

            var nestedTypes = node.Members
                .OfType<BaseTypeDeclarationSyntax>()
                .OrderBy(GetAccessModifierOrdinal);

            var orderedMembers = SyntaxFactory.List<MemberDeclarationSyntax>()
                .AddRange(fields)
                .AddRange(constructors)
                .AddRange(properties)
                .AddRange(indexers)
                .AddRange(events)
                .AddRange(methods)
                .AddRange(operators)
                .AddRange(nestedTypes);

            if (node.Members.Except(orderedMembers).Any())
            {
                throw new InvalidOperationException("Unaccounted for members found");
            }

            return node.WithMembers(orderedMembers);
        }

        private int GetAccessModifierOrdinal(MemberDeclarationSyntax member)
        {
            if (member.Modifiers.Any(m => m.IsKind(SyntaxKind.PublicKeyword)))
            {
                return 1;
            }

            if (member.Modifiers.Any(m => m.IsKind(SyntaxKind.InternalKeyword))
            && !member.Modifiers.Any(m => m.IsKind(SyntaxKind.ProtectedKeyword)))
            {
                return 2;
            }

            if (member.Modifiers.Any(m => m.IsKind(SyntaxKind.InternalKeyword))
             && member.Modifiers.Any(m => m.IsKind(SyntaxKind.ProtectedKeyword)))
            {
                return 3;
            }

            if (member.Modifiers.Any(m => m.IsKind(SyntaxKind.PrivateKeyword))
             && member.Modifiers.Any(m => m.IsKind(SyntaxKind.ProtectedKeyword)))
            {
                return 4;
            }

            return 5;
        }
    }
}