using Glitch.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Glitch.CodeAnalysis
{
    /// <summary>
    /// Wrapper around <see cref="SyntaxFactory"/> to allow adding convenience
    /// methods while using a unified interface.
    /// </summary>
    public static partial class CSharpSyntax
    {
        public static SyntaxTree Parse(string code) => CSharpSyntaxTree.ParseText(code);

        public static SyntaxTree Load(string path) => Load(new FileInfo(path));

        public static SyntaxTree Load(FileInfo file) => Parse(file.ReadAllText()).WithFilePath(file.FullName);
    }
}
