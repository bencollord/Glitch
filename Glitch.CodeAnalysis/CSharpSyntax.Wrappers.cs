using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace Glitch.CodeAnalysis;

/// <summary>
    /// Wrapper around <see cref="SyntaxFactory"/> to allow adding convenience
    /// methods while using a unified interface.
    /// </summary>
public static partial class CSharpSyntax
{
    public static SyntaxTrivia CarriageReturnLineFeed => SyntaxFactory.CarriageReturnLineFeed;
    public static SyntaxTrivia LineFeed => SyntaxFactory.LineFeed;
    public static SyntaxTrivia CarriageReturn => SyntaxFactory.CarriageReturn;
    public static SyntaxTrivia Space => SyntaxFactory.Space;
    public static SyntaxTrivia Tab => SyntaxFactory.Tab;
    public static SyntaxTrivia ElasticCarriageReturnLineFeed => SyntaxFactory.ElasticCarriageReturnLineFeed;
    public static SyntaxTrivia ElasticLineFeed => SyntaxFactory.ElasticLineFeed;
    public static SyntaxTrivia ElasticCarriageReturn => SyntaxFactory.ElasticCarriageReturn;
    public static SyntaxTrivia ElasticSpace => SyntaxFactory.ElasticSpace;
    public static SyntaxTrivia ElasticTab => SyntaxFactory.ElasticTab;
    public static SyntaxTrivia ElasticMarker => SyntaxFactory.ElasticMarker;

    public static AccessorDeclarationSyntax AccessorDeclaration(SyntaxKind kind)
        => SyntaxFactory.AccessorDeclaration(kind);

    public static AccessorDeclarationSyntax AccessorDeclaration(SyntaxKind kind, BlockSyntax body)
        => SyntaxFactory.AccessorDeclaration(kind, body);

    public static AccessorDeclarationSyntax AccessorDeclaration(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, BlockSyntax body)
        => SyntaxFactory.AccessorDeclaration(kind, attributeLists, modifiers, body);

    public static AccessorDeclarationSyntax AccessorDeclaration(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, ArrowExpressionClauseSyntax expressionBody)
        => SyntaxFactory.AccessorDeclaration(kind, attributeLists, modifiers, expressionBody);

    public static AccessorDeclarationSyntax AccessorDeclaration(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody)
        => SyntaxFactory.AccessorDeclaration(kind, attributeLists, modifiers, body, expressionBody);

    public static AccessorDeclarationSyntax AccessorDeclaration(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, BlockSyntax body, SyntaxToken semicolonToken)
        => SyntaxFactory.AccessorDeclaration(kind, attributeLists, modifiers, keyword, body, semicolonToken);

    public static AccessorDeclarationSyntax AccessorDeclaration(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
        => SyntaxFactory.AccessorDeclaration(kind, attributeLists, modifiers, keyword, expressionBody, semicolonToken);

    public static AccessorDeclarationSyntax AccessorDeclaration(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
        => SyntaxFactory.AccessorDeclaration(kind, attributeLists, modifiers, keyword, body, expressionBody, semicolonToken);

    public static AccessorListSyntax AccessorList(SyntaxList<AccessorDeclarationSyntax> accessors)
        => SyntaxFactory.AccessorList(accessors);

    public static AccessorListSyntax AccessorList(SyntaxToken openBraceToken, SyntaxList<AccessorDeclarationSyntax> accessors, SyntaxToken closeBraceToken)
        => SyntaxFactory.AccessorList(openBraceToken, accessors, closeBraceToken);

    public static AliasQualifiedNameSyntax AliasQualifiedName(IdentifierNameSyntax alias, SimpleNameSyntax name)
        => SyntaxFactory.AliasQualifiedName(alias, name);

    public static AliasQualifiedNameSyntax AliasQualifiedName(string alias, SimpleNameSyntax name)
        => SyntaxFactory.AliasQualifiedName(alias, name);

    public static AliasQualifiedNameSyntax AliasQualifiedName(IdentifierNameSyntax alias, SyntaxToken colonColonToken, SimpleNameSyntax name)
        => SyntaxFactory.AliasQualifiedName(alias, colonColonToken, name);

    public static AllowsConstraintClauseSyntax AllowsConstraintClause(SeparatedSyntaxList<AllowsConstraintSyntax> constraints)
        => SyntaxFactory.AllowsConstraintClause(constraints);

    public static AllowsConstraintClauseSyntax AllowsConstraintClause(SyntaxToken allowsKeyword, SeparatedSyntaxList<AllowsConstraintSyntax> constraints)
        => SyntaxFactory.AllowsConstraintClause(allowsKeyword, constraints);

    public static AnonymousMethodExpressionSyntax AnonymousMethodExpression()
        => SyntaxFactory.AnonymousMethodExpression();

    public static AnonymousMethodExpressionSyntax AnonymousMethodExpression(CSharpSyntaxNode body)
        => SyntaxFactory.AnonymousMethodExpression(body);

    public static AnonymousMethodExpressionSyntax AnonymousMethodExpression(ParameterListSyntax parameterList, CSharpSyntaxNode body)
        => SyntaxFactory.AnonymousMethodExpression(parameterList, body);

    public static AnonymousMethodExpressionSyntax AnonymousMethodExpression(SyntaxToken asyncKeyword, SyntaxToken delegateKeyword, ParameterListSyntax parameterList, CSharpSyntaxNode body)
        => SyntaxFactory.AnonymousMethodExpression(asyncKeyword, delegateKeyword, parameterList, body);

    public static AnonymousMethodExpressionSyntax AnonymousMethodExpression(SyntaxToken asyncKeyword, SyntaxToken delegateKeyword, ParameterListSyntax parameterList, BlockSyntax block, ExpressionSyntax expressionBody)
        => SyntaxFactory.AnonymousMethodExpression(asyncKeyword, delegateKeyword, parameterList, block, expressionBody);

    public static AnonymousMethodExpressionSyntax AnonymousMethodExpression(SyntaxTokenList modifiers, SyntaxToken delegateKeyword, ParameterListSyntax parameterList, BlockSyntax block, ExpressionSyntax expressionBody)
        => SyntaxFactory.AnonymousMethodExpression(modifiers, delegateKeyword, parameterList, block, expressionBody);

    public static AnonymousObjectCreationExpressionSyntax AnonymousObjectCreationExpression(SeparatedSyntaxList<AnonymousObjectMemberDeclaratorSyntax> initializers)
        => SyntaxFactory.AnonymousObjectCreationExpression(initializers);

    public static AnonymousObjectCreationExpressionSyntax AnonymousObjectCreationExpression(SyntaxToken newKeyword, SyntaxToken openBraceToken, SeparatedSyntaxList<AnonymousObjectMemberDeclaratorSyntax> initializers, SyntaxToken closeBraceToken)
        => SyntaxFactory.AnonymousObjectCreationExpression(newKeyword, openBraceToken, initializers, closeBraceToken);

    public static AnonymousObjectMemberDeclaratorSyntax AnonymousObjectMemberDeclarator(ExpressionSyntax expression)
        => SyntaxFactory.AnonymousObjectMemberDeclarator(expression);

    public static AnonymousObjectMemberDeclaratorSyntax AnonymousObjectMemberDeclarator(NameEqualsSyntax nameEquals, ExpressionSyntax expression)
        => SyntaxFactory.AnonymousObjectMemberDeclarator(nameEquals, expression);

    public static bool AreEquivalent(SyntaxToken oldToken, SyntaxToken newToken)
        => SyntaxFactory.AreEquivalent(oldToken, newToken);

    public static bool AreEquivalent(SyntaxTokenList oldList, SyntaxTokenList newList)
        => SyntaxFactory.AreEquivalent(oldList, newList);

    public static bool AreEquivalent(SyntaxTree oldTree, SyntaxTree newTree, bool topLevel)
        => SyntaxFactory.AreEquivalent(oldTree, newTree, topLevel);

    public static bool AreEquivalent(SyntaxNode oldNode, SyntaxNode newNode, bool topLevel)
        => SyntaxFactory.AreEquivalent(oldNode, newNode, topLevel);

    public static bool AreEquivalent(SyntaxNode oldNode, SyntaxNode newNode, Func<SyntaxKind, bool> ignoreChildNode)
        => SyntaxFactory.AreEquivalent(oldNode, newNode, ignoreChildNode);

    public static bool AreEquivalent<TNode>(SyntaxList<TNode> oldList, SyntaxList<TNode> newList, bool topLevel)
        where TNode : CSharpSyntaxNode
        => SyntaxFactory.AreEquivalent<TNode>(oldList, newList, topLevel);

    public static bool AreEquivalent<TNode>(SyntaxList<TNode> oldList, SyntaxList<TNode> newList, Func<SyntaxKind, bool> ignoreChildNode)
        where TNode : SyntaxNode
        => SyntaxFactory.AreEquivalent(oldList, newList, ignoreChildNode);

    public static bool AreEquivalent<TNode>(SeparatedSyntaxList<TNode> oldList, SeparatedSyntaxList<TNode> newList, bool topLevel)
        where TNode : SyntaxNode
        => SyntaxFactory.AreEquivalent(oldList, newList, topLevel);

    public static bool AreEquivalent<TNode>(SeparatedSyntaxList<TNode> oldList, SeparatedSyntaxList<TNode> newList, Func<SyntaxKind, bool> ignoreChildNode)
        where TNode : SyntaxNode
        => SyntaxFactory.AreEquivalent(oldList, newList, ignoreChildNode);

    public static ArgumentSyntax Argument(ExpressionSyntax expression)
        => SyntaxFactory.Argument(expression);

    public static ArgumentSyntax Argument(NameColonSyntax nameColon, SyntaxToken refKindKeyword, ExpressionSyntax expression)
        => SyntaxFactory.Argument(nameColon, refKindKeyword, expression);

    public static ArgumentListSyntax ArgumentList(SeparatedSyntaxList<ArgumentSyntax> arguments)
        => SyntaxFactory.ArgumentList(arguments);

    public static ArgumentListSyntax ArgumentList(SyntaxToken openParenToken, SeparatedSyntaxList<ArgumentSyntax> arguments, SyntaxToken closeParenToken)
        => SyntaxFactory.ArgumentList(openParenToken, arguments, closeParenToken);

    public static ArrayCreationExpressionSyntax ArrayCreationExpression(ArrayTypeSyntax type)
        => SyntaxFactory.ArrayCreationExpression(type);

    public static ArrayCreationExpressionSyntax ArrayCreationExpression(ArrayTypeSyntax type, InitializerExpressionSyntax initializer)
        => SyntaxFactory.ArrayCreationExpression(type, initializer);

    public static ArrayCreationExpressionSyntax ArrayCreationExpression(SyntaxToken newKeyword, ArrayTypeSyntax type, InitializerExpressionSyntax initializer)
        => SyntaxFactory.ArrayCreationExpression(newKeyword, type, initializer);

    public static ArrayRankSpecifierSyntax ArrayRankSpecifier(SeparatedSyntaxList<ExpressionSyntax> sizes)
        => SyntaxFactory.ArrayRankSpecifier(sizes);

    public static ArrayRankSpecifierSyntax ArrayRankSpecifier(SyntaxToken openBracketToken, SeparatedSyntaxList<ExpressionSyntax> sizes, SyntaxToken closeBracketToken)
        => SyntaxFactory.ArrayRankSpecifier(openBracketToken, sizes, closeBracketToken);

    public static ArrayTypeSyntax ArrayType(TypeSyntax elementType)
        => SyntaxFactory.ArrayType(elementType);

    public static ArrayTypeSyntax ArrayType(TypeSyntax elementType, SyntaxList<ArrayRankSpecifierSyntax> rankSpecifiers)
        => SyntaxFactory.ArrayType(elementType, rankSpecifiers);

    public static ArrowExpressionClauseSyntax ArrowExpressionClause(ExpressionSyntax expression)
        => SyntaxFactory.ArrowExpressionClause(expression);

    public static ArrowExpressionClauseSyntax ArrowExpressionClause(SyntaxToken arrowToken, ExpressionSyntax expression)
        => SyntaxFactory.ArrowExpressionClause(arrowToken, expression);

    public static AssignmentExpressionSyntax AssignmentExpression(SyntaxKind kind, ExpressionSyntax left, ExpressionSyntax right)
        => SyntaxFactory.AssignmentExpression(kind, left, right);

    public static AssignmentExpressionSyntax AssignmentExpression(SyntaxKind kind, ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
        => SyntaxFactory.AssignmentExpression(kind, left, operatorToken, right);

    public static AttributeSyntax Attribute(NameSyntax name)
        => SyntaxFactory.Attribute(name);

    public static AttributeSyntax Attribute(NameSyntax name, AttributeArgumentListSyntax argumentList)
        => SyntaxFactory.Attribute(name, argumentList);

    public static AttributeArgumentSyntax AttributeArgument(ExpressionSyntax expression)
        => SyntaxFactory.AttributeArgument(expression);

    public static AttributeArgumentSyntax AttributeArgument(NameEqualsSyntax nameEquals, NameColonSyntax nameColon, ExpressionSyntax expression)
        => SyntaxFactory.AttributeArgument(nameEquals, nameColon, expression);

    public static AttributeArgumentListSyntax AttributeArgumentList(SeparatedSyntaxList<AttributeArgumentSyntax> arguments)
        => SyntaxFactory.AttributeArgumentList(arguments);

    public static AttributeArgumentListSyntax AttributeArgumentList(SyntaxToken openParenToken, SeparatedSyntaxList<AttributeArgumentSyntax> arguments, SyntaxToken closeParenToken)
        => SyntaxFactory.AttributeArgumentList(openParenToken, arguments, closeParenToken);

    public static AttributeListSyntax AttributeList(SeparatedSyntaxList<AttributeSyntax> attributes)
        => SyntaxFactory.AttributeList(attributes);

    public static AttributeListSyntax AttributeList(AttributeTargetSpecifierSyntax target, SeparatedSyntaxList<AttributeSyntax> attributes)
        => SyntaxFactory.AttributeList(target, attributes);

    public static AttributeListSyntax AttributeList(SyntaxToken openBracketToken, AttributeTargetSpecifierSyntax target, SeparatedSyntaxList<AttributeSyntax> attributes, SyntaxToken closeBracketToken)
        => SyntaxFactory.AttributeList(openBracketToken, target, attributes, closeBracketToken);

    public static AttributeTargetSpecifierSyntax AttributeTargetSpecifier(SyntaxToken identifier)
        => SyntaxFactory.AttributeTargetSpecifier(identifier);

    public static AttributeTargetSpecifierSyntax AttributeTargetSpecifier(SyntaxToken identifier, SyntaxToken colonToken)
        => SyntaxFactory.AttributeTargetSpecifier(identifier, colonToken);

    public static AwaitExpressionSyntax AwaitExpression(ExpressionSyntax expression)
        => SyntaxFactory.AwaitExpression(expression);

    public static AwaitExpressionSyntax AwaitExpression(SyntaxToken awaitKeyword, ExpressionSyntax expression)
        => SyntaxFactory.AwaitExpression(awaitKeyword, expression);

    public static BadDirectiveTriviaSyntax BadDirectiveTrivia(SyntaxToken identifier, bool isActive)
        => SyntaxFactory.BadDirectiveTrivia(identifier, isActive);

    public static BadDirectiveTriviaSyntax BadDirectiveTrivia(SyntaxToken hashToken, SyntaxToken identifier, SyntaxToken endOfDirectiveToken, bool isActive)
        => SyntaxFactory.BadDirectiveTrivia(hashToken, identifier, endOfDirectiveToken, isActive);

    public static SyntaxToken BadToken(SyntaxTriviaList leading, string text, SyntaxTriviaList trailing)
        => SyntaxFactory.BadToken(leading, text, trailing);

    public static BaseExpressionSyntax BaseExpression()
        => SyntaxFactory.BaseExpression();

    public static BaseExpressionSyntax BaseExpression(SyntaxToken token)
        => SyntaxFactory.BaseExpression(token);

    public static BaseListSyntax BaseList(SeparatedSyntaxList<BaseTypeSyntax> types)
        => SyntaxFactory.BaseList(types);

    public static BaseListSyntax BaseList(SyntaxToken colonToken, SeparatedSyntaxList<BaseTypeSyntax> types)
        => SyntaxFactory.BaseList(colonToken, types);

    public static BinaryExpressionSyntax BinaryExpression(SyntaxKind kind, ExpressionSyntax left, ExpressionSyntax right)
        => SyntaxFactory.BinaryExpression(kind, left, right);

    public static BinaryExpressionSyntax BinaryExpression(SyntaxKind kind, ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
        => SyntaxFactory.BinaryExpression(kind, left, operatorToken, right);

    public static BinaryPatternSyntax BinaryPattern(SyntaxKind kind, PatternSyntax left, PatternSyntax right)
        => SyntaxFactory.BinaryPattern(kind, left, right);

    public static BinaryPatternSyntax BinaryPattern(SyntaxKind kind, PatternSyntax left, SyntaxToken operatorToken, PatternSyntax right)
        => SyntaxFactory.BinaryPattern(kind, left, operatorToken, right);

    public static BlockSyntax Block(StatementSyntax[] statements)
        => SyntaxFactory.Block(statements);

    public static BlockSyntax Block(IEnumerable<StatementSyntax> statements)
        => SyntaxFactory.Block(statements);

    public static BlockSyntax Block(SyntaxList<StatementSyntax> statements)
        => SyntaxFactory.Block(statements);

    public static BlockSyntax Block(SyntaxList<AttributeListSyntax> attributeLists, SyntaxList<StatementSyntax> statements)
        => SyntaxFactory.Block(attributeLists, statements);

    public static BlockSyntax Block(SyntaxToken openBraceToken, SyntaxList<StatementSyntax> statements, SyntaxToken closeBraceToken)
        => SyntaxFactory.Block(openBraceToken, statements, closeBraceToken);

    public static BlockSyntax Block(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken openBraceToken, SyntaxList<StatementSyntax> statements, SyntaxToken closeBraceToken)
        => SyntaxFactory.Block(attributeLists, openBraceToken, statements, closeBraceToken);

    public static BracketedArgumentListSyntax BracketedArgumentList(SeparatedSyntaxList<ArgumentSyntax> arguments)
        => SyntaxFactory.BracketedArgumentList(arguments);

    public static BracketedArgumentListSyntax BracketedArgumentList(SyntaxToken openBracketToken, SeparatedSyntaxList<ArgumentSyntax> arguments, SyntaxToken closeBracketToken)
        => SyntaxFactory.BracketedArgumentList(openBracketToken, arguments, closeBracketToken);

    public static BracketedParameterListSyntax BracketedParameterList(SeparatedSyntaxList<ParameterSyntax> parameters)
        => SyntaxFactory.BracketedParameterList(parameters);

    public static BracketedParameterListSyntax BracketedParameterList(SyntaxToken openBracketToken, SeparatedSyntaxList<ParameterSyntax> parameters, SyntaxToken closeBracketToken)
        => SyntaxFactory.BracketedParameterList(openBracketToken, parameters, closeBracketToken);

    public static BreakStatementSyntax BreakStatement()
        => SyntaxFactory.BreakStatement();

    public static BreakStatementSyntax BreakStatement(SyntaxList<AttributeListSyntax> attributeLists)
        => SyntaxFactory.BreakStatement(attributeLists);

    public static BreakStatementSyntax BreakStatement(SyntaxToken breakKeyword, SyntaxToken semicolonToken)
        => SyntaxFactory.BreakStatement(breakKeyword, semicolonToken);

    public static BreakStatementSyntax BreakStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken breakKeyword, SyntaxToken semicolonToken)
        => SyntaxFactory.BreakStatement(attributeLists, breakKeyword, semicolonToken);

    public static CasePatternSwitchLabelSyntax CasePatternSwitchLabel(PatternSyntax pattern, SyntaxToken colonToken)
        => SyntaxFactory.CasePatternSwitchLabel(pattern, colonToken);

    public static CasePatternSwitchLabelSyntax CasePatternSwitchLabel(PatternSyntax pattern, WhenClauseSyntax whenClause, SyntaxToken colonToken)
        => SyntaxFactory.CasePatternSwitchLabel(pattern, whenClause, colonToken);

    public static CasePatternSwitchLabelSyntax CasePatternSwitchLabel(SyntaxToken keyword, PatternSyntax pattern, WhenClauseSyntax whenClause, SyntaxToken colonToken)
        => SyntaxFactory.CasePatternSwitchLabel(keyword, pattern, whenClause, colonToken);

    public static CaseSwitchLabelSyntax CaseSwitchLabel(ExpressionSyntax value)
        => SyntaxFactory.CaseSwitchLabel(value);

    public static CaseSwitchLabelSyntax CaseSwitchLabel(ExpressionSyntax value, SyntaxToken colonToken)
        => SyntaxFactory.CaseSwitchLabel(value, colonToken);

    public static CaseSwitchLabelSyntax CaseSwitchLabel(SyntaxToken keyword, ExpressionSyntax value, SyntaxToken colonToken)
        => SyntaxFactory.CaseSwitchLabel(keyword, value, colonToken);

    public static CastExpressionSyntax CastExpression(TypeSyntax type, ExpressionSyntax expression)
        => SyntaxFactory.CastExpression(type, expression);

    public static CastExpressionSyntax CastExpression(SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken, ExpressionSyntax expression)
        => SyntaxFactory.CastExpression(openParenToken, type, closeParenToken, expression);

    public static CatchClauseSyntax CatchClause()
        => SyntaxFactory.CatchClause();

    public static CatchClauseSyntax CatchClause(CatchDeclarationSyntax declaration, CatchFilterClauseSyntax filter, BlockSyntax block)
        => SyntaxFactory.CatchClause(declaration, filter, block);

    public static CatchClauseSyntax CatchClause(SyntaxToken catchKeyword, CatchDeclarationSyntax declaration, CatchFilterClauseSyntax filter, BlockSyntax block)
        => SyntaxFactory.CatchClause(catchKeyword, declaration, filter, block);

    public static CatchDeclarationSyntax CatchDeclaration(TypeSyntax type)
        => SyntaxFactory.CatchDeclaration(type);

    public static CatchDeclarationSyntax CatchDeclaration(TypeSyntax type, SyntaxToken identifier)
        => SyntaxFactory.CatchDeclaration(type, identifier);

    public static CatchDeclarationSyntax CatchDeclaration(SyntaxToken openParenToken, TypeSyntax type, SyntaxToken identifier, SyntaxToken closeParenToken)
        => SyntaxFactory.CatchDeclaration(openParenToken, type, identifier, closeParenToken);

    public static CatchFilterClauseSyntax CatchFilterClause(ExpressionSyntax filterExpression)
        => SyntaxFactory.CatchFilterClause(filterExpression);

    public static CatchFilterClauseSyntax CatchFilterClause(SyntaxToken whenKeyword, SyntaxToken openParenToken, ExpressionSyntax filterExpression, SyntaxToken closeParenToken)
        => SyntaxFactory.CatchFilterClause(whenKeyword, openParenToken, filterExpression, closeParenToken);

    public static CheckedExpressionSyntax CheckedExpression(SyntaxKind kind, ExpressionSyntax expression)
        => SyntaxFactory.CheckedExpression(kind, expression);

    public static CheckedExpressionSyntax CheckedExpression(SyntaxKind kind, SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
        => SyntaxFactory.CheckedExpression(kind, keyword, openParenToken, expression, closeParenToken);

    public static CheckedStatementSyntax CheckedStatement(SyntaxKind kind, BlockSyntax block)
        => SyntaxFactory.CheckedStatement(kind, block);

    public static CheckedStatementSyntax CheckedStatement(SyntaxKind kind, SyntaxToken keyword, BlockSyntax block)
        => SyntaxFactory.CheckedStatement(kind, keyword, block);

    public static CheckedStatementSyntax CheckedStatement(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, BlockSyntax block)
        => SyntaxFactory.CheckedStatement(kind, attributeLists, block);

    public static CheckedStatementSyntax CheckedStatement(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken keyword, BlockSyntax block)
        => SyntaxFactory.CheckedStatement(kind, attributeLists, keyword, block);

    public static ClassDeclarationSyntax ClassDeclaration(SyntaxToken identifier)
        => SyntaxFactory.ClassDeclaration(identifier);

    public static ClassDeclarationSyntax ClassDeclaration(string identifier)
        => SyntaxFactory.ClassDeclaration(identifier);

    public static ClassDeclarationSyntax ClassDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxList<MemberDeclarationSyntax> members)
        => SyntaxFactory.ClassDeclaration(attributeLists, modifiers, identifier, typeParameterList, baseList, constraintClauses, members);

    public static ClassDeclarationSyntax ClassDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxList<MemberDeclarationSyntax> members)
        => SyntaxFactory.ClassDeclaration(attributeLists, modifiers, identifier, typeParameterList, parameterList, baseList, constraintClauses, members);

    public static ClassDeclarationSyntax ClassDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
        => SyntaxFactory.ClassDeclaration(attributeLists, modifiers, keyword, identifier, typeParameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);

    public static ClassDeclarationSyntax ClassDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
        => SyntaxFactory.ClassDeclaration(attributeLists, modifiers, keyword, identifier, typeParameterList, parameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);

    public static ClassOrStructConstraintSyntax ClassOrStructConstraint(SyntaxKind kind)
        => SyntaxFactory.ClassOrStructConstraint(kind);

    public static ClassOrStructConstraintSyntax ClassOrStructConstraint(SyntaxKind kind, SyntaxToken classOrStructKeyword)
        => SyntaxFactory.ClassOrStructConstraint(kind, classOrStructKeyword);

    public static ClassOrStructConstraintSyntax ClassOrStructConstraint(SyntaxKind kind, SyntaxToken classOrStructKeyword, SyntaxToken questionToken)
        => SyntaxFactory.ClassOrStructConstraint(kind, classOrStructKeyword, questionToken);

    public static CollectionExpressionSyntax CollectionExpression(SeparatedSyntaxList<CollectionElementSyntax> elements)
        => SyntaxFactory.CollectionExpression(elements);

    public static CollectionExpressionSyntax CollectionExpression(SyntaxToken openBracketToken, SeparatedSyntaxList<CollectionElementSyntax> elements, SyntaxToken closeBracketToken)
        => SyntaxFactory.CollectionExpression(openBracketToken, elements, closeBracketToken);

    public static SyntaxTrivia Comment(string text)
        => SyntaxFactory.Comment(text);

    public static CompilationUnitSyntax CompilationUnit()
        => SyntaxFactory.CompilationUnit();

    public static CompilationUnitSyntax CompilationUnit(SyntaxList<ExternAliasDirectiveSyntax> externs, SyntaxList<UsingDirectiveSyntax> usings, SyntaxList<AttributeListSyntax> attributeLists, SyntaxList<MemberDeclarationSyntax> members)
        => SyntaxFactory.CompilationUnit(externs, usings, attributeLists, members);

    public static CompilationUnitSyntax CompilationUnit(SyntaxList<ExternAliasDirectiveSyntax> externs, SyntaxList<UsingDirectiveSyntax> usings, SyntaxList<AttributeListSyntax> attributeLists, SyntaxList<MemberDeclarationSyntax> members, SyntaxToken endOfFileToken)
        => SyntaxFactory.CompilationUnit(externs, usings, attributeLists, members, endOfFileToken);

    public static ConditionalAccessExpressionSyntax ConditionalAccessExpression(ExpressionSyntax expression, ExpressionSyntax whenNotNull)
        => SyntaxFactory.ConditionalAccessExpression(expression, whenNotNull);

    public static ConditionalAccessExpressionSyntax ConditionalAccessExpression(ExpressionSyntax expression, SyntaxToken operatorToken, ExpressionSyntax whenNotNull)
        => SyntaxFactory.ConditionalAccessExpression(expression, operatorToken, whenNotNull);

    public static ConditionalExpressionSyntax ConditionalExpression(ExpressionSyntax condition, ExpressionSyntax whenTrue, ExpressionSyntax whenFalse)
        => SyntaxFactory.ConditionalExpression(condition, whenTrue, whenFalse);

    public static ConditionalExpressionSyntax ConditionalExpression(ExpressionSyntax condition, SyntaxToken questionToken, ExpressionSyntax whenTrue, SyntaxToken colonToken, ExpressionSyntax whenFalse)
        => SyntaxFactory.ConditionalExpression(condition, questionToken, whenTrue, colonToken, whenFalse);

    public static ConstantPatternSyntax ConstantPattern(ExpressionSyntax expression)
        => SyntaxFactory.ConstantPattern(expression);

    public static ConstructorConstraintSyntax ConstructorConstraint()
        => SyntaxFactory.ConstructorConstraint();

    public static ConstructorConstraintSyntax ConstructorConstraint(SyntaxToken newKeyword, SyntaxToken openParenToken, SyntaxToken closeParenToken)
        => SyntaxFactory.ConstructorConstraint(newKeyword, openParenToken, closeParenToken);

    public static ConstructorDeclarationSyntax ConstructorDeclaration(SyntaxToken identifier)
        => SyntaxFactory.ConstructorDeclaration(identifier);

    public static ConstructorDeclarationSyntax ConstructorDeclaration(string identifier)
        => SyntaxFactory.ConstructorDeclaration(identifier);

    public static ConstructorDeclarationSyntax ConstructorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, ParameterListSyntax parameterList, ConstructorInitializerSyntax initializer, BlockSyntax body)
        => SyntaxFactory.ConstructorDeclaration(attributeLists, modifiers, identifier, parameterList, initializer, body);

    public static ConstructorDeclarationSyntax ConstructorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, ParameterListSyntax parameterList, ConstructorInitializerSyntax initializer, ArrowExpressionClauseSyntax expressionBody)
        => SyntaxFactory.ConstructorDeclaration(attributeLists, modifiers, identifier, parameterList, initializer, expressionBody);

