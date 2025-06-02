using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Glitch.CodeAnalysis.Builders
{
    using static CSharpSyntax;

    public class PropertySyntaxBuilder : SyntaxBuilder<PropertyDeclarationSyntax>
    {
        private TypeSyntax type;
        private SyntaxToken identifier;

        public PropertySyntaxBuilder(TypeSyntax type, string name)
        {
            this.type = type;
            identifier = Identifier(name);
        }

        public PropertySyntaxBuilder(PropertyDeclarationSyntax property)
        {
            identifier = property.Identifier;
            type = property.Type;
        }

        public override PropertyDeclarationSyntax Build()
        {
            return PropertyDeclaration(type, identifier);
        }
    }
}
