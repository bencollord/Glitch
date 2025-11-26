using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections;

namespace Glitch.CodeAnalysis.Builders;

using static CSharpSyntax;

internal class TokenListBuilder : IEnumerable<SyntaxToken>
{
    private List<SyntaxToken> tokens = [];

    internal TokenListBuilder Add(SyntaxKind token) => Add(Token(token));

    internal TokenListBuilder Add(SyntaxToken token)
    {
        tokens.Add(token);
        return this;
    }

    internal TokenListBuilder AddIf(bool condition, SyntaxKind kind)
        => condition ? Add(kind) : this;

    internal TokenListBuilder AddIf(bool condition, SyntaxToken token)
        => condition ? Add(token) : this;

    internal TokenListBuilder AddRange(IEnumerable<SyntaxToken> tokens)
    {
        this.tokens.AddRange(tokens);
        return this;
    }

    internal TokenListBuilder Remove(Func<SyntaxToken, bool> predicate)
    {
        tokens.RemoveAll(tokens => predicate(tokens));
        return this;
    }

    internal TokenListBuilder Sort(IComparer<SyntaxToken> comparer)
    {
        tokens.Sort(comparer);
        return this;
    }

    internal SyntaxTokenList ToTokenList() => TokenList(tokens);

    public IEnumerator<SyntaxToken> GetEnumerator() => tokens.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
