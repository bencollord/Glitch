using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glitch.CodeAnalysis;

internal class ModifierComparer : Comparer<SyntaxToken>
{
    private static readonly ImmutableList<SyntaxKind> ModifierKindsInOrder =
    [
        SyntaxKind.UnsafeKeyword,

        SyntaxKind.PublicKeyword,
        SyntaxKind.PrivateKeyword,
        SyntaxKind.ProtectedKeyword,
        SyntaxKind.InternalKeyword,

        SyntaxKind.StaticKeyword,
        SyntaxKind.ExternKeyword,
        SyntaxKind.ReadOnlyKeyword,

        SyntaxKind.AbstractKeyword,
        SyntaxKind.SealedKeyword,
        SyntaxKind.VirtualKeyword,
        SyntaxKind.OverrideKeyword,

        SyntaxKind.AsyncKeyword,
    ];


    public override int Compare(SyntaxToken x, SyntaxToken y)
    {
        return GetTokenOrder(x).CompareTo(GetTokenOrder(y));
    }

    private int GetTokenOrder(SyntaxToken token)
    {
        var index = ModifierKindsInOrder.IndexOf(token.Kind());

        return index >= 0 ? index : 100; // Kick non modifiers to the bottom of the list.
    }
}
