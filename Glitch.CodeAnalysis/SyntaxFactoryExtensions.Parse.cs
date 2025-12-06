using Glitch.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace Glitch.CodeAnalysis;

public static partial class SyntaxFactoryExtensions
{
    extension(SyntaxFactory)
    {
        public static SyntaxTree ParseTree(string code) => CSharpSyntaxTree.ParseText(code);

        public static SyntaxTree LoadTree(string path) => Load(new FileInfo(path));

        public static SyntaxTree LoadTree(FileInfo file) => Load(file);
    }

    extension(CSharpSyntaxTree)
    {
        public static SyntaxTree Load(string path) => Load(new FileInfo(path));

        public static SyntaxTree Load(FileInfo file)
        {
            using var stream = file.OpenRead();

            var text = SourceText.From(stream);

            return CSharpSyntaxTree.ParseText(text).WithFilePath(file.FullName);
        }
    }
}