    public static ConstructorDeclarationSyntax ConstructorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, ParameterListSyntax parameterList, ConstructorInitializerSyntax initializer, BlockSyntax body, SyntaxToken semicolonToken)
        => SyntaxFactory.ConstructorDeclaration(attributeLists, modifiers, identifier, parameterList, initializer, body, semicolonToken);

    public static ConstructorDeclarationSyntax ConstructorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, ParameterListSyntax parameterList, ConstructorInitializerSyntax initializer, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
        => SyntaxFactory.ConstructorDeclaration(attributeLists, modifiers, identifier, parameterList, initializer, expressionBody, semicolonToken);

    public static ConstructorDeclarationSyntax ConstructorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, ParameterListSyntax parameterList, ConstructorInitializerSyntax initializer, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody)
        => SyntaxFactory.ConstructorDeclaration(attributeLists, modifiers, identifier, parameterList, initializer, body, expressionBody);

    public static ConstructorDeclarationSyntax ConstructorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, ParameterListSyntax parameterList, ConstructorInitializerSyntax initializer, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
        => SyntaxFactory.ConstructorDeclaration(attributeLists, modifiers, identifier, parameterList, initializer, body, expressionBody, semicolonToken);

    public static ConstructorInitializerSyntax ConstructorInitializer(SyntaxKind kind, ArgumentListSyntax argumentList)
        => SyntaxFactory.ConstructorInitializer(kind, argumentList);

    public static ConstructorInitializerSyntax ConstructorInitializer(SyntaxKind kind, SyntaxToken colonToken, SyntaxToken thisOrBaseKeyword, ArgumentListSyntax argumentList)
        => SyntaxFactory.ConstructorInitializer(kind, colonToken, thisOrBaseKeyword, argumentList);

    public static ContinueStatementSyntax ContinueStatement()
        => SyntaxFactory.ContinueStatement();

    public static ContinueStatementSyntax ContinueStatement(SyntaxList<AttributeListSyntax> attributeLists)
        => SyntaxFactory.ContinueStatement(attributeLists);

    public static ContinueStatementSyntax ContinueStatement(SyntaxToken continueKeyword, SyntaxToken semicolonToken)
        => SyntaxFactory.ContinueStatement(continueKeyword, semicolonToken);

    public static ContinueStatementSyntax ContinueStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken continueKeyword, SyntaxToken semicolonToken)
        => SyntaxFactory.ContinueStatement(attributeLists, continueKeyword, semicolonToken);

    public static ConversionOperatorDeclarationSyntax ConversionOperatorDeclaration(SyntaxToken implicitOrExplicitKeyword, TypeSyntax type)
        => SyntaxFactory.ConversionOperatorDeclaration(implicitOrExplicitKeyword, type);

    public static ConversionOperatorDeclarationSyntax ConversionOperatorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken implicitOrExplicitKeyword, TypeSyntax type, ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody)
        => SyntaxFactory.ConversionOperatorDeclaration(attributeLists, modifiers, implicitOrExplicitKeyword, type, parameterList, body, expressionBody);

    public static ConversionOperatorDeclarationSyntax ConversionOperatorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken implicitOrExplicitKeyword, SyntaxToken operatorKeyword, TypeSyntax type, ParameterListSyntax parameterList, BlockSyntax body, SyntaxToken semicolonToken)
        => SyntaxFactory.ConversionOperatorDeclaration(attributeLists, modifiers, implicitOrExplicitKeyword, operatorKeyword, type, parameterList, body, semicolonToken);

    public static ConversionOperatorDeclarationSyntax ConversionOperatorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken implicitOrExplicitKeyword, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, TypeSyntax type, ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody)
        => SyntaxFactory.ConversionOperatorDeclaration(attributeLists, modifiers, implicitOrExplicitKeyword, explicitInterfaceSpecifier, type, parameterList, body, expressionBody);

    public static ConversionOperatorDeclarationSyntax ConversionOperatorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken implicitOrExplicitKeyword, SyntaxToken operatorKeyword, TypeSyntax type, ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
        => SyntaxFactory.ConversionOperatorDeclaration(attributeLists, modifiers, implicitOrExplicitKeyword, operatorKeyword, type, parameterList, body, expressionBody, semicolonToken);

    public static ConversionOperatorDeclarationSyntax ConversionOperatorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken implicitOrExplicitKeyword, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken operatorKeyword, TypeSyntax type, ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
        => SyntaxFactory.ConversionOperatorDeclaration(attributeLists, modifiers, implicitOrExplicitKeyword, explicitInterfaceSpecifier, operatorKeyword, type, parameterList, body, expressionBody, semicolonToken);

    public static ConversionOperatorDeclarationSyntax ConversionOperatorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken implicitOrExplicitKeyword, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken operatorKeyword, SyntaxToken checkedKeyword, TypeSyntax type, ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
        => SyntaxFactory.ConversionOperatorDeclaration(attributeLists, modifiers, implicitOrExplicitKeyword, explicitInterfaceSpecifier, operatorKeyword, checkedKeyword, type, parameterList, body, expressionBody, semicolonToken);

    public static ConversionOperatorMemberCrefSyntax ConversionOperatorMemberCref(SyntaxToken implicitOrExplicitKeyword, TypeSyntax type)
        => SyntaxFactory.ConversionOperatorMemberCref(implicitOrExplicitKeyword, type);

    public static ConversionOperatorMemberCrefSyntax ConversionOperatorMemberCref(SyntaxToken implicitOrExplicitKeyword, TypeSyntax type, CrefParameterListSyntax parameters)
        => SyntaxFactory.ConversionOperatorMemberCref(implicitOrExplicitKeyword, type, parameters);

    public static ConversionOperatorMemberCrefSyntax ConversionOperatorMemberCref(SyntaxToken implicitOrExplicitKeyword, SyntaxToken operatorKeyword, TypeSyntax type, CrefParameterListSyntax parameters)
        => SyntaxFactory.ConversionOperatorMemberCref(implicitOrExplicitKeyword, operatorKeyword, type, parameters);

    public static ConversionOperatorMemberCrefSyntax ConversionOperatorMemberCref(SyntaxToken implicitOrExplicitKeyword, SyntaxToken operatorKeyword, SyntaxToken checkedKeyword, TypeSyntax type, CrefParameterListSyntax parameters)
        => SyntaxFactory.ConversionOperatorMemberCref(implicitOrExplicitKeyword, operatorKeyword, checkedKeyword, type, parameters);

    public static CrefBracketedParameterListSyntax CrefBracketedParameterList(SeparatedSyntaxList<CrefParameterSyntax> parameters)
        => SyntaxFactory.CrefBracketedParameterList(parameters);

    public static CrefBracketedParameterListSyntax CrefBracketedParameterList(SyntaxToken openBracketToken, SeparatedSyntaxList<CrefParameterSyntax> parameters, SyntaxToken closeBracketToken)
        => SyntaxFactory.CrefBracketedParameterList(openBracketToken, parameters, closeBracketToken);

    public static CrefParameterSyntax CrefParameter(TypeSyntax type)
        => SyntaxFactory.CrefParameter(type);

    public static CrefParameterSyntax CrefParameter(SyntaxToken refKindKeyword, TypeSyntax type)
        => SyntaxFactory.CrefParameter(refKindKeyword, type);

    public static CrefParameterSyntax CrefParameter(SyntaxToken refKindKeyword, SyntaxToken readOnlyKeyword, TypeSyntax type)
        => SyntaxFactory.CrefParameter(refKindKeyword, readOnlyKeyword, type);

    public static CrefParameterListSyntax CrefParameterList(SeparatedSyntaxList<CrefParameterSyntax> parameters)
        => SyntaxFactory.CrefParameterList(parameters);

    public static CrefParameterListSyntax CrefParameterList(SyntaxToken openParenToken, SeparatedSyntaxList<CrefParameterSyntax> parameters, SyntaxToken closeParenToken)
        => SyntaxFactory.CrefParameterList(openParenToken, parameters, closeParenToken);

    public static DeclarationExpressionSyntax DeclarationExpression(TypeSyntax type, VariableDesignationSyntax designation)
        => SyntaxFactory.DeclarationExpression(type, designation);

    public static DeclarationPatternSyntax DeclarationPattern(TypeSyntax type, VariableDesignationSyntax designation)
        => SyntaxFactory.DeclarationPattern(type, designation);

    public static DefaultConstraintSyntax DefaultConstraint()
        => SyntaxFactory.DefaultConstraint();

    public static DefaultConstraintSyntax DefaultConstraint(SyntaxToken defaultKeyword)
        => SyntaxFactory.DefaultConstraint(defaultKeyword);

    public static DefaultExpressionSyntax DefaultExpression(TypeSyntax type)
        => SyntaxFactory.DefaultExpression(type);

    public static DefaultExpressionSyntax DefaultExpression(SyntaxToken keyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken)
        => SyntaxFactory.DefaultExpression(keyword, openParenToken, type, closeParenToken);

    public static DefaultSwitchLabelSyntax DefaultSwitchLabel()
        => SyntaxFactory.DefaultSwitchLabel();

    public static DefaultSwitchLabelSyntax DefaultSwitchLabel(SyntaxToken colonToken)
        => SyntaxFactory.DefaultSwitchLabel(colonToken);

    public static DefaultSwitchLabelSyntax DefaultSwitchLabel(SyntaxToken keyword, SyntaxToken colonToken)
        => SyntaxFactory.DefaultSwitchLabel(keyword, colonToken);

    public static DefineDirectiveTriviaSyntax DefineDirectiveTrivia(SyntaxToken name, bool isActive)
        => SyntaxFactory.DefineDirectiveTrivia(name, isActive);

    public static DefineDirectiveTriviaSyntax DefineDirectiveTrivia(string name, bool isActive)
        => SyntaxFactory.DefineDirectiveTrivia(name, isActive);

    public static DefineDirectiveTriviaSyntax DefineDirectiveTrivia(SyntaxToken hashToken, SyntaxToken defineKeyword, SyntaxToken name, SyntaxToken endOfDirectiveToken, bool isActive)
        => SyntaxFactory.DefineDirectiveTrivia(hashToken, defineKeyword, name, endOfDirectiveToken, isActive);

    public static DelegateDeclarationSyntax DelegateDeclaration(TypeSyntax returnType, SyntaxToken identifier)
        => SyntaxFactory.DelegateDeclaration(returnType, identifier);

    public static DelegateDeclarationSyntax DelegateDeclaration(TypeSyntax returnType, string identifier)
        => SyntaxFactory.DelegateDeclaration(returnType, identifier);

    public static DelegateDeclarationSyntax DelegateDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses)
        => SyntaxFactory.DelegateDeclaration(attributeLists, modifiers, returnType, identifier, typeParameterList, parameterList, constraintClauses);

    public static DelegateDeclarationSyntax DelegateDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken delegateKeyword, TypeSyntax returnType, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken semicolonToken)
        => SyntaxFactory.DelegateDeclaration(attributeLists, modifiers, delegateKeyword, returnType, identifier, typeParameterList, parameterList, constraintClauses, semicolonToken);

    public static DestructorDeclarationSyntax DestructorDeclaration(SyntaxToken identifier)
        => SyntaxFactory.DestructorDeclaration(identifier);

    public static DestructorDeclarationSyntax DestructorDeclaration(string identifier)
        => SyntaxFactory.DestructorDeclaration(identifier);

    public static DestructorDeclarationSyntax DestructorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, ParameterListSyntax parameterList, BlockSyntax body)
        => SyntaxFactory.DestructorDeclaration(attributeLists, modifiers, identifier, parameterList, body);

    public static DestructorDeclarationSyntax DestructorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, ParameterListSyntax parameterList, ArrowExpressionClauseSyntax expressionBody)
        => SyntaxFactory.DestructorDeclaration(attributeLists, modifiers, identifier, parameterList, expressionBody);

    public static DestructorDeclarationSyntax DestructorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody)
        => SyntaxFactory.DestructorDeclaration(attributeLists, modifiers, identifier, parameterList, body, expressionBody);

    public static DestructorDeclarationSyntax DestructorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken tildeToken, SyntaxToken identifier, ParameterListSyntax parameterList, BlockSyntax body, SyntaxToken semicolonToken)
        => SyntaxFactory.DestructorDeclaration(attributeLists, modifiers, tildeToken, identifier, parameterList, body, semicolonToken);

    public static DestructorDeclarationSyntax DestructorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken tildeToken, SyntaxToken identifier, ParameterListSyntax parameterList, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
        => SyntaxFactory.DestructorDeclaration(attributeLists, modifiers, tildeToken, identifier, parameterList, expressionBody, semicolonToken);

    public static DestructorDeclarationSyntax DestructorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken tildeToken, SyntaxToken identifier, ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
        => SyntaxFactory.DestructorDeclaration(attributeLists, modifiers, tildeToken, identifier, parameterList, body, expressionBody, semicolonToken);

    public static SyntaxTrivia DisabledText(string text)
        => SyntaxFactory.DisabledText(text);

    public static DiscardDesignationSyntax DiscardDesignation()
        => SyntaxFactory.DiscardDesignation();

    public static DiscardDesignationSyntax DiscardDesignation(SyntaxToken underscoreToken)
        => SyntaxFactory.DiscardDesignation(underscoreToken);

    public static DiscardPatternSyntax DiscardPattern()
        => SyntaxFactory.DiscardPattern();

    public static DiscardPatternSyntax DiscardPattern(SyntaxToken underscoreToken)
        => SyntaxFactory.DiscardPattern(underscoreToken);

    public static DocumentationCommentTriviaSyntax DocumentationComment(XmlNodeSyntax[] content)
        => SyntaxFactory.DocumentationComment(content);

    public static SyntaxTrivia DocumentationCommentExterior(string text)
        => SyntaxFactory.DocumentationCommentExterior(text);

    public static DocumentationCommentTriviaSyntax DocumentationCommentTrivia(SyntaxKind kind, SyntaxList<XmlNodeSyntax> content)
        => SyntaxFactory.DocumentationCommentTrivia(kind, content);

    public static DocumentationCommentTriviaSyntax DocumentationCommentTrivia(SyntaxKind kind, SyntaxList<XmlNodeSyntax> content, SyntaxToken endOfComment)
        => SyntaxFactory.DocumentationCommentTrivia(kind, content, endOfComment);

    public static DoStatementSyntax DoStatement(StatementSyntax statement, ExpressionSyntax condition)
        => SyntaxFactory.DoStatement(statement, condition);

    public static DoStatementSyntax DoStatement(SyntaxList<AttributeListSyntax> attributeLists, StatementSyntax statement, ExpressionSyntax condition)
        => SyntaxFactory.DoStatement(attributeLists, statement, condition);

    public static DoStatementSyntax DoStatement(SyntaxToken doKeyword, StatementSyntax statement, SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, SyntaxToken semicolonToken)
        => SyntaxFactory.DoStatement(doKeyword, statement, whileKeyword, openParenToken, condition, closeParenToken, semicolonToken);

    public static DoStatementSyntax DoStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken doKeyword, StatementSyntax statement, SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, SyntaxToken semicolonToken)
        => SyntaxFactory.DoStatement(attributeLists, doKeyword, statement, whileKeyword, openParenToken, condition, closeParenToken, semicolonToken);

    public static SyntaxTrivia ElasticEndOfLine(string text)
        => SyntaxFactory.ElasticEndOfLine(text);

    public static SyntaxTrivia ElasticWhitespace(string text)
        => SyntaxFactory.ElasticWhitespace(text);

    public static ElementAccessExpressionSyntax ElementAccessExpression(ExpressionSyntax expression)
        => SyntaxFactory.ElementAccessExpression(expression);

    public static ElementAccessExpressionSyntax ElementAccessExpression(ExpressionSyntax expression, BracketedArgumentListSyntax argumentList)
        => SyntaxFactory.ElementAccessExpression(expression, argumentList);

    public static ElementBindingExpressionSyntax ElementBindingExpression()
        => SyntaxFactory.ElementBindingExpression();

    public static ElementBindingExpressionSyntax ElementBindingExpression(BracketedArgumentListSyntax argumentList)
        => SyntaxFactory.ElementBindingExpression(argumentList);

    public static ElifDirectiveTriviaSyntax ElifDirectiveTrivia(ExpressionSyntax condition, bool isActive, bool branchTaken, bool conditionValue)
        => SyntaxFactory.ElifDirectiveTrivia(condition, isActive, branchTaken, conditionValue);

    public static ElifDirectiveTriviaSyntax ElifDirectiveTrivia(SyntaxToken hashToken, SyntaxToken elifKeyword, ExpressionSyntax condition, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken, bool conditionValue)
        => SyntaxFactory.ElifDirectiveTrivia(hashToken, elifKeyword, condition, endOfDirectiveToken, isActive, branchTaken, conditionValue);

    public static ElseClauseSyntax ElseClause(StatementSyntax statement)
        => SyntaxFactory.ElseClause(statement);

    public static ElseClauseSyntax ElseClause(SyntaxToken elseKeyword, StatementSyntax statement)
        => SyntaxFactory.ElseClause(elseKeyword, statement);

    public static ElseDirectiveTriviaSyntax ElseDirectiveTrivia(bool isActive, bool branchTaken)
        => SyntaxFactory.ElseDirectiveTrivia(isActive, branchTaken);

    public static ElseDirectiveTriviaSyntax ElseDirectiveTrivia(SyntaxToken hashToken, SyntaxToken elseKeyword, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken)
        => SyntaxFactory.ElseDirectiveTrivia(hashToken, elseKeyword, endOfDirectiveToken, isActive, branchTaken);

    public static EmptyStatementSyntax EmptyStatement()
        => SyntaxFactory.EmptyStatement();

    public static EmptyStatementSyntax EmptyStatement(SyntaxToken semicolonToken)
        => SyntaxFactory.EmptyStatement(semicolonToken);

    public static EmptyStatementSyntax EmptyStatement(SyntaxList<AttributeListSyntax> attributeLists)
        => SyntaxFactory.EmptyStatement(attributeLists);

    public static EmptyStatementSyntax EmptyStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken semicolonToken)
        => SyntaxFactory.EmptyStatement(attributeLists, semicolonToken);

    public static EndIfDirectiveTriviaSyntax EndIfDirectiveTrivia(bool isActive)
        => SyntaxFactory.EndIfDirectiveTrivia(isActive);

    public static EndIfDirectiveTriviaSyntax EndIfDirectiveTrivia(SyntaxToken hashToken, SyntaxToken endIfKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
        => SyntaxFactory.EndIfDirectiveTrivia(hashToken, endIfKeyword, endOfDirectiveToken, isActive);

    public static SyntaxTrivia EndOfLine(string text)
        => SyntaxFactory.EndOfLine(text);

    public static EndRegionDirectiveTriviaSyntax EndRegionDirectiveTrivia(bool isActive)
        => SyntaxFactory.EndRegionDirectiveTrivia(isActive);

    public static EndRegionDirectiveTriviaSyntax EndRegionDirectiveTrivia(SyntaxToken hashToken, SyntaxToken endRegionKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
        => SyntaxFactory.EndRegionDirectiveTrivia(hashToken, endRegionKeyword, endOfDirectiveToken, isActive);

    public static EnumDeclarationSyntax EnumDeclaration(SyntaxToken identifier)
        => SyntaxFactory.EnumDeclaration(identifier);

    public static EnumDeclarationSyntax EnumDeclaration(string identifier)
        => SyntaxFactory.EnumDeclaration(identifier);

    public static EnumDeclarationSyntax EnumDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, BaseListSyntax baseList, SeparatedSyntaxList<EnumMemberDeclarationSyntax> members)
        => SyntaxFactory.EnumDeclaration(attributeLists, modifiers, identifier, baseList, members);

    public static EnumDeclarationSyntax EnumDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken enumKeyword, SyntaxToken identifier, BaseListSyntax baseList, SyntaxToken openBraceToken, SeparatedSyntaxList<EnumMemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
        => SyntaxFactory.EnumDeclaration(attributeLists, modifiers, enumKeyword, identifier, baseList, openBraceToken, members, closeBraceToken, semicolonToken);

    public static EnumMemberDeclarationSyntax EnumMemberDeclaration(string identifier)
        => SyntaxFactory.EnumMemberDeclaration(identifier);

    public static EnumMemberDeclarationSyntax EnumMemberDeclaration(SyntaxToken identifier)
        => SyntaxFactory.EnumMemberDeclaration(identifier);

    public static EnumMemberDeclarationSyntax EnumMemberDeclaration(string identifier, EqualsValueClauseSyntax equalsValue)
        => SyntaxFactory.EnumMemberDeclaration(List<AttributeListSyntax>(), Identifier(identifier), equalsValue);

    public static EnumMemberDeclarationSyntax EnumMemberDeclaration(SyntaxToken identifier, EqualsValueClauseSyntax equalsValue)
        => SyntaxFactory.EnumMemberDeclaration(List<AttributeListSyntax>(), identifier, equalsValue);

    public static EnumMemberDeclarationSyntax EnumMemberDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken identifier, EqualsValueClauseSyntax equalsValue)
        => SyntaxFactory.EnumMemberDeclaration(attributeLists, identifier, equalsValue);

    public static EnumMemberDeclarationSyntax EnumMemberDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, EqualsValueClauseSyntax equalsValue)
         => SyntaxFactory.EnumMemberDeclaration(attributeLists, modifiers, identifier, equalsValue);

    public static EqualsValueClauseSyntax EqualsValueClause(ExpressionSyntax value)
        => SyntaxFactory.EqualsValueClause(value);

    public static EqualsValueClauseSyntax EqualsValueClause(SyntaxToken equalsToken, ExpressionSyntax value)
        => SyntaxFactory.EqualsValueClause(equalsToken, value);

    public static ErrorDirectiveTriviaSyntax ErrorDirectiveTrivia(bool isActive)
        => SyntaxFactory.ErrorDirectiveTrivia(isActive);

    public static ErrorDirectiveTriviaSyntax ErrorDirectiveTrivia(SyntaxToken hashToken, SyntaxToken errorKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
        => SyntaxFactory.ErrorDirectiveTrivia(hashToken, errorKeyword, endOfDirectiveToken, isActive);

    public static EventDeclarationSyntax EventDeclaration(TypeSyntax type, SyntaxToken identifier)
        => SyntaxFactory.EventDeclaration(type, identifier);

    public static EventDeclarationSyntax EventDeclaration(TypeSyntax type, string identifier)
        => SyntaxFactory.EventDeclaration(type, identifier);

    public static EventDeclarationSyntax EventDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, AccessorListSyntax accessorList)
        => SyntaxFactory.EventDeclaration(attributeLists, modifiers, type, explicitInterfaceSpecifier, identifier, accessorList);

    public static EventDeclarationSyntax EventDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken eventKeyword, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, AccessorListSyntax accessorList)
        => SyntaxFactory.EventDeclaration(attributeLists, modifiers, eventKeyword, type, explicitInterfaceSpecifier, identifier, accessorList);

    public static EventDeclarationSyntax EventDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken eventKeyword, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, SyntaxToken semicolonToken)
        => SyntaxFactory.EventDeclaration(attributeLists, modifiers, eventKeyword, type, explicitInterfaceSpecifier, identifier, semicolonToken);

    public static EventDeclarationSyntax EventDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken eventKeyword, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, AccessorListSyntax accessorList, SyntaxToken semicolonToken)
        => SyntaxFactory.EventDeclaration(attributeLists, modifiers, eventKeyword, type, explicitInterfaceSpecifier, identifier, accessorList, semicolonToken);

    public static EventFieldDeclarationSyntax EventFieldDeclaration(VariableDeclarationSyntax declaration)
        => SyntaxFactory.EventFieldDeclaration(declaration);

    public static EventFieldDeclarationSyntax EventFieldDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, VariableDeclarationSyntax declaration)
        => SyntaxFactory.EventFieldDeclaration(attributeLists, modifiers, declaration);

    public static EventFieldDeclarationSyntax EventFieldDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken eventKeyword, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
        => SyntaxFactory.EventFieldDeclaration(attributeLists, modifiers, eventKeyword, declaration, semicolonToken);

    public static ExplicitInterfaceSpecifierSyntax ExplicitInterfaceSpecifier(NameSyntax name)
        => SyntaxFactory.ExplicitInterfaceSpecifier(name);

    public static ExplicitInterfaceSpecifierSyntax ExplicitInterfaceSpecifier(NameSyntax name, SyntaxToken dotToken)
        => SyntaxFactory.ExplicitInterfaceSpecifier(name, dotToken);

    public static ExpressionColonSyntax ExpressionColon(ExpressionSyntax expression, SyntaxToken colonToken)
        => SyntaxFactory.ExpressionColon(expression, colonToken);

    public static ExpressionElementSyntax ExpressionElement(ExpressionSyntax expression)
        => SyntaxFactory.ExpressionElement(expression);

    public static ExpressionStatementSyntax ExpressionStatement(ExpressionSyntax expression)
        => SyntaxFactory.ExpressionStatement(expression);

    public static ExpressionStatementSyntax ExpressionStatement(ExpressionSyntax expression, SyntaxToken semicolonToken)
        => SyntaxFactory.ExpressionStatement(expression, semicolonToken);

    public static ExpressionStatementSyntax ExpressionStatement(SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax expression)
        => SyntaxFactory.ExpressionStatement(attributeLists, expression);

    public static ExpressionStatementSyntax ExpressionStatement(SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax expression, SyntaxToken semicolonToken)
        => SyntaxFactory.ExpressionStatement(attributeLists, expression, semicolonToken);

    public static ExternAliasDirectiveSyntax ExternAliasDirective(SyntaxToken identifier)
        => SyntaxFactory.ExternAliasDirective(identifier);

    public static ExternAliasDirectiveSyntax ExternAliasDirective(string identifier)
        => SyntaxFactory.ExternAliasDirective(identifier);

    public static ExternAliasDirectiveSyntax ExternAliasDirective(SyntaxToken externKeyword, SyntaxToken aliasKeyword, SyntaxToken identifier, SyntaxToken semicolonToken)
        => SyntaxFactory.ExternAliasDirective(externKeyword, aliasKeyword, identifier, semicolonToken);

    public static FieldDeclarationSyntax FieldDeclaration(VariableDeclarationSyntax declaration)
        => SyntaxFactory.FieldDeclaration(declaration);

    public static FieldDeclarationSyntax FieldDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, VariableDeclarationSyntax declaration)
        => SyntaxFactory.FieldDeclaration(attributeLists, modifiers, declaration);

    public static FieldDeclarationSyntax FieldDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
        => SyntaxFactory.FieldDeclaration(attributeLists, modifiers, declaration, semicolonToken);

    public static FieldExpressionSyntax FieldExpression()
        => SyntaxFactory.FieldExpression();

    public static FieldExpressionSyntax FieldExpression(SyntaxToken token)
        => SyntaxFactory.FieldExpression(token);

    public static FileScopedNamespaceDeclarationSyntax FileScopedNamespaceDeclaration(NameSyntax name)
        => SyntaxFactory.FileScopedNamespaceDeclaration(name);

    public static FileScopedNamespaceDeclarationSyntax FileScopedNamespaceDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, NameSyntax name, SyntaxList<ExternAliasDirectiveSyntax> externs, SyntaxList<UsingDirectiveSyntax> usings, SyntaxList<MemberDeclarationSyntax> members)
        => SyntaxFactory.FileScopedNamespaceDeclaration(attributeLists, modifiers, name, externs, usings, members);

    public static FileScopedNamespaceDeclarationSyntax FileScopedNamespaceDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken namespaceKeyword, NameSyntax name, SyntaxToken semicolonToken, SyntaxList<ExternAliasDirectiveSyntax> externs, SyntaxList<UsingDirectiveSyntax> usings, SyntaxList<MemberDeclarationSyntax> members)
        => SyntaxFactory.FileScopedNamespaceDeclaration(attributeLists, modifiers, namespaceKeyword, name, semicolonToken, externs, usings, members);

    public static FinallyClauseSyntax FinallyClause(BlockSyntax block)
        => SyntaxFactory.FinallyClause(block);

    public static FinallyClauseSyntax FinallyClause(SyntaxToken finallyKeyword, BlockSyntax block)
        => SyntaxFactory.FinallyClause(finallyKeyword, block);

    public static FixedStatementSyntax FixedStatement(VariableDeclarationSyntax declaration, StatementSyntax statement)
        => SyntaxFactory.FixedStatement(declaration, statement);

    public static FixedStatementSyntax FixedStatement(SyntaxList<AttributeListSyntax> attributeLists, VariableDeclarationSyntax declaration, StatementSyntax statement)
        => SyntaxFactory.FixedStatement(attributeLists, declaration, statement);

    public static FixedStatementSyntax FixedStatement(SyntaxToken fixedKeyword, SyntaxToken openParenToken, VariableDeclarationSyntax declaration, SyntaxToken closeParenToken, StatementSyntax statement)
        => SyntaxFactory.FixedStatement(fixedKeyword, openParenToken, declaration, closeParenToken, statement);

    public static FixedStatementSyntax FixedStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken fixedKeyword, SyntaxToken openParenToken, VariableDeclarationSyntax declaration, SyntaxToken closeParenToken, StatementSyntax statement)
        => SyntaxFactory.FixedStatement(attributeLists, fixedKeyword, openParenToken, declaration, closeParenToken, statement);

    public static ForEachStatementSyntax ForEachStatement(TypeSyntax type, SyntaxToken identifier, ExpressionSyntax expression, StatementSyntax statement)
        => SyntaxFactory.ForEachStatement(type, identifier, expression, statement);

    public static ForEachStatementSyntax ForEachStatement(TypeSyntax type, string identifier, ExpressionSyntax expression, StatementSyntax statement)
        => SyntaxFactory.ForEachStatement(type, identifier, expression, statement);

    public static ForEachStatementSyntax ForEachStatement(SyntaxList<AttributeListSyntax> attributeLists, TypeSyntax type, SyntaxToken identifier, ExpressionSyntax expression, StatementSyntax statement)
        => SyntaxFactory.ForEachStatement(attributeLists, type, identifier, expression, statement);

    public static ForEachStatementSyntax ForEachStatement(SyntaxToken forEachKeyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
        => SyntaxFactory.ForEachStatement(forEachKeyword, openParenToken, type, identifier, inKeyword, expression, closeParenToken, statement);

    public static ForEachStatementSyntax ForEachStatement(SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
        => SyntaxFactory.ForEachStatement(awaitKeyword, forEachKeyword, openParenToken, type, identifier, inKeyword, expression, closeParenToken, statement);

    public static ForEachStatementSyntax ForEachStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
        => SyntaxFactory.ForEachStatement(attributeLists, awaitKeyword, forEachKeyword, openParenToken, type, identifier, inKeyword, expression, closeParenToken, statement);

    public static ForEachVariableStatementSyntax ForEachVariableStatement(ExpressionSyntax variable, ExpressionSyntax expression, StatementSyntax statement)
        => SyntaxFactory.ForEachVariableStatement(variable, expression, statement);

    public static ForEachVariableStatementSyntax ForEachVariableStatement(SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax variable, ExpressionSyntax expression, StatementSyntax statement)
        => SyntaxFactory.ForEachVariableStatement(attributeLists, variable, expression, statement);

    public static ForEachVariableStatementSyntax ForEachVariableStatement(SyntaxToken forEachKeyword, SyntaxToken openParenToken, ExpressionSyntax variable, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
        => SyntaxFactory.ForEachVariableStatement(forEachKeyword, openParenToken, variable, inKeyword, expression, closeParenToken, statement);

    public static ForEachVariableStatementSyntax ForEachVariableStatement(SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, ExpressionSyntax variable, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
        => SyntaxFactory.ForEachVariableStatement(awaitKeyword, forEachKeyword, openParenToken, variable, inKeyword, expression, closeParenToken, statement);

    public static ForEachVariableStatementSyntax ForEachVariableStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, ExpressionSyntax variable, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
        => SyntaxFactory.ForEachVariableStatement(attributeLists, awaitKeyword, forEachKeyword, openParenToken, variable, inKeyword, expression, closeParenToken, statement);

    public static ForStatementSyntax ForStatement(StatementSyntax statement)
        => SyntaxFactory.ForStatement(statement);

    public static ForStatementSyntax ForStatement(VariableDeclarationSyntax declaration, SeparatedSyntaxList<ExpressionSyntax> initializers, ExpressionSyntax condition, SeparatedSyntaxList<ExpressionSyntax> incrementors, StatementSyntax statement)
        => SyntaxFactory.ForStatement(declaration, initializers, condition, incrementors, statement);

    public static ForStatementSyntax ForStatement(SyntaxList<AttributeListSyntax> attributeLists, VariableDeclarationSyntax declaration, SeparatedSyntaxList<ExpressionSyntax> initializers, ExpressionSyntax condition, SeparatedSyntaxList<ExpressionSyntax> incrementors, StatementSyntax statement)
        => SyntaxFactory.ForStatement(attributeLists, declaration, initializers, condition, incrementors, statement);

    public static ForStatementSyntax ForStatement(SyntaxToken forKeyword, SyntaxToken openParenToken, VariableDeclarationSyntax declaration, SeparatedSyntaxList<ExpressionSyntax> initializers, SyntaxToken firstSemicolonToken, ExpressionSyntax condition, SyntaxToken secondSemicolonToken, SeparatedSyntaxList<ExpressionSyntax> incrementors, SyntaxToken closeParenToken, StatementSyntax statement)
        => SyntaxFactory.ForStatement(forKeyword, openParenToken, declaration, initializers, firstSemicolonToken, condition, secondSemicolonToken, incrementors, closeParenToken, statement);

    public static ForStatementSyntax ForStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken forKeyword, SyntaxToken openParenToken, VariableDeclarationSyntax declaration, SeparatedSyntaxList<ExpressionSyntax> initializers, SyntaxToken firstSemicolonToken, ExpressionSyntax condition, SyntaxToken secondSemicolonToken, SeparatedSyntaxList<ExpressionSyntax> incrementors, SyntaxToken closeParenToken, StatementSyntax statement)
        => SyntaxFactory.ForStatement(attributeLists, forKeyword, openParenToken, declaration, initializers, firstSemicolonToken, condition, secondSemicolonToken, incrementors, closeParenToken, statement);

    public static FromClauseSyntax FromClause(SyntaxToken identifier, ExpressionSyntax expression)
        => SyntaxFactory.FromClause(identifier, expression);

    public static FromClauseSyntax FromClause(string identifier, ExpressionSyntax expression)
        => SyntaxFactory.FromClause(identifier, expression);

    public static FromClauseSyntax FromClause(TypeSyntax type, SyntaxToken identifier, ExpressionSyntax expression)
        => SyntaxFactory.FromClause(type, identifier, expression);

    public static FromClauseSyntax FromClause(SyntaxToken fromKeyword, TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax expression)
        => SyntaxFactory.FromClause(fromKeyword, type, identifier, inKeyword, expression);

    public static FunctionPointerCallingConventionSyntax FunctionPointerCallingConvention(SyntaxToken managedOrUnmanagedKeyword)
        => SyntaxFactory.FunctionPointerCallingConvention(managedOrUnmanagedKeyword);

    public static FunctionPointerCallingConventionSyntax FunctionPointerCallingConvention(SyntaxToken managedOrUnmanagedKeyword, FunctionPointerUnmanagedCallingConventionListSyntax unmanagedCallingConventionList)
        => SyntaxFactory.FunctionPointerCallingConvention(managedOrUnmanagedKeyword, unmanagedCallingConventionList);

    public static FunctionPointerParameterSyntax FunctionPointerParameter(TypeSyntax type)
        => SyntaxFactory.FunctionPointerParameter(type);

    public static FunctionPointerParameterSyntax FunctionPointerParameter(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax type)
        => SyntaxFactory.FunctionPointerParameter(attributeLists, modifiers, type);

    public static FunctionPointerParameterListSyntax FunctionPointerParameterList(SeparatedSyntaxList<FunctionPointerParameterSyntax> parameters)
        => SyntaxFactory.FunctionPointerParameterList(parameters);

    public static FunctionPointerParameterListSyntax FunctionPointerParameterList(SyntaxToken lessThanToken, SeparatedSyntaxList<FunctionPointerParameterSyntax> parameters, SyntaxToken greaterThanToken)
        => SyntaxFactory.FunctionPointerParameterList(lessThanToken, parameters, greaterThanToken);

    public static FunctionPointerTypeSyntax FunctionPointerType()
        => SyntaxFactory.FunctionPointerType();

    public static FunctionPointerTypeSyntax FunctionPointerType(FunctionPointerCallingConventionSyntax callingConvention, FunctionPointerParameterListSyntax parameterList)
        => SyntaxFactory.FunctionPointerType(callingConvention, parameterList);

    public static FunctionPointerTypeSyntax FunctionPointerType(SyntaxToken delegateKeyword, SyntaxToken asteriskToken, FunctionPointerCallingConventionSyntax callingConvention, FunctionPointerParameterListSyntax parameterList)
        => SyntaxFactory.FunctionPointerType(delegateKeyword, asteriskToken, callingConvention, parameterList);

    public static FunctionPointerUnmanagedCallingConventionSyntax FunctionPointerUnmanagedCallingConvention(SyntaxToken name)
        => SyntaxFactory.FunctionPointerUnmanagedCallingConvention(name);

    public static FunctionPointerUnmanagedCallingConventionListSyntax FunctionPointerUnmanagedCallingConventionList(SeparatedSyntaxList<FunctionPointerUnmanagedCallingConventionSyntax> callingConventions)
        => SyntaxFactory.FunctionPointerUnmanagedCallingConventionList(callingConventions);

    public static FunctionPointerUnmanagedCallingConventionListSyntax FunctionPointerUnmanagedCallingConventionList(SyntaxToken openBracketToken, SeparatedSyntaxList<FunctionPointerUnmanagedCallingConventionSyntax> callingConventions, SyntaxToken closeBracketToken)
        => SyntaxFactory.FunctionPointerUnmanagedCallingConventionList(openBracketToken, callingConventions, closeBracketToken);

    public static GenericNameSyntax GenericName(SyntaxToken identifier)
        => SyntaxFactory.GenericName(identifier);

    public static GenericNameSyntax GenericName(string identifier)
        => SyntaxFactory.GenericName(identifier);

    public static GenericNameSyntax GenericName(SyntaxToken identifier, TypeArgumentListSyntax typeArgumentList)
        => SyntaxFactory.GenericName(identifier, typeArgumentList);

    public static ExpressionSyntax? GetNonGenericExpression(ExpressionSyntax expression)
        => SyntaxFactory.GetNonGenericExpression(expression);

    public static ExpressionSyntax GetStandaloneExpression(ExpressionSyntax expression)
        => SyntaxFactory.GetStandaloneExpression(expression);

    public static GlobalStatementSyntax GlobalStatement(StatementSyntax statement)
        => SyntaxFactory.GlobalStatement(statement);

    public static GlobalStatementSyntax GlobalStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, StatementSyntax statement)
        => SyntaxFactory.GlobalStatement(attributeLists, modifiers, statement);

    public static GotoStatementSyntax GotoStatement(SyntaxKind kind, ExpressionSyntax expression)
        => SyntaxFactory.GotoStatement(kind, expression);

    public static GotoStatementSyntax GotoStatement(SyntaxKind kind, SyntaxToken caseOrDefaultKeyword, ExpressionSyntax expression)
        => SyntaxFactory.GotoStatement(kind, caseOrDefaultKeyword, expression);

    public static GotoStatementSyntax GotoStatement(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken caseOrDefaultKeyword, ExpressionSyntax expression)
        => SyntaxFactory.GotoStatement(kind, attributeLists, caseOrDefaultKeyword, expression);

    public static GotoStatementSyntax GotoStatement(SyntaxKind kind, SyntaxToken gotoKeyword, SyntaxToken caseOrDefaultKeyword, ExpressionSyntax expression, SyntaxToken semicolonToken)
        => SyntaxFactory.GotoStatement(kind, gotoKeyword, caseOrDefaultKeyword, expression, semicolonToken);

    public static GotoStatementSyntax GotoStatement(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken gotoKeyword, SyntaxToken caseOrDefaultKeyword, ExpressionSyntax expression, SyntaxToken semicolonToken)
        => SyntaxFactory.GotoStatement(kind, attributeLists, gotoKeyword, caseOrDefaultKeyword, expression, semicolonToken);

    public static GroupClauseSyntax GroupClause(ExpressionSyntax groupExpression, ExpressionSyntax byExpression)
        => SyntaxFactory.GroupClause(groupExpression, byExpression);

    public static GroupClauseSyntax GroupClause(SyntaxToken groupKeyword, ExpressionSyntax groupExpression, SyntaxToken byKeyword, ExpressionSyntax byExpression)
        => SyntaxFactory.GroupClause(groupKeyword, groupExpression, byKeyword, byExpression);

    public static SyntaxToken Identifier(string text)
        => SyntaxFactory.Identifier(text);

    public static SyntaxToken Identifier(SyntaxTriviaList leading, string text, SyntaxTriviaList trailing)
        => SyntaxFactory.Identifier(leading, text, trailing);

    public static SyntaxToken Identifier(SyntaxTriviaList leading, SyntaxKind contextualKind, string text, string valueText, SyntaxTriviaList trailing)
        => SyntaxFactory.Identifier(leading, contextualKind, text, valueText, trailing);

    public static IdentifierNameSyntax IdentifierName(string name)
        => SyntaxFactory.IdentifierName(name);

    public static IdentifierNameSyntax IdentifierName(SyntaxToken identifier)
        => SyntaxFactory.IdentifierName(identifier);

    public static IfDirectiveTriviaSyntax IfDirectiveTrivia(ExpressionSyntax condition, bool isActive, bool branchTaken, bool conditionValue)
        => SyntaxFactory.IfDirectiveTrivia(condition, isActive, branchTaken, conditionValue);

    public static IfDirectiveTriviaSyntax IfDirectiveTrivia(SyntaxToken hashToken, SyntaxToken ifKeyword, ExpressionSyntax condition, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken, bool conditionValue)
        => SyntaxFactory.IfDirectiveTrivia(hashToken, ifKeyword, condition, endOfDirectiveToken, isActive, branchTaken, conditionValue);

    public static IfStatementSyntax IfStatement(ExpressionSyntax condition, StatementSyntax statement)
        => SyntaxFactory.IfStatement(condition, statement);

    public static IfStatementSyntax IfStatement(ExpressionSyntax condition, StatementSyntax statement, ElseClauseSyntax @else)
        => SyntaxFactory.IfStatement(condition, statement, @else);

    public static IfStatementSyntax IfStatement(SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax condition, StatementSyntax statement, ElseClauseSyntax @else)
        => SyntaxFactory.IfStatement(attributeLists, condition, statement, @else);

    public static IfStatementSyntax IfStatement(SyntaxToken ifKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement, ElseClauseSyntax @else)
        => SyntaxFactory.IfStatement(ifKeyword, openParenToken, condition, closeParenToken, statement, @else);

    public static IfStatementSyntax IfStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken ifKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement, ElseClauseSyntax @else)
        => SyntaxFactory.IfStatement(attributeLists, ifKeyword, openParenToken, condition, closeParenToken, statement, @else);

    public static ImplicitArrayCreationExpressionSyntax ImplicitArrayCreationExpression(InitializerExpressionSyntax initializer)
        => SyntaxFactory.ImplicitArrayCreationExpression(initializer);

    public static ImplicitArrayCreationExpressionSyntax ImplicitArrayCreationExpression(SyntaxTokenList commas, InitializerExpressionSyntax initializer)
        => SyntaxFactory.ImplicitArrayCreationExpression(commas, initializer);

    public static ImplicitArrayCreationExpressionSyntax ImplicitArrayCreationExpression(SyntaxToken newKeyword, SyntaxToken openBracketToken, SyntaxTokenList commas, SyntaxToken closeBracketToken, InitializerExpressionSyntax initializer)
        => SyntaxFactory.ImplicitArrayCreationExpression(newKeyword, openBracketToken, commas, closeBracketToken, initializer);

    public static ImplicitElementAccessSyntax ImplicitElementAccess()
        => SyntaxFactory.ImplicitElementAccess();

    public static ImplicitElementAccessSyntax ImplicitElementAccess(BracketedArgumentListSyntax argumentList)
        => SyntaxFactory.ImplicitElementAccess(argumentList);

    public static ImplicitObjectCreationExpressionSyntax ImplicitObjectCreationExpression()
        => SyntaxFactory.ImplicitObjectCreationExpression();

    public static ImplicitObjectCreationExpressionSyntax ImplicitObjectCreationExpression(ArgumentListSyntax argumentList, InitializerExpressionSyntax initializer)
        => SyntaxFactory.ImplicitObjectCreationExpression(argumentList, initializer);

    public static ImplicitObjectCreationExpressionSyntax ImplicitObjectCreationExpression(SyntaxToken newKeyword, ArgumentListSyntax argumentList, InitializerExpressionSyntax initializer)
        => SyntaxFactory.ImplicitObjectCreationExpression(newKeyword, argumentList, initializer);

    public static ImplicitStackAllocArrayCreationExpressionSyntax ImplicitStackAllocArrayCreationExpression(InitializerExpressionSyntax initializer)
        => SyntaxFactory.ImplicitStackAllocArrayCreationExpression(initializer);

    public static ImplicitStackAllocArrayCreationExpressionSyntax ImplicitStackAllocArrayCreationExpression(SyntaxToken stackAllocKeyword, SyntaxToken openBracketToken, SyntaxToken closeBracketToken, InitializerExpressionSyntax initializer)
        => SyntaxFactory.ImplicitStackAllocArrayCreationExpression(stackAllocKeyword, openBracketToken, closeBracketToken, initializer);

    public static IncompleteMemberSyntax IncompleteMember(TypeSyntax type)
        => SyntaxFactory.IncompleteMember(type);

    public static IncompleteMemberSyntax IncompleteMember(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax type)
        => SyntaxFactory.IncompleteMember(attributeLists, modifiers, type);

    public static IndexerDeclarationSyntax IndexerDeclaration(TypeSyntax type)
        => SyntaxFactory.IndexerDeclaration(type);

    public static IndexerDeclarationSyntax IndexerDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, BracketedParameterListSyntax parameterList, AccessorListSyntax accessorList)
        => SyntaxFactory.IndexerDeclaration(attributeLists, modifiers, type, explicitInterfaceSpecifier, parameterList, accessorList);

    public static IndexerDeclarationSyntax IndexerDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, BracketedParameterListSyntax parameterList, AccessorListSyntax accessorList, ArrowExpressionClauseSyntax expressionBody)
        => SyntaxFactory.IndexerDeclaration(attributeLists, modifiers, type, explicitInterfaceSpecifier, parameterList, accessorList, expressionBody);

    public static IndexerDeclarationSyntax IndexerDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken thisKeyword, BracketedParameterListSyntax parameterList, AccessorListSyntax accessorList, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
        => SyntaxFactory.IndexerDeclaration(attributeLists, modifiers, type, explicitInterfaceSpecifier, thisKeyword, parameterList, accessorList, expressionBody, semicolonToken);

    public static IndexerMemberCrefSyntax IndexerMemberCref(CrefBracketedParameterListSyntax parameters)
        => SyntaxFactory.IndexerMemberCref(parameters);

    public static IndexerMemberCrefSyntax IndexerMemberCref(SyntaxToken thisKeyword, CrefBracketedParameterListSyntax parameters)
        => SyntaxFactory.IndexerMemberCref(thisKeyword, parameters);

    public static InitializerExpressionSyntax InitializerExpression(SyntaxKind kind, SeparatedSyntaxList<ExpressionSyntax> expressions)
        => SyntaxFactory.InitializerExpression(kind, expressions);

    public static InitializerExpressionSyntax InitializerExpression(SyntaxKind kind, SyntaxToken openBraceToken, SeparatedSyntaxList<ExpressionSyntax> expressions, SyntaxToken closeBraceToken)
        => SyntaxFactory.InitializerExpression(kind, openBraceToken, expressions, closeBraceToken);

    public static InterfaceDeclarationSyntax InterfaceDeclaration(SyntaxToken identifier)
        => SyntaxFactory.InterfaceDeclaration(identifier);

    public static InterfaceDeclarationSyntax InterfaceDeclaration(string identifier)
        => SyntaxFactory.InterfaceDeclaration(identifier);

    public static InterfaceDeclarationSyntax InterfaceDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxList<MemberDeclarationSyntax> members)
        => SyntaxFactory.InterfaceDeclaration(attributeLists, modifiers, identifier, typeParameterList, baseList, constraintClauses, members);

    public static InterfaceDeclarationSyntax InterfaceDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
        => SyntaxFactory.InterfaceDeclaration(attributeLists, modifiers, keyword, identifier, typeParameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);

    public static InterfaceDeclarationSyntax InterfaceDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
        => SyntaxFactory.InterfaceDeclaration(attributeLists, modifiers, keyword, identifier, typeParameterList, parameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);

    public static InterpolatedStringExpressionSyntax InterpolatedStringExpression(SyntaxToken stringStartToken)
        => SyntaxFactory.InterpolatedStringExpression(stringStartToken);

    public static InterpolatedStringExpressionSyntax InterpolatedStringExpression(SyntaxToken stringStartToken, SyntaxList<InterpolatedStringContentSyntax> contents)
        => SyntaxFactory.InterpolatedStringExpression(stringStartToken, contents);

    public static InterpolatedStringExpressionSyntax InterpolatedStringExpression(SyntaxToken stringStartToken, SyntaxToken stringEndToken)
        => SyntaxFactory.InterpolatedStringExpression(stringStartToken, stringEndToken);

    public static InterpolatedStringExpressionSyntax InterpolatedStringExpression(SyntaxToken stringStartToken, SyntaxList<InterpolatedStringContentSyntax> contents, SyntaxToken stringEndToken)
        => SyntaxFactory.InterpolatedStringExpression(stringStartToken, contents, stringEndToken);

    public static InterpolatedStringTextSyntax InterpolatedStringText()
        => SyntaxFactory.InterpolatedStringText();

    public static InterpolatedStringTextSyntax InterpolatedStringText(SyntaxToken textToken)
        => SyntaxFactory.InterpolatedStringText(textToken);

    public static InterpolationSyntax Interpolation(ExpressionSyntax expression)
        => SyntaxFactory.Interpolation(expression);

    public static InterpolationSyntax Interpolation(ExpressionSyntax expression, InterpolationAlignmentClauseSyntax alignmentClause, InterpolationFormatClauseSyntax formatClause)
        => SyntaxFactory.Interpolation(expression, alignmentClause, formatClause);

    public static InterpolationSyntax Interpolation(SyntaxToken openBraceToken, ExpressionSyntax expression, InterpolationAlignmentClauseSyntax alignmentClause, InterpolationFormatClauseSyntax formatClause, SyntaxToken closeBraceToken)
        => SyntaxFactory.Interpolation(openBraceToken, expression, alignmentClause, formatClause, closeBraceToken);

    public static InterpolationAlignmentClauseSyntax InterpolationAlignmentClause(SyntaxToken commaToken, ExpressionSyntax value)
        => SyntaxFactory.InterpolationAlignmentClause(commaToken, value);

    public static InterpolationFormatClauseSyntax InterpolationFormatClause(SyntaxToken colonToken)
        => SyntaxFactory.InterpolationFormatClause(colonToken);

    public static InterpolationFormatClauseSyntax InterpolationFormatClause(SyntaxToken colonToken, SyntaxToken formatStringToken)
        => SyntaxFactory.InterpolationFormatClause(colonToken, formatStringToken);

    public static InvocationExpressionSyntax InvocationExpression(ExpressionSyntax expression)
        => SyntaxFactory.InvocationExpression(expression);

    public static InvocationExpressionSyntax InvocationExpression(ExpressionSyntax expression, ArgumentListSyntax argumentList)
        => SyntaxFactory.InvocationExpression(expression, argumentList);

    public static bool IsCompleteSubmission(SyntaxTree tree)
        => SyntaxFactory.IsCompleteSubmission(tree);

    public static IsPatternExpressionSyntax IsPatternExpression(ExpressionSyntax expression, PatternSyntax pattern)
        => SyntaxFactory.IsPatternExpression(expression, pattern);

    public static IsPatternExpressionSyntax IsPatternExpression(ExpressionSyntax expression, SyntaxToken isKeyword, PatternSyntax pattern)
        => SyntaxFactory.IsPatternExpression(expression, isKeyword, pattern);

    public static JoinClauseSyntax JoinClause(SyntaxToken identifier, ExpressionSyntax inExpression, ExpressionSyntax leftExpression, ExpressionSyntax rightExpression)
        => SyntaxFactory.JoinClause(identifier, inExpression, leftExpression, rightExpression);

    public static JoinClauseSyntax JoinClause(string identifier, ExpressionSyntax inExpression, ExpressionSyntax leftExpression, ExpressionSyntax rightExpression)
        => SyntaxFactory.JoinClause(identifier, inExpression, leftExpression, rightExpression);

    public static JoinClauseSyntax JoinClause(TypeSyntax type, SyntaxToken identifier, ExpressionSyntax inExpression, ExpressionSyntax leftExpression, ExpressionSyntax rightExpression, JoinIntoClauseSyntax into)
        => SyntaxFactory.JoinClause(type, identifier, inExpression, leftExpression, rightExpression, into);

    public static JoinClauseSyntax JoinClause(SyntaxToken joinKeyword, TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax inExpression, SyntaxToken onKeyword, ExpressionSyntax leftExpression, SyntaxToken equalsKeyword, ExpressionSyntax rightExpression, JoinIntoClauseSyntax into)
        => SyntaxFactory.JoinClause(joinKeyword, type, identifier, inKeyword, inExpression, onKeyword, leftExpression, equalsKeyword, rightExpression, into);

    public static JoinIntoClauseSyntax JoinIntoClause(SyntaxToken identifier)
        => SyntaxFactory.JoinIntoClause(identifier);

    public static JoinIntoClauseSyntax JoinIntoClause(string identifier)
        => SyntaxFactory.JoinIntoClause(identifier);

    public static JoinIntoClauseSyntax JoinIntoClause(SyntaxToken intoKeyword, SyntaxToken identifier)
        => SyntaxFactory.JoinIntoClause(intoKeyword, identifier);

    public static LabeledStatementSyntax LabeledStatement(SyntaxToken identifier, StatementSyntax statement)
        => SyntaxFactory.LabeledStatement(identifier, statement);

    public static LabeledStatementSyntax LabeledStatement(string identifier, StatementSyntax statement)
        => SyntaxFactory.LabeledStatement(identifier, statement);

    public static LabeledStatementSyntax LabeledStatement(SyntaxToken identifier, SyntaxToken colonToken, StatementSyntax statement)
        => SyntaxFactory.LabeledStatement(identifier, colonToken, statement);

    public static LabeledStatementSyntax LabeledStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken identifier, StatementSyntax statement)
        => SyntaxFactory.LabeledStatement(attributeLists, identifier, statement);

    public static LabeledStatementSyntax LabeledStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken identifier, SyntaxToken colonToken, StatementSyntax statement)
        => SyntaxFactory.LabeledStatement(attributeLists, identifier, colonToken, statement);

    public static LetClauseSyntax LetClause(SyntaxToken identifier, ExpressionSyntax expression)
        => SyntaxFactory.LetClause(identifier, expression);

    public static LetClauseSyntax LetClause(string identifier, ExpressionSyntax expression)
        => SyntaxFactory.LetClause(identifier, expression);

    public static LetClauseSyntax LetClause(SyntaxToken letKeyword, SyntaxToken identifier, SyntaxToken equalsToken, ExpressionSyntax expression)
        => SyntaxFactory.LetClause(letKeyword, identifier, equalsToken, expression);

    public static LineDirectivePositionSyntax LineDirectivePosition(SyntaxToken line, SyntaxToken character)
        => SyntaxFactory.LineDirectivePosition(line, character);

    public static LineDirectivePositionSyntax LineDirectivePosition(SyntaxToken openParenToken, SyntaxToken line, SyntaxToken commaToken, SyntaxToken character, SyntaxToken closeParenToken)
        => SyntaxFactory.LineDirectivePosition(openParenToken, line, commaToken, character, closeParenToken);

    public static LineDirectiveTriviaSyntax LineDirectiveTrivia(SyntaxToken line, bool isActive)
        => SyntaxFactory.LineDirectiveTrivia(line, isActive);

    public static LineDirectiveTriviaSyntax LineDirectiveTrivia(SyntaxToken line, SyntaxToken file, bool isActive)
        => SyntaxFactory.LineDirectiveTrivia(line, file, isActive);

    public static LineDirectiveTriviaSyntax LineDirectiveTrivia(SyntaxToken hashToken, SyntaxToken lineKeyword, SyntaxToken line, SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive)
        => SyntaxFactory.LineDirectiveTrivia(hashToken, lineKeyword, line, file, endOfDirectiveToken, isActive);

    public static LineSpanDirectiveTriviaSyntax LineSpanDirectiveTrivia(LineDirectivePositionSyntax start, LineDirectivePositionSyntax end, SyntaxToken file, bool isActive)
        => SyntaxFactory.LineSpanDirectiveTrivia(start, end, file, isActive);

    public static LineSpanDirectiveTriviaSyntax LineSpanDirectiveTrivia(LineDirectivePositionSyntax start, LineDirectivePositionSyntax end, SyntaxToken characterOffset, SyntaxToken file, bool isActive)
        => SyntaxFactory.LineSpanDirectiveTrivia(start, end, characterOffset, file, isActive);

    public static LineSpanDirectiveTriviaSyntax LineSpanDirectiveTrivia(SyntaxToken hashToken, SyntaxToken lineKeyword, LineDirectivePositionSyntax start, SyntaxToken minusToken, LineDirectivePositionSyntax end, SyntaxToken characterOffset, SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive)
        => SyntaxFactory.LineSpanDirectiveTrivia(hashToken, lineKeyword, start, minusToken, end, characterOffset, file, endOfDirectiveToken, isActive);

    public static SyntaxList<TNode> List<TNode>()
        where TNode : SyntaxNode
        => SyntaxFactory.List<TNode>();

    public static SyntaxList<TNode> List<TNode>(IEnumerable<TNode> nodes)
        where TNode : SyntaxNode
        => SyntaxFactory.List(nodes);

    public static ListPatternSyntax ListPattern(SeparatedSyntaxList<PatternSyntax> patterns)
        => SyntaxFactory.ListPattern(patterns);

    public static ListPatternSyntax ListPattern(SeparatedSyntaxList<PatternSyntax> patterns, VariableDesignationSyntax designation)
        => SyntaxFactory.ListPattern(patterns, designation);

    public static ListPatternSyntax ListPattern(SyntaxToken openBracketToken, SeparatedSyntaxList<PatternSyntax> patterns, SyntaxToken closeBracketToken, VariableDesignationSyntax designation)
        => SyntaxFactory.ListPattern(openBracketToken, patterns, closeBracketToken, designation);

    public static SyntaxToken Literal(int value)
        => SyntaxFactory.Literal(value);

    public static SyntaxToken Literal(uint value)
        => SyntaxFactory.Literal(value);

    public static SyntaxToken Literal(long value)
        => SyntaxFactory.Literal(value);

    public static SyntaxToken Literal(ulong value)
        => SyntaxFactory.Literal(value);

    public static SyntaxToken Literal(float value)
        => SyntaxFactory.Literal(value);

    public static SyntaxToken Literal(double value)
        => SyntaxFactory.Literal(value);

    public static SyntaxToken Literal(decimal value)
        => SyntaxFactory.Literal(value);

    public static SyntaxToken Literal(string value)
        => SyntaxFactory.Literal(value);

    public static SyntaxToken Literal(char value)
        => SyntaxFactory.Literal(value);

    public static SyntaxToken Literal(string text, int value)
        => SyntaxFactory.Literal(text, value);

    public static SyntaxToken Literal(string text, uint value)
        => SyntaxFactory.Literal(text, value);

    public static SyntaxToken Literal(string text, long value)
        => SyntaxFactory.Literal(text, value);

    public static SyntaxToken Literal(string text, ulong value)
        => SyntaxFactory.Literal(text, value);

    public static SyntaxToken Literal(string text, float value)
        => SyntaxFactory.Literal(text, value);

    public static SyntaxToken Literal(string text, double value)
        => SyntaxFactory.Literal(text, value);

    public static SyntaxToken Literal(string text, decimal value)
        => SyntaxFactory.Literal(text, value);

    public static SyntaxToken Literal(string text, string value)
        => SyntaxFactory.Literal(text, value);

    public static SyntaxToken Literal(string text, char value)
        => SyntaxFactory.Literal(text, value);

    public static SyntaxToken Literal(SyntaxTriviaList leading, string text, int value, SyntaxTriviaList trailing)
        => SyntaxFactory.Literal(leading, text, value, trailing);

    public static SyntaxToken Literal(SyntaxTriviaList leading, string text, uint value, SyntaxTriviaList trailing)
        => SyntaxFactory.Literal(leading, text, value, trailing);

    public static SyntaxToken Literal(SyntaxTriviaList leading, string text, long value, SyntaxTriviaList trailing)
        => SyntaxFactory.Literal(leading, text, value, trailing);

    public static SyntaxToken Literal(SyntaxTriviaList leading, string text, ulong value, SyntaxTriviaList trailing)
        => SyntaxFactory.Literal(leading, text, value, trailing);

    public static SyntaxToken Literal(SyntaxTriviaList leading, string text, float value, SyntaxTriviaList trailing)
        => SyntaxFactory.Literal(leading, text, value, trailing);

    public static SyntaxToken Literal(SyntaxTriviaList leading, string text, double value, SyntaxTriviaList trailing)
        => SyntaxFactory.Literal(leading, text, value, trailing);

    public static SyntaxToken Literal(SyntaxTriviaList leading, string text, decimal value, SyntaxTriviaList trailing)
        => SyntaxFactory.Literal(leading, text, value, trailing);

    public static SyntaxToken Literal(SyntaxTriviaList leading, string text, string value, SyntaxTriviaList trailing)
        => SyntaxFactory.Literal(leading, text, value, trailing);

    public static SyntaxToken Literal(SyntaxTriviaList leading, string text, char value, SyntaxTriviaList trailing)
        => SyntaxFactory.Literal(leading, text, value, trailing);

    public static LiteralExpressionSyntax LiteralExpression(SyntaxKind kind)
        => SyntaxFactory.LiteralExpression(kind);

    public static LiteralExpressionSyntax LiteralExpression(SyntaxKind kind, SyntaxToken token)
        => SyntaxFactory.LiteralExpression(kind, token);

    public static LoadDirectiveTriviaSyntax LoadDirectiveTrivia(SyntaxToken file, bool isActive)
        => SyntaxFactory.LoadDirectiveTrivia(file, isActive);

    public static LoadDirectiveTriviaSyntax LoadDirectiveTrivia(SyntaxToken hashToken, SyntaxToken loadKeyword, SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive)
        => SyntaxFactory.LoadDirectiveTrivia(hashToken, loadKeyword, file, endOfDirectiveToken, isActive);

    public static LocalDeclarationStatementSyntax LocalDeclarationStatement(VariableDeclarationSyntax declaration)
        => SyntaxFactory.LocalDeclarationStatement(declaration);

    public static LocalDeclarationStatementSyntax LocalDeclarationStatement(SyntaxTokenList modifiers, VariableDeclarationSyntax declaration)
        => SyntaxFactory.LocalDeclarationStatement(modifiers, declaration);

    public static LocalDeclarationStatementSyntax LocalDeclarationStatement(SyntaxTokenList modifiers, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
        => SyntaxFactory.LocalDeclarationStatement(modifiers, declaration, semicolonToken);

    public static LocalDeclarationStatementSyntax LocalDeclarationStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, VariableDeclarationSyntax declaration)
        => SyntaxFactory.LocalDeclarationStatement(attributeLists, modifiers, declaration);

    public static LocalDeclarationStatementSyntax LocalDeclarationStatement(SyntaxToken awaitKeyword, SyntaxToken usingKeyword, SyntaxTokenList modifiers, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
        => SyntaxFactory.LocalDeclarationStatement(awaitKeyword, usingKeyword, modifiers, declaration, semicolonToken);

    public static LocalDeclarationStatementSyntax LocalDeclarationStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken awaitKeyword, SyntaxToken usingKeyword, SyntaxTokenList modifiers, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
        => SyntaxFactory.LocalDeclarationStatement(attributeLists, awaitKeyword, usingKeyword, modifiers, declaration, semicolonToken);

    public static LocalFunctionStatementSyntax LocalFunctionStatement(TypeSyntax returnType, SyntaxToken identifier)
        => SyntaxFactory.LocalFunctionStatement(returnType, identifier);

    public static LocalFunctionStatementSyntax LocalFunctionStatement(TypeSyntax returnType, string identifier)
        => SyntaxFactory.LocalFunctionStatement(returnType, identifier);

    public static LocalFunctionStatementSyntax LocalFunctionStatement(SyntaxTokenList modifiers, TypeSyntax returnType, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody)
        => SyntaxFactory.LocalFunctionStatement(modifiers, returnType, identifier, typeParameterList, parameterList, constraintClauses, body, expressionBody);

    public static LocalFunctionStatementSyntax LocalFunctionStatement(SyntaxTokenList modifiers, TypeSyntax returnType, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
        => SyntaxFactory.LocalFunctionStatement(modifiers, returnType, identifier, typeParameterList, parameterList, constraintClauses, body, expressionBody, semicolonToken);

    public static LocalFunctionStatementSyntax LocalFunctionStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody)
        => SyntaxFactory.LocalFunctionStatement(attributeLists, modifiers, returnType, identifier, typeParameterList, parameterList, constraintClauses, body, expressionBody);

    public static LocalFunctionStatementSyntax LocalFunctionStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
        => SyntaxFactory.LocalFunctionStatement(attributeLists, modifiers, returnType, identifier, typeParameterList, parameterList, constraintClauses, body, expressionBody, semicolonToken);

    public static LockStatementSyntax LockStatement(ExpressionSyntax expression, StatementSyntax statement)
        => SyntaxFactory.LockStatement(expression, statement);

    public static LockStatementSyntax LockStatement(SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax expression, StatementSyntax statement)
        => SyntaxFactory.LockStatement(attributeLists, expression, statement);

    public static LockStatementSyntax LockStatement(SyntaxToken lockKeyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
        => SyntaxFactory.LockStatement(lockKeyword, openParenToken, expression, closeParenToken, statement);

    public static LockStatementSyntax LockStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken lockKeyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
        => SyntaxFactory.LockStatement(attributeLists, lockKeyword, openParenToken, expression, closeParenToken, statement);

    public static MakeRefExpressionSyntax MakeRefExpression(ExpressionSyntax expression)
        => SyntaxFactory.MakeRefExpression(expression);

    public static MakeRefExpressionSyntax MakeRefExpression(SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
        => SyntaxFactory.MakeRefExpression(keyword, openParenToken, expression, closeParenToken);

    public static MemberAccessExpressionSyntax MemberAccessExpression(SyntaxKind kind, ExpressionSyntax expression, SimpleNameSyntax name)
        => SyntaxFactory.MemberAccessExpression(kind, expression, name);

    public static MemberAccessExpressionSyntax MemberAccessExpression(SyntaxKind kind, ExpressionSyntax expression, SyntaxToken operatorToken, SimpleNameSyntax name)
        => SyntaxFactory.MemberAccessExpression(kind, expression, operatorToken, name);

    public static MemberBindingExpressionSyntax MemberBindingExpression(SimpleNameSyntax name)
        => SyntaxFactory.MemberBindingExpression(name);

    public static MemberBindingExpressionSyntax MemberBindingExpression(SyntaxToken operatorToken, SimpleNameSyntax name)
        => SyntaxFactory.MemberBindingExpression(operatorToken, name);

    public static MethodDeclarationSyntax MethodDeclaration(TypeSyntax returnType, SyntaxToken identifier)
        => SyntaxFactory.MethodDeclaration(returnType, identifier);

    public static MethodDeclarationSyntax MethodDeclaration(TypeSyntax returnType, string identifier)
        => SyntaxFactory.MethodDeclaration(returnType, identifier);

    public static MethodDeclarationSyntax MethodDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, BlockSyntax body, SyntaxToken semicolonToken)
        => SyntaxFactory.MethodDeclaration(attributeLists, modifiers, returnType, explicitInterfaceSpecifier, identifier, typeParameterList, parameterList, constraintClauses, body, semicolonToken);

    public static MethodDeclarationSyntax MethodDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody)
        => SyntaxFactory.MethodDeclaration(attributeLists, modifiers, returnType, explicitInterfaceSpecifier, identifier, typeParameterList, parameterList, constraintClauses, body, expressionBody);

    public static MethodDeclarationSyntax MethodDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
        => SyntaxFactory.MethodDeclaration(attributeLists, modifiers, returnType, explicitInterfaceSpecifier, identifier, typeParameterList, parameterList, constraintClauses, body, expressionBody, semicolonToken);

    public static SyntaxToken MissingToken(SyntaxKind kind)
        => SyntaxFactory.MissingToken(kind);

    public static SyntaxToken MissingToken(SyntaxTriviaList leading, SyntaxKind kind, SyntaxTriviaList trailing)
        => SyntaxFactory.MissingToken(leading, kind, trailing);

    public static NameColonSyntax NameColon(IdentifierNameSyntax name)
        => SyntaxFactory.NameColon(name);

    public static NameColonSyntax NameColon(string name)
        => SyntaxFactory.NameColon(name);

    public static NameColonSyntax NameColon(IdentifierNameSyntax name, SyntaxToken colonToken)
        => SyntaxFactory.NameColon(name, colonToken);

    public static NameEqualsSyntax NameEquals(IdentifierNameSyntax name)
        => SyntaxFactory.NameEquals(name);

    public static NameEqualsSyntax NameEquals(string name)
        => SyntaxFactory.NameEquals(name);

    public static NameEqualsSyntax NameEquals(IdentifierNameSyntax name, SyntaxToken equalsToken)
        => SyntaxFactory.NameEquals(name, equalsToken);

    public static NameMemberCrefSyntax NameMemberCref(TypeSyntax name)
        => SyntaxFactory.NameMemberCref(name);

    public static NameMemberCrefSyntax NameMemberCref(TypeSyntax name, CrefParameterListSyntax parameters)
        => SyntaxFactory.NameMemberCref(name, parameters);

    public static NamespaceDeclarationSyntax NamespaceDeclaration(NameSyntax name)
        => SyntaxFactory.NamespaceDeclaration(name);

    public static NamespaceDeclarationSyntax NamespaceDeclaration(NameSyntax name, SyntaxList<ExternAliasDirectiveSyntax> externs, SyntaxList<UsingDirectiveSyntax> usings, SyntaxList<MemberDeclarationSyntax> members)
        => SyntaxFactory.NamespaceDeclaration(name, externs, usings, members);

    public static NamespaceDeclarationSyntax NamespaceDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, NameSyntax name, SyntaxList<ExternAliasDirectiveSyntax> externs, SyntaxList<UsingDirectiveSyntax> usings, SyntaxList<MemberDeclarationSyntax> members)
        => SyntaxFactory.NamespaceDeclaration(attributeLists, modifiers, name, externs, usings, members);

    public static NamespaceDeclarationSyntax NamespaceDeclaration(SyntaxToken namespaceKeyword, NameSyntax name, SyntaxToken openBraceToken, SyntaxList<ExternAliasDirectiveSyntax> externs, SyntaxList<UsingDirectiveSyntax> usings, SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
        => SyntaxFactory.NamespaceDeclaration(namespaceKeyword, name, openBraceToken, externs, usings, members, closeBraceToken, semicolonToken);

    public static NamespaceDeclarationSyntax NamespaceDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken namespaceKeyword, NameSyntax name, SyntaxToken openBraceToken, SyntaxList<ExternAliasDirectiveSyntax> externs, SyntaxList<UsingDirectiveSyntax> usings, SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
        => SyntaxFactory.NamespaceDeclaration(attributeLists, modifiers, namespaceKeyword, name, openBraceToken, externs, usings, members, closeBraceToken, semicolonToken);

    public static SyntaxNodeOrTokenList NodeOrTokenList()
        => SyntaxFactory.NodeOrTokenList();

    public static SyntaxNodeOrTokenList NodeOrTokenList(IEnumerable<SyntaxNodeOrToken> nodesAndTokens)
        => SyntaxFactory.NodeOrTokenList(nodesAndTokens);

    public static SyntaxNodeOrTokenList NodeOrTokenList(SyntaxNodeOrToken[] nodesAndTokens)
        => SyntaxFactory.NodeOrTokenList(nodesAndTokens);

    public static NullableDirectiveTriviaSyntax NullableDirectiveTrivia(SyntaxToken settingToken, bool isActive)
        => SyntaxFactory.NullableDirectiveTrivia(settingToken, isActive);

    public static NullableDirectiveTriviaSyntax NullableDirectiveTrivia(SyntaxToken settingToken, SyntaxToken targetToken, bool isActive)
        => SyntaxFactory.NullableDirectiveTrivia(settingToken, targetToken, isActive);

    public static NullableDirectiveTriviaSyntax NullableDirectiveTrivia(SyntaxToken hashToken, SyntaxToken nullableKeyword, SyntaxToken settingToken, SyntaxToken targetToken, SyntaxToken endOfDirectiveToken, bool isActive)
        => SyntaxFactory.NullableDirectiveTrivia(hashToken, nullableKeyword, settingToken, targetToken, endOfDirectiveToken, isActive);

    public static NullableTypeSyntax NullableType(TypeSyntax elementType)
        => SyntaxFactory.NullableType(elementType);

    public static NullableTypeSyntax NullableType(TypeSyntax elementType, SyntaxToken questionToken)
        => SyntaxFactory.NullableType(elementType, questionToken);

    public static ObjectCreationExpressionSyntax ObjectCreationExpression(TypeSyntax type)
        => SyntaxFactory.ObjectCreationExpression(type);

    public static ObjectCreationExpressionSyntax ObjectCreationExpression(TypeSyntax type, ArgumentListSyntax argumentList, InitializerExpressionSyntax initializer)
        => SyntaxFactory.ObjectCreationExpression(type, argumentList, initializer);

    public static ObjectCreationExpressionSyntax ObjectCreationExpression(SyntaxToken newKeyword, TypeSyntax type, ArgumentListSyntax argumentList, InitializerExpressionSyntax initializer)
        => SyntaxFactory.ObjectCreationExpression(newKeyword, type, argumentList, initializer);

    public static OmittedArraySizeExpressionSyntax OmittedArraySizeExpression()
        => SyntaxFactory.OmittedArraySizeExpression();

    public static OmittedArraySizeExpressionSyntax OmittedArraySizeExpression(SyntaxToken omittedArraySizeExpressionToken)
        => SyntaxFactory.OmittedArraySizeExpression(omittedArraySizeExpressionToken);

    public static OmittedTypeArgumentSyntax OmittedTypeArgument()
        => SyntaxFactory.OmittedTypeArgument();

    public static OmittedTypeArgumentSyntax OmittedTypeArgument(SyntaxToken omittedTypeArgumentToken)
        => SyntaxFactory.OmittedTypeArgument(omittedTypeArgumentToken);

    public static OperatorDeclarationSyntax OperatorDeclaration(TypeSyntax returnType, SyntaxToken operatorToken)
        => SyntaxFactory.OperatorDeclaration(returnType, operatorToken);

    public static OperatorDeclarationSyntax OperatorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, SyntaxToken operatorToken, ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody)
        => SyntaxFactory.OperatorDeclaration(attributeLists, modifiers, returnType, operatorToken, parameterList, body, expressionBody);

    public static OperatorDeclarationSyntax OperatorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, SyntaxToken operatorKeyword, SyntaxToken operatorToken, ParameterListSyntax parameterList, BlockSyntax body, SyntaxToken semicolonToken)
        => SyntaxFactory.OperatorDeclaration(attributeLists, modifiers, returnType, operatorKeyword, operatorToken, parameterList, body, semicolonToken);

    public static OperatorDeclarationSyntax OperatorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken operatorToken, ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody)
        => SyntaxFactory.OperatorDeclaration(attributeLists, modifiers, returnType, explicitInterfaceSpecifier, operatorToken, parameterList, body, expressionBody);

    public static OperatorDeclarationSyntax OperatorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, SyntaxToken operatorKeyword, SyntaxToken operatorToken, ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
        => SyntaxFactory.OperatorDeclaration(attributeLists, modifiers, returnType, operatorKeyword, operatorToken, parameterList, body, expressionBody, semicolonToken);

    public static OperatorDeclarationSyntax OperatorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken operatorKeyword, SyntaxToken operatorToken, ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
        => SyntaxFactory.OperatorDeclaration(attributeLists, modifiers, returnType, explicitInterfaceSpecifier, operatorKeyword, operatorToken, parameterList, body, expressionBody, semicolonToken);

    public static OperatorDeclarationSyntax OperatorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken operatorKeyword, SyntaxToken checkedKeyword, SyntaxToken operatorToken, ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
        => SyntaxFactory.OperatorDeclaration(attributeLists, modifiers, returnType, explicitInterfaceSpecifier, operatorKeyword, checkedKeyword, operatorToken, parameterList, body, expressionBody, semicolonToken);

    public static OperatorMemberCrefSyntax OperatorMemberCref(SyntaxToken operatorToken)
        => SyntaxFactory.OperatorMemberCref(operatorToken);

    public static OperatorMemberCrefSyntax OperatorMemberCref(SyntaxToken operatorToken, CrefParameterListSyntax parameters)
        => SyntaxFactory.OperatorMemberCref(operatorToken, parameters);

    public static OperatorMemberCrefSyntax OperatorMemberCref(SyntaxToken operatorKeyword, SyntaxToken operatorToken, CrefParameterListSyntax parameters)
        => SyntaxFactory.OperatorMemberCref(operatorKeyword, operatorToken, parameters);

    public static OperatorMemberCrefSyntax OperatorMemberCref(SyntaxToken operatorKeyword, SyntaxToken checkedKeyword, SyntaxToken operatorToken, CrefParameterListSyntax parameters)
        => SyntaxFactory.OperatorMemberCref(operatorKeyword, checkedKeyword, operatorToken, parameters);

    public static OrderByClauseSyntax OrderByClause(SeparatedSyntaxList<OrderingSyntax> orderings)
        => SyntaxFactory.OrderByClause(orderings);

    public static OrderByClauseSyntax OrderByClause(SyntaxToken orderByKeyword, SeparatedSyntaxList<OrderingSyntax> orderings)
        => SyntaxFactory.OrderByClause(orderByKeyword, orderings);

    public static OrderingSyntax Ordering(SyntaxKind kind, ExpressionSyntax expression)
        => SyntaxFactory.Ordering(kind, expression);

    public static OrderingSyntax Ordering(SyntaxKind kind, ExpressionSyntax expression, SyntaxToken ascendingOrDescendingKeyword)
        => SyntaxFactory.Ordering(kind, expression, ascendingOrDescendingKeyword);

    public static ParameterSyntax Parameter(SyntaxToken identifier)
        => SyntaxFactory.Parameter(identifier);

    public static ParameterSyntax Parameter(TypeSyntax type, string identifier)
        => Parameter(type, Identifier(identifier));

    public static ParameterSyntax Parameter(TypeSyntax type, SyntaxToken identifier)
        => Parameter(identifier).WithType(type);

    public static ParameterSyntax Parameter(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax type, SyntaxToken identifier, EqualsValueClauseSyntax? @default)
        => SyntaxFactory.Parameter(attributeLists, modifiers, type, identifier, @default);

    public static ParameterListSyntax ParameterList(SeparatedSyntaxList<ParameterSyntax> parameters)
        => SyntaxFactory.ParameterList(parameters);

    public static ParameterListSyntax ParameterList(SyntaxToken openParenToken, SeparatedSyntaxList<ParameterSyntax> parameters, SyntaxToken closeParenToken)
        => SyntaxFactory.ParameterList(openParenToken, parameters, closeParenToken);

    public static ParenthesizedExpressionSyntax ParenthesizedExpression(ExpressionSyntax expression)
        => SyntaxFactory.ParenthesizedExpression(expression);

    public static ParenthesizedExpressionSyntax ParenthesizedExpression(SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
        => SyntaxFactory.ParenthesizedExpression(openParenToken, expression, closeParenToken);

    public static ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression()
        => SyntaxFactory.ParenthesizedLambdaExpression();

    public static ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression(CSharpSyntaxNode body)
        => SyntaxFactory.ParenthesizedLambdaExpression(body);

    public static ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression(ParameterListSyntax parameterList, CSharpSyntaxNode body)
        => SyntaxFactory.ParenthesizedLambdaExpression(parameterList, body);

    public static ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression(ParameterListSyntax parameterList, BlockSyntax block, ExpressionSyntax expressionBody)
        => SyntaxFactory.ParenthesizedLambdaExpression(parameterList, block, expressionBody);

    public static ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression(SyntaxTokenList modifiers, ParameterListSyntax parameterList, BlockSyntax block, ExpressionSyntax expressionBody)
        => SyntaxFactory.ParenthesizedLambdaExpression(modifiers, parameterList, block, expressionBody);

    public static ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression(SyntaxToken asyncKeyword, ParameterListSyntax parameterList, SyntaxToken arrowToken, CSharpSyntaxNode body)
        => SyntaxFactory.ParenthesizedLambdaExpression(asyncKeyword, parameterList, arrowToken, body);

    public static ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression(SyntaxToken asyncKeyword, ParameterListSyntax parameterList, SyntaxToken arrowToken, BlockSyntax block, ExpressionSyntax expressionBody)
        => SyntaxFactory.ParenthesizedLambdaExpression(asyncKeyword, parameterList, arrowToken, block, expressionBody);

    public static ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression(SyntaxTokenList modifiers, ParameterListSyntax parameterList, SyntaxToken arrowToken, BlockSyntax block, ExpressionSyntax expressionBody)
        => SyntaxFactory.ParenthesizedLambdaExpression(modifiers, parameterList, arrowToken, block, expressionBody);

    public static ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, ParameterListSyntax parameterList, BlockSyntax block, ExpressionSyntax expressionBody)
        => SyntaxFactory.ParenthesizedLambdaExpression(attributeLists, modifiers, parameterList, block, expressionBody);

    public static ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, ParameterListSyntax parameterList, BlockSyntax block, ExpressionSyntax expressionBody)
        => SyntaxFactory.ParenthesizedLambdaExpression(attributeLists, modifiers, returnType, parameterList, block, expressionBody);

    public static ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, ParameterListSyntax parameterList, SyntaxToken arrowToken, BlockSyntax block, ExpressionSyntax expressionBody)
        => SyntaxFactory.ParenthesizedLambdaExpression(attributeLists, modifiers, returnType, parameterList, arrowToken, block, expressionBody);

    public static ParenthesizedPatternSyntax ParenthesizedPattern(PatternSyntax pattern)
        => SyntaxFactory.ParenthesizedPattern(pattern);

    public static ParenthesizedPatternSyntax ParenthesizedPattern(SyntaxToken openParenToken, PatternSyntax pattern, SyntaxToken closeParenToken)
        => SyntaxFactory.ParenthesizedPattern(openParenToken, pattern, closeParenToken);

    public static ParenthesizedVariableDesignationSyntax ParenthesizedVariableDesignation(SeparatedSyntaxList<VariableDesignationSyntax> variables)
        => SyntaxFactory.ParenthesizedVariableDesignation(variables);

    public static ParenthesizedVariableDesignationSyntax ParenthesizedVariableDesignation(SyntaxToken openParenToken, SeparatedSyntaxList<VariableDesignationSyntax> variables, SyntaxToken closeParenToken)
        => SyntaxFactory.ParenthesizedVariableDesignation(openParenToken, variables, closeParenToken);

    public static ArgumentListSyntax ParseArgumentList(string text, int offset = 0, ParseOptions? options = null, bool consumeFullText = true)
        => SyntaxFactory.ParseArgumentList(text, offset, options, consumeFullText);

    public static AttributeArgumentListSyntax? ParseAttributeArgumentList(string text, int offset = 0, ParseOptions? options = null, bool consumeFullText = true)
        => SyntaxFactory.ParseAttributeArgumentList(text, offset, options, consumeFullText);

    public static BracketedArgumentListSyntax ParseBracketedArgumentList(string text, int offset = 0, ParseOptions? options = null, bool consumeFullText = true)
        => SyntaxFactory.ParseBracketedArgumentList(text, offset, options, consumeFullText);

    public static BracketedParameterListSyntax ParseBracketedParameterList(string text, int offset = 0, ParseOptions? options = null, bool consumeFullText = true)
        => SyntaxFactory.ParseBracketedParameterList(text, offset, options, consumeFullText);

    public static CompilationUnitSyntax ParseCompilationUnit(string text, int offset = 0, CSharpParseOptions? options = null)
        => SyntaxFactory.ParseCompilationUnit(text, offset, options);

    public static ExpressionSyntax ParseExpression(string text, int offset = 0, ParseOptions? options = null, bool consumeFullText = true)
        => SyntaxFactory.ParseExpression(text, offset, options, consumeFullText);

    public static SyntaxTriviaList ParseLeadingTrivia(string text, int offset = 0)
        => SyntaxFactory.ParseLeadingTrivia(text, offset);

    public static MemberDeclarationSyntax? ParseMemberDeclaration(string text, int offset = 0, ParseOptions? options = null, bool consumeFullText = true)
        => SyntaxFactory.ParseMemberDeclaration(text, offset, options, consumeFullText);

    public static NameSyntax ParseName(string text, int offset = 0, bool consumeFullText = true)
        => SyntaxFactory.ParseName(text, offset, consumeFullText);

    public static ParameterListSyntax ParseParameterList(string text, int offset = 0, ParseOptions? options = null, bool consumeFullText = true)
        => SyntaxFactory.ParseParameterList(text, offset, options, consumeFullText);

    public static StatementSyntax ParseStatement(string text, int offset = 0, ParseOptions? options = null, bool consumeFullText = true)
        => SyntaxFactory.ParseStatement(text, offset, options, consumeFullText);

    public static SyntaxTree ParseSyntaxTree(SourceText text, ParseOptions options, string path, CancellationToken cancellationToken)
        => SyntaxFactory.ParseSyntaxTree(text, options, path, cancellationToken);

    public static SyntaxTree ParseSyntaxTree(string text, ParseOptions options, string path, Encoding encoding, CancellationToken cancellationToken)
        => SyntaxFactory.ParseSyntaxTree(text, options, path, encoding, cancellationToken);

    public static SyntaxToken ParseToken(string text, int offset = 0)
        => SyntaxFactory.ParseToken(text, offset);

    public static IEnumerable<SyntaxToken> ParseTokens(string text, int offset = 0, int initialTokenPosition = 0, CSharpParseOptions? options = null)
        => SyntaxFactory.ParseTokens(text, offset, initialTokenPosition, options);

    public static SyntaxTriviaList ParseTrailingTrivia(string text, int offset = 0)
        => SyntaxFactory.ParseTrailingTrivia(text, offset);

    public static TypeSyntax ParseTypeName(string text, int offset, bool consumeFullText)
        => SyntaxFactory.ParseTypeName(text, offset, consumeFullText);

    public static TypeSyntax ParseTypeName(string text, int offset = 0, ParseOptions? options = null, bool consumeFullText = true)
        => SyntaxFactory.ParseTypeName(text, offset, options, consumeFullText);

    public static PointerTypeSyntax PointerType(TypeSyntax elementType)
        => SyntaxFactory.PointerType(elementType);

    public static PointerTypeSyntax PointerType(TypeSyntax elementType, SyntaxToken asteriskToken)
        => SyntaxFactory.PointerType(elementType, asteriskToken);

    public static PositionalPatternClauseSyntax PositionalPatternClause(SeparatedSyntaxList<SubpatternSyntax> subpatterns)
        => SyntaxFactory.PositionalPatternClause(subpatterns);

    public static PositionalPatternClauseSyntax PositionalPatternClause(SyntaxToken openParenToken, SeparatedSyntaxList<SubpatternSyntax> subpatterns, SyntaxToken closeParenToken)
        => SyntaxFactory.PositionalPatternClause(openParenToken, subpatterns, closeParenToken);

    public static PostfixUnaryExpressionSyntax PostfixUnaryExpression(SyntaxKind kind, ExpressionSyntax operand)
        => SyntaxFactory.PostfixUnaryExpression(kind, operand);

    public static PostfixUnaryExpressionSyntax PostfixUnaryExpression(SyntaxKind kind, ExpressionSyntax operand, SyntaxToken operatorToken)
        => SyntaxFactory.PostfixUnaryExpression(kind, operand, operatorToken);

    public static PragmaChecksumDirectiveTriviaSyntax PragmaChecksumDirectiveTrivia(SyntaxToken file, SyntaxToken guid, SyntaxToken bytes, bool isActive)
        => SyntaxFactory.PragmaChecksumDirectiveTrivia(file, guid, bytes, isActive);

    public static PragmaChecksumDirectiveTriviaSyntax PragmaChecksumDirectiveTrivia(SyntaxToken hashToken, SyntaxToken pragmaKeyword, SyntaxToken checksumKeyword, SyntaxToken file, SyntaxToken guid, SyntaxToken bytes, SyntaxToken endOfDirectiveToken, bool isActive)
        => SyntaxFactory.PragmaChecksumDirectiveTrivia(hashToken, pragmaKeyword, checksumKeyword, file, guid, bytes, endOfDirectiveToken, isActive);

    public static PragmaWarningDirectiveTriviaSyntax PragmaWarningDirectiveTrivia(SyntaxToken disableOrRestoreKeyword, bool isActive)
        => SyntaxFactory.PragmaWarningDirectiveTrivia(disableOrRestoreKeyword, isActive);

    public static PragmaWarningDirectiveTriviaSyntax PragmaWarningDirectiveTrivia(SyntaxToken disableOrRestoreKeyword, SeparatedSyntaxList<ExpressionSyntax> errorCodes, bool isActive)
        => SyntaxFactory.PragmaWarningDirectiveTrivia(disableOrRestoreKeyword, errorCodes, isActive);

    public static PragmaWarningDirectiveTriviaSyntax PragmaWarningDirectiveTrivia(SyntaxToken hashToken, SyntaxToken pragmaKeyword, SyntaxToken warningKeyword, SyntaxToken disableOrRestoreKeyword, SeparatedSyntaxList<ExpressionSyntax> errorCodes, SyntaxToken endOfDirectiveToken, bool isActive)
        => SyntaxFactory.PragmaWarningDirectiveTrivia(hashToken, pragmaKeyword, warningKeyword, disableOrRestoreKeyword, errorCodes, endOfDirectiveToken, isActive);

    public static PredefinedTypeSyntax PredefinedType(SyntaxToken keyword)
        => SyntaxFactory.PredefinedType(keyword);

    public static PrefixUnaryExpressionSyntax PrefixUnaryExpression(SyntaxKind kind, ExpressionSyntax operand)
        => SyntaxFactory.PrefixUnaryExpression(kind, operand);

    public static PrefixUnaryExpressionSyntax PrefixUnaryExpression(SyntaxKind kind, SyntaxToken operatorToken, ExpressionSyntax operand)
        => SyntaxFactory.PrefixUnaryExpression(kind, operatorToken, operand);

    public static SyntaxTrivia PreprocessingMessage(string text)
        => SyntaxFactory.PreprocessingMessage(text);

    public static PrimaryConstructorBaseTypeSyntax PrimaryConstructorBaseType(TypeSyntax type)
        => SyntaxFactory.PrimaryConstructorBaseType(type);

    public static PrimaryConstructorBaseTypeSyntax PrimaryConstructorBaseType(TypeSyntax type, ArgumentListSyntax argumentList)
        => SyntaxFactory.PrimaryConstructorBaseType(type, argumentList);

    public static PropertyDeclarationSyntax PropertyDeclaration(TypeSyntax type, SyntaxToken identifier)
        => SyntaxFactory.PropertyDeclaration(type, identifier);

    public static PropertyDeclarationSyntax PropertyDeclaration(TypeSyntax type, string identifier)
        => SyntaxFactory.PropertyDeclaration(type, identifier);

    public static PropertyDeclarationSyntax PropertyDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, AccessorListSyntax accessorList)
        => SyntaxFactory.PropertyDeclaration(attributeLists, modifiers, type, explicitInterfaceSpecifier, identifier, accessorList);

    public static PropertyDeclarationSyntax PropertyDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, AccessorListSyntax accessorList, ArrowExpressionClauseSyntax expressionBody, EqualsValueClauseSyntax initializer)
        => SyntaxFactory.PropertyDeclaration(attributeLists, modifiers, type, explicitInterfaceSpecifier, identifier, accessorList, expressionBody, initializer);

    public static PropertyDeclarationSyntax PropertyDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, AccessorListSyntax accessorList, ArrowExpressionClauseSyntax expressionBody, EqualsValueClauseSyntax initializer, SyntaxToken semicolonToken)
        => SyntaxFactory.PropertyDeclaration(attributeLists, modifiers, type, explicitInterfaceSpecifier, identifier, accessorList, expressionBody, initializer, semicolonToken);

    public static PropertyPatternClauseSyntax PropertyPatternClause(SeparatedSyntaxList<SubpatternSyntax> subpatterns)
        => SyntaxFactory.PropertyPatternClause(subpatterns);

    public static PropertyPatternClauseSyntax PropertyPatternClause(SyntaxToken openBraceToken, SeparatedSyntaxList<SubpatternSyntax> subpatterns, SyntaxToken closeBraceToken)
        => SyntaxFactory.PropertyPatternClause(openBraceToken, subpatterns, closeBraceToken);

    public static QualifiedCrefSyntax QualifiedCref(TypeSyntax container, MemberCrefSyntax member)
        => SyntaxFactory.QualifiedCref(container, member);

    public static QualifiedCrefSyntax QualifiedCref(TypeSyntax container, SyntaxToken dotToken, MemberCrefSyntax member)
        => SyntaxFactory.QualifiedCref(container, dotToken, member);

    public static QualifiedNameSyntax QualifiedName(NameSyntax left, SimpleNameSyntax right)
        => SyntaxFactory.QualifiedName(left, right);

    public static QualifiedNameSyntax QualifiedName(NameSyntax left, SyntaxToken dotToken, SimpleNameSyntax right)
        => SyntaxFactory.QualifiedName(left, dotToken, right);

    public static QueryBodySyntax QueryBody(SelectOrGroupClauseSyntax selectOrGroup)
        => SyntaxFactory.QueryBody(selectOrGroup);

    public static QueryBodySyntax QueryBody(SyntaxList<QueryClauseSyntax> clauses, SelectOrGroupClauseSyntax selectOrGroup, QueryContinuationSyntax continuation)
        => SyntaxFactory.QueryBody(clauses, selectOrGroup, continuation);

    public static QueryContinuationSyntax QueryContinuation(SyntaxToken identifier, QueryBodySyntax body)
        => SyntaxFactory.QueryContinuation(identifier, body);

    public static QueryContinuationSyntax QueryContinuation(string identifier, QueryBodySyntax body)
        => SyntaxFactory.QueryContinuation(identifier, body);

    public static QueryContinuationSyntax QueryContinuation(SyntaxToken intoKeyword, SyntaxToken identifier, QueryBodySyntax body)
        => SyntaxFactory.QueryContinuation(intoKeyword, identifier, body);

    public static QueryExpressionSyntax QueryExpression(FromClauseSyntax fromClause, QueryBodySyntax body)
        => SyntaxFactory.QueryExpression(fromClause, body);

    public static RangeExpressionSyntax RangeExpression()
        => SyntaxFactory.RangeExpression();

    public static RangeExpressionSyntax RangeExpression(ExpressionSyntax leftOperand, ExpressionSyntax rightOperand)
        => SyntaxFactory.RangeExpression(leftOperand, rightOperand);

    public static RangeExpressionSyntax RangeExpression(ExpressionSyntax leftOperand, SyntaxToken operatorToken, ExpressionSyntax rightOperand)
        => SyntaxFactory.RangeExpression(leftOperand, operatorToken, rightOperand);

    public static RecordDeclarationSyntax RecordDeclaration(SyntaxToken keyword, string identifier)
        => SyntaxFactory.RecordDeclaration(keyword, identifier);

    public static RecordDeclarationSyntax RecordDeclaration(SyntaxToken keyword, SyntaxToken identifier)
        => SyntaxFactory.RecordDeclaration(keyword, identifier);

    public static RecordDeclarationSyntax RecordDeclaration(SyntaxKind kind, SyntaxToken keyword, SyntaxToken identifier)
        => SyntaxFactory.RecordDeclaration(kind, keyword, identifier);

    public static RecordDeclarationSyntax RecordDeclaration(SyntaxKind kind, SyntaxToken keyword, string identifier)
        => SyntaxFactory.RecordDeclaration(kind, keyword, identifier);

    public static RecordDeclarationSyntax RecordDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxList<MemberDeclarationSyntax> members)
        => SyntaxFactory.RecordDeclaration(attributeLists, modifiers, keyword, identifier, typeParameterList, parameterList, baseList, constraintClauses, members);

    public static RecordDeclarationSyntax RecordDeclaration(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxList<MemberDeclarationSyntax> members)
        => SyntaxFactory.RecordDeclaration(kind, attributeLists, modifiers, keyword, identifier, typeParameterList, parameterList, baseList, constraintClauses, members);

    public static RecordDeclarationSyntax RecordDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
        => SyntaxFactory.RecordDeclaration(attributeLists, modifiers, keyword, identifier, typeParameterList, parameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);

    public static RecordDeclarationSyntax RecordDeclaration(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken classOrStructKeyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
        => SyntaxFactory.RecordDeclaration(kind, attributeLists, modifiers, keyword, classOrStructKeyword, identifier, typeParameterList, parameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);

    public static RecursivePatternSyntax RecursivePattern()
        => SyntaxFactory.RecursivePattern();

    public static RecursivePatternSyntax RecursivePattern(TypeSyntax type, PositionalPatternClauseSyntax positionalPatternClause, PropertyPatternClauseSyntax propertyPatternClause, VariableDesignationSyntax designation)
        => SyntaxFactory.RecursivePattern(type, positionalPatternClause, propertyPatternClause, designation);

    public static ReferenceDirectiveTriviaSyntax ReferenceDirectiveTrivia(SyntaxToken file, bool isActive)
        => SyntaxFactory.ReferenceDirectiveTrivia(file, isActive);

    public static ReferenceDirectiveTriviaSyntax ReferenceDirectiveTrivia(SyntaxToken hashToken, SyntaxToken referenceKeyword, SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive)
        => SyntaxFactory.ReferenceDirectiveTrivia(hashToken, referenceKeyword, file, endOfDirectiveToken, isActive);

    public static RefExpressionSyntax RefExpression(ExpressionSyntax expression)
        => SyntaxFactory.RefExpression(expression);

    public static RefExpressionSyntax RefExpression(SyntaxToken refKeyword, ExpressionSyntax expression)
        => SyntaxFactory.RefExpression(refKeyword, expression);

    public static RefStructConstraintSyntax RefStructConstraint()
        => SyntaxFactory.RefStructConstraint();

    public static RefStructConstraintSyntax RefStructConstraint(SyntaxToken refKeyword, SyntaxToken structKeyword)
        => SyntaxFactory.RefStructConstraint(refKeyword, structKeyword);

    public static RefTypeSyntax RefType(TypeSyntax type)
        => SyntaxFactory.RefType(type);

    public static RefTypeSyntax RefType(SyntaxToken refKeyword, TypeSyntax type)
        => SyntaxFactory.RefType(refKeyword, type);

    public static RefTypeSyntax RefType(SyntaxToken refKeyword, SyntaxToken readOnlyKeyword, TypeSyntax type)
        => SyntaxFactory.RefType(refKeyword, readOnlyKeyword, type);

    public static RefTypeExpressionSyntax RefTypeExpression(ExpressionSyntax expression)
        => SyntaxFactory.RefTypeExpression(expression);

    public static RefTypeExpressionSyntax RefTypeExpression(SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
        => SyntaxFactory.RefTypeExpression(keyword, openParenToken, expression, closeParenToken);

    public static RefValueExpressionSyntax RefValueExpression(ExpressionSyntax expression, TypeSyntax type)
        => SyntaxFactory.RefValueExpression(expression, type);

    public static RefValueExpressionSyntax RefValueExpression(SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken comma, TypeSyntax type, SyntaxToken closeParenToken)
        => SyntaxFactory.RefValueExpression(keyword, openParenToken, expression, comma, type, closeParenToken);

    public static RegionDirectiveTriviaSyntax RegionDirectiveTrivia(bool isActive)
        => SyntaxFactory.RegionDirectiveTrivia(isActive);

    public static RegionDirectiveTriviaSyntax RegionDirectiveTrivia(SyntaxToken hashToken, SyntaxToken regionKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
        => SyntaxFactory.RegionDirectiveTrivia(hashToken, regionKeyword, endOfDirectiveToken, isActive);

    public static RelationalPatternSyntax RelationalPattern(SyntaxToken operatorToken, ExpressionSyntax expression)
        => SyntaxFactory.RelationalPattern(operatorToken, expression);

    public static ReturnStatementSyntax ReturnStatement(ExpressionSyntax expression)
        => SyntaxFactory.ReturnStatement(expression);

    public static ReturnStatementSyntax ReturnStatement(SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax expression)
        => SyntaxFactory.ReturnStatement(attributeLists, expression);

    public static ReturnStatementSyntax ReturnStatement(SyntaxToken returnKeyword, ExpressionSyntax expression, SyntaxToken semicolonToken)
        => SyntaxFactory.ReturnStatement(returnKeyword, expression, semicolonToken);

    public static ReturnStatementSyntax ReturnStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken returnKeyword, ExpressionSyntax expression, SyntaxToken semicolonToken)
        => SyntaxFactory.ReturnStatement(attributeLists, returnKeyword, expression, semicolonToken);

    public static ScopedTypeSyntax ScopedType(TypeSyntax type)
        => SyntaxFactory.ScopedType(type);

    public static ScopedTypeSyntax ScopedType(SyntaxToken scopedKeyword, TypeSyntax type)
        => SyntaxFactory.ScopedType(scopedKeyword, type);

    public static SelectClauseSyntax SelectClause(ExpressionSyntax expression)
        => SyntaxFactory.SelectClause(expression);

    public static SelectClauseSyntax SelectClause(SyntaxToken selectKeyword, ExpressionSyntax expression)
        => SyntaxFactory.SelectClause(selectKeyword, expression);

    public static SeparatedSyntaxList<TNode> SeparatedList<TNode>()
        where TNode : SyntaxNode
        => SyntaxFactory.SeparatedList<TNode>();

    public static SeparatedSyntaxList<TNode> SeparatedList<TNode>(IEnumerable<TNode> nodes)
        where TNode : SyntaxNode
        => SyntaxFactory.SeparatedList(nodes);

    public static SeparatedSyntaxList<TNode> SeparatedList<TNode>(IEnumerable<SyntaxNodeOrToken> nodesAndTokens)
        where TNode : SyntaxNode
        => SyntaxFactory.SeparatedList<TNode>(nodesAndTokens);

    public static SeparatedSyntaxList<TNode> SeparatedList<TNode>(SyntaxNodeOrTokenList nodesAndTokens)
        where TNode : SyntaxNode
        => SyntaxFactory.SeparatedList<TNode>(nodesAndTokens);

    public static SeparatedSyntaxList<TNode> SeparatedList<TNode>(IEnumerable<TNode> nodes, IEnumerable<SyntaxToken> separators)
        where TNode : SyntaxNode
        => SyntaxFactory.SeparatedList(nodes, separators);

    public static ShebangDirectiveTriviaSyntax ShebangDirectiveTrivia(bool isActive)
        => SyntaxFactory.ShebangDirectiveTrivia(isActive);

    public static ShebangDirectiveTriviaSyntax ShebangDirectiveTrivia(SyntaxToken hashToken, SyntaxToken exclamationToken, SyntaxToken endOfDirectiveToken, bool isActive)
        => SyntaxFactory.ShebangDirectiveTrivia(hashToken, exclamationToken, endOfDirectiveToken, isActive);

    public static SimpleBaseTypeSyntax SimpleBaseType(TypeSyntax type)
        => SyntaxFactory.SimpleBaseType(type);

    public static SimpleLambdaExpressionSyntax SimpleLambdaExpression(ParameterSyntax parameter)
        => SyntaxFactory.SimpleLambdaExpression(parameter);

    public static SimpleLambdaExpressionSyntax SimpleLambdaExpression(ParameterSyntax parameter, CSharpSyntaxNode body)
        => SyntaxFactory.SimpleLambdaExpression(parameter, body);

    public static SimpleLambdaExpressionSyntax SimpleLambdaExpression(ParameterSyntax parameter, BlockSyntax block, ExpressionSyntax expressionBody)
        => SyntaxFactory.SimpleLambdaExpression(parameter, block, expressionBody);

    public static SimpleLambdaExpressionSyntax SimpleLambdaExpression(SyntaxTokenList modifiers, ParameterSyntax parameter, BlockSyntax block, ExpressionSyntax expressionBody)
        => SyntaxFactory.SimpleLambdaExpression(modifiers, parameter, block, expressionBody);

    public static SimpleLambdaExpressionSyntax SimpleLambdaExpression(SyntaxToken asyncKeyword, ParameterSyntax parameter, SyntaxToken arrowToken, CSharpSyntaxNode body)
        => SyntaxFactory.SimpleLambdaExpression(asyncKeyword, parameter, arrowToken, body);

    public static SimpleLambdaExpressionSyntax SimpleLambdaExpression(SyntaxToken asyncKeyword, ParameterSyntax parameter, SyntaxToken arrowToken, BlockSyntax block, ExpressionSyntax expressionBody)
        => SyntaxFactory.SimpleLambdaExpression(asyncKeyword, parameter, arrowToken, block, expressionBody);

    public static SimpleLambdaExpressionSyntax SimpleLambdaExpression(SyntaxTokenList modifiers, ParameterSyntax parameter, SyntaxToken arrowToken, BlockSyntax block, ExpressionSyntax expressionBody)
        => SyntaxFactory.SimpleLambdaExpression(modifiers, parameter, arrowToken, block, expressionBody);

    public static SimpleLambdaExpressionSyntax SimpleLambdaExpression(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, ParameterSyntax parameter, BlockSyntax block, ExpressionSyntax expressionBody)
        => SyntaxFactory.SimpleLambdaExpression(attributeLists, modifiers, parameter, block, expressionBody);

    public static SimpleLambdaExpressionSyntax SimpleLambdaExpression(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, ParameterSyntax parameter, SyntaxToken arrowToken, BlockSyntax block, ExpressionSyntax expressionBody)
        => SyntaxFactory.SimpleLambdaExpression(attributeLists, modifiers, parameter, arrowToken, block, expressionBody);

    public static SyntaxList<TNode> SingletonList<TNode>(TNode node)
        where TNode : SyntaxNode
        => SyntaxFactory.SingletonList(node);

    public static SeparatedSyntaxList<TNode> SingletonSeparatedList<TNode>(TNode node)
        where TNode : SyntaxNode
        => SyntaxFactory.SingletonSeparatedList(node);

    public static SingleVariableDesignationSyntax SingleVariableDesignation(SyntaxToken identifier)
        => SyntaxFactory.SingleVariableDesignation(identifier);

    public static SizeOfExpressionSyntax SizeOfExpression(TypeSyntax type)
        => SyntaxFactory.SizeOfExpression(type);

    public static SizeOfExpressionSyntax SizeOfExpression(SyntaxToken keyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken)
        => SyntaxFactory.SizeOfExpression(keyword, openParenToken, type, closeParenToken);

    public static SkippedTokensTriviaSyntax SkippedTokensTrivia()
        => SyntaxFactory.SkippedTokensTrivia();

    public static SkippedTokensTriviaSyntax SkippedTokensTrivia(SyntaxTokenList tokens)
        => SyntaxFactory.SkippedTokensTrivia(tokens);

    public static SlicePatternSyntax SlicePattern(PatternSyntax pattern)
        => SyntaxFactory.SlicePattern(pattern);

    public static SlicePatternSyntax SlicePattern(SyntaxToken dotDotToken, PatternSyntax pattern)
        => SyntaxFactory.SlicePattern(dotDotToken, pattern);

    public static SpreadElementSyntax SpreadElement(ExpressionSyntax expression)
        => SyntaxFactory.SpreadElement(expression);

    public static SpreadElementSyntax SpreadElement(SyntaxToken operatorToken, ExpressionSyntax expression)
        => SyntaxFactory.SpreadElement(operatorToken, expression);

    public static StackAllocArrayCreationExpressionSyntax StackAllocArrayCreationExpression(TypeSyntax type)
        => SyntaxFactory.StackAllocArrayCreationExpression(type);

    public static StackAllocArrayCreationExpressionSyntax StackAllocArrayCreationExpression(SyntaxToken stackAllocKeyword, TypeSyntax type)
        => SyntaxFactory.StackAllocArrayCreationExpression(stackAllocKeyword, type);

    public static StackAllocArrayCreationExpressionSyntax StackAllocArrayCreationExpression(TypeSyntax type, InitializerExpressionSyntax initializer)
        => SyntaxFactory.StackAllocArrayCreationExpression(type, initializer);

    public static StackAllocArrayCreationExpressionSyntax StackAllocArrayCreationExpression(SyntaxToken stackAllocKeyword, TypeSyntax type, InitializerExpressionSyntax initializer)
        => SyntaxFactory.StackAllocArrayCreationExpression(stackAllocKeyword, type, initializer);

    public static StructDeclarationSyntax StructDeclaration(SyntaxToken identifier)
        => SyntaxFactory.StructDeclaration(identifier);

    public static StructDeclarationSyntax StructDeclaration(string identifier)
        => SyntaxFactory.StructDeclaration(identifier);

    public static StructDeclarationSyntax StructDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxList<MemberDeclarationSyntax> members)
        => SyntaxFactory.StructDeclaration(attributeLists, modifiers, identifier, typeParameterList, baseList, constraintClauses, members);

    public static StructDeclarationSyntax StructDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxList<MemberDeclarationSyntax> members)
        => SyntaxFactory.StructDeclaration(attributeLists, modifiers, identifier, typeParameterList, parameterList, baseList, constraintClauses, members);

    public static StructDeclarationSyntax StructDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
        => SyntaxFactory.StructDeclaration(attributeLists, modifiers, keyword, identifier, typeParameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);

    public static StructDeclarationSyntax StructDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
        => SyntaxFactory.StructDeclaration(attributeLists, modifiers, keyword, identifier, typeParameterList, parameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);

    public static SubpatternSyntax Subpattern(PatternSyntax pattern)
        => SyntaxFactory.Subpattern(pattern);

    public static SubpatternSyntax Subpattern(NameColonSyntax nameColon, PatternSyntax pattern)
        => SyntaxFactory.Subpattern(nameColon, pattern);

    public static SubpatternSyntax Subpattern(BaseExpressionColonSyntax expressionColon, PatternSyntax pattern)
        => SyntaxFactory.Subpattern(expressionColon, pattern);

    public static SwitchExpressionSyntax SwitchExpression(ExpressionSyntax governingExpression)
        => SyntaxFactory.SwitchExpression(governingExpression);

    public static SwitchExpressionSyntax SwitchExpression(ExpressionSyntax governingExpression, SeparatedSyntaxList<SwitchExpressionArmSyntax> arms)
        => SyntaxFactory.SwitchExpression(governingExpression, arms);

    public static SwitchExpressionSyntax SwitchExpression(ExpressionSyntax governingExpression, SyntaxToken switchKeyword, SyntaxToken openBraceToken, SeparatedSyntaxList<SwitchExpressionArmSyntax> arms, SyntaxToken closeBraceToken)
        => SyntaxFactory.SwitchExpression(governingExpression, switchKeyword, openBraceToken, arms, closeBraceToken);

    public static SwitchExpressionArmSyntax SwitchExpressionArm(PatternSyntax pattern, ExpressionSyntax expression)
        => SyntaxFactory.SwitchExpressionArm(pattern, expression);

    public static SwitchExpressionArmSyntax SwitchExpressionArm(PatternSyntax pattern, WhenClauseSyntax whenClause, ExpressionSyntax expression)
        => SyntaxFactory.SwitchExpressionArm(pattern, whenClause, expression);

    public static SwitchExpressionArmSyntax SwitchExpressionArm(PatternSyntax pattern, WhenClauseSyntax whenClause, SyntaxToken equalsGreaterThanToken, ExpressionSyntax expression)
        => SyntaxFactory.SwitchExpressionArm(pattern, whenClause, equalsGreaterThanToken, expression);

    public static SwitchSectionSyntax SwitchSection()
        => SyntaxFactory.SwitchSection();

    public static SwitchSectionSyntax SwitchSection(SyntaxList<SwitchLabelSyntax> labels, SyntaxList<StatementSyntax> statements)
        => SyntaxFactory.SwitchSection(labels, statements);

    public static SwitchStatementSyntax SwitchStatement(ExpressionSyntax expression)
        => SyntaxFactory.SwitchStatement(expression);

    public static SwitchStatementSyntax SwitchStatement(ExpressionSyntax expression, SyntaxList<SwitchSectionSyntax> sections)
        => SyntaxFactory.SwitchStatement(expression, sections);

    public static SwitchStatementSyntax SwitchStatement(SyntaxToken switchKeyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, SyntaxToken openBraceToken, SyntaxList<SwitchSectionSyntax> sections, SyntaxToken closeBraceToken)
        => SyntaxFactory.SwitchStatement(switchKeyword, openParenToken, expression, closeParenToken, openBraceToken, sections, closeBraceToken);

    public static SwitchStatementSyntax SwitchStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken switchKeyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, SyntaxToken openBraceToken, SyntaxList<SwitchSectionSyntax> sections, SyntaxToken closeBraceToken)
        => SyntaxFactory.SwitchStatement(attributeLists, switchKeyword, openParenToken, expression, closeParenToken, openBraceToken, sections, closeBraceToken);

    public static SyntaxTree SyntaxTree(SyntaxNode root, ParseOptions options, string path, Encoding encoding)
        => SyntaxFactory.SyntaxTree(root, options, path, encoding);

    public static SyntaxTrivia SyntaxTrivia(SyntaxKind kind, string text)
        => SyntaxFactory.SyntaxTrivia(kind, text);

    public static ThisExpressionSyntax ThisExpression()
        => SyntaxFactory.ThisExpression();

    public static ThisExpressionSyntax ThisExpression(SyntaxToken token)
        => SyntaxFactory.ThisExpression(token);

    public static ThrowExpressionSyntax ThrowExpression(ExpressionSyntax expression)
        => SyntaxFactory.ThrowExpression(expression);

    public static ThrowExpressionSyntax ThrowExpression(SyntaxToken throwKeyword, ExpressionSyntax expression)
        => SyntaxFactory.ThrowExpression(throwKeyword, expression);

    public static ThrowStatementSyntax ThrowStatement(ExpressionSyntax expression)
        => SyntaxFactory.ThrowStatement(expression);

    public static ThrowStatementSyntax ThrowStatement(SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax expression)
        => SyntaxFactory.ThrowStatement(attributeLists, expression);

    public static ThrowStatementSyntax ThrowStatement(SyntaxToken throwKeyword, ExpressionSyntax expression, SyntaxToken semicolonToken)
        => SyntaxFactory.ThrowStatement(throwKeyword, expression, semicolonToken);

    public static ThrowStatementSyntax ThrowStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken throwKeyword, ExpressionSyntax expression, SyntaxToken semicolonToken)
        => SyntaxFactory.ThrowStatement(attributeLists, throwKeyword, expression, semicolonToken);

    public static SyntaxToken Token(SyntaxKind kind)
        => SyntaxFactory.Token(kind);

    public static SyntaxToken Token(SyntaxTriviaList leading, SyntaxKind kind, SyntaxTriviaList trailing)
        => SyntaxFactory.Token(leading, kind, trailing);

    public static SyntaxToken Token(SyntaxTriviaList leading, SyntaxKind kind, string text, string valueText, SyntaxTriviaList trailing)
        => SyntaxFactory.Token(leading, kind, text, valueText, trailing);

    public static SyntaxTokenList TokenList()
        => SyntaxFactory.TokenList();

    public static SyntaxTokenList TokenList(SyntaxToken token)
        => SyntaxFactory.TokenList(token);

    public static SyntaxTokenList TokenList(SyntaxToken[] tokens)
        => SyntaxFactory.TokenList(tokens);

    public static SyntaxTokenList TokenList(IEnumerable<SyntaxToken> tokens)
        => SyntaxFactory.TokenList(tokens);

    public static SyntaxTrivia Trivia(StructuredTriviaSyntax node)
        => SyntaxFactory.Trivia(node);

    public static SyntaxTriviaList TriviaList()
        => SyntaxFactory.TriviaList();

    public static SyntaxTriviaList TriviaList(SyntaxTrivia trivia)
        => SyntaxFactory.TriviaList(trivia);

    public static SyntaxTriviaList TriviaList(SyntaxTrivia[] trivias)
        => SyntaxFactory.TriviaList(trivias);

    public static SyntaxTriviaList TriviaList(IEnumerable<SyntaxTrivia> trivias)
        => SyntaxFactory.TriviaList(trivias);

    public static TryStatementSyntax TryStatement(SyntaxList<CatchClauseSyntax> catches)
        => SyntaxFactory.TryStatement(catches);

    public static TryStatementSyntax TryStatement(BlockSyntax block, SyntaxList<CatchClauseSyntax> catches, FinallyClauseSyntax @finally)
        => SyntaxFactory.TryStatement(block, catches, @finally);

    public static TryStatementSyntax TryStatement(SyntaxToken tryKeyword, BlockSyntax block, SyntaxList<CatchClauseSyntax> catches, FinallyClauseSyntax @finally)
        => SyntaxFactory.TryStatement(tryKeyword, block, catches, @finally);

    public static TryStatementSyntax TryStatement(SyntaxList<AttributeListSyntax> attributeLists, BlockSyntax block, SyntaxList<CatchClauseSyntax> catches, FinallyClauseSyntax @finally)
        => SyntaxFactory.TryStatement(attributeLists, block, catches, @finally);

    public static TryStatementSyntax TryStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken tryKeyword, BlockSyntax block, SyntaxList<CatchClauseSyntax> catches, FinallyClauseSyntax @finally)
        => SyntaxFactory.TryStatement(attributeLists, tryKeyword, block, catches, @finally);

    public static TupleElementSyntax TupleElement(TypeSyntax type)
        => SyntaxFactory.TupleElement(type);

    public static TupleElementSyntax TupleElement(TypeSyntax type, SyntaxToken identifier)
        => SyntaxFactory.TupleElement(type, identifier);

    public static TupleExpressionSyntax TupleExpression(SeparatedSyntaxList<ArgumentSyntax> arguments)
        => SyntaxFactory.TupleExpression(arguments);

    public static TupleExpressionSyntax TupleExpression(SyntaxToken openParenToken, SeparatedSyntaxList<ArgumentSyntax> arguments, SyntaxToken closeParenToken)
        => SyntaxFactory.TupleExpression(openParenToken, arguments, closeParenToken);

    public static TupleTypeSyntax TupleType(SeparatedSyntaxList<TupleElementSyntax> elements)
        => SyntaxFactory.TupleType(elements);

    public static TupleTypeSyntax TupleType(SyntaxToken openParenToken, SeparatedSyntaxList<TupleElementSyntax> elements, SyntaxToken closeParenToken)
        => SyntaxFactory.TupleType(openParenToken, elements, closeParenToken);

    public static TypeArgumentListSyntax TypeArgumentList(SeparatedSyntaxList<TypeSyntax> arguments)
        => SyntaxFactory.TypeArgumentList(arguments);

    public static TypeArgumentListSyntax TypeArgumentList(SyntaxToken lessThanToken, SeparatedSyntaxList<TypeSyntax> arguments, SyntaxToken greaterThanToken)
        => SyntaxFactory.TypeArgumentList(lessThanToken, arguments, greaterThanToken);

    public static TypeConstraintSyntax TypeConstraint(TypeSyntax type)
        => SyntaxFactory.TypeConstraint(type);

    public static TypeCrefSyntax TypeCref(TypeSyntax type)
        => SyntaxFactory.TypeCref(type);

    public static TypeDeclarationSyntax TypeDeclaration(SyntaxKind kind, SyntaxToken identifier)
        => SyntaxFactory.TypeDeclaration(kind, identifier);

    public static TypeDeclarationSyntax TypeDeclaration(SyntaxKind kind, string identifier)
        => SyntaxFactory.TypeDeclaration(kind, identifier);

    public static TypeDeclarationSyntax TypeDeclaration(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributes, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
        => SyntaxFactory.TypeDeclaration(kind, attributes, modifiers, keyword, identifier, typeParameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);

    public static TypeOfExpressionSyntax TypeOfExpression(TypeSyntax type)
        => SyntaxFactory.TypeOfExpression(type);

    public static TypeOfExpressionSyntax TypeOfExpression(SyntaxToken keyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken)
        => SyntaxFactory.TypeOfExpression(keyword, openParenToken, type, closeParenToken);

    public static TypeParameterSyntax TypeParameter(SyntaxToken identifier)
        => SyntaxFactory.TypeParameter(identifier);

    public static TypeParameterSyntax TypeParameter(string identifier)
        => SyntaxFactory.TypeParameter(identifier);

    public static TypeParameterSyntax TypeParameter(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken varianceKeyword, SyntaxToken identifier)
        => SyntaxFactory.TypeParameter(attributeLists, varianceKeyword, identifier);

    public static TypeParameterConstraintClauseSyntax TypeParameterConstraintClause(IdentifierNameSyntax name)
        => SyntaxFactory.TypeParameterConstraintClause(name);

    public static TypeParameterConstraintClauseSyntax TypeParameterConstraintClause(string name)
        => SyntaxFactory.TypeParameterConstraintClause(name);

    public static TypeParameterConstraintClauseSyntax TypeParameterConstraintClause(IdentifierNameSyntax name, SeparatedSyntaxList<TypeParameterConstraintSyntax> constraints)
        => SyntaxFactory.TypeParameterConstraintClause(name, constraints);

    public static TypeParameterConstraintClauseSyntax TypeParameterConstraintClause(SyntaxToken whereKeyword, IdentifierNameSyntax name, SyntaxToken colonToken, SeparatedSyntaxList<TypeParameterConstraintSyntax> constraints)
        => SyntaxFactory.TypeParameterConstraintClause(whereKeyword, name, colonToken, constraints);

    public static TypeParameterListSyntax TypeParameterList(SeparatedSyntaxList<TypeParameterSyntax> parameters)
        => SyntaxFactory.TypeParameterList(parameters);

    public static TypeParameterListSyntax TypeParameterList(SyntaxToken lessThanToken, SeparatedSyntaxList<TypeParameterSyntax> parameters, SyntaxToken greaterThanToken)
        => SyntaxFactory.TypeParameterList(lessThanToken, parameters, greaterThanToken);

    public static TypePatternSyntax TypePattern(TypeSyntax type)
        => SyntaxFactory.TypePattern(type);

    public static UnaryPatternSyntax UnaryPattern(PatternSyntax pattern)
        => SyntaxFactory.UnaryPattern(pattern);

    public static UnaryPatternSyntax UnaryPattern(SyntaxToken operatorToken, PatternSyntax pattern)
        => SyntaxFactory.UnaryPattern(operatorToken, pattern);

    public static UndefDirectiveTriviaSyntax UndefDirectiveTrivia(SyntaxToken name, bool isActive)
        => SyntaxFactory.UndefDirectiveTrivia(name, isActive);

    public static UndefDirectiveTriviaSyntax UndefDirectiveTrivia(string name, bool isActive)
        => SyntaxFactory.UndefDirectiveTrivia(name, isActive);

    public static UndefDirectiveTriviaSyntax UndefDirectiveTrivia(SyntaxToken hashToken, SyntaxToken undefKeyword, SyntaxToken name, SyntaxToken endOfDirectiveToken, bool isActive)
        => SyntaxFactory.UndefDirectiveTrivia(hashToken, undefKeyword, name, endOfDirectiveToken, isActive);

    public static UnsafeStatementSyntax UnsafeStatement(BlockSyntax block)
        => SyntaxFactory.UnsafeStatement(block);

    public static UnsafeStatementSyntax UnsafeStatement(SyntaxToken unsafeKeyword, BlockSyntax block)
        => SyntaxFactory.UnsafeStatement(unsafeKeyword, block);

    public static UnsafeStatementSyntax UnsafeStatement(SyntaxList<AttributeListSyntax> attributeLists, BlockSyntax block)
        => SyntaxFactory.UnsafeStatement(attributeLists, block);

    public static UnsafeStatementSyntax UnsafeStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken unsafeKeyword, BlockSyntax block)
        => SyntaxFactory.UnsafeStatement(attributeLists, unsafeKeyword, block);

    public static UsingDirectiveSyntax UsingDirective(NameSyntax name)
        => SyntaxFactory.UsingDirective(name);

    public static UsingDirectiveSyntax UsingDirective(TypeSyntax namespaceOrType)
        => SyntaxFactory.UsingDirective(namespaceOrType);

    public static UsingDirectiveSyntax UsingDirective(NameEqualsSyntax alias, NameSyntax name)
        => SyntaxFactory.UsingDirective(alias, name);

    public static UsingDirectiveSyntax UsingDirective(NameEqualsSyntax alias, TypeSyntax namespaceOrType)
        => SyntaxFactory.UsingDirective(alias, namespaceOrType);

    public static UsingDirectiveSyntax UsingDirective(SyntaxToken staticKeyword, NameEqualsSyntax alias, NameSyntax name)
        => SyntaxFactory.UsingDirective(staticKeyword, alias, name);

    public static UsingDirectiveSyntax UsingDirective(SyntaxToken usingKeyword, SyntaxToken staticKeyword, NameEqualsSyntax alias, NameSyntax name, SyntaxToken semicolonToken)
        => SyntaxFactory.UsingDirective(usingKeyword, staticKeyword, alias, name, semicolonToken);

    public static UsingDirectiveSyntax UsingDirective(SyntaxToken globalKeyword, SyntaxToken usingKeyword, SyntaxToken staticKeyword, NameEqualsSyntax alias, NameSyntax name, SyntaxToken semicolonToken)
        => SyntaxFactory.UsingDirective(globalKeyword, usingKeyword, staticKeyword, alias, name, semicolonToken);

    public static UsingDirectiveSyntax UsingDirective(SyntaxToken globalKeyword, SyntaxToken usingKeyword, SyntaxToken staticKeyword, SyntaxToken unsafeKeyword, NameEqualsSyntax alias, TypeSyntax namespaceOrType, SyntaxToken semicolonToken)
        => SyntaxFactory.UsingDirective(globalKeyword, usingKeyword, staticKeyword, unsafeKeyword, alias, namespaceOrType, semicolonToken);

    public static UsingStatementSyntax UsingStatement(StatementSyntax statement)
        => SyntaxFactory.UsingStatement(statement);

    public static UsingStatementSyntax UsingStatement(VariableDeclarationSyntax declaration, ExpressionSyntax expression, StatementSyntax statement)
        => SyntaxFactory.UsingStatement(declaration, expression, statement);

    public static UsingStatementSyntax UsingStatement(SyntaxList<AttributeListSyntax> attributeLists, VariableDeclarationSyntax declaration, ExpressionSyntax expression, StatementSyntax statement)
        => SyntaxFactory.UsingStatement(attributeLists, declaration, expression, statement);

    public static UsingStatementSyntax UsingStatement(SyntaxToken usingKeyword, SyntaxToken openParenToken, VariableDeclarationSyntax declaration, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
        => SyntaxFactory.UsingStatement(usingKeyword, openParenToken, declaration, expression, closeParenToken, statement);

    public static UsingStatementSyntax UsingStatement(SyntaxToken awaitKeyword, SyntaxToken usingKeyword, SyntaxToken openParenToken, VariableDeclarationSyntax declaration, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
        => SyntaxFactory.UsingStatement(awaitKeyword, usingKeyword, openParenToken, declaration, expression, closeParenToken, statement);

    public static UsingStatementSyntax UsingStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken awaitKeyword, SyntaxToken usingKeyword, SyntaxToken openParenToken, VariableDeclarationSyntax declaration, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
        => SyntaxFactory.UsingStatement(attributeLists, awaitKeyword, usingKeyword, openParenToken, declaration, expression, closeParenToken, statement);

    public static VariableDeclarationSyntax VariableDeclaration(TypeSyntax type)
        => SyntaxFactory.VariableDeclaration(type);

    public static VariableDeclarationSyntax VariableDeclaration(TypeSyntax type, string identifier)
        => VariableDeclaration(type, Identifier(identifier));

    public static VariableDeclarationSyntax VariableDeclaration(TypeSyntax type, SyntaxToken identifier)
        => VariableDeclaration(type, VariableDeclarator(identifier));

    public static VariableDeclarationSyntax VariableDeclaration(TypeSyntax type, VariableDeclaratorSyntax variable)
        => VariableDeclaration(type, SingletonSeparatedList(variable));

    public static VariableDeclarationSyntax VariableDeclaration(TypeSyntax type, SeparatedSyntaxList<VariableDeclaratorSyntax> variables)
        => SyntaxFactory.VariableDeclaration(type, variables);

    public static VariableDeclaratorSyntax VariableDeclarator(SyntaxToken identifier)
        => SyntaxFactory.VariableDeclarator(identifier);

    public static VariableDeclaratorSyntax VariableDeclarator(string identifier)
        => SyntaxFactory.VariableDeclarator(identifier);

    public static VariableDeclaratorSyntax VariableDeclarator(SyntaxToken identifier, BracketedArgumentListSyntax argumentList, EqualsValueClauseSyntax initializer)
        => SyntaxFactory.VariableDeclarator(identifier, argumentList, initializer);

    public static VarPatternSyntax VarPattern(VariableDesignationSyntax designation)
        => SyntaxFactory.VarPattern(designation);

    public static VarPatternSyntax VarPattern(SyntaxToken varKeyword, VariableDesignationSyntax designation)
        => SyntaxFactory.VarPattern(varKeyword, designation);

    public static SyntaxToken VerbatimIdentifier(SyntaxTriviaList leading, string text, string valueText, SyntaxTriviaList trailing)
        => SyntaxFactory.VerbatimIdentifier(leading, text, valueText, trailing);

    public static WarningDirectiveTriviaSyntax WarningDirectiveTrivia(bool isActive)
        => SyntaxFactory.WarningDirectiveTrivia(isActive);

    public static WarningDirectiveTriviaSyntax WarningDirectiveTrivia(SyntaxToken hashToken, SyntaxToken warningKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
        => SyntaxFactory.WarningDirectiveTrivia(hashToken, warningKeyword, endOfDirectiveToken, isActive);

    public static WhenClauseSyntax WhenClause(ExpressionSyntax condition)
        => SyntaxFactory.WhenClause(condition);

    public static WhenClauseSyntax WhenClause(SyntaxToken whenKeyword, ExpressionSyntax condition)
        => SyntaxFactory.WhenClause(whenKeyword, condition);

    public static WhereClauseSyntax WhereClause(ExpressionSyntax condition)
        => SyntaxFactory.WhereClause(condition);

    public static WhereClauseSyntax WhereClause(SyntaxToken whereKeyword, ExpressionSyntax condition)
        => SyntaxFactory.WhereClause(whereKeyword, condition);

    public static WhileStatementSyntax WhileStatement(ExpressionSyntax condition, StatementSyntax statement)
        => SyntaxFactory.WhileStatement(condition, statement);

    public static WhileStatementSyntax WhileStatement(SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax condition, StatementSyntax statement)
        => SyntaxFactory.WhileStatement(attributeLists, condition, statement);

    public static WhileStatementSyntax WhileStatement(SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement)
        => SyntaxFactory.WhileStatement(whileKeyword, openParenToken, condition, closeParenToken, statement);

    public static WhileStatementSyntax WhileStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement)
        => SyntaxFactory.WhileStatement(attributeLists, whileKeyword, openParenToken, condition, closeParenToken, statement);

    public static SyntaxTrivia Whitespace(string text)
        => SyntaxFactory.Whitespace(text);

    public static WithExpressionSyntax WithExpression(ExpressionSyntax expression, InitializerExpressionSyntax initializer)
        => SyntaxFactory.WithExpression(expression, initializer);

    public static WithExpressionSyntax WithExpression(ExpressionSyntax expression, SyntaxToken withKeyword, InitializerExpressionSyntax initializer)
        => SyntaxFactory.WithExpression(expression, withKeyword, initializer);

    public static XmlCDataSectionSyntax XmlCDataSection(SyntaxTokenList textTokens)
        => SyntaxFactory.XmlCDataSection(textTokens);

    public static XmlCDataSectionSyntax XmlCDataSection(SyntaxToken startCDataToken, SyntaxTokenList textTokens, SyntaxToken endCDataToken)
        => SyntaxFactory.XmlCDataSection(startCDataToken, textTokens, endCDataToken);

    public static XmlCommentSyntax XmlComment(SyntaxTokenList textTokens)
        => SyntaxFactory.XmlComment(textTokens);

    public static XmlCommentSyntax XmlComment(SyntaxToken lessThanExclamationMinusMinusToken, SyntaxTokenList textTokens, SyntaxToken minusMinusGreaterThanToken)
        => SyntaxFactory.XmlComment(lessThanExclamationMinusMinusToken, textTokens, minusMinusGreaterThanToken);

    public static XmlCrefAttributeSyntax XmlCrefAttribute(CrefSyntax cref)
        => SyntaxFactory.XmlCrefAttribute(cref);

    public static XmlCrefAttributeSyntax XmlCrefAttribute(CrefSyntax cref, SyntaxKind quoteKind)
        => SyntaxFactory.XmlCrefAttribute(cref, quoteKind);

    public static XmlCrefAttributeSyntax XmlCrefAttribute(XmlNameSyntax name, SyntaxToken startQuoteToken, CrefSyntax cref, SyntaxToken endQuoteToken)
        => SyntaxFactory.XmlCrefAttribute(name, startQuoteToken, cref, endQuoteToken);

    public static XmlCrefAttributeSyntax XmlCrefAttribute(XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, CrefSyntax cref, SyntaxToken endQuoteToken)
        => SyntaxFactory.XmlCrefAttribute(name, equalsToken, startQuoteToken, cref, endQuoteToken);

    public static XmlElementSyntax XmlElement(string localName, SyntaxList<XmlNodeSyntax> content)
        => SyntaxFactory.XmlElement(localName, content);

    public static XmlElementSyntax XmlElement(XmlNameSyntax name, SyntaxList<XmlNodeSyntax> content)
        => SyntaxFactory.XmlElement(name, content);

    public static XmlElementSyntax XmlElement(XmlElementStartTagSyntax startTag, XmlElementEndTagSyntax endTag)
        => SyntaxFactory.XmlElement(startTag, endTag);

    public static XmlElementSyntax XmlElement(XmlElementStartTagSyntax startTag, SyntaxList<XmlNodeSyntax> content, XmlElementEndTagSyntax endTag)
        => SyntaxFactory.XmlElement(startTag, content, endTag);

    public static XmlElementEndTagSyntax XmlElementEndTag(XmlNameSyntax name)
        => SyntaxFactory.XmlElementEndTag(name);

    public static XmlElementEndTagSyntax XmlElementEndTag(SyntaxToken lessThanSlashToken, XmlNameSyntax name, SyntaxToken greaterThanToken)
        => SyntaxFactory.XmlElementEndTag(lessThanSlashToken, name, greaterThanToken);

    public static XmlElementStartTagSyntax XmlElementStartTag(XmlNameSyntax name)
        => SyntaxFactory.XmlElementStartTag(name);

    public static XmlElementStartTagSyntax XmlElementStartTag(XmlNameSyntax name, SyntaxList<XmlAttributeSyntax> attributes)
        => SyntaxFactory.XmlElementStartTag(name, attributes);

    public static XmlElementStartTagSyntax XmlElementStartTag(SyntaxToken lessThanToken, XmlNameSyntax name, SyntaxList<XmlAttributeSyntax> attributes, SyntaxToken greaterThanToken)
        => SyntaxFactory.XmlElementStartTag(lessThanToken, name, attributes, greaterThanToken);

    public static XmlEmptyElementSyntax XmlEmptyElement(string localName)
        => SyntaxFactory.XmlEmptyElement(localName);

    public static XmlEmptyElementSyntax XmlEmptyElement(XmlNameSyntax name)
        => SyntaxFactory.XmlEmptyElement(name);

    public static XmlEmptyElementSyntax XmlEmptyElement(XmlNameSyntax name, SyntaxList<XmlAttributeSyntax> attributes)
        => SyntaxFactory.XmlEmptyElement(name, attributes);

    public static XmlEmptyElementSyntax XmlEmptyElement(SyntaxToken lessThanToken, XmlNameSyntax name, SyntaxList<XmlAttributeSyntax> attributes, SyntaxToken slashGreaterThanToken)
        => SyntaxFactory.XmlEmptyElement(lessThanToken, name, attributes, slashGreaterThanToken);

    public static SyntaxToken XmlEntity(SyntaxTriviaList leading, string text, string value, SyntaxTriviaList trailing)
        => SyntaxFactory.XmlEntity(leading, text, value, trailing);

    public static XmlElementSyntax XmlExampleElement(XmlNodeSyntax[] content)
        => SyntaxFactory.XmlExampleElement(content);

    public static XmlElementSyntax XmlExampleElement(SyntaxList<XmlNodeSyntax> content)
        => SyntaxFactory.XmlExampleElement(content);

    public static XmlElementSyntax XmlExceptionElement(CrefSyntax cref, XmlNodeSyntax[] content)
        => SyntaxFactory.XmlExceptionElement(cref, content);

    public static XmlElementSyntax XmlExceptionElement(CrefSyntax cref, SyntaxList<XmlNodeSyntax> content)
        => SyntaxFactory.XmlExceptionElement(cref, content);

    public static XmlElementSyntax XmlMultiLineElement(string localName, SyntaxList<XmlNodeSyntax> content)
        => SyntaxFactory.XmlMultiLineElement(localName, content);

    public static XmlElementSyntax XmlMultiLineElement(XmlNameSyntax name, SyntaxList<XmlNodeSyntax> content)
        => SyntaxFactory.XmlMultiLineElement(name, content);

    public static XmlNameSyntax XmlName(SyntaxToken localName)
        => SyntaxFactory.XmlName(localName);

    public static XmlNameSyntax XmlName(string localName)
        => SyntaxFactory.XmlName(localName);

    public static XmlNameSyntax XmlName(XmlPrefixSyntax prefix, SyntaxToken localName)
        => SyntaxFactory.XmlName(prefix, localName);

    public static XmlNameAttributeSyntax XmlNameAttribute(string parameterName)
        => SyntaxFactory.XmlNameAttribute(parameterName);

    public static XmlNameAttributeSyntax XmlNameAttribute(XmlNameSyntax name, SyntaxToken startQuoteToken, IdentifierNameSyntax identifier, SyntaxToken endQuoteToken)
        => SyntaxFactory.XmlNameAttribute(name, startQuoteToken, identifier, endQuoteToken);

    public static XmlNameAttributeSyntax XmlNameAttribute(XmlNameSyntax name, SyntaxToken startQuoteToken, string identifier, SyntaxToken endQuoteToken)
        => SyntaxFactory.XmlNameAttribute(name, startQuoteToken, identifier, endQuoteToken);

    public static XmlNameAttributeSyntax XmlNameAttribute(XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, IdentifierNameSyntax identifier, SyntaxToken endQuoteToken)
        => SyntaxFactory.XmlNameAttribute(name, equalsToken, startQuoteToken, identifier, endQuoteToken);

    public static XmlTextSyntax XmlNewLine(string text)
        => SyntaxFactory.XmlNewLine(text);

    public static XmlEmptyElementSyntax XmlNullKeywordElement()
        => SyntaxFactory.XmlNullKeywordElement();

    public static XmlElementSyntax XmlParaElement(XmlNodeSyntax[] content)
        => SyntaxFactory.XmlParaElement(content);

    public static XmlElementSyntax XmlParaElement(SyntaxList<XmlNodeSyntax> content)
        => SyntaxFactory.XmlParaElement(content);

    public static XmlElementSyntax XmlParamElement(string parameterName, XmlNodeSyntax[] content)
        => SyntaxFactory.XmlParamElement(parameterName, content);

    public static XmlElementSyntax XmlParamElement(string parameterName, SyntaxList<XmlNodeSyntax> content)
        => SyntaxFactory.XmlParamElement(parameterName, content);

    public static XmlEmptyElementSyntax XmlParamRefElement(string parameterName)
        => SyntaxFactory.XmlParamRefElement(parameterName);

    public static XmlElementSyntax XmlPermissionElement(CrefSyntax cref, XmlNodeSyntax[] content)
        => SyntaxFactory.XmlPermissionElement(cref, content);

    public static XmlElementSyntax XmlPermissionElement(CrefSyntax cref, SyntaxList<XmlNodeSyntax> content)
        => SyntaxFactory.XmlPermissionElement(cref, content);

    public static XmlElementSyntax XmlPlaceholderElement(XmlNodeSyntax[] content)
        => SyntaxFactory.XmlPlaceholderElement(content);

    public static XmlElementSyntax XmlPlaceholderElement(SyntaxList<XmlNodeSyntax> content)
        => SyntaxFactory.XmlPlaceholderElement(content);

    public static XmlPrefixSyntax XmlPrefix(SyntaxToken prefix)
        => SyntaxFactory.XmlPrefix(prefix);

    public static XmlPrefixSyntax XmlPrefix(string prefix)
        => SyntaxFactory.XmlPrefix(prefix);

    public static XmlPrefixSyntax XmlPrefix(SyntaxToken prefix, SyntaxToken colonToken)
        => SyntaxFactory.XmlPrefix(prefix, colonToken);

    public static XmlEmptyElementSyntax XmlPreliminaryElement()
        => SyntaxFactory.XmlPreliminaryElement();

    public static XmlProcessingInstructionSyntax XmlProcessingInstruction(XmlNameSyntax name)
        => SyntaxFactory.XmlProcessingInstruction(name);

    public static XmlProcessingInstructionSyntax XmlProcessingInstruction(XmlNameSyntax name, SyntaxTokenList textTokens)
        => SyntaxFactory.XmlProcessingInstruction(name, textTokens);

    public static XmlProcessingInstructionSyntax XmlProcessingInstruction(SyntaxToken startProcessingInstructionToken, XmlNameSyntax name, SyntaxTokenList textTokens, SyntaxToken endProcessingInstructionToken)
        => SyntaxFactory.XmlProcessingInstruction(startProcessingInstructionToken, name, textTokens, endProcessingInstructionToken);

    public static XmlElementSyntax XmlRemarksElement(XmlNodeSyntax[] content)
        => SyntaxFactory.XmlRemarksElement(content);

    public static XmlElementSyntax XmlRemarksElement(SyntaxList<XmlNodeSyntax> content)
        => SyntaxFactory.XmlRemarksElement(content);

    public static XmlElementSyntax XmlReturnsElement(XmlNodeSyntax[] content)
        => SyntaxFactory.XmlReturnsElement(content);

    public static XmlElementSyntax XmlReturnsElement(SyntaxList<XmlNodeSyntax> content)
        => SyntaxFactory.XmlReturnsElement(content);

    public static XmlEmptyElementSyntax XmlSeeAlsoElement(CrefSyntax cref)
        => SyntaxFactory.XmlSeeAlsoElement(cref);

    public static XmlElementSyntax XmlSeeAlsoElement(Uri linkAddress, SyntaxList<XmlNodeSyntax> linkText)
        => SyntaxFactory.XmlSeeAlsoElement(linkAddress, linkText);

    public static XmlEmptyElementSyntax XmlSeeElement(CrefSyntax cref)
        => SyntaxFactory.XmlSeeElement(cref);

    public static XmlElementSyntax XmlSummaryElement(XmlNodeSyntax[] content)
        => SyntaxFactory.XmlSummaryElement(content);

    public static XmlElementSyntax XmlSummaryElement(SyntaxList<XmlNodeSyntax> content)
        => SyntaxFactory.XmlSummaryElement(content);

    public static XmlTextSyntax XmlText()
        => SyntaxFactory.XmlText();

    public static XmlTextSyntax XmlText(string value)
        => SyntaxFactory.XmlText(value);

    public static XmlTextSyntax XmlText(SyntaxToken[] textTokens)
        => SyntaxFactory.XmlText(textTokens);

    public static XmlTextSyntax XmlText(SyntaxTokenList textTokens)
        => SyntaxFactory.XmlText(textTokens);

    public static XmlTextAttributeSyntax XmlTextAttribute(string name, string value)
        => SyntaxFactory.XmlTextAttribute(name, value);

    public static XmlTextAttributeSyntax XmlTextAttribute(string name, SyntaxToken[] textTokens)
        => SyntaxFactory.XmlTextAttribute(name, textTokens);

    public static XmlTextAttributeSyntax XmlTextAttribute(string name, SyntaxKind quoteKind, SyntaxTokenList textTokens)
        => SyntaxFactory.XmlTextAttribute(name, quoteKind, textTokens);

    public static XmlTextAttributeSyntax XmlTextAttribute(XmlNameSyntax name, SyntaxKind quoteKind, SyntaxTokenList textTokens)
        => SyntaxFactory.XmlTextAttribute(name, quoteKind, textTokens);

    public static XmlTextAttributeSyntax XmlTextAttribute(XmlNameSyntax name, SyntaxToken startQuoteToken, SyntaxToken endQuoteToken)
        => SyntaxFactory.XmlTextAttribute(name, startQuoteToken, endQuoteToken);

    public static XmlTextAttributeSyntax XmlTextAttribute(XmlNameSyntax name, SyntaxToken startQuoteToken, SyntaxTokenList textTokens, SyntaxToken endQuoteToken)
        => SyntaxFactory.XmlTextAttribute(name, startQuoteToken, textTokens, endQuoteToken);

    public static XmlTextAttributeSyntax XmlTextAttribute(XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, SyntaxTokenList textTokens, SyntaxToken endQuoteToken)
        => SyntaxFactory.XmlTextAttribute(name, equalsToken, startQuoteToken, textTokens, endQuoteToken);

    public static SyntaxToken XmlTextLiteral(string value)
        => SyntaxFactory.XmlTextLiteral(value);

    public static SyntaxToken XmlTextLiteral(string text, string value)
        => SyntaxFactory.XmlTextLiteral(text, value);

    public static SyntaxToken XmlTextLiteral(SyntaxTriviaList leading, string text, string value, SyntaxTriviaList trailing)
        => SyntaxFactory.XmlTextLiteral(leading, text, value, trailing);

    public static SyntaxToken XmlTextNewLine(string text)
        => SyntaxFactory.XmlTextNewLine(text);

    public static SyntaxToken XmlTextNewLine(string text, bool continueXmlDocumentationComment)
        => SyntaxFactory.XmlTextNewLine(text, continueXmlDocumentationComment);

    public static SyntaxToken XmlTextNewLine(SyntaxTriviaList leading, string text, string value, SyntaxTriviaList trailing)
        => SyntaxFactory.XmlTextNewLine(leading, text, value, trailing);

    public static XmlEmptyElementSyntax XmlThreadSafetyElement()
        => SyntaxFactory.XmlThreadSafetyElement();

    public static XmlEmptyElementSyntax XmlThreadSafetyElement(bool isStatic, bool isInstance)
        => SyntaxFactory.XmlThreadSafetyElement(isStatic, isInstance);

    public static XmlElementSyntax XmlValueElement(XmlNodeSyntax[] content)
        => SyntaxFactory.XmlValueElement(content);

    public static XmlElementSyntax XmlValueElement(SyntaxList<XmlNodeSyntax> content)
        => SyntaxFactory.XmlValueElement(content);

    public static YieldStatementSyntax YieldStatement(SyntaxKind kind, ExpressionSyntax expression)
        => SyntaxFactory.YieldStatement(kind, expression);

    public static YieldStatementSyntax YieldStatement(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax expression)
        => SyntaxFactory.YieldStatement(kind, attributeLists, expression);

    public static YieldStatementSyntax YieldStatement(SyntaxKind kind, SyntaxToken yieldKeyword, SyntaxToken returnOrBreakKeyword, ExpressionSyntax expression, SyntaxToken semicolonToken)
        => SyntaxFactory.YieldStatement(kind, yieldKeyword, returnOrBreakKeyword, expression, semicolonToken);

    public static YieldStatementSyntax YieldStatement(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken yieldKeyword, SyntaxToken returnOrBreakKeyword, ExpressionSyntax expression, SyntaxToken semicolonToken)
        => SyntaxFactory.YieldStatement(kind, attributeLists, yieldKeyword, returnOrBreakKeyword, expression, semicolonToken);
}
