using Glitch.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Glitch.CodeAnalysis.Rewriters
{
    using static CSharpSyntax;

    public class WhitespaceNormalizer : CSharpSyntaxRewriter, IIndentable
    {
        public WhitespaceNormalizer()
            : this(Indentation.Spaces) { }

        public WhitespaceNormalizer(Indentation indentation)
        {
            Indentation = indentation;
        }

        public Indentation Indentation { get; set; }
        public SyntaxTrivia LineBreak { get; set; } = CarriageReturnLineFeed;
        public int ExpressionBodyNewLineThreshold { get; set; } = 160;
        public bool ExtraLineBreakBetweenMembers { get; set; } = true;

        public override SyntaxNode? VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            var updated = node.WithNamespaceKeyword(
                node.NamespaceKeyword
                    .WithTrailingTrivia(Space))
                .WithName(
                    node.Name.WithTrailingTrivia(LineBreak))
                .WithOpenBraceToken(
                    node.OpenBraceToken.WithTrailingTrivia(LineBreak))
                .WithCloseBraceToken(
                    node.CloseBraceToken.WithLeadingTrivia(LineBreak));

            using (new IndentationScope(this))
            {
                return base.VisitNamespaceDeclaration(updated);
            }
        }

        public override SyntaxNode? VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            return VisitTypeDeclaration((TypeDeclarationSyntax)base.VisitClassDeclaration(node)!);
        }

        public override SyntaxNode? VisitStructDeclaration(StructDeclarationSyntax node)
        {
            return VisitTypeDeclaration((TypeDeclarationSyntax)base.VisitStructDeclaration(node)!);
        }

        public override SyntaxNode? VisitRecordDeclaration(RecordDeclarationSyntax node)
        {
            return VisitTypeDeclaration((TypeDeclarationSyntax)base.VisitRecordDeclaration(node)!);
        }

        public override SyntaxNode? VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            return VisitTypeDeclaration((TypeDeclarationSyntax)base.VisitInterfaceDeclaration(node)!);
        }

        private TypeDeclarationSyntax VisitTypeDeclaration(TypeDeclarationSyntax node)
        {
            return node
                .WithLeadingTrivia(
                    TriviaList([LineBreak, Whitespace(Indentation)]))
                .WithOpenBraceToken(
                    node.OpenBraceToken
                        .WithLeadingTrivia(
                            TriviaList([LineBreak, Whitespace(Indentation)]))
                        .WithTrailingTrivia(LineBreak))
                .WithCloseBraceToken(
                    node.CloseBraceToken
                        .WithLeadingTrivia(
                            TriviaList([LineBreak, Whitespace(Indentation)]))
                        .WithTrailingTrivia(LineBreak))
                .WithMembers(
                    VisitMemberList(node.Members));
        }

        private SyntaxList<MemberDeclarationSyntax> VisitMemberList(SyntaxList<MemberDeclarationSyntax> members)
        {
            using (new IndentationScope(this))
            {
                var visited = members
                    .Select(Visit)
                    .Cast<MemberDeclarationSyntax>();

                if (ExtraLineBreakBetweenMembers)
                {
                    visited = visited.SkipLast(1)
                        .Select(m => m.WithTrailingTrivia(
                            m.GetTrailingTrivia()
                             .Add(LineBreak)))
                        .Append(visited.Last());
                }

                return List(visited);
            }
        }

        public override SyntaxToken VisitToken(SyntaxToken token)
        {
            if (token.IsKeyword())
            {
                return token.WithTrailingTrivia(Space);
            }

            return base.VisitToken(token);
        }

        public override SyntaxNode? VisitArgumentList(ArgumentListSyntax node)
        {
            var separators = node.Arguments
                .GetSeparators()
                .Select(s => s.WithTrailingTrivia(Space));

            var updated = node.WithArguments(
                SeparatedList(
                    node.Arguments,
                    separators));

            return base.VisitArgumentList(updated);
        }

        public override SyntaxNode? VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var expressionBody = from expr in Maybe(node.ExpressionBody)
                                 let visitedBody = (ArrowExpressionClauseSyntax)Visit(expr)
                                 let declaration = node.WithExpressionBody(visitedBody)
                                 let withLineBreak = from text in Some(declaration.ToString())
                                                     where text.Length > ExpressionBodyNewLineThreshold
                                                     let indent = Whitespace(Indentation + 1)
                                                     let body = visitedBody.WithLeadingTrivia(LineBreak, indent)
                                                     select declaration.WithExpressionBody(body)
                                 select withLineBreak.IfNone(declaration);

            var blockBody = from block in Maybe(node.Body)
                            let visitedBody = (BlockSyntax)Visit(block)
                            select node.WithBody(visitedBody);

            var updated = expressionBody
                .Or(blockBody)
                .IfNone(AddSemicolonToken(node))
                .WithReturnType(node.ReturnType.WithTrailingTrivia(Space))
                .WithLeadingTrivia(
                    Whitespace(Indentation))
                .WithTrailingTrivia(LineBreak);

            return base.VisitMethodDeclaration(updated);

            MethodDeclarationSyntax AddSemicolonToken(MethodDeclarationSyntax node)
            {
                return node.WithSemicolonToken(
                    Token(SyntaxKind.SemicolonToken)
                        .WithTrailingTrivia(LineBreak));
            }
        }

        public override SyntaxNode? VisitArrowExpressionClause(ArrowExpressionClauseSyntax node)
        {
            var updated = node.WithArrowToken(
                node.ArrowToken
                    .WithLeadingTrivia(Space)
                    .WithTrailingTrivia(Space));

            return base.VisitArrowExpressionClause(updated);
        }

        public override SyntaxNode? VisitBlock(BlockSyntax node)
        {
            var updated = node
                .WithOpenBraceToken(
                    node.OpenBraceToken
                        .WithLeadingTrivia(
                            TriviaList([LineBreak, Whitespace(Indentation)]))
                        .WithTrailingTrivia(LineBreak))
                .WithCloseBraceToken(
                    node.CloseBraceToken
                        .WithLeadingTrivia(
                            TriviaList([LineBreak, Whitespace(Indentation)]))
                        .WithTrailingTrivia(LineBreak));

            using (new IndentationScope(this))
            {
                var indentTrivia = Whitespace(Indentation.ToString());
                
                updated = updated.WithStatements(
                    List(
                        updated.Statements
                               .Select(s => s.WithLeadingTrivia(
                                    Whitespace(Indentation)))));

                return updated;
            }
        }

        public override SyntaxNode? VisitParameter(ParameterSyntax node)
        {
            return Maybe(node.Type)
                .Map(t => node.WithType(t.WithTrailingTrivia(Space)))
                .IfNone(node)
                .WithModifiers(
                    TokenList(
                        node.Modifiers
                            .Select(m => m.WithTrailingTrivia(Space))));
        }

        public override SyntaxNode? VisitAssignmentExpression(AssignmentExpressionSyntax node)
        {
            var updated = node.WithOperatorToken(
                node.OperatorToken
                    .WithLeadingTrivia(Space)
                    .WithTrailingTrivia(Space));

            return base.VisitAssignmentExpression(updated);
        }

        private static SyntaxTrivia Whitespace(Indentation indentation)
            => Whitespace(indentation.ToString());

        private static SyntaxTrivia Whitespace(string value)
            => CSharpSyntax.Whitespace(value);
    }

}
