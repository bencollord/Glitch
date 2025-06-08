using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace Glitch.CodeAnalysis.Builders
{
    using static CSharpSyntax;

    public class MethodSyntaxBuilder : SyntaxBuilder<MethodDeclarationSyntax>
    {
        private TypeSyntax returnType;
        private SyntaxToken identifier;
        private List<ParameterSyntax> parameters = [];

        public MethodSyntaxBuilder(TypeSyntax returnType, SyntaxToken identifier)
        {
            if (!identifier.IsKind(SyntaxKind.IdentifierToken))
            {
                throw new ArgumentException($"Invalid identifier: {identifier.Kind()}");
            }

            this.returnType = returnType;
            this.identifier = identifier;
        }

        public override MethodDeclarationSyntax Build()
        {
            return MethodDeclaration(returnType, identifier)
                .WithParameterList(
                    ParameterList(
                        SeparatedList(parameters)));
        }
    }
}
