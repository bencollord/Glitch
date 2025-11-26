using Glitch.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Glitch.CodeAnalysis;

/// <summary>
/// Wrapper around <see cref="SyntaxFactory"/> to allow adding convenience
/// methods while using a unified interface.
/// </summary>
public static partial class CSharpSyntax
{
    public static SyntaxTree ParseTree(string code) => CSharpSyntaxTree.ParseText(code);

    public static SyntaxTree LoadTree(string path) => LoadTree(new FileInfo(path));

    public static SyntaxTree LoadTree(FileInfo file) => ParseTree(file.ReadAllText()).WithFilePath(file.FullName);
}
