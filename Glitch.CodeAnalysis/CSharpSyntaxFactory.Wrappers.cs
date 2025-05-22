using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Glitch.CodeAnalysis
{
    /// <summary>
    /// Instance wrapper around <see cref="SyntaxFactory"/>
    /// for easier aliasing and extensions.
    /// </summary>
    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "This is intentionally an instance wrapper around a static class")]
    [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "Intentionally being suppressed at the classe level")]
    public partial class CSharpSyntaxFactory
    {
        public SyntaxTrivia CarriageReturnLineFeed => SyntaxFactory.CarriageReturnLineFeed;
        public SyntaxTrivia LineFeed => SyntaxFactory.LineFeed;
        public SyntaxTrivia CarriageReturn => SyntaxFactory.CarriageReturn;
        public SyntaxTrivia Space => SyntaxFactory.Space;
        public SyntaxTrivia Tab => SyntaxFactory.Tab;
        public SyntaxTrivia ElasticCarriageReturnLineFeed => SyntaxFactory.ElasticCarriageReturnLineFeed;
        public SyntaxTrivia ElasticLineFeed => SyntaxFactory.ElasticLineFeed;
        public SyntaxTrivia ElasticCarriageReturn => SyntaxFactory.ElasticCarriageReturn;
        public SyntaxTrivia ElasticSpace => SyntaxFactory.ElasticSpace;
        public SyntaxTrivia ElasticTab => SyntaxFactory.ElasticTab;
        public SyntaxTrivia ElasticMarker => SyntaxFactory.ElasticMarker;

        public AnonymousMethodExpressionSyntax AnonymousMethodExpression()
            => SyntaxFactory.AnonymousMethodExpression();

        public AnonymousMethodExpressionSyntax AnonymousMethodExpression(SyntaxToken asyncKeyword, SyntaxToken delegateKeyword, ParameterListSyntax parameterList, BlockSyntax block, ExpressionSyntax expressionBody)
            => SyntaxFactory.AnonymousMethodExpression(asyncKeyword, delegateKeyword, parameterList, block, expressionBody);

        public BlockSyntax Block(SyntaxToken openBraceToken, SyntaxList<StatementSyntax> statements, SyntaxToken closeBraceToken)
            => SyntaxFactory.Block(openBraceToken, statements, closeBraceToken);

        public BreakStatementSyntax BreakStatement(SyntaxToken breakKeyword, SyntaxToken semicolonToken)
            => SyntaxFactory.BreakStatement(breakKeyword, semicolonToken);

        public CheckedStatementSyntax CheckedStatement(SyntaxKind kind, SyntaxToken keyword, BlockSyntax block)
            => SyntaxFactory.CheckedStatement(kind, keyword, block);

        public ConstructorDeclarationSyntax ConstructorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, ParameterListSyntax parameterList, ConstructorInitializerSyntax initializer, BlockSyntax body)
            => SyntaxFactory.ConstructorDeclaration(attributeLists, modifiers, identifier, parameterList, initializer, body);

        public ConstructorDeclarationSyntax ConstructorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, ParameterListSyntax parameterList, ConstructorInitializerSyntax initializer, BlockSyntax body, SyntaxToken semicolonToken)
            => SyntaxFactory.ConstructorDeclaration(attributeLists, modifiers, identifier, parameterList, initializer, body, semicolonToken);

        public ConstructorDeclarationSyntax ConstructorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, ParameterListSyntax parameterList, ConstructorInitializerSyntax initializer, ArrowExpressionClauseSyntax expressionBody)
            => SyntaxFactory.ConstructorDeclaration(attributeLists, modifiers, identifier, parameterList, initializer, expressionBody);

        public ConstructorDeclarationSyntax ConstructorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, ParameterListSyntax parameterList, ConstructorInitializerSyntax initializer, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
            => SyntaxFactory.ConstructorDeclaration(attributeLists, modifiers, identifier, parameterList, initializer, expressionBody, semicolonToken);

        public ContinueStatementSyntax ContinueStatement(SyntaxToken continueKeyword, SyntaxToken semicolonToken)
            => SyntaxFactory.ContinueStatement(continueKeyword, semicolonToken);

        public DestructorDeclarationSyntax DestructorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, ParameterListSyntax parameterList, BlockSyntax body)
            => SyntaxFactory.DestructorDeclaration(attributeLists, modifiers, identifier, parameterList, body);

        public DestructorDeclarationSyntax DestructorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken tildeToken, SyntaxToken identifier, ParameterListSyntax parameterList, BlockSyntax body, SyntaxToken semicolonToken)
            => SyntaxFactory.DestructorDeclaration(attributeLists, modifiers, tildeToken, identifier, parameterList, body, semicolonToken);

        public DestructorDeclarationSyntax DestructorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, ParameterListSyntax parameterList, ArrowExpressionClauseSyntax expressionBody)
            => SyntaxFactory.DestructorDeclaration(attributeLists, modifiers, identifier, parameterList, expressionBody);

        public DestructorDeclarationSyntax DestructorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken tildeToken, SyntaxToken identifier, ParameterListSyntax parameterList, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
            => SyntaxFactory.DestructorDeclaration(attributeLists, modifiers, tildeToken, identifier, parameterList, expressionBody, semicolonToken);

        public DoStatementSyntax DoStatement(SyntaxToken doKeyword, StatementSyntax statement, SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, SyntaxToken semicolonToken)
            => SyntaxFactory.DoStatement(doKeyword, statement, whileKeyword, openParenToken, condition, closeParenToken, semicolonToken);

        public EmptyStatementSyntax EmptyStatement(SyntaxToken semicolonToken)
            => SyntaxFactory.EmptyStatement(semicolonToken);

        public ExpressionStatementSyntax ExpressionStatement(ExpressionSyntax expression, SyntaxToken semicolonToken)
            => SyntaxFactory.ExpressionStatement(expression, semicolonToken);

        public FixedStatementSyntax FixedStatement(SyntaxToken fixedKeyword, SyntaxToken openParenToken, VariableDeclarationSyntax declaration, SyntaxToken closeParenToken, StatementSyntax statement)
            => SyntaxFactory.FixedStatement(fixedKeyword, openParenToken, declaration, closeParenToken, statement);

        public ForEachStatementSyntax ForEachStatement(SyntaxToken forEachKeyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
            => SyntaxFactory.ForEachStatement(forEachKeyword, openParenToken, type, identifier, inKeyword, expression, closeParenToken, statement);

        public ForEachStatementSyntax ForEachStatement(SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
            => SyntaxFactory.ForEachStatement(awaitKeyword, forEachKeyword, openParenToken, type, identifier, inKeyword, expression, closeParenToken, statement);

        public ForEachVariableStatementSyntax ForEachVariableStatement(SyntaxToken forEachKeyword, SyntaxToken openParenToken, ExpressionSyntax variable, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
            => SyntaxFactory.ForEachVariableStatement(forEachKeyword, openParenToken, variable, inKeyword, expression, closeParenToken, statement);

        public ForEachVariableStatementSyntax ForEachVariableStatement(SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, ExpressionSyntax variable, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
            => SyntaxFactory.ForEachVariableStatement(awaitKeyword, forEachKeyword, openParenToken, variable, inKeyword, expression, closeParenToken, statement);

        public ForStatementSyntax ForStatement(VariableDeclarationSyntax declaration, SeparatedSyntaxList<ExpressionSyntax> initializers, ExpressionSyntax condition, SeparatedSyntaxList<ExpressionSyntax> incrementors, StatementSyntax statement)
            => SyntaxFactory.ForStatement(declaration, initializers, condition, incrementors, statement);

        public ForStatementSyntax ForStatement(SyntaxToken forKeyword, SyntaxToken openParenToken, VariableDeclarationSyntax declaration, SeparatedSyntaxList<ExpressionSyntax> initializers, SyntaxToken firstSemicolonToken, ExpressionSyntax condition, SyntaxToken secondSemicolonToken, SeparatedSyntaxList<ExpressionSyntax> incrementors, SyntaxToken closeParenToken, StatementSyntax statement)
            => SyntaxFactory.ForStatement(forKeyword, openParenToken, declaration, initializers, firstSemicolonToken, condition, secondSemicolonToken, incrementors, closeParenToken, statement);

        public GotoStatementSyntax GotoStatement(SyntaxKind kind, SyntaxToken caseOrDefaultKeyword, ExpressionSyntax expression)
            => SyntaxFactory.GotoStatement(kind, caseOrDefaultKeyword, expression);

        public GotoStatementSyntax GotoStatement(SyntaxKind kind, SyntaxToken gotoKeyword, SyntaxToken caseOrDefaultKeyword, ExpressionSyntax expression, SyntaxToken semicolonToken)
            => SyntaxFactory.GotoStatement(kind, gotoKeyword, caseOrDefaultKeyword, expression, semicolonToken);

        public IfStatementSyntax IfStatement(ExpressionSyntax condition, StatementSyntax statement, ElseClauseSyntax @else)
            => SyntaxFactory.IfStatement(condition, statement, @else);

        public IfStatementSyntax IfStatement(SyntaxToken ifKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement, ElseClauseSyntax @else)
            => SyntaxFactory.IfStatement(ifKeyword, openParenToken, condition, closeParenToken, statement, @else);

        public IndexerDeclarationSyntax IndexerDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, BracketedParameterListSyntax parameterList, AccessorListSyntax accessorList)
            => SyntaxFactory.IndexerDeclaration(attributeLists, modifiers, type, explicitInterfaceSpecifier, parameterList, accessorList);

        public InterpolatedStringExpressionSyntax InterpolatedStringExpression(SyntaxToken stringStartToken)
            => SyntaxFactory.InterpolatedStringExpression(stringStartToken);

        public InterpolatedStringExpressionSyntax InterpolatedStringExpression(SyntaxToken stringStartToken, SyntaxList<InterpolatedStringContentSyntax> contents)
            => SyntaxFactory.InterpolatedStringExpression(stringStartToken, contents);

        public LabeledStatementSyntax LabeledStatement(SyntaxToken identifier, SyntaxToken colonToken, StatementSyntax statement)
            => SyntaxFactory.LabeledStatement(identifier, colonToken, statement);

        public LiteralExpressionSyntax LiteralExpression(SyntaxKind kind)
            => SyntaxFactory.LiteralExpression(kind);

        public LocalDeclarationStatementSyntax LocalDeclarationStatement(SyntaxTokenList modifiers, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
            => SyntaxFactory.LocalDeclarationStatement(modifiers, declaration, semicolonToken);

        public LocalDeclarationStatementSyntax LocalDeclarationStatement(SyntaxToken awaitKeyword, SyntaxToken usingKeyword, SyntaxTokenList modifiers, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
            => SyntaxFactory.LocalDeclarationStatement(awaitKeyword, usingKeyword, modifiers, declaration, semicolonToken);

        public LocalDeclarationStatementSyntax LocalDeclarationStatement(SyntaxTokenList modifiers, VariableDeclarationSyntax declaration)
            => SyntaxFactory.LocalDeclarationStatement(modifiers, declaration);

        public LocalFunctionStatementSyntax LocalFunctionStatement(SyntaxTokenList modifiers, TypeSyntax returnType, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody)
            => SyntaxFactory.LocalFunctionStatement(modifiers, returnType, identifier, typeParameterList, parameterList, constraintClauses, body, expressionBody);

        public LocalFunctionStatementSyntax LocalFunctionStatement(SyntaxTokenList modifiers, TypeSyntax returnType, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
            => SyntaxFactory.LocalFunctionStatement(modifiers, returnType, identifier, typeParameterList, parameterList, constraintClauses, body, expressionBody, semicolonToken);

        public LockStatementSyntax LockStatement(SyntaxToken lockKeyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
            => SyntaxFactory.LockStatement(lockKeyword, openParenToken, expression, closeParenToken, statement);

        public MethodDeclarationSyntax MethodDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, BlockSyntax body, SyntaxToken semicolonToken)
            => SyntaxFactory.MethodDeclaration(attributeLists, modifiers, returnType, explicitInterfaceSpecifier, identifier, typeParameterList, parameterList, constraintClauses, body, semicolonToken);

        public NameColonSyntax NameColon(IdentifierNameSyntax name)
            => SyntaxFactory.NameColon(name);

        public NameColonSyntax NameColon(string name)
            => SyntaxFactory.NameColon(name);

        public ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression(SyntaxToken asyncKeyword, ParameterListSyntax parameterList, SyntaxToken arrowToken, BlockSyntax block, ExpressionSyntax expressionBody)
            => SyntaxFactory.ParenthesizedLambdaExpression(asyncKeyword, parameterList, arrowToken, block, expressionBody);

        public ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression(ParameterListSyntax parameterList, BlockSyntax block, ExpressionSyntax expressionBody)
            => SyntaxFactory.ParenthesizedLambdaExpression(parameterList, block, expressionBody);

        public ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression(SyntaxTokenList modifiers, ParameterListSyntax parameterList, SyntaxToken arrowToken, BlockSyntax block, ExpressionSyntax expressionBody)
            => SyntaxFactory.ParenthesizedLambdaExpression(modifiers, parameterList, arrowToken, block, expressionBody);

        public ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression(SyntaxTokenList modifiers, ParameterListSyntax parameterList, BlockSyntax block, ExpressionSyntax expressionBody)
            => SyntaxFactory.ParenthesizedLambdaExpression(modifiers, parameterList, block, expressionBody);

        public ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, ParameterListSyntax parameterList, BlockSyntax block, ExpressionSyntax expressionBody)
            => SyntaxFactory.ParenthesizedLambdaExpression(attributeLists, modifiers, parameterList, block, expressionBody);

        public AccessorDeclarationSyntax AccessorDeclaration(SyntaxKind kind, BlockSyntax body)
            => SyntaxFactory.AccessorDeclaration(kind, body);

        public RecordDeclarationSyntax RecordDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
            => SyntaxFactory.RecordDeclaration(attributeLists, modifiers, keyword, identifier, typeParameterList, parameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);

        public RecordDeclarationSyntax RecordDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxList<MemberDeclarationSyntax> members)
            => SyntaxFactory.RecordDeclaration(attributeLists, modifiers, keyword, identifier, typeParameterList, parameterList, baseList, constraintClauses, members);

        public RecordDeclarationSyntax RecordDeclaration(SyntaxToken keyword, string identifier)
            => SyntaxFactory.RecordDeclaration(keyword, identifier);

        public RecordDeclarationSyntax RecordDeclaration(SyntaxToken keyword, SyntaxToken identifier)
            => SyntaxFactory.RecordDeclaration(keyword, identifier);

        public RefTypeSyntax RefType(SyntaxToken refKeyword, TypeSyntax type)
            => SyntaxFactory.RefType(refKeyword, type);

        public ReturnStatementSyntax ReturnStatement(SyntaxToken returnKeyword, ExpressionSyntax expression, SyntaxToken semicolonToken)
            => SyntaxFactory.ReturnStatement(returnKeyword, expression, semicolonToken);

        public SimpleLambdaExpressionSyntax SimpleLambdaExpression(SyntaxToken asyncKeyword, ParameterSyntax parameter, SyntaxToken arrowToken, BlockSyntax block, ExpressionSyntax expressionBody)
            => SyntaxFactory.SimpleLambdaExpression(asyncKeyword, parameter, arrowToken, block, expressionBody);

        public SimpleLambdaExpressionSyntax SimpleLambdaExpression(ParameterSyntax parameter, BlockSyntax block, ExpressionSyntax expressionBody)
            => SyntaxFactory.SimpleLambdaExpression(parameter, block, expressionBody);

        public SimpleLambdaExpressionSyntax SimpleLambdaExpression(SyntaxTokenList modifiers, ParameterSyntax parameter, SyntaxToken arrowToken, BlockSyntax block, ExpressionSyntax expressionBody)
            => SyntaxFactory.SimpleLambdaExpression(modifiers, parameter, arrowToken, block, expressionBody);

        public SimpleLambdaExpressionSyntax SimpleLambdaExpression(SyntaxTokenList modifiers, ParameterSyntax parameter, BlockSyntax block, ExpressionSyntax expressionBody)
            => SyntaxFactory.SimpleLambdaExpression(modifiers, parameter, block, expressionBody);

        public StackAllocArrayCreationExpressionSyntax StackAllocArrayCreationExpression(SyntaxToken stackAllocKeyword, TypeSyntax type)
            => SyntaxFactory.StackAllocArrayCreationExpression(stackAllocKeyword, type);

        public SubpatternSyntax Subpattern(NameColonSyntax nameColon, PatternSyntax pattern)
            => SyntaxFactory.Subpattern(nameColon, pattern);

        public SwitchStatementSyntax SwitchStatement(SyntaxToken switchKeyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, SyntaxToken openBraceToken, SyntaxList<SwitchSectionSyntax> sections, SyntaxToken closeBraceToken)
            => SyntaxFactory.SwitchStatement(switchKeyword, openParenToken, expression, closeParenToken, openBraceToken, sections, closeBraceToken);

        public SyntaxTrivia EndOfLine(string text)
            => SyntaxFactory.EndOfLine(text);

        public SyntaxTrivia ElasticEndOfLine(string text)
            => SyntaxFactory.ElasticEndOfLine(text);

        public SyntaxTrivia Whitespace(string text)
            => SyntaxFactory.Whitespace(text);

        public SyntaxTrivia ElasticWhitespace(string text)
            => SyntaxFactory.ElasticWhitespace(text);

        public SyntaxTrivia Comment(string text)
            => SyntaxFactory.Comment(text);

        public SyntaxTrivia DisabledText(string text)
            => SyntaxFactory.DisabledText(text);

        public SyntaxTrivia PreprocessingMessage(string text)
            => SyntaxFactory.PreprocessingMessage(text);

        public SyntaxTrivia SyntaxTrivia(SyntaxKind kind, string text)
            => SyntaxFactory.SyntaxTrivia(kind, text);

        public SyntaxToken Token(SyntaxKind kind)
            => SyntaxFactory.Token(kind);

        public SyntaxToken Token(SyntaxTriviaList leading, SyntaxKind kind, SyntaxTriviaList trailing)
            => SyntaxFactory.Token(leading, kind, trailing);

        public SyntaxToken Token(SyntaxTriviaList leading, SyntaxKind kind, string text, string valueText, SyntaxTriviaList trailing)
            => SyntaxFactory.Token(leading, kind, text, valueText, trailing);

        public SyntaxToken MissingToken(SyntaxKind kind)
            => SyntaxFactory.MissingToken(kind);

        public SyntaxToken MissingToken(SyntaxTriviaList leading, SyntaxKind kind, SyntaxTriviaList trailing)
            => SyntaxFactory.MissingToken(leading, kind, trailing);

        public SyntaxToken Identifier(string text)
            => SyntaxFactory.Identifier(text);

        public SyntaxToken Identifier(SyntaxTriviaList leading, string text, SyntaxTriviaList trailing)
            => SyntaxFactory.Identifier(leading, text, trailing);

        public SyntaxToken VerbatimIdentifier(SyntaxTriviaList leading, string text, string valueText, SyntaxTriviaList trailing)
            => SyntaxFactory.VerbatimIdentifier(leading, text, valueText, trailing);

        public SyntaxToken Identifier(SyntaxTriviaList leading, SyntaxKind contextualKind, string text, string valueText, SyntaxTriviaList trailing)
            => SyntaxFactory.Identifier(leading, contextualKind, text, valueText, trailing);

        public SyntaxToken Literal(int value)
            => SyntaxFactory.Literal(value);

        public SyntaxToken Literal(string text, int value)
            => SyntaxFactory.Literal(text, value);

        public SyntaxToken Literal(SyntaxTriviaList leading, string text, int value, SyntaxTriviaList trailing)
            => SyntaxFactory.Literal(leading, text, value, trailing);

        public SyntaxToken Literal(uint value)
            => SyntaxFactory.Literal(value);

        public SyntaxToken Literal(string text, uint value)
            => SyntaxFactory.Literal(text, value);

        public SyntaxToken Literal(SyntaxTriviaList leading, string text, uint value, SyntaxTriviaList trailing)
            => SyntaxFactory.Literal(leading, text, value, trailing);

        public SyntaxToken Literal(long value)
            => SyntaxFactory.Literal(value);

        public SyntaxToken Literal(string text, long value)
            => SyntaxFactory.Literal(text, value);

        public SyntaxToken Literal(SyntaxTriviaList leading, string text, long value, SyntaxTriviaList trailing)
            => SyntaxFactory.Literal(leading, text, value, trailing);

        public SyntaxToken Literal(ulong value)
            => SyntaxFactory.Literal(value);

        public SyntaxToken Literal(string text, ulong value)
            => SyntaxFactory.Literal(text, value);

        public SyntaxToken Literal(SyntaxTriviaList leading, string text, ulong value, SyntaxTriviaList trailing)
            => SyntaxFactory.Literal(leading, text, value, trailing);

        public SyntaxToken Literal(float value)
            => SyntaxFactory.Literal(value);

        public SyntaxToken Literal(string text, float value)
            => SyntaxFactory.Literal(text, value);

        public SyntaxToken Literal(SyntaxTriviaList leading, string text, float value, SyntaxTriviaList trailing)
            => SyntaxFactory.Literal(leading, text, value, trailing);

        public SyntaxToken Literal(double value)
            => SyntaxFactory.Literal(value);

        public SyntaxToken Literal(string text, double value)
            => SyntaxFactory.Literal(text, value);

        public SyntaxToken Literal(SyntaxTriviaList leading, string text, double value, SyntaxTriviaList trailing)
            => SyntaxFactory.Literal(leading, text, value, trailing);

        public SyntaxToken Literal(decimal value)
            => SyntaxFactory.Literal(value);

        public SyntaxToken Literal(string text, decimal value)
            => SyntaxFactory.Literal(text, value);

        public SyntaxToken Literal(SyntaxTriviaList leading, string text, decimal value, SyntaxTriviaList trailing)
            => SyntaxFactory.Literal(leading, text, value, trailing);

        public SyntaxToken Literal(string value)
            => SyntaxFactory.Literal(value);

        public SyntaxToken Literal(string text, string value)
            => SyntaxFactory.Literal(text, value);

        public SyntaxToken Literal(SyntaxTriviaList leading, string text, string value, SyntaxTriviaList trailing)
            => SyntaxFactory.Literal(leading, text, value, trailing);

        public SyntaxToken Literal(char value)
            => SyntaxFactory.Literal(value);

        public SyntaxToken Literal(string text, char value)
            => SyntaxFactory.Literal(text, value);

        public SyntaxToken Literal(SyntaxTriviaList leading, string text, char value, SyntaxTriviaList trailing)
            => SyntaxFactory.Literal(leading, text, value, trailing);

        public SyntaxToken BadToken(SyntaxTriviaList leading, string text, SyntaxTriviaList trailing)
            => SyntaxFactory.BadToken(leading, text, trailing);

        public SyntaxToken XmlTextLiteral(SyntaxTriviaList leading, string text, string value, SyntaxTriviaList trailing)
            => SyntaxFactory.XmlTextLiteral(leading, text, value, trailing);

        public SyntaxToken XmlEntity(SyntaxTriviaList leading, string text, string value, SyntaxTriviaList trailing)
            => SyntaxFactory.XmlEntity(leading, text, value, trailing);

        public DocumentationCommentTriviaSyntax DocumentationComment(XmlNodeSyntax[] content)
            => SyntaxFactory.DocumentationComment(content);

        public XmlElementSyntax XmlSummaryElement(XmlNodeSyntax[] content)
            => SyntaxFactory.XmlSummaryElement(content);

        public XmlElementSyntax XmlSummaryElement(SyntaxList<XmlNodeSyntax> content)
            => SyntaxFactory.XmlSummaryElement(content);

        public XmlEmptyElementSyntax XmlSeeElement(CrefSyntax cref)
            => SyntaxFactory.XmlSeeElement(cref);

        public XmlEmptyElementSyntax XmlSeeAlsoElement(CrefSyntax cref)
            => SyntaxFactory.XmlSeeAlsoElement(cref);

        public XmlElementSyntax XmlSeeAlsoElement(Uri linkAddress, SyntaxList<XmlNodeSyntax> linkText)
            => SyntaxFactory.XmlSeeAlsoElement(linkAddress, linkText);

        public XmlEmptyElementSyntax XmlThreadSafetyElement()
            => SyntaxFactory.XmlThreadSafetyElement();

        public XmlEmptyElementSyntax XmlThreadSafetyElement(bool isStatic, bool isInstance)
            => SyntaxFactory.XmlThreadSafetyElement(isStatic, isInstance);

        public XmlNameAttributeSyntax XmlNameAttribute(string parameterName)
            => SyntaxFactory.XmlNameAttribute(parameterName);

        public XmlEmptyElementSyntax XmlPreliminaryElement()
            => SyntaxFactory.XmlPreliminaryElement();

        public XmlCrefAttributeSyntax XmlCrefAttribute(CrefSyntax cref)
            => SyntaxFactory.XmlCrefAttribute(cref);

        public XmlCrefAttributeSyntax XmlCrefAttribute(CrefSyntax cref, SyntaxKind quoteKind)
            => SyntaxFactory.XmlCrefAttribute(cref, quoteKind);

        public XmlElementSyntax XmlRemarksElement(XmlNodeSyntax[] content)
            => SyntaxFactory.XmlRemarksElement(content);

        public XmlElementSyntax XmlRemarksElement(SyntaxList<XmlNodeSyntax> content)
            => SyntaxFactory.XmlRemarksElement(content);

        public XmlElementSyntax XmlReturnsElement(XmlNodeSyntax[] content)
            => SyntaxFactory.XmlReturnsElement(content);

        public XmlElementSyntax XmlReturnsElement(SyntaxList<XmlNodeSyntax> content)
            => SyntaxFactory.XmlReturnsElement(content);

        public XmlElementSyntax XmlValueElement(XmlNodeSyntax[] content)
            => SyntaxFactory.XmlValueElement(content);

        public XmlElementSyntax XmlValueElement(SyntaxList<XmlNodeSyntax> content)
            => SyntaxFactory.XmlValueElement(content);

        public XmlElementSyntax XmlExceptionElement(CrefSyntax cref, XmlNodeSyntax[] content)
            => SyntaxFactory.XmlExceptionElement(cref, content);

        public XmlElementSyntax XmlExceptionElement(CrefSyntax cref, SyntaxList<XmlNodeSyntax> content)
            => SyntaxFactory.XmlExceptionElement(cref, content);

        public XmlElementSyntax XmlPermissionElement(CrefSyntax cref, XmlNodeSyntax[] content)
            => SyntaxFactory.XmlPermissionElement(cref, content);

        public XmlElementSyntax XmlPermissionElement(CrefSyntax cref, SyntaxList<XmlNodeSyntax> content)
            => SyntaxFactory.XmlPermissionElement(cref, content);

        public XmlElementSyntax XmlExampleElement(XmlNodeSyntax[] content)
            => SyntaxFactory.XmlExampleElement(content);

        public XmlElementSyntax XmlExampleElement(SyntaxList<XmlNodeSyntax> content)
            => SyntaxFactory.XmlExampleElement(content);

        public XmlElementSyntax XmlParaElement(XmlNodeSyntax[] content)
            => SyntaxFactory.XmlParaElement(content);

        public XmlElementSyntax XmlParaElement(SyntaxList<XmlNodeSyntax> content)
            => SyntaxFactory.XmlParaElement(content);

        public XmlElementSyntax XmlParamElement(string parameterName, XmlNodeSyntax[] content)
            => SyntaxFactory.XmlParamElement(parameterName, content);

        public XmlElementSyntax XmlParamElement(string parameterName, SyntaxList<XmlNodeSyntax> content)
            => SyntaxFactory.XmlParamElement(parameterName, content);

        public XmlEmptyElementSyntax XmlParamRefElement(string parameterName)
            => SyntaxFactory.XmlParamRefElement(parameterName);

        public XmlEmptyElementSyntax XmlNullKeywordElement()
            => SyntaxFactory.XmlNullKeywordElement();

        public XmlElementSyntax XmlPlaceholderElement(XmlNodeSyntax[] content)
            => SyntaxFactory.XmlPlaceholderElement(content);

        public XmlElementSyntax XmlPlaceholderElement(SyntaxList<XmlNodeSyntax> content)
            => SyntaxFactory.XmlPlaceholderElement(content);

        public XmlEmptyElementSyntax XmlEmptyElement(string localName)
            => SyntaxFactory.XmlEmptyElement(localName);

        public XmlElementSyntax XmlElement(string localName, SyntaxList<XmlNodeSyntax> content)
            => SyntaxFactory.XmlElement(localName, content);

        public XmlElementSyntax XmlElement(XmlNameSyntax name, SyntaxList<XmlNodeSyntax> content)
            => SyntaxFactory.XmlElement(name, content);

        public XmlTextAttributeSyntax XmlTextAttribute(string name, string value)
            => SyntaxFactory.XmlTextAttribute(name, value);

        public XmlTextAttributeSyntax XmlTextAttribute(string name, SyntaxToken[] textTokens)
            => SyntaxFactory.XmlTextAttribute(name, textTokens);

        public XmlTextAttributeSyntax XmlTextAttribute(string name, SyntaxKind quoteKind, SyntaxTokenList textTokens)
            => SyntaxFactory.XmlTextAttribute(name, quoteKind, textTokens);

        public XmlTextAttributeSyntax XmlTextAttribute(XmlNameSyntax name, SyntaxKind quoteKind, SyntaxTokenList textTokens)
            => SyntaxFactory.XmlTextAttribute(name, quoteKind, textTokens);

        public XmlElementSyntax XmlMultiLineElement(string localName, SyntaxList<XmlNodeSyntax> content)
            => SyntaxFactory.XmlMultiLineElement(localName, content);

        public XmlElementSyntax XmlMultiLineElement(XmlNameSyntax name, SyntaxList<XmlNodeSyntax> content)
            => SyntaxFactory.XmlMultiLineElement(name, content);

        public XmlTextSyntax XmlNewLine(string text)
            => SyntaxFactory.XmlNewLine(text);

        public SyntaxToken XmlTextNewLine(string text)
            => SyntaxFactory.XmlTextNewLine(text);

        public SyntaxToken XmlTextNewLine(SyntaxTriviaList leading, string text, string value, SyntaxTriviaList trailing)
            => SyntaxFactory.XmlTextNewLine(leading, text, value, trailing);

        public SyntaxToken XmlTextNewLine(string text, bool continueXmlDocumentationComment)
            => SyntaxFactory.XmlTextNewLine(text, continueXmlDocumentationComment);

        public XmlTextSyntax XmlText(string value)
            => SyntaxFactory.XmlText(value);

        public XmlTextSyntax XmlText(SyntaxToken[] textTokens)
            => SyntaxFactory.XmlText(textTokens);

        public SyntaxToken XmlTextLiteral(string value)
            => SyntaxFactory.XmlTextLiteral(value);

        public SyntaxToken XmlTextLiteral(string text, string value)
            => SyntaxFactory.XmlTextLiteral(text, value);

        public SyntaxTrivia DocumentationCommentExterior(string text)
            => SyntaxFactory.DocumentationCommentExterior(text);

        public SyntaxList<TNode> List<TNode>()
            where TNode : SyntaxNode
            => SyntaxFactory.List<TNode>();

        public SyntaxList<TNode> SingletonList<TNode>(TNode node)
            where TNode : SyntaxNode
            => SyntaxFactory.SingletonList(node);

        public SyntaxList<TNode> List<TNode>(IEnumerable<TNode> nodes)
            where TNode : SyntaxNode
            => SyntaxFactory.List(nodes);

        public SyntaxTokenList TokenList()
            => SyntaxFactory.TokenList();

        public SyntaxTokenList TokenList(SyntaxToken token)
            => SyntaxFactory.TokenList(token);

        public SyntaxTokenList TokenList(SyntaxToken[] tokens)
            => SyntaxFactory.TokenList(tokens);

        public SyntaxTokenList TokenList(IEnumerable<SyntaxToken> tokens)
            => SyntaxFactory.TokenList(tokens);

        public SyntaxTrivia Trivia(StructuredTriviaSyntax node)
            => SyntaxFactory.Trivia(node);

        public SyntaxTriviaList TriviaList()
            => SyntaxFactory.TriviaList();

        public SyntaxTriviaList TriviaList(SyntaxTrivia trivia)
            => SyntaxFactory.TriviaList(trivia);

        public SyntaxTriviaList TriviaList(SyntaxTrivia[] trivias)
            => SyntaxFactory.TriviaList(trivias);

        public SyntaxTriviaList TriviaList(IEnumerable<SyntaxTrivia> trivias)
            => SyntaxFactory.TriviaList(trivias);

        public SeparatedSyntaxList<TNode> SeparatedList<TNode>()
            where TNode : SyntaxNode
            => SyntaxFactory.SeparatedList<TNode>();

        public SeparatedSyntaxList<TNode> SingletonSeparatedList<TNode>(TNode node)
            where TNode : SyntaxNode
            => SyntaxFactory.SingletonSeparatedList(node);

        public SeparatedSyntaxList<TNode> SeparatedList<TNode>(IEnumerable<TNode> nodes)
            where TNode : SyntaxNode
            => SyntaxFactory.SeparatedList(nodes);

        public SeparatedSyntaxList<TNode> SeparatedList<TNode>(IEnumerable<TNode> nodes, SyntaxToken separator)
            where TNode : SyntaxNode
            => SeparatedList(nodes, Enumerable.Repeat(separator, nodes.Count() - 1));

        public SeparatedSyntaxList<TNode> SeparatedList<TNode>(IEnumerable<TNode> nodes, IEnumerable<SyntaxToken> separators)
            where TNode : SyntaxNode
            => SyntaxFactory.SeparatedList(nodes, separators);

        public SeparatedSyntaxList<TNode> SeparatedList<TNode>(IEnumerable<SyntaxNodeOrToken> nodesAndTokens)
            where TNode : SyntaxNode
            => SyntaxFactory.SeparatedList<TNode>(nodesAndTokens);

        public SeparatedSyntaxList<TNode> SeparatedList<TNode>(SyntaxNodeOrTokenList nodesAndTokens)
            where TNode : SyntaxNode
            => SyntaxFactory.SeparatedList<TNode>(nodesAndTokens);

        public SyntaxNodeOrTokenList NodeOrTokenList()
            => SyntaxFactory.NodeOrTokenList();

        public SyntaxNodeOrTokenList NodeOrTokenList(IEnumerable<SyntaxNodeOrToken> nodesAndTokens)
            => SyntaxFactory.NodeOrTokenList(nodesAndTokens);

        public SyntaxNodeOrTokenList NodeOrTokenList(SyntaxNodeOrToken[] nodesAndTokens)
            => SyntaxFactory.NodeOrTokenList(nodesAndTokens);

        public IdentifierNameSyntax IdentifierName(string name)
            => SyntaxFactory.IdentifierName(name);

        public SyntaxTree SyntaxTree(SyntaxNode root, ParseOptions options, string path, Encoding encoding)
            => SyntaxFactory.SyntaxTree(root, options, path, encoding);

        public SyntaxTree ParseSyntaxTree(string text, ParseOptions options, string path, Encoding encoding, CancellationToken cancellationToken)
            => SyntaxFactory.ParseSyntaxTree(text, options, path, encoding, cancellationToken);

        public SyntaxTree ParseSyntaxTree(SourceText text, ParseOptions options, string path, CancellationToken cancellationToken)
            => SyntaxFactory.ParseSyntaxTree(text, options, path, cancellationToken);

        public SyntaxTriviaList ParseLeadingTrivia(string text, int offset = 0)
            => SyntaxFactory.ParseLeadingTrivia(text, offset);

        public SyntaxTriviaList ParseTrailingTrivia(string text, int offset = 0)
            => SyntaxFactory.ParseTrailingTrivia(text, offset);

        public SyntaxToken ParseToken(string text, int offset = 0)
            => SyntaxFactory.ParseToken(text, offset);

        public IEnumerable<SyntaxToken> ParseTokens(string text, int offset = 0, int initialTokenPosition = 0, CSharpParseOptions? options = null)
            => SyntaxFactory.ParseTokens(text, offset, initialTokenPosition, options);

        public NameSyntax ParseName(string text, int offset = 0, bool consumeFullText = true)
            => SyntaxFactory.ParseName(text, offset, consumeFullText);

        public TypeSyntax ParseTypeName(string text, int offset, bool consumeFullText)
            => SyntaxFactory.ParseTypeName(text, offset, consumeFullText);

        public TypeSyntax ParseTypeName(string text, int offset = 0, ParseOptions? options = null, bool consumeFullText = true)
            => SyntaxFactory.ParseTypeName(text, offset, options, consumeFullText);

        public ExpressionSyntax ParseExpression(string text, int offset = 0, ParseOptions? options = null, bool consumeFullText = true)
            => SyntaxFactory.ParseExpression(text, offset, options, consumeFullText);

        public StatementSyntax ParseStatement(string text, int offset = 0, ParseOptions? options = null, bool consumeFullText = true)
            => SyntaxFactory.ParseStatement(text, offset, options, consumeFullText);

        public MemberDeclarationSyntax? ParseMemberDeclaration(string text, int offset = 0, ParseOptions? options = null, bool consumeFullText = true)
            => SyntaxFactory.ParseMemberDeclaration(text, offset, options, consumeFullText);

        public CompilationUnitSyntax ParseCompilationUnit(string text, int offset = 0, CSharpParseOptions? options = null)
            => SyntaxFactory.ParseCompilationUnit(text, offset, options);

        public ParameterListSyntax ParseParameterList(string text, int offset = 0, ParseOptions? options = null, bool consumeFullText = true)
            => SyntaxFactory.ParseParameterList(text, offset, options, consumeFullText);

        public BracketedParameterListSyntax ParseBracketedParameterList(string text, int offset = 0, ParseOptions? options = null, bool consumeFullText = true)
            => SyntaxFactory.ParseBracketedParameterList(text, offset, options, consumeFullText);

        public ArgumentListSyntax ParseArgumentList(string text, int offset = 0, ParseOptions? options = null, bool consumeFullText = true)
            => SyntaxFactory.ParseArgumentList(text, offset, options, consumeFullText);

        public BracketedArgumentListSyntax ParseBracketedArgumentList(string text, int offset = 0, ParseOptions? options = null, bool consumeFullText = true)
            => SyntaxFactory.ParseBracketedArgumentList(text, offset, options, consumeFullText);

        public AttributeArgumentListSyntax? ParseAttributeArgumentList(string text, int offset = 0, ParseOptions? options = null, bool consumeFullText = true)
            => SyntaxFactory.ParseAttributeArgumentList(text, offset, options, consumeFullText);

        public bool AreEquivalent(SyntaxTree oldTree, SyntaxTree newTree, bool topLevel)
            => SyntaxFactory.AreEquivalent(oldTree, newTree, topLevel);

        public bool AreEquivalent(SyntaxNode oldNode, SyntaxNode newNode, bool topLevel)
            => SyntaxFactory.AreEquivalent(oldNode, newNode, topLevel);

        public bool AreEquivalent(SyntaxNode oldNode, SyntaxNode newNode, Func<SyntaxKind, bool> ignoreChildNode)
            => SyntaxFactory.AreEquivalent(oldNode, newNode, ignoreChildNode);

        public bool AreEquivalent(SyntaxToken oldToken, SyntaxToken newToken)
            => SyntaxFactory.AreEquivalent(oldToken, newToken);

        public bool AreEquivalent(SyntaxTokenList oldList, SyntaxTokenList newList)
            => SyntaxFactory.AreEquivalent(oldList, newList);

        public bool AreEquivalent<TNode>(SyntaxList<TNode> oldList, SyntaxList<TNode> newList, bool topLevel)
            where TNode : CSharpSyntaxNode
            => SyntaxFactory.AreEquivalent<TNode>(oldList, newList, topLevel);

        public bool AreEquivalent<TNode>(SyntaxList<TNode> oldList, SyntaxList<TNode> newList, Func<SyntaxKind, bool> ignoreChildNode)
            where TNode : SyntaxNode
            => SyntaxFactory.AreEquivalent(oldList, newList, ignoreChildNode);

        public bool AreEquivalent<TNode>(SeparatedSyntaxList<TNode> oldList, SeparatedSyntaxList<TNode> newList, bool topLevel)
            where TNode : SyntaxNode
            => SyntaxFactory.AreEquivalent(oldList, newList, topLevel);

        public bool AreEquivalent<TNode>(SeparatedSyntaxList<TNode> oldList, SeparatedSyntaxList<TNode> newList, Func<SyntaxKind, bool> ignoreChildNode)
            where TNode : SyntaxNode
            => SyntaxFactory.AreEquivalent(oldList, newList, ignoreChildNode);

        public ExpressionSyntax GetStandaloneExpression(ExpressionSyntax expression)
            => SyntaxFactory.GetStandaloneExpression(expression);

        public ExpressionSyntax? GetNonGenericExpression(ExpressionSyntax expression)
            => SyntaxFactory.GetNonGenericExpression(expression);

        public bool IsCompleteSubmission(SyntaxTree tree)
            => SyntaxFactory.IsCompleteSubmission(tree);

        public CaseSwitchLabelSyntax CaseSwitchLabel(ExpressionSyntax value)
            => SyntaxFactory.CaseSwitchLabel(value);

        public DefaultSwitchLabelSyntax DefaultSwitchLabel()
            => SyntaxFactory.DefaultSwitchLabel();

        public BlockSyntax Block(StatementSyntax[] statements)
            => SyntaxFactory.Block(statements);

        public BlockSyntax Block(IEnumerable<StatementSyntax> statements)
            => SyntaxFactory.Block(statements);

        public PropertyDeclarationSyntax PropertyDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, AccessorListSyntax accessorList)
            => SyntaxFactory.PropertyDeclaration(attributeLists, modifiers, type, explicitInterfaceSpecifier, identifier, accessorList);

        public ConversionOperatorDeclarationSyntax ConversionOperatorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken implicitOrExplicitKeyword, SyntaxToken operatorKeyword, TypeSyntax type, ParameterListSyntax parameterList, BlockSyntax body, SyntaxToken semicolonToken)
            => SyntaxFactory.ConversionOperatorDeclaration(attributeLists, modifiers, implicitOrExplicitKeyword, operatorKeyword, type, parameterList, body, semicolonToken);

        public ConversionOperatorDeclarationSyntax ConversionOperatorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken implicitOrExplicitKeyword, SyntaxToken operatorKeyword, TypeSyntax type, ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
            => SyntaxFactory.ConversionOperatorDeclaration(attributeLists, modifiers, implicitOrExplicitKeyword, operatorKeyword, type, parameterList, body, expressionBody, semicolonToken);

        public ConversionOperatorDeclarationSyntax ConversionOperatorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken implicitOrExplicitKeyword, TypeSyntax type, ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody)
            => SyntaxFactory.ConversionOperatorDeclaration(attributeLists, modifiers, implicitOrExplicitKeyword, type, parameterList, body, expressionBody);

        public ConversionOperatorDeclarationSyntax ConversionOperatorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken implicitOrExplicitKeyword, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken operatorKeyword, TypeSyntax type, ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
            => SyntaxFactory.ConversionOperatorDeclaration(attributeLists, modifiers, implicitOrExplicitKeyword, explicitInterfaceSpecifier, operatorKeyword, type, parameterList, body, expressionBody, semicolonToken);

        public OperatorDeclarationSyntax OperatorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, SyntaxToken operatorKeyword, SyntaxToken operatorToken, ParameterListSyntax parameterList, BlockSyntax body, SyntaxToken semicolonToken)
            => SyntaxFactory.OperatorDeclaration(attributeLists, modifiers, returnType, operatorKeyword, operatorToken, parameterList, body, semicolonToken);

        public OperatorDeclarationSyntax OperatorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, SyntaxToken operatorKeyword, SyntaxToken operatorToken, ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
            => SyntaxFactory.OperatorDeclaration(attributeLists, modifiers, returnType, operatorKeyword, operatorToken, parameterList, body, expressionBody, semicolonToken);

        public OperatorDeclarationSyntax OperatorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, SyntaxToken operatorToken, ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody)
            => SyntaxFactory.OperatorDeclaration(attributeLists, modifiers, returnType, operatorToken, parameterList, body, expressionBody);

        public OperatorDeclarationSyntax OperatorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken operatorKeyword, SyntaxToken operatorToken, ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
            => SyntaxFactory.OperatorDeclaration(attributeLists, modifiers, returnType, explicitInterfaceSpecifier, operatorKeyword, operatorToken, parameterList, body, expressionBody, semicolonToken);

        public UsingDirectiveSyntax UsingDirective(NameEqualsSyntax alias, NameSyntax name)
            => SyntaxFactory.UsingDirective(alias, name);

        public UsingDirectiveSyntax UsingDirective(SyntaxToken usingKeyword, SyntaxToken staticKeyword, NameEqualsSyntax alias, NameSyntax name, SyntaxToken semicolonToken)
            => SyntaxFactory.UsingDirective(usingKeyword, staticKeyword, alias, name, semicolonToken);

        public ClassOrStructConstraintSyntax ClassOrStructConstraint(SyntaxKind kind, SyntaxToken classOrStructKeyword)
            => SyntaxFactory.ClassOrStructConstraint(kind, classOrStructKeyword);

        public AccessorDeclarationSyntax AccessorDeclaration(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, BlockSyntax body)
            => SyntaxFactory.AccessorDeclaration(kind, attributeLists, modifiers, body);

        public AccessorDeclarationSyntax AccessorDeclaration(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, BlockSyntax body, SyntaxToken semicolonToken)
            => SyntaxFactory.AccessorDeclaration(kind, attributeLists, modifiers, keyword, body, semicolonToken);

        public AccessorDeclarationSyntax AccessorDeclaration(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, ArrowExpressionClauseSyntax expressionBody)
            => SyntaxFactory.AccessorDeclaration(kind, attributeLists, modifiers, expressionBody);

        public AccessorDeclarationSyntax AccessorDeclaration(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
            => SyntaxFactory.AccessorDeclaration(kind, attributeLists, modifiers, keyword, expressionBody, semicolonToken);

        public EnumMemberDeclarationSyntax EnumMemberDeclaration(string identifier)
            => SyntaxFactory.EnumMemberDeclaration(identifier);

        public EnumMemberDeclarationSyntax EnumMemberDeclaration(string identifier, EqualsValueClauseSyntax equalsValue)
            => SyntaxFactory.EnumMemberDeclaration(List<AttributeListSyntax>(), Identifier(identifier), equalsValue);

        public EnumMemberDeclarationSyntax EnumMemberDeclaration(SyntaxToken identifier)
            => SyntaxFactory.EnumMemberDeclaration(identifier);

        public EnumMemberDeclarationSyntax EnumMemberDeclaration(SyntaxToken identifier, EqualsValueClauseSyntax equalsValue)
            => SyntaxFactory.EnumMemberDeclaration(List<AttributeListSyntax>(), identifier, equalsValue);

        public EnumMemberDeclarationSyntax EnumMemberDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken identifier, EqualsValueClauseSyntax equalsValue)
            => SyntaxFactory.EnumMemberDeclaration(attributeLists, identifier, equalsValue);

        public EnumMemberDeclarationSyntax EnumMemberDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, EqualsValueClauseSyntax equalsValue)
             => SyntaxFactory.EnumMemberDeclaration(attributeLists, modifiers, identifier, equalsValue);

        public NamespaceDeclarationSyntax NamespaceDeclaration(NameSyntax name, SyntaxList<ExternAliasDirectiveSyntax> externs, SyntaxList<UsingDirectiveSyntax> usings, SyntaxList<MemberDeclarationSyntax> members)
            => SyntaxFactory.NamespaceDeclaration(name, externs, usings, members);

        public NamespaceDeclarationSyntax NamespaceDeclaration(SyntaxToken namespaceKeyword, NameSyntax name, SyntaxToken openBraceToken, SyntaxList<ExternAliasDirectiveSyntax> externs, SyntaxList<UsingDirectiveSyntax> usings, SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
            => SyntaxFactory.NamespaceDeclaration(namespaceKeyword, name, openBraceToken, externs, usings, members, closeBraceToken, semicolonToken);

        public EventDeclarationSyntax EventDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken eventKeyword, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, AccessorListSyntax accessorList)
            => SyntaxFactory.EventDeclaration(attributeLists, modifiers, eventKeyword, type, explicitInterfaceSpecifier, identifier, accessorList);

        public EventDeclarationSyntax EventDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken eventKeyword, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, SyntaxToken semicolonToken)
            => SyntaxFactory.EventDeclaration(attributeLists, modifiers, eventKeyword, type, explicitInterfaceSpecifier, identifier, semicolonToken);

        public SwitchStatementSyntax SwitchStatement(ExpressionSyntax expression, SyntaxList<SwitchSectionSyntax> sections)
            => SyntaxFactory.SwitchStatement(expression, sections);

        public SwitchStatementSyntax SwitchStatement(ExpressionSyntax expression)
            => SyntaxFactory.SwitchStatement(expression);

        public SimpleLambdaExpressionSyntax SimpleLambdaExpression(ParameterSyntax parameter, CSharpSyntaxNode body)
            => SyntaxFactory.SimpleLambdaExpression(parameter, body);

        public SimpleLambdaExpressionSyntax SimpleLambdaExpression(SyntaxToken asyncKeyword, ParameterSyntax parameter, SyntaxToken arrowToken, CSharpSyntaxNode body)
            => SyntaxFactory.SimpleLambdaExpression(asyncKeyword, parameter, arrowToken, body);

        public ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression(CSharpSyntaxNode body)
            => SyntaxFactory.ParenthesizedLambdaExpression(body);

        public ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression(ParameterListSyntax parameterList, CSharpSyntaxNode body)
            => SyntaxFactory.ParenthesizedLambdaExpression(parameterList, body);

        public ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression(SyntaxToken asyncKeyword, ParameterListSyntax parameterList, SyntaxToken arrowToken, CSharpSyntaxNode body)
            => SyntaxFactory.ParenthesizedLambdaExpression(asyncKeyword, parameterList, arrowToken, body);

        public AnonymousMethodExpressionSyntax AnonymousMethodExpression(CSharpSyntaxNode body)
            => SyntaxFactory.AnonymousMethodExpression(body);

        public AnonymousMethodExpressionSyntax AnonymousMethodExpression(ParameterListSyntax parameterList, CSharpSyntaxNode body)
            => SyntaxFactory.AnonymousMethodExpression(parameterList, body);

        public AnonymousMethodExpressionSyntax AnonymousMethodExpression(SyntaxToken asyncKeyword, SyntaxToken delegateKeyword, ParameterListSyntax parameterList, CSharpSyntaxNode body)
            => SyntaxFactory.AnonymousMethodExpression(asyncKeyword, delegateKeyword, parameterList, body);

        public OperatorMemberCrefSyntax OperatorMemberCref(SyntaxToken operatorKeyword, SyntaxToken operatorToken, CrefParameterListSyntax parameters)
            => SyntaxFactory.OperatorMemberCref(operatorKeyword, operatorToken, parameters);

        public ConversionOperatorMemberCrefSyntax ConversionOperatorMemberCref(SyntaxToken implicitOrExplicitKeyword, SyntaxToken operatorKeyword, TypeSyntax type, CrefParameterListSyntax parameters)
            => SyntaxFactory.ConversionOperatorMemberCref(implicitOrExplicitKeyword, operatorKeyword, type, parameters);

        public ClassDeclarationSyntax ClassDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
            => SyntaxFactory.ClassDeclaration(attributeLists, modifiers, keyword, identifier, typeParameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);

        public ClassDeclarationSyntax ClassDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxList<MemberDeclarationSyntax> members)
            => SyntaxFactory.ClassDeclaration(attributeLists, modifiers, identifier, typeParameterList, baseList, constraintClauses, members);

        public ClassDeclarationSyntax ClassDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxList<MemberDeclarationSyntax> members)
            => SyntaxFactory.ClassDeclaration(attributeLists, modifiers, identifier, typeParameterList, parameterList, baseList, constraintClauses, members);

        public ClassDeclarationSyntax ClassDeclaration(SyntaxToken identifier)
            => SyntaxFactory.ClassDeclaration(identifier);

        public ClassDeclarationSyntax ClassDeclaration(string identifier)
            => SyntaxFactory.ClassDeclaration(identifier);

        public StructDeclarationSyntax StructDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
            => SyntaxFactory.StructDeclaration(attributeLists, modifiers, keyword, identifier, typeParameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);

        public StructDeclarationSyntax StructDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxList<MemberDeclarationSyntax> members)
            => SyntaxFactory.StructDeclaration(attributeLists, modifiers, identifier, typeParameterList, baseList, constraintClauses, members);

        public StructDeclarationSyntax StructDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxList<MemberDeclarationSyntax> members)
            => SyntaxFactory.StructDeclaration(attributeLists, modifiers, identifier, typeParameterList, parameterList, baseList, constraintClauses, members);

        public StructDeclarationSyntax StructDeclaration(SyntaxToken identifier)
            => SyntaxFactory.StructDeclaration(identifier);

        public StructDeclarationSyntax StructDeclaration(string identifier)
            => SyntaxFactory.StructDeclaration(identifier);

        public InterfaceDeclarationSyntax InterfaceDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
            => SyntaxFactory.InterfaceDeclaration(attributeLists, modifiers, keyword, identifier, typeParameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);

        public InterfaceDeclarationSyntax InterfaceDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxList<MemberDeclarationSyntax> members)
            => SyntaxFactory.InterfaceDeclaration(attributeLists, modifiers, identifier, typeParameterList, baseList, constraintClauses, members);

        public InterfaceDeclarationSyntax InterfaceDeclaration(SyntaxToken identifier)
            => SyntaxFactory.InterfaceDeclaration(identifier);

        public InterfaceDeclarationSyntax InterfaceDeclaration(string identifier)
            => SyntaxFactory.InterfaceDeclaration(identifier);

        public EnumDeclarationSyntax EnumDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, BaseListSyntax baseList, SeparatedSyntaxList<EnumMemberDeclarationSyntax> members)
            => SyntaxFactory.EnumDeclaration(attributeLists, modifiers, identifier, baseList, members);

        public EnumDeclarationSyntax EnumDeclaration(SyntaxToken identifier)
            => SyntaxFactory.EnumDeclaration(identifier);

        public EnumDeclarationSyntax EnumDeclaration(string identifier)
            => SyntaxFactory.EnumDeclaration(identifier);

        public ThrowStatementSyntax ThrowStatement(SyntaxToken throwKeyword, ExpressionSyntax expression, SyntaxToken semicolonToken)
            => SyntaxFactory.ThrowStatement(throwKeyword, expression, semicolonToken);

        public TryStatementSyntax TryStatement(BlockSyntax block, SyntaxList<CatchClauseSyntax> catches, FinallyClauseSyntax @finally)
            => SyntaxFactory.TryStatement(block, catches, @finally);

        public TryStatementSyntax TryStatement(SyntaxToken tryKeyword, BlockSyntax block, SyntaxList<CatchClauseSyntax> catches, FinallyClauseSyntax @finally)
            => SyntaxFactory.TryStatement(tryKeyword, block, catches, @finally);

        public TypeDeclarationSyntax TypeDeclaration(SyntaxKind kind, SyntaxToken identifier)
            => SyntaxFactory.TypeDeclaration(kind, identifier);

        public TypeDeclarationSyntax TypeDeclaration(SyntaxKind kind, string identifier)
            => SyntaxFactory.TypeDeclaration(kind, identifier);

        public TypeDeclarationSyntax TypeDeclaration(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributes, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
            => SyntaxFactory.TypeDeclaration(kind, attributes, modifiers, keyword, identifier, typeParameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);

        public UnsafeStatementSyntax UnsafeStatement(SyntaxToken unsafeKeyword, BlockSyntax block)
            => SyntaxFactory.UnsafeStatement(unsafeKeyword, block);

        public UsingDirectiveSyntax UsingDirective(SyntaxToken staticKeyword, NameEqualsSyntax alias, NameSyntax name)
            => SyntaxFactory.UsingDirective(staticKeyword, alias, name);

        public UsingDirectiveSyntax UsingDirective(SyntaxToken globalKeyword, SyntaxToken usingKeyword, SyntaxToken staticKeyword, NameEqualsSyntax alias, NameSyntax name, SyntaxToken semicolonToken)
            => SyntaxFactory.UsingDirective(globalKeyword, usingKeyword, staticKeyword, alias, name, semicolonToken);

        public UsingDirectiveSyntax UsingDirective(NameSyntax name)
            => SyntaxFactory.UsingDirective(name);

        public UsingStatementSyntax UsingStatement(SyntaxToken usingKeyword, SyntaxToken openParenToken, VariableDeclarationSyntax declaration, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
            => SyntaxFactory.UsingStatement(usingKeyword, openParenToken, declaration, expression, closeParenToken, statement);

        public UsingStatementSyntax UsingStatement(SyntaxToken awaitKeyword, SyntaxToken usingKeyword, SyntaxToken openParenToken, VariableDeclarationSyntax declaration, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
            => SyntaxFactory.UsingStatement(awaitKeyword, usingKeyword, openParenToken, declaration, expression, closeParenToken, statement);

        public UsingStatementSyntax UsingStatement(VariableDeclarationSyntax declaration, ExpressionSyntax expression, StatementSyntax statement)
            => SyntaxFactory.UsingStatement(declaration, expression, statement);

        public WhileStatementSyntax WhileStatement(SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement)
            => SyntaxFactory.WhileStatement(whileKeyword, openParenToken, condition, closeParenToken, statement);

        public YieldStatementSyntax YieldStatement(SyntaxKind kind, SyntaxToken yieldKeyword, SyntaxToken returnOrBreakKeyword, ExpressionSyntax expression, SyntaxToken semicolonToken)
            => SyntaxFactory.YieldStatement(kind, yieldKeyword, returnOrBreakKeyword, expression, semicolonToken);

        public IdentifierNameSyntax IdentifierName(SyntaxToken identifier)
            => SyntaxFactory.IdentifierName(identifier);

        public QualifiedNameSyntax QualifiedName(NameSyntax left, SyntaxToken dotToken, SimpleNameSyntax right)
            => SyntaxFactory.QualifiedName(left, dotToken, right);

        public QualifiedNameSyntax QualifiedName(NameSyntax left, SimpleNameSyntax right)
            => SyntaxFactory.QualifiedName(left, right);

        public GenericNameSyntax GenericName(SyntaxToken identifier, TypeArgumentListSyntax typeArgumentList)
            => SyntaxFactory.GenericName(identifier, typeArgumentList);

        public GenericNameSyntax GenericName(SyntaxToken identifier)
            => SyntaxFactory.GenericName(identifier);

        public GenericNameSyntax GenericName(string identifier)
            => SyntaxFactory.GenericName(identifier);

        public TypeArgumentListSyntax TypeArgumentList(SyntaxToken lessThanToken, SeparatedSyntaxList<TypeSyntax> arguments, SyntaxToken greaterThanToken)
            => SyntaxFactory.TypeArgumentList(lessThanToken, arguments, greaterThanToken);

        public TypeArgumentListSyntax TypeArgumentList(SeparatedSyntaxList<TypeSyntax> arguments)
            => SyntaxFactory.TypeArgumentList(arguments);

        public AliasQualifiedNameSyntax AliasQualifiedName(IdentifierNameSyntax alias, SyntaxToken colonColonToken, SimpleNameSyntax name)
            => SyntaxFactory.AliasQualifiedName(alias, colonColonToken, name);

        public AliasQualifiedNameSyntax AliasQualifiedName(IdentifierNameSyntax alias, SimpleNameSyntax name)
            => SyntaxFactory.AliasQualifiedName(alias, name);

        public AliasQualifiedNameSyntax AliasQualifiedName(string alias, SimpleNameSyntax name)
            => SyntaxFactory.AliasQualifiedName(alias, name);

        public PredefinedTypeSyntax PredefinedType(SyntaxToken keyword)
            => SyntaxFactory.PredefinedType(keyword);

        public ArrayTypeSyntax ArrayType(TypeSyntax elementType, SyntaxList<ArrayRankSpecifierSyntax> rankSpecifiers)
            => SyntaxFactory.ArrayType(elementType, rankSpecifiers);

        public ArrayTypeSyntax ArrayType(TypeSyntax elementType)
            => SyntaxFactory.ArrayType(elementType);

        public ArrayRankSpecifierSyntax ArrayRankSpecifier(SyntaxToken openBracketToken, SeparatedSyntaxList<ExpressionSyntax> sizes, SyntaxToken closeBracketToken)
            => SyntaxFactory.ArrayRankSpecifier(openBracketToken, sizes, closeBracketToken);

        public ArrayRankSpecifierSyntax ArrayRankSpecifier(SeparatedSyntaxList<ExpressionSyntax> sizes)
            => SyntaxFactory.ArrayRankSpecifier(sizes);

        public PointerTypeSyntax PointerType(TypeSyntax elementType, SyntaxToken asteriskToken)
            => SyntaxFactory.PointerType(elementType, asteriskToken);

        public PointerTypeSyntax PointerType(TypeSyntax elementType)
            => SyntaxFactory.PointerType(elementType);

        public FunctionPointerTypeSyntax FunctionPointerType(SyntaxToken delegateKeyword, SyntaxToken asteriskToken, FunctionPointerCallingConventionSyntax callingConvention, FunctionPointerParameterListSyntax parameterList)
            => SyntaxFactory.FunctionPointerType(delegateKeyword, asteriskToken, callingConvention, parameterList);

        public FunctionPointerTypeSyntax FunctionPointerType(FunctionPointerCallingConventionSyntax callingConvention, FunctionPointerParameterListSyntax parameterList)
            => SyntaxFactory.FunctionPointerType(callingConvention, parameterList);

        public FunctionPointerTypeSyntax FunctionPointerType()
            => SyntaxFactory.FunctionPointerType();

        public FunctionPointerParameterListSyntax FunctionPointerParameterList(SyntaxToken lessThanToken, SeparatedSyntaxList<FunctionPointerParameterSyntax> parameters, SyntaxToken greaterThanToken)
            => SyntaxFactory.FunctionPointerParameterList(lessThanToken, parameters, greaterThanToken);

        public FunctionPointerParameterListSyntax FunctionPointerParameterList(SeparatedSyntaxList<FunctionPointerParameterSyntax> parameters)
            => SyntaxFactory.FunctionPointerParameterList(parameters);

        public FunctionPointerCallingConventionSyntax FunctionPointerCallingConvention(SyntaxToken managedOrUnmanagedKeyword, FunctionPointerUnmanagedCallingConventionListSyntax unmanagedCallingConventionList)
            => SyntaxFactory.FunctionPointerCallingConvention(managedOrUnmanagedKeyword, unmanagedCallingConventionList);

        public FunctionPointerCallingConventionSyntax FunctionPointerCallingConvention(SyntaxToken managedOrUnmanagedKeyword)
            => SyntaxFactory.FunctionPointerCallingConvention(managedOrUnmanagedKeyword);

        public FunctionPointerUnmanagedCallingConventionListSyntax FunctionPointerUnmanagedCallingConventionList(SyntaxToken openBracketToken, SeparatedSyntaxList<FunctionPointerUnmanagedCallingConventionSyntax> callingConventions, SyntaxToken closeBracketToken)
            => SyntaxFactory.FunctionPointerUnmanagedCallingConventionList(openBracketToken, callingConventions, closeBracketToken);

        public FunctionPointerUnmanagedCallingConventionListSyntax FunctionPointerUnmanagedCallingConventionList(SeparatedSyntaxList<FunctionPointerUnmanagedCallingConventionSyntax> callingConventions)
            => SyntaxFactory.FunctionPointerUnmanagedCallingConventionList(callingConventions);

        public FunctionPointerUnmanagedCallingConventionSyntax FunctionPointerUnmanagedCallingConvention(SyntaxToken name)
            => SyntaxFactory.FunctionPointerUnmanagedCallingConvention(name);

        public NullableTypeSyntax NullableType(TypeSyntax elementType, SyntaxToken questionToken)
            => SyntaxFactory.NullableType(elementType, questionToken);

        public NullableTypeSyntax NullableType(TypeSyntax elementType)
            => SyntaxFactory.NullableType(elementType);

        public TupleTypeSyntax TupleType(SyntaxToken openParenToken, SeparatedSyntaxList<TupleElementSyntax> elements, SyntaxToken closeParenToken)
            => SyntaxFactory.TupleType(openParenToken, elements, closeParenToken);

        public TupleTypeSyntax TupleType(SeparatedSyntaxList<TupleElementSyntax> elements)
            => SyntaxFactory.TupleType(elements);

        public TupleElementSyntax TupleElement(TypeSyntax type, SyntaxToken identifier)
            => SyntaxFactory.TupleElement(type, identifier);

        public TupleElementSyntax TupleElement(TypeSyntax type)
            => SyntaxFactory.TupleElement(type);

        public OmittedTypeArgumentSyntax OmittedTypeArgument(SyntaxToken omittedTypeArgumentToken)
            => SyntaxFactory.OmittedTypeArgument(omittedTypeArgumentToken);

        public OmittedTypeArgumentSyntax OmittedTypeArgument()
            => SyntaxFactory.OmittedTypeArgument();

        public RefTypeSyntax RefType(SyntaxToken refKeyword, SyntaxToken readOnlyKeyword, TypeSyntax type)
            => SyntaxFactory.RefType(refKeyword, readOnlyKeyword, type);

        public RefTypeSyntax RefType(TypeSyntax type)
            => SyntaxFactory.RefType(type);

        public ScopedTypeSyntax ScopedType(SyntaxToken scopedKeyword, TypeSyntax type)
            => SyntaxFactory.ScopedType(scopedKeyword, type);

        public ScopedTypeSyntax ScopedType(TypeSyntax type)
            => SyntaxFactory.ScopedType(type);

        public ParenthesizedExpressionSyntax ParenthesizedExpression(SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
            => SyntaxFactory.ParenthesizedExpression(openParenToken, expression, closeParenToken);

        public ParenthesizedExpressionSyntax ParenthesizedExpression(ExpressionSyntax expression)
            => SyntaxFactory.ParenthesizedExpression(expression);

        public TupleExpressionSyntax TupleExpression(SyntaxToken openParenToken, SeparatedSyntaxList<ArgumentSyntax> arguments, SyntaxToken closeParenToken)
            => SyntaxFactory.TupleExpression(openParenToken, arguments, closeParenToken);

        public TupleExpressionSyntax TupleExpression(SeparatedSyntaxList<ArgumentSyntax> arguments)
            => SyntaxFactory.TupleExpression(arguments);

        public PrefixUnaryExpressionSyntax PrefixUnaryExpression(SyntaxKind kind, SyntaxToken operatorToken, ExpressionSyntax operand)
            => SyntaxFactory.PrefixUnaryExpression(kind, operatorToken, operand);

        public PrefixUnaryExpressionSyntax PrefixUnaryExpression(SyntaxKind kind, ExpressionSyntax operand)
            => SyntaxFactory.PrefixUnaryExpression(kind, operand);

        public AwaitExpressionSyntax AwaitExpression(SyntaxToken awaitKeyword, ExpressionSyntax expression)
            => SyntaxFactory.AwaitExpression(awaitKeyword, expression);

        public AwaitExpressionSyntax AwaitExpression(ExpressionSyntax expression)
            => SyntaxFactory.AwaitExpression(expression);

        public PostfixUnaryExpressionSyntax PostfixUnaryExpression(SyntaxKind kind, ExpressionSyntax operand, SyntaxToken operatorToken)
            => SyntaxFactory.PostfixUnaryExpression(kind, operand, operatorToken);

        public PostfixUnaryExpressionSyntax PostfixUnaryExpression(SyntaxKind kind, ExpressionSyntax operand)
            => SyntaxFactory.PostfixUnaryExpression(kind, operand);

        public MemberAccessExpressionSyntax MemberAccessExpression(SyntaxKind kind, ExpressionSyntax expression, SyntaxToken operatorToken, SimpleNameSyntax name)
            => SyntaxFactory.MemberAccessExpression(kind, expression, operatorToken, name);

        public MemberAccessExpressionSyntax MemberAccessExpression(SyntaxKind kind, ExpressionSyntax expression, SimpleNameSyntax name)
            => SyntaxFactory.MemberAccessExpression(kind, expression, name);

        public ConditionalAccessExpressionSyntax ConditionalAccessExpression(ExpressionSyntax expression, SyntaxToken operatorToken, ExpressionSyntax whenNotNull)
            => SyntaxFactory.ConditionalAccessExpression(expression, operatorToken, whenNotNull);

        public ConditionalAccessExpressionSyntax ConditionalAccessExpression(ExpressionSyntax expression, ExpressionSyntax whenNotNull)
            => SyntaxFactory.ConditionalAccessExpression(expression, whenNotNull);

        public MemberBindingExpressionSyntax MemberBindingExpression(SyntaxToken operatorToken, SimpleNameSyntax name)
            => SyntaxFactory.MemberBindingExpression(operatorToken, name);

        public MemberBindingExpressionSyntax MemberBindingExpression(SimpleNameSyntax name)
            => SyntaxFactory.MemberBindingExpression(name);

        public ElementBindingExpressionSyntax ElementBindingExpression(BracketedArgumentListSyntax argumentList)
            => SyntaxFactory.ElementBindingExpression(argumentList);

        public ElementBindingExpressionSyntax ElementBindingExpression()
            => SyntaxFactory.ElementBindingExpression();

        public RangeExpressionSyntax RangeExpression(ExpressionSyntax leftOperand, SyntaxToken operatorToken, ExpressionSyntax rightOperand)
            => SyntaxFactory.RangeExpression(leftOperand, operatorToken, rightOperand);

        public RangeExpressionSyntax RangeExpression(ExpressionSyntax leftOperand, ExpressionSyntax rightOperand)
            => SyntaxFactory.RangeExpression(leftOperand, rightOperand);

        public RangeExpressionSyntax RangeExpression()
            => SyntaxFactory.RangeExpression();

        public ImplicitElementAccessSyntax ImplicitElementAccess(BracketedArgumentListSyntax argumentList)
            => SyntaxFactory.ImplicitElementAccess(argumentList);

        public ImplicitElementAccessSyntax ImplicitElementAccess()
            => SyntaxFactory.ImplicitElementAccess();

        public BinaryExpressionSyntax BinaryExpression(SyntaxKind kind, ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
            => SyntaxFactory.BinaryExpression(kind, left, operatorToken, right);

        public BinaryExpressionSyntax BinaryExpression(SyntaxKind kind, ExpressionSyntax left, ExpressionSyntax right)
            => SyntaxFactory.BinaryExpression(kind, left, right);

        public AssignmentExpressionSyntax AssignmentExpression(SyntaxKind kind, ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
            => SyntaxFactory.AssignmentExpression(kind, left, operatorToken, right);

        public AssignmentExpressionSyntax AssignmentExpression(SyntaxKind kind, ExpressionSyntax left, ExpressionSyntax right)
            => SyntaxFactory.AssignmentExpression(kind, left, right);

        public ConditionalExpressionSyntax ConditionalExpression(ExpressionSyntax condition, SyntaxToken questionToken, ExpressionSyntax whenTrue, SyntaxToken colonToken, ExpressionSyntax whenFalse)
            => SyntaxFactory.ConditionalExpression(condition, questionToken, whenTrue, colonToken, whenFalse);

        public ConditionalExpressionSyntax ConditionalExpression(ExpressionSyntax condition, ExpressionSyntax whenTrue, ExpressionSyntax whenFalse)
            => SyntaxFactory.ConditionalExpression(condition, whenTrue, whenFalse);

        public ThisExpressionSyntax ThisExpression(SyntaxToken token)
            => SyntaxFactory.ThisExpression(token);

        public ThisExpressionSyntax ThisExpression()
            => SyntaxFactory.ThisExpression();

        public BaseExpressionSyntax BaseExpression(SyntaxToken token)
            => SyntaxFactory.BaseExpression(token);

        public BaseExpressionSyntax BaseExpression()
            => SyntaxFactory.BaseExpression();

        public LiteralExpressionSyntax LiteralExpression(SyntaxKind kind, SyntaxToken token)
            => SyntaxFactory.LiteralExpression(kind, token);

        public FieldExpressionSyntax FieldExpression(SyntaxToken token)
            => SyntaxFactory.FieldExpression(token);

        public FieldExpressionSyntax FieldExpression()
            => SyntaxFactory.FieldExpression();

        public MakeRefExpressionSyntax MakeRefExpression(SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
            => SyntaxFactory.MakeRefExpression(keyword, openParenToken, expression, closeParenToken);

        public MakeRefExpressionSyntax MakeRefExpression(ExpressionSyntax expression)
            => SyntaxFactory.MakeRefExpression(expression);

        public RefTypeExpressionSyntax RefTypeExpression(SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
            => SyntaxFactory.RefTypeExpression(keyword, openParenToken, expression, closeParenToken);

        public RefTypeExpressionSyntax RefTypeExpression(ExpressionSyntax expression)
            => SyntaxFactory.RefTypeExpression(expression);

        public RefValueExpressionSyntax RefValueExpression(SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken comma, TypeSyntax type, SyntaxToken closeParenToken)
            => SyntaxFactory.RefValueExpression(keyword, openParenToken, expression, comma, type, closeParenToken);

        public RefValueExpressionSyntax RefValueExpression(ExpressionSyntax expression, TypeSyntax type)
            => SyntaxFactory.RefValueExpression(expression, type);

        public CheckedExpressionSyntax CheckedExpression(SyntaxKind kind, SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
            => SyntaxFactory.CheckedExpression(kind, keyword, openParenToken, expression, closeParenToken);

        public CheckedExpressionSyntax CheckedExpression(SyntaxKind kind, ExpressionSyntax expression)
            => SyntaxFactory.CheckedExpression(kind, expression);

        public DefaultExpressionSyntax DefaultExpression(SyntaxToken keyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken)
            => SyntaxFactory.DefaultExpression(keyword, openParenToken, type, closeParenToken);

        public DefaultExpressionSyntax DefaultExpression(TypeSyntax type)
            => SyntaxFactory.DefaultExpression(type);

        public TypeOfExpressionSyntax TypeOfExpression(SyntaxToken keyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken)
            => SyntaxFactory.TypeOfExpression(keyword, openParenToken, type, closeParenToken);

        public TypeOfExpressionSyntax TypeOfExpression(TypeSyntax type)
            => SyntaxFactory.TypeOfExpression(type);

        public SizeOfExpressionSyntax SizeOfExpression(SyntaxToken keyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken)
            => SyntaxFactory.SizeOfExpression(keyword, openParenToken, type, closeParenToken);

        public SizeOfExpressionSyntax SizeOfExpression(TypeSyntax type)
            => SyntaxFactory.SizeOfExpression(type);

        public InvocationExpressionSyntax InvocationExpression(ExpressionSyntax expression, ArgumentListSyntax argumentList)
            => SyntaxFactory.InvocationExpression(expression, argumentList);

        public InvocationExpressionSyntax InvocationExpression(ExpressionSyntax expression)
            => SyntaxFactory.InvocationExpression(expression);

        public ElementAccessExpressionSyntax ElementAccessExpression(ExpressionSyntax expression, BracketedArgumentListSyntax argumentList)
            => SyntaxFactory.ElementAccessExpression(expression, argumentList);

        public ElementAccessExpressionSyntax ElementAccessExpression(ExpressionSyntax expression)
            => SyntaxFactory.ElementAccessExpression(expression);

        public ArgumentListSyntax ArgumentList(SyntaxToken openParenToken, SeparatedSyntaxList<ArgumentSyntax> arguments, SyntaxToken closeParenToken)
            => SyntaxFactory.ArgumentList(openParenToken, arguments, closeParenToken);

        public ArgumentListSyntax ArgumentList(SeparatedSyntaxList<ArgumentSyntax> arguments)
            => SyntaxFactory.ArgumentList(arguments);

        public BracketedArgumentListSyntax BracketedArgumentList(SyntaxToken openBracketToken, SeparatedSyntaxList<ArgumentSyntax> arguments, SyntaxToken closeBracketToken)
            => SyntaxFactory.BracketedArgumentList(openBracketToken, arguments, closeBracketToken);

        public BracketedArgumentListSyntax BracketedArgumentList(SeparatedSyntaxList<ArgumentSyntax> arguments)
            => SyntaxFactory.BracketedArgumentList(arguments);

        public ArgumentSyntax Argument(NameColonSyntax nameColon, SyntaxToken refKindKeyword, ExpressionSyntax expression)
            => SyntaxFactory.Argument(nameColon, refKindKeyword, expression);

        public ArgumentSyntax Argument(ExpressionSyntax expression)
            => SyntaxFactory.Argument(expression);

        public ExpressionColonSyntax ExpressionColon(ExpressionSyntax expression, SyntaxToken colonToken)
            => SyntaxFactory.ExpressionColon(expression, colonToken);

        public NameColonSyntax NameColon(IdentifierNameSyntax name, SyntaxToken colonToken)
            => SyntaxFactory.NameColon(name, colonToken);

        public DeclarationExpressionSyntax DeclarationExpression(TypeSyntax type, VariableDesignationSyntax designation)
            => SyntaxFactory.DeclarationExpression(type, designation);

        public CastExpressionSyntax CastExpression(SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken, ExpressionSyntax expression)
            => SyntaxFactory.CastExpression(openParenToken, type, closeParenToken, expression);

        public CastExpressionSyntax CastExpression(TypeSyntax type, ExpressionSyntax expression)
            => SyntaxFactory.CastExpression(type, expression);

        public AnonymousMethodExpressionSyntax AnonymousMethodExpression(SyntaxTokenList modifiers, SyntaxToken delegateKeyword, ParameterListSyntax parameterList, BlockSyntax block, ExpressionSyntax expressionBody)
            => SyntaxFactory.AnonymousMethodExpression(modifiers, delegateKeyword, parameterList, block, expressionBody);

        public SimpleLambdaExpressionSyntax SimpleLambdaExpression(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, ParameterSyntax parameter, SyntaxToken arrowToken, BlockSyntax block, ExpressionSyntax expressionBody)
            => SyntaxFactory.SimpleLambdaExpression(attributeLists, modifiers, parameter, arrowToken, block, expressionBody);

        public SimpleLambdaExpressionSyntax SimpleLambdaExpression(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, ParameterSyntax parameter, BlockSyntax block, ExpressionSyntax expressionBody)
            => SyntaxFactory.SimpleLambdaExpression(attributeLists, modifiers, parameter, block, expressionBody);

        public SimpleLambdaExpressionSyntax SimpleLambdaExpression(ParameterSyntax parameter)
            => SyntaxFactory.SimpleLambdaExpression(parameter);

        public RefExpressionSyntax RefExpression(SyntaxToken refKeyword, ExpressionSyntax expression)
            => SyntaxFactory.RefExpression(refKeyword, expression);

        public RefExpressionSyntax RefExpression(ExpressionSyntax expression)
            => SyntaxFactory.RefExpression(expression);

        public ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, ParameterListSyntax parameterList, SyntaxToken arrowToken, BlockSyntax block, ExpressionSyntax expressionBody)
            => SyntaxFactory.ParenthesizedLambdaExpression(attributeLists, modifiers, returnType, parameterList, arrowToken, block, expressionBody);

        public ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, ParameterListSyntax parameterList, BlockSyntax block, ExpressionSyntax expressionBody)
            => SyntaxFactory.ParenthesizedLambdaExpression(attributeLists, modifiers, returnType, parameterList, block, expressionBody);

        public ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression()
            => SyntaxFactory.ParenthesizedLambdaExpression();

        public InitializerExpressionSyntax InitializerExpression(SyntaxKind kind, SyntaxToken openBraceToken, SeparatedSyntaxList<ExpressionSyntax> expressions, SyntaxToken closeBraceToken)
            => SyntaxFactory.InitializerExpression(kind, openBraceToken, expressions, closeBraceToken);

        public InitializerExpressionSyntax InitializerExpression(SyntaxKind kind, SeparatedSyntaxList<ExpressionSyntax> expressions)
            => SyntaxFactory.InitializerExpression(kind, expressions);

        public ImplicitObjectCreationExpressionSyntax ImplicitObjectCreationExpression(SyntaxToken newKeyword, ArgumentListSyntax argumentList, InitializerExpressionSyntax initializer)
            => SyntaxFactory.ImplicitObjectCreationExpression(newKeyword, argumentList, initializer);

        public ImplicitObjectCreationExpressionSyntax ImplicitObjectCreationExpression(ArgumentListSyntax argumentList, InitializerExpressionSyntax initializer)
            => SyntaxFactory.ImplicitObjectCreationExpression(argumentList, initializer);

        public ImplicitObjectCreationExpressionSyntax ImplicitObjectCreationExpression()
            => SyntaxFactory.ImplicitObjectCreationExpression();

        public ObjectCreationExpressionSyntax ObjectCreationExpression(SyntaxToken newKeyword, TypeSyntax type, ArgumentListSyntax argumentList, InitializerExpressionSyntax initializer)
            => SyntaxFactory.ObjectCreationExpression(newKeyword, type, argumentList, initializer);

        public ObjectCreationExpressionSyntax ObjectCreationExpression(TypeSyntax type, ArgumentListSyntax argumentList, InitializerExpressionSyntax initializer)
            => SyntaxFactory.ObjectCreationExpression(type, argumentList, initializer);

        public ObjectCreationExpressionSyntax ObjectCreationExpression(TypeSyntax type)
            => SyntaxFactory.ObjectCreationExpression(type);

        public WithExpressionSyntax WithExpression(ExpressionSyntax expression, SyntaxToken withKeyword, InitializerExpressionSyntax initializer)
            => SyntaxFactory.WithExpression(expression, withKeyword, initializer);

        public WithExpressionSyntax WithExpression(ExpressionSyntax expression, InitializerExpressionSyntax initializer)
            => SyntaxFactory.WithExpression(expression, initializer);

        public AnonymousObjectMemberDeclaratorSyntax AnonymousObjectMemberDeclarator(NameEqualsSyntax nameEquals, ExpressionSyntax expression)
            => SyntaxFactory.AnonymousObjectMemberDeclarator(nameEquals, expression);

        public AnonymousObjectMemberDeclaratorSyntax AnonymousObjectMemberDeclarator(ExpressionSyntax expression)
            => SyntaxFactory.AnonymousObjectMemberDeclarator(expression);

        public AnonymousObjectCreationExpressionSyntax AnonymousObjectCreationExpression(SyntaxToken newKeyword, SyntaxToken openBraceToken, SeparatedSyntaxList<AnonymousObjectMemberDeclaratorSyntax> initializers, SyntaxToken closeBraceToken)
            => SyntaxFactory.AnonymousObjectCreationExpression(newKeyword, openBraceToken, initializers, closeBraceToken);

        public AnonymousObjectCreationExpressionSyntax AnonymousObjectCreationExpression(SeparatedSyntaxList<AnonymousObjectMemberDeclaratorSyntax> initializers)
            => SyntaxFactory.AnonymousObjectCreationExpression(initializers);

        public ArrayCreationExpressionSyntax ArrayCreationExpression(SyntaxToken newKeyword, ArrayTypeSyntax type, InitializerExpressionSyntax initializer)
            => SyntaxFactory.ArrayCreationExpression(newKeyword, type, initializer);

        public ArrayCreationExpressionSyntax ArrayCreationExpression(ArrayTypeSyntax type, InitializerExpressionSyntax initializer)
            => SyntaxFactory.ArrayCreationExpression(type, initializer);

        public ArrayCreationExpressionSyntax ArrayCreationExpression(ArrayTypeSyntax type)
            => SyntaxFactory.ArrayCreationExpression(type);

        public ImplicitArrayCreationExpressionSyntax ImplicitArrayCreationExpression(SyntaxToken newKeyword, SyntaxToken openBracketToken, SyntaxTokenList commas, SyntaxToken closeBracketToken, InitializerExpressionSyntax initializer)
            => SyntaxFactory.ImplicitArrayCreationExpression(newKeyword, openBracketToken, commas, closeBracketToken, initializer);

        public ImplicitArrayCreationExpressionSyntax ImplicitArrayCreationExpression(SyntaxTokenList commas, InitializerExpressionSyntax initializer)
            => SyntaxFactory.ImplicitArrayCreationExpression(commas, initializer);

        public ImplicitArrayCreationExpressionSyntax ImplicitArrayCreationExpression(InitializerExpressionSyntax initializer)
            => SyntaxFactory.ImplicitArrayCreationExpression(initializer);

        public StackAllocArrayCreationExpressionSyntax StackAllocArrayCreationExpression(SyntaxToken stackAllocKeyword, TypeSyntax type, InitializerExpressionSyntax initializer)
            => SyntaxFactory.StackAllocArrayCreationExpression(stackAllocKeyword, type, initializer);

        public StackAllocArrayCreationExpressionSyntax StackAllocArrayCreationExpression(TypeSyntax type, InitializerExpressionSyntax initializer)
            => SyntaxFactory.StackAllocArrayCreationExpression(type, initializer);

        public StackAllocArrayCreationExpressionSyntax StackAllocArrayCreationExpression(TypeSyntax type)
            => SyntaxFactory.StackAllocArrayCreationExpression(type);

        public ImplicitStackAllocArrayCreationExpressionSyntax ImplicitStackAllocArrayCreationExpression(SyntaxToken stackAllocKeyword, SyntaxToken openBracketToken, SyntaxToken closeBracketToken, InitializerExpressionSyntax initializer)
            => SyntaxFactory.ImplicitStackAllocArrayCreationExpression(stackAllocKeyword, openBracketToken, closeBracketToken, initializer);

        public ImplicitStackAllocArrayCreationExpressionSyntax ImplicitStackAllocArrayCreationExpression(InitializerExpressionSyntax initializer)
            => SyntaxFactory.ImplicitStackAllocArrayCreationExpression(initializer);

        public CollectionExpressionSyntax CollectionExpression(SyntaxToken openBracketToken, SeparatedSyntaxList<CollectionElementSyntax> elements, SyntaxToken closeBracketToken)
            => SyntaxFactory.CollectionExpression(openBracketToken, elements, closeBracketToken);

        public CollectionExpressionSyntax CollectionExpression(SeparatedSyntaxList<CollectionElementSyntax> elements)
            => SyntaxFactory.CollectionExpression(elements);

        public ExpressionElementSyntax ExpressionElement(ExpressionSyntax expression)
            => SyntaxFactory.ExpressionElement(expression);

        public SpreadElementSyntax SpreadElement(SyntaxToken operatorToken, ExpressionSyntax expression)
            => SyntaxFactory.SpreadElement(operatorToken, expression);

        public SpreadElementSyntax SpreadElement(ExpressionSyntax expression)
            => SyntaxFactory.SpreadElement(expression);

        public QueryExpressionSyntax QueryExpression(FromClauseSyntax fromClause, QueryBodySyntax body)
            => SyntaxFactory.QueryExpression(fromClause, body);

        public QueryBodySyntax QueryBody(SyntaxList<QueryClauseSyntax> clauses, SelectOrGroupClauseSyntax selectOrGroup, QueryContinuationSyntax continuation)
            => SyntaxFactory.QueryBody(clauses, selectOrGroup, continuation);

        public QueryBodySyntax QueryBody(SelectOrGroupClauseSyntax selectOrGroup)
            => SyntaxFactory.QueryBody(selectOrGroup);

        public FromClauseSyntax FromClause(SyntaxToken fromKeyword, TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax expression)
            => SyntaxFactory.FromClause(fromKeyword, type, identifier, inKeyword, expression);

        public FromClauseSyntax FromClause(TypeSyntax type, SyntaxToken identifier, ExpressionSyntax expression)
            => SyntaxFactory.FromClause(type, identifier, expression);

        public FromClauseSyntax FromClause(SyntaxToken identifier, ExpressionSyntax expression)
            => SyntaxFactory.FromClause(identifier, expression);

        public FromClauseSyntax FromClause(string identifier, ExpressionSyntax expression)
            => SyntaxFactory.FromClause(identifier, expression);

        public LetClauseSyntax LetClause(SyntaxToken letKeyword, SyntaxToken identifier, SyntaxToken equalsToken, ExpressionSyntax expression)
            => SyntaxFactory.LetClause(letKeyword, identifier, equalsToken, expression);

        public LetClauseSyntax LetClause(SyntaxToken identifier, ExpressionSyntax expression)
            => SyntaxFactory.LetClause(identifier, expression);

        public LetClauseSyntax LetClause(string identifier, ExpressionSyntax expression)
            => SyntaxFactory.LetClause(identifier, expression);

        public JoinClauseSyntax JoinClause(SyntaxToken joinKeyword, TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax inExpression, SyntaxToken onKeyword, ExpressionSyntax leftExpression, SyntaxToken equalsKeyword, ExpressionSyntax rightExpression, JoinIntoClauseSyntax into)
            => SyntaxFactory.JoinClause(joinKeyword, type, identifier, inKeyword, inExpression, onKeyword, leftExpression, equalsKeyword, rightExpression, into);

        public JoinClauseSyntax JoinClause(TypeSyntax type, SyntaxToken identifier, ExpressionSyntax inExpression, ExpressionSyntax leftExpression, ExpressionSyntax rightExpression, JoinIntoClauseSyntax into)
            => SyntaxFactory.JoinClause(type, identifier, inExpression, leftExpression, rightExpression, into);

        public JoinClauseSyntax JoinClause(SyntaxToken identifier, ExpressionSyntax inExpression, ExpressionSyntax leftExpression, ExpressionSyntax rightExpression)
            => SyntaxFactory.JoinClause(identifier, inExpression, leftExpression, rightExpression);

        public JoinClauseSyntax JoinClause(string identifier, ExpressionSyntax inExpression, ExpressionSyntax leftExpression, ExpressionSyntax rightExpression)
            => SyntaxFactory.JoinClause(identifier, inExpression, leftExpression, rightExpression);

        public JoinIntoClauseSyntax JoinIntoClause(SyntaxToken intoKeyword, SyntaxToken identifier)
            => SyntaxFactory.JoinIntoClause(intoKeyword, identifier);

        public JoinIntoClauseSyntax JoinIntoClause(SyntaxToken identifier)
            => SyntaxFactory.JoinIntoClause(identifier);

        public JoinIntoClauseSyntax JoinIntoClause(string identifier)
            => SyntaxFactory.JoinIntoClause(identifier);

        public WhereClauseSyntax WhereClause(SyntaxToken whereKeyword, ExpressionSyntax condition)
            => SyntaxFactory.WhereClause(whereKeyword, condition);

        public WhereClauseSyntax WhereClause(ExpressionSyntax condition)
            => SyntaxFactory.WhereClause(condition);

        public OrderByClauseSyntax OrderByClause(SyntaxToken orderByKeyword, SeparatedSyntaxList<OrderingSyntax> orderings)
            => SyntaxFactory.OrderByClause(orderByKeyword, orderings);

        public OrderByClauseSyntax OrderByClause(SeparatedSyntaxList<OrderingSyntax> orderings)
            => SyntaxFactory.OrderByClause(orderings);

        public OrderingSyntax Ordering(SyntaxKind kind, ExpressionSyntax expression, SyntaxToken ascendingOrDescendingKeyword)
            => SyntaxFactory.Ordering(kind, expression, ascendingOrDescendingKeyword);

        public OrderingSyntax Ordering(SyntaxKind kind, ExpressionSyntax expression)
            => SyntaxFactory.Ordering(kind, expression);

        public SelectClauseSyntax SelectClause(SyntaxToken selectKeyword, ExpressionSyntax expression)
            => SyntaxFactory.SelectClause(selectKeyword, expression);

        public SelectClauseSyntax SelectClause(ExpressionSyntax expression)
            => SyntaxFactory.SelectClause(expression);

        public GroupClauseSyntax GroupClause(SyntaxToken groupKeyword, ExpressionSyntax groupExpression, SyntaxToken byKeyword, ExpressionSyntax byExpression)
            => SyntaxFactory.GroupClause(groupKeyword, groupExpression, byKeyword, byExpression);

        public GroupClauseSyntax GroupClause(ExpressionSyntax groupExpression, ExpressionSyntax byExpression)
            => SyntaxFactory.GroupClause(groupExpression, byExpression);

        public QueryContinuationSyntax QueryContinuation(SyntaxToken intoKeyword, SyntaxToken identifier, QueryBodySyntax body)
            => SyntaxFactory.QueryContinuation(intoKeyword, identifier, body);

        public QueryContinuationSyntax QueryContinuation(SyntaxToken identifier, QueryBodySyntax body)
            => SyntaxFactory.QueryContinuation(identifier, body);

        public QueryContinuationSyntax QueryContinuation(string identifier, QueryBodySyntax body)
            => SyntaxFactory.QueryContinuation(identifier, body);

        public OmittedArraySizeExpressionSyntax OmittedArraySizeExpression(SyntaxToken omittedArraySizeExpressionToken)
            => SyntaxFactory.OmittedArraySizeExpression(omittedArraySizeExpressionToken);

        public OmittedArraySizeExpressionSyntax OmittedArraySizeExpression()
            => SyntaxFactory.OmittedArraySizeExpression();

        public InterpolatedStringExpressionSyntax InterpolatedStringExpression(SyntaxToken stringStartToken, SyntaxList<InterpolatedStringContentSyntax> contents, SyntaxToken stringEndToken)
            => SyntaxFactory.InterpolatedStringExpression(stringStartToken, contents, stringEndToken);

        public InterpolatedStringExpressionSyntax InterpolatedStringExpression(SyntaxToken stringStartToken, SyntaxToken stringEndToken)
            => SyntaxFactory.InterpolatedStringExpression(stringStartToken, stringEndToken);

        public IsPatternExpressionSyntax IsPatternExpression(ExpressionSyntax expression, SyntaxToken isKeyword, PatternSyntax pattern)
            => SyntaxFactory.IsPatternExpression(expression, isKeyword, pattern);

        public IsPatternExpressionSyntax IsPatternExpression(ExpressionSyntax expression, PatternSyntax pattern)
            => SyntaxFactory.IsPatternExpression(expression, pattern);

        public ThrowExpressionSyntax ThrowExpression(SyntaxToken throwKeyword, ExpressionSyntax expression)
            => SyntaxFactory.ThrowExpression(throwKeyword, expression);

        public ThrowExpressionSyntax ThrowExpression(ExpressionSyntax expression)
            => SyntaxFactory.ThrowExpression(expression);

        public WhenClauseSyntax WhenClause(SyntaxToken whenKeyword, ExpressionSyntax condition)
            => SyntaxFactory.WhenClause(whenKeyword, condition);

        public WhenClauseSyntax WhenClause(ExpressionSyntax condition)
            => SyntaxFactory.WhenClause(condition);

        public DiscardPatternSyntax DiscardPattern(SyntaxToken underscoreToken)
            => SyntaxFactory.DiscardPattern(underscoreToken);

        public DiscardPatternSyntax DiscardPattern()
            => SyntaxFactory.DiscardPattern();

        public DeclarationPatternSyntax DeclarationPattern(TypeSyntax type, VariableDesignationSyntax designation)
            => SyntaxFactory.DeclarationPattern(type, designation);

        public VarPatternSyntax VarPattern(SyntaxToken varKeyword, VariableDesignationSyntax designation)
            => SyntaxFactory.VarPattern(varKeyword, designation);

        public VarPatternSyntax VarPattern(VariableDesignationSyntax designation)
            => SyntaxFactory.VarPattern(designation);

        public RecursivePatternSyntax RecursivePattern(TypeSyntax type, PositionalPatternClauseSyntax positionalPatternClause, PropertyPatternClauseSyntax propertyPatternClause, VariableDesignationSyntax designation)
            => SyntaxFactory.RecursivePattern(type, positionalPatternClause, propertyPatternClause, designation);

        public RecursivePatternSyntax RecursivePattern()
            => SyntaxFactory.RecursivePattern();

        public PositionalPatternClauseSyntax PositionalPatternClause(SyntaxToken openParenToken, SeparatedSyntaxList<SubpatternSyntax> subpatterns, SyntaxToken closeParenToken)
            => SyntaxFactory.PositionalPatternClause(openParenToken, subpatterns, closeParenToken);

        public PositionalPatternClauseSyntax PositionalPatternClause(SeparatedSyntaxList<SubpatternSyntax> subpatterns)
            => SyntaxFactory.PositionalPatternClause(subpatterns);

        public PropertyPatternClauseSyntax PropertyPatternClause(SyntaxToken openBraceToken, SeparatedSyntaxList<SubpatternSyntax> subpatterns, SyntaxToken closeBraceToken)
            => SyntaxFactory.PropertyPatternClause(openBraceToken, subpatterns, closeBraceToken);

        public PropertyPatternClauseSyntax PropertyPatternClause(SeparatedSyntaxList<SubpatternSyntax> subpatterns)
            => SyntaxFactory.PropertyPatternClause(subpatterns);

        public SubpatternSyntax Subpattern(BaseExpressionColonSyntax expressionColon, PatternSyntax pattern)
            => SyntaxFactory.Subpattern(expressionColon, pattern);

        public SubpatternSyntax Subpattern(PatternSyntax pattern)
            => SyntaxFactory.Subpattern(pattern);

        public ConstantPatternSyntax ConstantPattern(ExpressionSyntax expression)
            => SyntaxFactory.ConstantPattern(expression);

        public ParenthesizedPatternSyntax ParenthesizedPattern(SyntaxToken openParenToken, PatternSyntax pattern, SyntaxToken closeParenToken)
            => SyntaxFactory.ParenthesizedPattern(openParenToken, pattern, closeParenToken);

        public ParenthesizedPatternSyntax ParenthesizedPattern(PatternSyntax pattern)
            => SyntaxFactory.ParenthesizedPattern(pattern);

        public RelationalPatternSyntax RelationalPattern(SyntaxToken operatorToken, ExpressionSyntax expression)
            => SyntaxFactory.RelationalPattern(operatorToken, expression);

        public TypePatternSyntax TypePattern(TypeSyntax type)
            => SyntaxFactory.TypePattern(type);

        public BinaryPatternSyntax BinaryPattern(SyntaxKind kind, PatternSyntax left, SyntaxToken operatorToken, PatternSyntax right)
            => SyntaxFactory.BinaryPattern(kind, left, operatorToken, right);

        public BinaryPatternSyntax BinaryPattern(SyntaxKind kind, PatternSyntax left, PatternSyntax right)
            => SyntaxFactory.BinaryPattern(kind, left, right);

        public UnaryPatternSyntax UnaryPattern(SyntaxToken operatorToken, PatternSyntax pattern)
            => SyntaxFactory.UnaryPattern(operatorToken, pattern);

        public UnaryPatternSyntax UnaryPattern(PatternSyntax pattern)
            => SyntaxFactory.UnaryPattern(pattern);

        public ListPatternSyntax ListPattern(SyntaxToken openBracketToken, SeparatedSyntaxList<PatternSyntax> patterns, SyntaxToken closeBracketToken, VariableDesignationSyntax designation)
            => SyntaxFactory.ListPattern(openBracketToken, patterns, closeBracketToken, designation);

        public ListPatternSyntax ListPattern(SeparatedSyntaxList<PatternSyntax> patterns, VariableDesignationSyntax designation)
            => SyntaxFactory.ListPattern(patterns, designation);

        public ListPatternSyntax ListPattern(SeparatedSyntaxList<PatternSyntax> patterns)
            => SyntaxFactory.ListPattern(patterns);

        public SlicePatternSyntax SlicePattern(SyntaxToken dotDotToken, PatternSyntax pattern)
            => SyntaxFactory.SlicePattern(dotDotToken, pattern);

        public SlicePatternSyntax SlicePattern(PatternSyntax pattern)
            => SyntaxFactory.SlicePattern(pattern);

        public InterpolatedStringTextSyntax InterpolatedStringText(SyntaxToken textToken)
            => SyntaxFactory.InterpolatedStringText(textToken);

        public InterpolatedStringTextSyntax InterpolatedStringText()
            => SyntaxFactory.InterpolatedStringText();

        public InterpolationSyntax Interpolation(SyntaxToken openBraceToken, ExpressionSyntax expression, InterpolationAlignmentClauseSyntax alignmentClause, InterpolationFormatClauseSyntax formatClause, SyntaxToken closeBraceToken)
            => SyntaxFactory.Interpolation(openBraceToken, expression, alignmentClause, formatClause, closeBraceToken);

        public InterpolationSyntax Interpolation(ExpressionSyntax expression, InterpolationAlignmentClauseSyntax alignmentClause, InterpolationFormatClauseSyntax formatClause)
            => SyntaxFactory.Interpolation(expression, alignmentClause, formatClause);

        public InterpolationSyntax Interpolation(ExpressionSyntax expression)
            => SyntaxFactory.Interpolation(expression);

        public InterpolationAlignmentClauseSyntax InterpolationAlignmentClause(SyntaxToken commaToken, ExpressionSyntax value)
            => SyntaxFactory.InterpolationAlignmentClause(commaToken, value);

        public InterpolationFormatClauseSyntax InterpolationFormatClause(SyntaxToken colonToken, SyntaxToken formatStringToken)
            => SyntaxFactory.InterpolationFormatClause(colonToken, formatStringToken);

        public InterpolationFormatClauseSyntax InterpolationFormatClause(SyntaxToken colonToken)
            => SyntaxFactory.InterpolationFormatClause(colonToken);

        public GlobalStatementSyntax GlobalStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, StatementSyntax statement)
            => SyntaxFactory.GlobalStatement(attributeLists, modifiers, statement);

        public GlobalStatementSyntax GlobalStatement(StatementSyntax statement)
            => SyntaxFactory.GlobalStatement(statement);

        public BlockSyntax Block(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken openBraceToken, SyntaxList<StatementSyntax> statements, SyntaxToken closeBraceToken)
            => SyntaxFactory.Block(attributeLists, openBraceToken, statements, closeBraceToken);

        public BlockSyntax Block(SyntaxList<AttributeListSyntax> attributeLists, SyntaxList<StatementSyntax> statements)
            => SyntaxFactory.Block(attributeLists, statements);

        public BlockSyntax Block(SyntaxList<StatementSyntax> statements)
            => SyntaxFactory.Block(statements);

        public LocalFunctionStatementSyntax LocalFunctionStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
            => SyntaxFactory.LocalFunctionStatement(attributeLists, modifiers, returnType, identifier, typeParameterList, parameterList, constraintClauses, body, expressionBody, semicolonToken);

        public LocalFunctionStatementSyntax LocalFunctionStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody)
            => SyntaxFactory.LocalFunctionStatement(attributeLists, modifiers, returnType, identifier, typeParameterList, parameterList, constraintClauses, body, expressionBody);

        public LocalFunctionStatementSyntax LocalFunctionStatement(TypeSyntax returnType, SyntaxToken identifier)
            => SyntaxFactory.LocalFunctionStatement(returnType, identifier);

        public LocalFunctionStatementSyntax LocalFunctionStatement(TypeSyntax returnType, string identifier)
            => SyntaxFactory.LocalFunctionStatement(returnType, identifier);

        public LocalDeclarationStatementSyntax LocalDeclarationStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken awaitKeyword, SyntaxToken usingKeyword, SyntaxTokenList modifiers, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
            => SyntaxFactory.LocalDeclarationStatement(attributeLists, awaitKeyword, usingKeyword, modifiers, declaration, semicolonToken);

        public LocalDeclarationStatementSyntax LocalDeclarationStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, VariableDeclarationSyntax declaration)
            => SyntaxFactory.LocalDeclarationStatement(attributeLists, modifiers, declaration);

        public LocalDeclarationStatementSyntax LocalDeclarationStatement(VariableDeclarationSyntax declaration)
            => SyntaxFactory.LocalDeclarationStatement(declaration);

        public VariableDeclarationSyntax VariableDeclaration(TypeSyntax type, string identifier)
            => VariableDeclaration(type, Identifier(identifier));

        public VariableDeclarationSyntax VariableDeclaration(TypeSyntax type, SyntaxToken identifier)
            => VariableDeclaration(type, VariableDeclarator(identifier));

        public VariableDeclarationSyntax VariableDeclaration(TypeSyntax type, VariableDeclaratorSyntax variable)
            => VariableDeclaration(type, SingletonSeparatedList(variable));

        public VariableDeclarationSyntax VariableDeclaration(TypeSyntax type, SeparatedSyntaxList<VariableDeclaratorSyntax> variables)
            => SyntaxFactory.VariableDeclaration(type, variables);

        public VariableDeclarationSyntax VariableDeclaration(TypeSyntax type)
            => SyntaxFactory.VariableDeclaration(type);

        public VariableDeclaratorSyntax VariableDeclarator(SyntaxToken identifier, BracketedArgumentListSyntax argumentList, EqualsValueClauseSyntax initializer)
            => SyntaxFactory.VariableDeclarator(identifier, argumentList, initializer);

        public VariableDeclaratorSyntax VariableDeclarator(SyntaxToken identifier)
            => SyntaxFactory.VariableDeclarator(identifier);

        public VariableDeclaratorSyntax VariableDeclarator(string identifier)
            => SyntaxFactory.VariableDeclarator(identifier);

        public EqualsValueClauseSyntax EqualsValueClause(SyntaxToken equalsToken, ExpressionSyntax value)
            => SyntaxFactory.EqualsValueClause(equalsToken, value);

        public EqualsValueClauseSyntax EqualsValueClause(ExpressionSyntax value)
            => SyntaxFactory.EqualsValueClause(value);

        public SingleVariableDesignationSyntax SingleVariableDesignation(SyntaxToken identifier)
            => SyntaxFactory.SingleVariableDesignation(identifier);

        public DiscardDesignationSyntax DiscardDesignation(SyntaxToken underscoreToken)
            => SyntaxFactory.DiscardDesignation(underscoreToken);

        public DiscardDesignationSyntax DiscardDesignation()
            => SyntaxFactory.DiscardDesignation();

        public ParenthesizedVariableDesignationSyntax ParenthesizedVariableDesignation(SyntaxToken openParenToken, SeparatedSyntaxList<VariableDesignationSyntax> variables, SyntaxToken closeParenToken)
            => SyntaxFactory.ParenthesizedVariableDesignation(openParenToken, variables, closeParenToken);

        public ParenthesizedVariableDesignationSyntax ParenthesizedVariableDesignation(SeparatedSyntaxList<VariableDesignationSyntax> variables)
            => SyntaxFactory.ParenthesizedVariableDesignation(variables);

        public ExpressionStatementSyntax ExpressionStatement(SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax expression, SyntaxToken semicolonToken)
            => SyntaxFactory.ExpressionStatement(attributeLists, expression, semicolonToken);

        public ExpressionStatementSyntax ExpressionStatement(SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax expression)
            => SyntaxFactory.ExpressionStatement(attributeLists, expression);

        public ExpressionStatementSyntax ExpressionStatement(ExpressionSyntax expression)
            => SyntaxFactory.ExpressionStatement(expression);

        public EmptyStatementSyntax EmptyStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken semicolonToken)
            => SyntaxFactory.EmptyStatement(attributeLists, semicolonToken);

        public EmptyStatementSyntax EmptyStatement(SyntaxList<AttributeListSyntax> attributeLists)
            => SyntaxFactory.EmptyStatement(attributeLists);

        public EmptyStatementSyntax EmptyStatement()
            => SyntaxFactory.EmptyStatement();

        public LabeledStatementSyntax LabeledStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken identifier, SyntaxToken colonToken, StatementSyntax statement)
            => SyntaxFactory.LabeledStatement(attributeLists, identifier, colonToken, statement);

        public LabeledStatementSyntax LabeledStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken identifier, StatementSyntax statement)
            => SyntaxFactory.LabeledStatement(attributeLists, identifier, statement);

        public LabeledStatementSyntax LabeledStatement(SyntaxToken identifier, StatementSyntax statement)
            => SyntaxFactory.LabeledStatement(identifier, statement);

        public LabeledStatementSyntax LabeledStatement(string identifier, StatementSyntax statement)
            => SyntaxFactory.LabeledStatement(identifier, statement);

        public GotoStatementSyntax GotoStatement(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken gotoKeyword, SyntaxToken caseOrDefaultKeyword, ExpressionSyntax expression, SyntaxToken semicolonToken)
            => SyntaxFactory.GotoStatement(kind, attributeLists, gotoKeyword, caseOrDefaultKeyword, expression, semicolonToken);

        public GotoStatementSyntax GotoStatement(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken caseOrDefaultKeyword, ExpressionSyntax expression)
            => SyntaxFactory.GotoStatement(kind, attributeLists, caseOrDefaultKeyword, expression);

        public GotoStatementSyntax GotoStatement(SyntaxKind kind, ExpressionSyntax expression)
            => SyntaxFactory.GotoStatement(kind, expression);

        public BreakStatementSyntax BreakStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken breakKeyword, SyntaxToken semicolonToken)
            => SyntaxFactory.BreakStatement(attributeLists, breakKeyword, semicolonToken);

        public BreakStatementSyntax BreakStatement(SyntaxList<AttributeListSyntax> attributeLists)
            => SyntaxFactory.BreakStatement(attributeLists);

        public BreakStatementSyntax BreakStatement()
            => SyntaxFactory.BreakStatement();

        public ContinueStatementSyntax ContinueStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken continueKeyword, SyntaxToken semicolonToken)
            => SyntaxFactory.ContinueStatement(attributeLists, continueKeyword, semicolonToken);

        public ContinueStatementSyntax ContinueStatement(SyntaxList<AttributeListSyntax> attributeLists)
            => SyntaxFactory.ContinueStatement(attributeLists);

        public ContinueStatementSyntax ContinueStatement()
            => SyntaxFactory.ContinueStatement();

        public ReturnStatementSyntax ReturnStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken returnKeyword, ExpressionSyntax expression, SyntaxToken semicolonToken)
            => SyntaxFactory.ReturnStatement(attributeLists, returnKeyword, expression, semicolonToken);

        public ReturnStatementSyntax ReturnStatement(SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax expression)
            => SyntaxFactory.ReturnStatement(attributeLists, expression);

        public ReturnStatementSyntax ReturnStatement(ExpressionSyntax expression)
            => SyntaxFactory.ReturnStatement(expression);

        public ThrowStatementSyntax ThrowStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken throwKeyword, ExpressionSyntax expression, SyntaxToken semicolonToken)
            => SyntaxFactory.ThrowStatement(attributeLists, throwKeyword, expression, semicolonToken);

        public ThrowStatementSyntax ThrowStatement(SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax expression)
            => SyntaxFactory.ThrowStatement(attributeLists, expression);

        public ThrowStatementSyntax ThrowStatement(ExpressionSyntax expression)
            => SyntaxFactory.ThrowStatement(expression);

        public YieldStatementSyntax YieldStatement(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken yieldKeyword, SyntaxToken returnOrBreakKeyword, ExpressionSyntax expression, SyntaxToken semicolonToken)
            => SyntaxFactory.YieldStatement(kind, attributeLists, yieldKeyword, returnOrBreakKeyword, expression, semicolonToken);

        public YieldStatementSyntax YieldStatement(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax expression)
            => SyntaxFactory.YieldStatement(kind, attributeLists, expression);

        public YieldStatementSyntax YieldStatement(SyntaxKind kind, ExpressionSyntax expression)
            => SyntaxFactory.YieldStatement(kind, expression);

        public WhileStatementSyntax WhileStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement)
            => SyntaxFactory.WhileStatement(attributeLists, whileKeyword, openParenToken, condition, closeParenToken, statement);

        public WhileStatementSyntax WhileStatement(SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax condition, StatementSyntax statement)
            => SyntaxFactory.WhileStatement(attributeLists, condition, statement);

        public WhileStatementSyntax WhileStatement(ExpressionSyntax condition, StatementSyntax statement)
            => SyntaxFactory.WhileStatement(condition, statement);

        public DoStatementSyntax DoStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken doKeyword, StatementSyntax statement, SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, SyntaxToken semicolonToken)
            => SyntaxFactory.DoStatement(attributeLists, doKeyword, statement, whileKeyword, openParenToken, condition, closeParenToken, semicolonToken);

        public DoStatementSyntax DoStatement(SyntaxList<AttributeListSyntax> attributeLists, StatementSyntax statement, ExpressionSyntax condition)
            => SyntaxFactory.DoStatement(attributeLists, statement, condition);

        public DoStatementSyntax DoStatement(StatementSyntax statement, ExpressionSyntax condition)
            => SyntaxFactory.DoStatement(statement, condition);

        public ForStatementSyntax ForStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken forKeyword, SyntaxToken openParenToken, VariableDeclarationSyntax declaration, SeparatedSyntaxList<ExpressionSyntax> initializers, SyntaxToken firstSemicolonToken, ExpressionSyntax condition, SyntaxToken secondSemicolonToken, SeparatedSyntaxList<ExpressionSyntax> incrementors, SyntaxToken closeParenToken, StatementSyntax statement)
            => SyntaxFactory.ForStatement(attributeLists, forKeyword, openParenToken, declaration, initializers, firstSemicolonToken, condition, secondSemicolonToken, incrementors, closeParenToken, statement);

        public ForStatementSyntax ForStatement(SyntaxList<AttributeListSyntax> attributeLists, VariableDeclarationSyntax declaration, SeparatedSyntaxList<ExpressionSyntax> initializers, ExpressionSyntax condition, SeparatedSyntaxList<ExpressionSyntax> incrementors, StatementSyntax statement)
            => SyntaxFactory.ForStatement(attributeLists, declaration, initializers, condition, incrementors, statement);

        public ForStatementSyntax ForStatement(StatementSyntax statement)
            => SyntaxFactory.ForStatement(statement);

        public ForEachStatementSyntax ForEachStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
            => SyntaxFactory.ForEachStatement(attributeLists, awaitKeyword, forEachKeyword, openParenToken, type, identifier, inKeyword, expression, closeParenToken, statement);

        public ForEachStatementSyntax ForEachStatement(SyntaxList<AttributeListSyntax> attributeLists, TypeSyntax type, SyntaxToken identifier, ExpressionSyntax expression, StatementSyntax statement)
            => SyntaxFactory.ForEachStatement(attributeLists, type, identifier, expression, statement);

        public ForEachStatementSyntax ForEachStatement(TypeSyntax type, SyntaxToken identifier, ExpressionSyntax expression, StatementSyntax statement)
            => SyntaxFactory.ForEachStatement(type, identifier, expression, statement);

        public ForEachStatementSyntax ForEachStatement(TypeSyntax type, string identifier, ExpressionSyntax expression, StatementSyntax statement)
            => SyntaxFactory.ForEachStatement(type, identifier, expression, statement);

        public ForEachVariableStatementSyntax ForEachVariableStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, ExpressionSyntax variable, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
            => SyntaxFactory.ForEachVariableStatement(attributeLists, awaitKeyword, forEachKeyword, openParenToken, variable, inKeyword, expression, closeParenToken, statement);

        public ForEachVariableStatementSyntax ForEachVariableStatement(SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax variable, ExpressionSyntax expression, StatementSyntax statement)
            => SyntaxFactory.ForEachVariableStatement(attributeLists, variable, expression, statement);

        public ForEachVariableStatementSyntax ForEachVariableStatement(ExpressionSyntax variable, ExpressionSyntax expression, StatementSyntax statement)
            => SyntaxFactory.ForEachVariableStatement(variable, expression, statement);

        public UsingStatementSyntax UsingStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken awaitKeyword, SyntaxToken usingKeyword, SyntaxToken openParenToken, VariableDeclarationSyntax declaration, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
            => SyntaxFactory.UsingStatement(attributeLists, awaitKeyword, usingKeyword, openParenToken, declaration, expression, closeParenToken, statement);

        public UsingStatementSyntax UsingStatement(SyntaxList<AttributeListSyntax> attributeLists, VariableDeclarationSyntax declaration, ExpressionSyntax expression, StatementSyntax statement)
            => SyntaxFactory.UsingStatement(attributeLists, declaration, expression, statement);

        public UsingStatementSyntax UsingStatement(StatementSyntax statement)
            => SyntaxFactory.UsingStatement(statement);

        public FixedStatementSyntax FixedStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken fixedKeyword, SyntaxToken openParenToken, VariableDeclarationSyntax declaration, SyntaxToken closeParenToken, StatementSyntax statement)
            => SyntaxFactory.FixedStatement(attributeLists, fixedKeyword, openParenToken, declaration, closeParenToken, statement);

        public FixedStatementSyntax FixedStatement(SyntaxList<AttributeListSyntax> attributeLists, VariableDeclarationSyntax declaration, StatementSyntax statement)
            => SyntaxFactory.FixedStatement(attributeLists, declaration, statement);

        public FixedStatementSyntax FixedStatement(VariableDeclarationSyntax declaration, StatementSyntax statement)
            => SyntaxFactory.FixedStatement(declaration, statement);

        public CheckedStatementSyntax CheckedStatement(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken keyword, BlockSyntax block)
            => SyntaxFactory.CheckedStatement(kind, attributeLists, keyword, block);

        public CheckedStatementSyntax CheckedStatement(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, BlockSyntax block)
            => SyntaxFactory.CheckedStatement(kind, attributeLists, block);

        public CheckedStatementSyntax CheckedStatement(SyntaxKind kind, BlockSyntax block)
            => SyntaxFactory.CheckedStatement(kind, block);

        public UnsafeStatementSyntax UnsafeStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken unsafeKeyword, BlockSyntax block)
            => SyntaxFactory.UnsafeStatement(attributeLists, unsafeKeyword, block);

        public UnsafeStatementSyntax UnsafeStatement(SyntaxList<AttributeListSyntax> attributeLists, BlockSyntax block)
            => SyntaxFactory.UnsafeStatement(attributeLists, block);

        public UnsafeStatementSyntax UnsafeStatement(BlockSyntax block)
            => SyntaxFactory.UnsafeStatement(block);

        public LockStatementSyntax LockStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken lockKeyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
            => SyntaxFactory.LockStatement(attributeLists, lockKeyword, openParenToken, expression, closeParenToken, statement);

        public LockStatementSyntax LockStatement(SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax expression, StatementSyntax statement)
            => SyntaxFactory.LockStatement(attributeLists, expression, statement);

        public LockStatementSyntax LockStatement(ExpressionSyntax expression, StatementSyntax statement)
            => SyntaxFactory.LockStatement(expression, statement);

        public IfStatementSyntax IfStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken ifKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement, ElseClauseSyntax @else)
            => SyntaxFactory.IfStatement(attributeLists, ifKeyword, openParenToken, condition, closeParenToken, statement, @else);

        public IfStatementSyntax IfStatement(SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax condition, StatementSyntax statement, ElseClauseSyntax @else)
            => SyntaxFactory.IfStatement(attributeLists, condition, statement, @else);

        public IfStatementSyntax IfStatement(ExpressionSyntax condition, StatementSyntax statement)
            => SyntaxFactory.IfStatement(condition, statement);

        public ElseClauseSyntax ElseClause(SyntaxToken elseKeyword, StatementSyntax statement)
            => SyntaxFactory.ElseClause(elseKeyword, statement);

        public ElseClauseSyntax ElseClause(StatementSyntax statement)
            => SyntaxFactory.ElseClause(statement);

        public SwitchStatementSyntax SwitchStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken switchKeyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, SyntaxToken openBraceToken, SyntaxList<SwitchSectionSyntax> sections, SyntaxToken closeBraceToken)
            => SyntaxFactory.SwitchStatement(attributeLists, switchKeyword, openParenToken, expression, closeParenToken, openBraceToken, sections, closeBraceToken);

        public SwitchSectionSyntax SwitchSection(SyntaxList<SwitchLabelSyntax> labels, SyntaxList<StatementSyntax> statements)
            => SyntaxFactory.SwitchSection(labels, statements);

        public SwitchSectionSyntax SwitchSection()
            => SyntaxFactory.SwitchSection();

        public CasePatternSwitchLabelSyntax CasePatternSwitchLabel(SyntaxToken keyword, PatternSyntax pattern, WhenClauseSyntax whenClause, SyntaxToken colonToken)
            => SyntaxFactory.CasePatternSwitchLabel(keyword, pattern, whenClause, colonToken);

        public CasePatternSwitchLabelSyntax CasePatternSwitchLabel(PatternSyntax pattern, WhenClauseSyntax whenClause, SyntaxToken colonToken)
            => SyntaxFactory.CasePatternSwitchLabel(pattern, whenClause, colonToken);

        public CasePatternSwitchLabelSyntax CasePatternSwitchLabel(PatternSyntax pattern, SyntaxToken colonToken)
            => SyntaxFactory.CasePatternSwitchLabel(pattern, colonToken);

        public CaseSwitchLabelSyntax CaseSwitchLabel(SyntaxToken keyword, ExpressionSyntax value, SyntaxToken colonToken)
            => SyntaxFactory.CaseSwitchLabel(keyword, value, colonToken);

        public CaseSwitchLabelSyntax CaseSwitchLabel(ExpressionSyntax value, SyntaxToken colonToken)
            => SyntaxFactory.CaseSwitchLabel(value, colonToken);

        public DefaultSwitchLabelSyntax DefaultSwitchLabel(SyntaxToken keyword, SyntaxToken colonToken)
            => SyntaxFactory.DefaultSwitchLabel(keyword, colonToken);

        public DefaultSwitchLabelSyntax DefaultSwitchLabel(SyntaxToken colonToken)
            => SyntaxFactory.DefaultSwitchLabel(colonToken);

        public SwitchExpressionSyntax SwitchExpression(ExpressionSyntax governingExpression, SyntaxToken switchKeyword, SyntaxToken openBraceToken, SeparatedSyntaxList<SwitchExpressionArmSyntax> arms, SyntaxToken closeBraceToken)
            => SyntaxFactory.SwitchExpression(governingExpression, switchKeyword, openBraceToken, arms, closeBraceToken);

        public SwitchExpressionSyntax SwitchExpression(ExpressionSyntax governingExpression, SeparatedSyntaxList<SwitchExpressionArmSyntax> arms)
            => SyntaxFactory.SwitchExpression(governingExpression, arms);

        public SwitchExpressionSyntax SwitchExpression(ExpressionSyntax governingExpression)
            => SyntaxFactory.SwitchExpression(governingExpression);

        public SwitchExpressionArmSyntax SwitchExpressionArm(PatternSyntax pattern, WhenClauseSyntax whenClause, SyntaxToken equalsGreaterThanToken, ExpressionSyntax expression)
            => SyntaxFactory.SwitchExpressionArm(pattern, whenClause, equalsGreaterThanToken, expression);

        public SwitchExpressionArmSyntax SwitchExpressionArm(PatternSyntax pattern, WhenClauseSyntax whenClause, ExpressionSyntax expression)
            => SyntaxFactory.SwitchExpressionArm(pattern, whenClause, expression);

        public SwitchExpressionArmSyntax SwitchExpressionArm(PatternSyntax pattern, ExpressionSyntax expression)
            => SyntaxFactory.SwitchExpressionArm(pattern, expression);

        public TryStatementSyntax TryStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken tryKeyword, BlockSyntax block, SyntaxList<CatchClauseSyntax> catches, FinallyClauseSyntax @finally)
            => SyntaxFactory.TryStatement(attributeLists, tryKeyword, block, catches, @finally);

        public TryStatementSyntax TryStatement(SyntaxList<AttributeListSyntax> attributeLists, BlockSyntax block, SyntaxList<CatchClauseSyntax> catches, FinallyClauseSyntax @finally)
            => SyntaxFactory.TryStatement(attributeLists, block, catches, @finally);

        public TryStatementSyntax TryStatement(SyntaxList<CatchClauseSyntax> catches)
            => SyntaxFactory.TryStatement(catches);

        public CatchClauseSyntax CatchClause(SyntaxToken catchKeyword, CatchDeclarationSyntax declaration, CatchFilterClauseSyntax filter, BlockSyntax block)
            => SyntaxFactory.CatchClause(catchKeyword, declaration, filter, block);

        public CatchClauseSyntax CatchClause(CatchDeclarationSyntax declaration, CatchFilterClauseSyntax filter, BlockSyntax block)
            => SyntaxFactory.CatchClause(declaration, filter, block);

        public CatchClauseSyntax CatchClause()
            => SyntaxFactory.CatchClause();

        public CatchDeclarationSyntax CatchDeclaration(SyntaxToken openParenToken, TypeSyntax type, SyntaxToken identifier, SyntaxToken closeParenToken)
            => SyntaxFactory.CatchDeclaration(openParenToken, type, identifier, closeParenToken);

        public CatchDeclarationSyntax CatchDeclaration(TypeSyntax type, SyntaxToken identifier)
            => SyntaxFactory.CatchDeclaration(type, identifier);

        public CatchDeclarationSyntax CatchDeclaration(TypeSyntax type)
            => SyntaxFactory.CatchDeclaration(type);

        public CatchFilterClauseSyntax CatchFilterClause(SyntaxToken whenKeyword, SyntaxToken openParenToken, ExpressionSyntax filterExpression, SyntaxToken closeParenToken)
            => SyntaxFactory.CatchFilterClause(whenKeyword, openParenToken, filterExpression, closeParenToken);

        public CatchFilterClauseSyntax CatchFilterClause(ExpressionSyntax filterExpression)
            => SyntaxFactory.CatchFilterClause(filterExpression);

        public FinallyClauseSyntax FinallyClause(SyntaxToken finallyKeyword, BlockSyntax block)
            => SyntaxFactory.FinallyClause(finallyKeyword, block);

        public FinallyClauseSyntax FinallyClause(BlockSyntax block)
            => SyntaxFactory.FinallyClause(block);

        public CompilationUnitSyntax CompilationUnit(SyntaxList<ExternAliasDirectiveSyntax> externs, SyntaxList<UsingDirectiveSyntax> usings, SyntaxList<AttributeListSyntax> attributeLists, SyntaxList<MemberDeclarationSyntax> members, SyntaxToken endOfFileToken)
            => SyntaxFactory.CompilationUnit(externs, usings, attributeLists, members, endOfFileToken);

        public CompilationUnitSyntax CompilationUnit(SyntaxList<ExternAliasDirectiveSyntax> externs, SyntaxList<UsingDirectiveSyntax> usings, SyntaxList<AttributeListSyntax> attributeLists, SyntaxList<MemberDeclarationSyntax> members)
            => SyntaxFactory.CompilationUnit(externs, usings, attributeLists, members);

        public CompilationUnitSyntax CompilationUnit()
            => SyntaxFactory.CompilationUnit();

        public ExternAliasDirectiveSyntax ExternAliasDirective(SyntaxToken externKeyword, SyntaxToken aliasKeyword, SyntaxToken identifier, SyntaxToken semicolonToken)
            => SyntaxFactory.ExternAliasDirective(externKeyword, aliasKeyword, identifier, semicolonToken);

        public ExternAliasDirectiveSyntax ExternAliasDirective(SyntaxToken identifier)
            => SyntaxFactory.ExternAliasDirective(identifier);

        public ExternAliasDirectiveSyntax ExternAliasDirective(string identifier)
            => SyntaxFactory.ExternAliasDirective(identifier);

        public UsingDirectiveSyntax UsingDirective(SyntaxToken globalKeyword, SyntaxToken usingKeyword, SyntaxToken staticKeyword, SyntaxToken unsafeKeyword, NameEqualsSyntax alias, TypeSyntax namespaceOrType, SyntaxToken semicolonToken)
            => SyntaxFactory.UsingDirective(globalKeyword, usingKeyword, staticKeyword, unsafeKeyword, alias, namespaceOrType, semicolonToken);

        public UsingDirectiveSyntax UsingDirective(NameEqualsSyntax alias, TypeSyntax namespaceOrType)
            => SyntaxFactory.UsingDirective(alias, namespaceOrType);

        public UsingDirectiveSyntax UsingDirective(TypeSyntax namespaceOrType)
            => SyntaxFactory.UsingDirective(namespaceOrType);

        public NamespaceDeclarationSyntax NamespaceDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken namespaceKeyword, NameSyntax name, SyntaxToken openBraceToken, SyntaxList<ExternAliasDirectiveSyntax> externs, SyntaxList<UsingDirectiveSyntax> usings, SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
            => SyntaxFactory.NamespaceDeclaration(attributeLists, modifiers, namespaceKeyword, name, openBraceToken, externs, usings, members, closeBraceToken, semicolonToken);

        public NamespaceDeclarationSyntax NamespaceDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, NameSyntax name, SyntaxList<ExternAliasDirectiveSyntax> externs, SyntaxList<UsingDirectiveSyntax> usings, SyntaxList<MemberDeclarationSyntax> members)
            => SyntaxFactory.NamespaceDeclaration(attributeLists, modifiers, name, externs, usings, members);

        public NamespaceDeclarationSyntax NamespaceDeclaration(NameSyntax name)
            => SyntaxFactory.NamespaceDeclaration(name);

        public FileScopedNamespaceDeclarationSyntax FileScopedNamespaceDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken namespaceKeyword, NameSyntax name, SyntaxToken semicolonToken, SyntaxList<ExternAliasDirectiveSyntax> externs, SyntaxList<UsingDirectiveSyntax> usings, SyntaxList<MemberDeclarationSyntax> members)
            => SyntaxFactory.FileScopedNamespaceDeclaration(attributeLists, modifiers, namespaceKeyword, name, semicolonToken, externs, usings, members);

        public FileScopedNamespaceDeclarationSyntax FileScopedNamespaceDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, NameSyntax name, SyntaxList<ExternAliasDirectiveSyntax> externs, SyntaxList<UsingDirectiveSyntax> usings, SyntaxList<MemberDeclarationSyntax> members)
            => SyntaxFactory.FileScopedNamespaceDeclaration(attributeLists, modifiers, name, externs, usings, members);

        public FileScopedNamespaceDeclarationSyntax FileScopedNamespaceDeclaration(NameSyntax name)
            => SyntaxFactory.FileScopedNamespaceDeclaration(name);

        public AttributeListSyntax AttributeList(SyntaxToken openBracketToken, AttributeTargetSpecifierSyntax target, SeparatedSyntaxList<AttributeSyntax> attributes, SyntaxToken closeBracketToken)
            => SyntaxFactory.AttributeList(openBracketToken, target, attributes, closeBracketToken);

        public AttributeListSyntax AttributeList(AttributeTargetSpecifierSyntax target, SeparatedSyntaxList<AttributeSyntax> attributes)
            => SyntaxFactory.AttributeList(target, attributes);

        public AttributeListSyntax AttributeList(SeparatedSyntaxList<AttributeSyntax> attributes)
            => SyntaxFactory.AttributeList(attributes);

        public AttributeTargetSpecifierSyntax AttributeTargetSpecifier(SyntaxToken identifier, SyntaxToken colonToken)
            => SyntaxFactory.AttributeTargetSpecifier(identifier, colonToken);

        public AttributeTargetSpecifierSyntax AttributeTargetSpecifier(SyntaxToken identifier)
            => SyntaxFactory.AttributeTargetSpecifier(identifier);

        public AttributeSyntax Attribute(NameSyntax name, AttributeArgumentListSyntax argumentList)
            => SyntaxFactory.Attribute(name, argumentList);

        public AttributeSyntax Attribute(NameSyntax name)
            => SyntaxFactory.Attribute(name);

        public AttributeArgumentListSyntax AttributeArgumentList(SyntaxToken openParenToken, SeparatedSyntaxList<AttributeArgumentSyntax> arguments, SyntaxToken closeParenToken)
            => SyntaxFactory.AttributeArgumentList(openParenToken, arguments, closeParenToken);

        public AttributeArgumentListSyntax AttributeArgumentList(SeparatedSyntaxList<AttributeArgumentSyntax> arguments)
            => SyntaxFactory.AttributeArgumentList(arguments);

        public AttributeArgumentSyntax AttributeArgument(NameEqualsSyntax nameEquals, NameColonSyntax nameColon, ExpressionSyntax expression)
            => SyntaxFactory.AttributeArgument(nameEquals, nameColon, expression);

        public AttributeArgumentSyntax AttributeArgument(ExpressionSyntax expression)
            => SyntaxFactory.AttributeArgument(expression);

        public NameEqualsSyntax NameEquals(IdentifierNameSyntax name, SyntaxToken equalsToken)
            => SyntaxFactory.NameEquals(name, equalsToken);

        public NameEqualsSyntax NameEquals(IdentifierNameSyntax name)
            => SyntaxFactory.NameEquals(name);

        public NameEqualsSyntax NameEquals(string name)
            => SyntaxFactory.NameEquals(name);

        public TypeParameterListSyntax TypeParameterList(SyntaxToken lessThanToken, SeparatedSyntaxList<TypeParameterSyntax> parameters, SyntaxToken greaterThanToken)
            => SyntaxFactory.TypeParameterList(lessThanToken, parameters, greaterThanToken);

        public TypeParameterListSyntax TypeParameterList(SeparatedSyntaxList<TypeParameterSyntax> parameters)
            => SyntaxFactory.TypeParameterList(parameters);

        public TypeParameterSyntax TypeParameter(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken varianceKeyword, SyntaxToken identifier)
            => SyntaxFactory.TypeParameter(attributeLists, varianceKeyword, identifier);

        public TypeParameterSyntax TypeParameter(SyntaxToken identifier)
            => SyntaxFactory.TypeParameter(identifier);

        public TypeParameterSyntax TypeParameter(string identifier)
            => SyntaxFactory.TypeParameter(identifier);

        public ClassDeclarationSyntax ClassDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
            => SyntaxFactory.ClassDeclaration(attributeLists, modifiers, keyword, identifier, typeParameterList, parameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);

        public StructDeclarationSyntax StructDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
            => SyntaxFactory.StructDeclaration(attributeLists, modifiers, keyword, identifier, typeParameterList, parameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);

        public InterfaceDeclarationSyntax InterfaceDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
            => SyntaxFactory.InterfaceDeclaration(attributeLists, modifiers, keyword, identifier, typeParameterList, parameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);

        public RecordDeclarationSyntax RecordDeclaration(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken classOrStructKeyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
            => SyntaxFactory.RecordDeclaration(kind, attributeLists, modifiers, keyword, classOrStructKeyword, identifier, typeParameterList, parameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);

        public RecordDeclarationSyntax RecordDeclaration(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, BaseListSyntax baseList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxList<MemberDeclarationSyntax> members)
            => SyntaxFactory.RecordDeclaration(kind, attributeLists, modifiers, keyword, identifier, typeParameterList, parameterList, baseList, constraintClauses, members);

        public RecordDeclarationSyntax RecordDeclaration(SyntaxKind kind, SyntaxToken keyword, SyntaxToken identifier)
            => SyntaxFactory.RecordDeclaration(kind, keyword, identifier);

        public RecordDeclarationSyntax RecordDeclaration(SyntaxKind kind, SyntaxToken keyword, string identifier)
            => SyntaxFactory.RecordDeclaration(kind, keyword, identifier);

        public EnumDeclarationSyntax EnumDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken enumKeyword, SyntaxToken identifier, BaseListSyntax baseList, SyntaxToken openBraceToken, SeparatedSyntaxList<EnumMemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
            => SyntaxFactory.EnumDeclaration(attributeLists, modifiers, enumKeyword, identifier, baseList, openBraceToken, members, closeBraceToken, semicolonToken);

        public DelegateDeclarationSyntax DelegateDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken delegateKeyword, TypeSyntax returnType, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken semicolonToken)
            => SyntaxFactory.DelegateDeclaration(attributeLists, modifiers, delegateKeyword, returnType, identifier, typeParameterList, parameterList, constraintClauses, semicolonToken);

        public DelegateDeclarationSyntax DelegateDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses)
            => SyntaxFactory.DelegateDeclaration(attributeLists, modifiers, returnType, identifier, typeParameterList, parameterList, constraintClauses);

        public DelegateDeclarationSyntax DelegateDeclaration(TypeSyntax returnType, SyntaxToken identifier)
            => SyntaxFactory.DelegateDeclaration(returnType, identifier);

        public DelegateDeclarationSyntax DelegateDeclaration(TypeSyntax returnType, string identifier)
            => SyntaxFactory.DelegateDeclaration(returnType, identifier);

        public BaseListSyntax BaseList(SyntaxToken colonToken, SeparatedSyntaxList<BaseTypeSyntax> types)
            => SyntaxFactory.BaseList(colonToken, types);

        public BaseListSyntax BaseList(SeparatedSyntaxList<BaseTypeSyntax> types)
            => SyntaxFactory.BaseList(types);

        public SimpleBaseTypeSyntax SimpleBaseType(TypeSyntax type)
            => SyntaxFactory.SimpleBaseType(type);

        public PrimaryConstructorBaseTypeSyntax PrimaryConstructorBaseType(TypeSyntax type, ArgumentListSyntax argumentList)
            => SyntaxFactory.PrimaryConstructorBaseType(type, argumentList);

        public PrimaryConstructorBaseTypeSyntax PrimaryConstructorBaseType(TypeSyntax type)
            => SyntaxFactory.PrimaryConstructorBaseType(type);

        public TypeParameterConstraintClauseSyntax TypeParameterConstraintClause(SyntaxToken whereKeyword, IdentifierNameSyntax name, SyntaxToken colonToken, SeparatedSyntaxList<TypeParameterConstraintSyntax> constraints)
            => SyntaxFactory.TypeParameterConstraintClause(whereKeyword, name, colonToken, constraints);

        public TypeParameterConstraintClauseSyntax TypeParameterConstraintClause(IdentifierNameSyntax name, SeparatedSyntaxList<TypeParameterConstraintSyntax> constraints)
            => SyntaxFactory.TypeParameterConstraintClause(name, constraints);

        public TypeParameterConstraintClauseSyntax TypeParameterConstraintClause(IdentifierNameSyntax name)
            => SyntaxFactory.TypeParameterConstraintClause(name);

        public TypeParameterConstraintClauseSyntax TypeParameterConstraintClause(string name)
            => SyntaxFactory.TypeParameterConstraintClause(name);

        public ConstructorConstraintSyntax ConstructorConstraint(SyntaxToken newKeyword, SyntaxToken openParenToken, SyntaxToken closeParenToken)
            => SyntaxFactory.ConstructorConstraint(newKeyword, openParenToken, closeParenToken);

        public ConstructorConstraintSyntax ConstructorConstraint()
            => SyntaxFactory.ConstructorConstraint();

        public ClassOrStructConstraintSyntax ClassOrStructConstraint(SyntaxKind kind, SyntaxToken classOrStructKeyword, SyntaxToken questionToken)
            => SyntaxFactory.ClassOrStructConstraint(kind, classOrStructKeyword, questionToken);

        public ClassOrStructConstraintSyntax ClassOrStructConstraint(SyntaxKind kind)
            => SyntaxFactory.ClassOrStructConstraint(kind);

        public TypeConstraintSyntax TypeConstraint(TypeSyntax type)
            => SyntaxFactory.TypeConstraint(type);

        public DefaultConstraintSyntax DefaultConstraint(SyntaxToken defaultKeyword)
            => SyntaxFactory.DefaultConstraint(defaultKeyword);

        public DefaultConstraintSyntax DefaultConstraint()
            => SyntaxFactory.DefaultConstraint();

        public AllowsConstraintClauseSyntax AllowsConstraintClause(SyntaxToken allowsKeyword, SeparatedSyntaxList<AllowsConstraintSyntax> constraints)
            => SyntaxFactory.AllowsConstraintClause(allowsKeyword, constraints);

        public AllowsConstraintClauseSyntax AllowsConstraintClause(SeparatedSyntaxList<AllowsConstraintSyntax> constraints)
            => SyntaxFactory.AllowsConstraintClause(constraints);

        public RefStructConstraintSyntax RefStructConstraint(SyntaxToken refKeyword, SyntaxToken structKeyword)
            => SyntaxFactory.RefStructConstraint(refKeyword, structKeyword);

        public RefStructConstraintSyntax RefStructConstraint()
            => SyntaxFactory.RefStructConstraint();

        public FieldDeclarationSyntax FieldDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
            => SyntaxFactory.FieldDeclaration(attributeLists, modifiers, declaration, semicolonToken);

        public FieldDeclarationSyntax FieldDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, VariableDeclarationSyntax declaration)
            => SyntaxFactory.FieldDeclaration(attributeLists, modifiers, declaration);

        public FieldDeclarationSyntax FieldDeclaration(VariableDeclarationSyntax declaration)
            => SyntaxFactory.FieldDeclaration(declaration);

        public EventFieldDeclarationSyntax EventFieldDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken eventKeyword, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
            => SyntaxFactory.EventFieldDeclaration(attributeLists, modifiers, eventKeyword, declaration, semicolonToken);

        public EventFieldDeclarationSyntax EventFieldDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, VariableDeclarationSyntax declaration)
            => SyntaxFactory.EventFieldDeclaration(attributeLists, modifiers, declaration);

        public EventFieldDeclarationSyntax EventFieldDeclaration(VariableDeclarationSyntax declaration)
            => SyntaxFactory.EventFieldDeclaration(declaration);

        public ExplicitInterfaceSpecifierSyntax ExplicitInterfaceSpecifier(NameSyntax name, SyntaxToken dotToken)
            => SyntaxFactory.ExplicitInterfaceSpecifier(name, dotToken);

        public ExplicitInterfaceSpecifierSyntax ExplicitInterfaceSpecifier(NameSyntax name)
            => SyntaxFactory.ExplicitInterfaceSpecifier(name);

        public MethodDeclarationSyntax MethodDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
            => SyntaxFactory.MethodDeclaration(attributeLists, modifiers, returnType, explicitInterfaceSpecifier, identifier, typeParameterList, parameterList, constraintClauses, body, expressionBody, semicolonToken);

        public MethodDeclarationSyntax MethodDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody)
            => SyntaxFactory.MethodDeclaration(attributeLists, modifiers, returnType, explicitInterfaceSpecifier, identifier, typeParameterList, parameterList, constraintClauses, body, expressionBody);

        public MethodDeclarationSyntax MethodDeclaration(TypeSyntax returnType, SyntaxToken identifier)
            => SyntaxFactory.MethodDeclaration(returnType, identifier);

        public MethodDeclarationSyntax MethodDeclaration(TypeSyntax returnType, string identifier)
            => SyntaxFactory.MethodDeclaration(returnType, identifier);

        public OperatorDeclarationSyntax OperatorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken operatorKeyword, SyntaxToken checkedKeyword, SyntaxToken operatorToken, ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
            => SyntaxFactory.OperatorDeclaration(attributeLists, modifiers, returnType, explicitInterfaceSpecifier, operatorKeyword, checkedKeyword, operatorToken, parameterList, body, expressionBody, semicolonToken);

        public OperatorDeclarationSyntax OperatorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken operatorToken, ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody)
            => SyntaxFactory.OperatorDeclaration(attributeLists, modifiers, returnType, explicitInterfaceSpecifier, operatorToken, parameterList, body, expressionBody);

        public OperatorDeclarationSyntax OperatorDeclaration(TypeSyntax returnType, SyntaxToken operatorToken)
            => SyntaxFactory.OperatorDeclaration(returnType, operatorToken);

        public ConversionOperatorDeclarationSyntax ConversionOperatorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken implicitOrExplicitKeyword, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken operatorKeyword, SyntaxToken checkedKeyword, TypeSyntax type, ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
            => SyntaxFactory.ConversionOperatorDeclaration(attributeLists, modifiers, implicitOrExplicitKeyword, explicitInterfaceSpecifier, operatorKeyword, checkedKeyword, type, parameterList, body, expressionBody, semicolonToken);

        public ConversionOperatorDeclarationSyntax ConversionOperatorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken implicitOrExplicitKeyword, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, TypeSyntax type, ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody)
            => SyntaxFactory.ConversionOperatorDeclaration(attributeLists, modifiers, implicitOrExplicitKeyword, explicitInterfaceSpecifier, type, parameterList, body, expressionBody);

        public ConversionOperatorDeclarationSyntax ConversionOperatorDeclaration(SyntaxToken implicitOrExplicitKeyword, TypeSyntax type)
            => SyntaxFactory.ConversionOperatorDeclaration(implicitOrExplicitKeyword, type);

        public ConstructorDeclarationSyntax ConstructorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, ParameterListSyntax parameterList, ConstructorInitializerSyntax initializer, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
            => SyntaxFactory.ConstructorDeclaration(attributeLists, modifiers, identifier, parameterList, initializer, body, expressionBody, semicolonToken);

        public ConstructorDeclarationSyntax ConstructorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, ParameterListSyntax parameterList, ConstructorInitializerSyntax initializer, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody)
            => SyntaxFactory.ConstructorDeclaration(attributeLists, modifiers, identifier, parameterList, initializer, body, expressionBody);

        public ConstructorDeclarationSyntax ConstructorDeclaration(SyntaxToken identifier)
            => SyntaxFactory.ConstructorDeclaration(identifier);

        public ConstructorDeclarationSyntax ConstructorDeclaration(string identifier)
            => SyntaxFactory.ConstructorDeclaration(identifier);

        public ConstructorInitializerSyntax ConstructorInitializer(SyntaxKind kind, SyntaxToken colonToken, SyntaxToken thisOrBaseKeyword, ArgumentListSyntax argumentList)
            => SyntaxFactory.ConstructorInitializer(kind, colonToken, thisOrBaseKeyword, argumentList);

        public ConstructorInitializerSyntax ConstructorInitializer(SyntaxKind kind, ArgumentListSyntax argumentList)
            => SyntaxFactory.ConstructorInitializer(kind, argumentList);

        public DestructorDeclarationSyntax DestructorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken tildeToken, SyntaxToken identifier, ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
            => SyntaxFactory.DestructorDeclaration(attributeLists, modifiers, tildeToken, identifier, parameterList, body, expressionBody, semicolonToken);

        public DestructorDeclarationSyntax DestructorDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody)
            => SyntaxFactory.DestructorDeclaration(attributeLists, modifiers, identifier, parameterList, body, expressionBody);

        public DestructorDeclarationSyntax DestructorDeclaration(SyntaxToken identifier)
            => SyntaxFactory.DestructorDeclaration(identifier);

        public DestructorDeclarationSyntax DestructorDeclaration(string identifier)
            => SyntaxFactory.DestructorDeclaration(identifier);

        public PropertyDeclarationSyntax PropertyDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, AccessorListSyntax accessorList, ArrowExpressionClauseSyntax expressionBody, EqualsValueClauseSyntax initializer, SyntaxToken semicolonToken)
            => SyntaxFactory.PropertyDeclaration(attributeLists, modifiers, type, explicitInterfaceSpecifier, identifier, accessorList, expressionBody, initializer, semicolonToken);

        public PropertyDeclarationSyntax PropertyDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, AccessorListSyntax accessorList, ArrowExpressionClauseSyntax expressionBody, EqualsValueClauseSyntax initializer)
            => SyntaxFactory.PropertyDeclaration(attributeLists, modifiers, type, explicitInterfaceSpecifier, identifier, accessorList, expressionBody, initializer);

        public PropertyDeclarationSyntax PropertyDeclaration(TypeSyntax type, SyntaxToken identifier)
            => SyntaxFactory.PropertyDeclaration(type, identifier);

        public PropertyDeclarationSyntax PropertyDeclaration(TypeSyntax type, string identifier)
            => SyntaxFactory.PropertyDeclaration(type, identifier);

        public ArrowExpressionClauseSyntax ArrowExpressionClause(SyntaxToken arrowToken, ExpressionSyntax expression)
            => SyntaxFactory.ArrowExpressionClause(arrowToken, expression);

        public ArrowExpressionClauseSyntax ArrowExpressionClause(ExpressionSyntax expression)
            => SyntaxFactory.ArrowExpressionClause(expression);

        public EventDeclarationSyntax EventDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken eventKeyword, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, AccessorListSyntax accessorList, SyntaxToken semicolonToken)
            => SyntaxFactory.EventDeclaration(attributeLists, modifiers, eventKeyword, type, explicitInterfaceSpecifier, identifier, accessorList, semicolonToken);

        public EventDeclarationSyntax EventDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, AccessorListSyntax accessorList)
            => SyntaxFactory.EventDeclaration(attributeLists, modifiers, type, explicitInterfaceSpecifier, identifier, accessorList);

        public EventDeclarationSyntax EventDeclaration(TypeSyntax type, SyntaxToken identifier)
            => SyntaxFactory.EventDeclaration(type, identifier);

        public EventDeclarationSyntax EventDeclaration(TypeSyntax type, string identifier)
            => SyntaxFactory.EventDeclaration(type, identifier);

        public IndexerDeclarationSyntax IndexerDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken thisKeyword, BracketedParameterListSyntax parameterList, AccessorListSyntax accessorList, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
            => SyntaxFactory.IndexerDeclaration(attributeLists, modifiers, type, explicitInterfaceSpecifier, thisKeyword, parameterList, accessorList, expressionBody, semicolonToken);

        public IndexerDeclarationSyntax IndexerDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, BracketedParameterListSyntax parameterList, AccessorListSyntax accessorList, ArrowExpressionClauseSyntax expressionBody)
            => SyntaxFactory.IndexerDeclaration(attributeLists, modifiers, type, explicitInterfaceSpecifier, parameterList, accessorList, expressionBody);

        public IndexerDeclarationSyntax IndexerDeclaration(TypeSyntax type)
            => SyntaxFactory.IndexerDeclaration(type);

        public AccessorListSyntax AccessorList(SyntaxToken openBraceToken, SyntaxList<AccessorDeclarationSyntax> accessors, SyntaxToken closeBraceToken)
            => SyntaxFactory.AccessorList(openBraceToken, accessors, closeBraceToken);

        public AccessorListSyntax AccessorList(SyntaxList<AccessorDeclarationSyntax> accessors)
            => SyntaxFactory.AccessorList(accessors);

        public AccessorDeclarationSyntax AccessorDeclaration(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
            => SyntaxFactory.AccessorDeclaration(kind, attributeLists, modifiers, keyword, body, expressionBody, semicolonToken);

        public AccessorDeclarationSyntax AccessorDeclaration(SyntaxKind kind, SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody)
            => SyntaxFactory.AccessorDeclaration(kind, attributeLists, modifiers, body, expressionBody);

        public AccessorDeclarationSyntax AccessorDeclaration(SyntaxKind kind)
            => SyntaxFactory.AccessorDeclaration(kind);

        public ParameterListSyntax ParameterList(SyntaxToken openParenToken, SeparatedSyntaxList<ParameterSyntax> parameters, SyntaxToken closeParenToken)
            => SyntaxFactory.ParameterList(openParenToken, parameters, closeParenToken);

        public ParameterListSyntax ParameterList(SeparatedSyntaxList<ParameterSyntax> parameters)
            => SyntaxFactory.ParameterList(parameters);

        public BracketedParameterListSyntax BracketedParameterList(SyntaxToken openBracketToken, SeparatedSyntaxList<ParameterSyntax> parameters, SyntaxToken closeBracketToken)
            => SyntaxFactory.BracketedParameterList(openBracketToken, parameters, closeBracketToken);

        public BracketedParameterListSyntax BracketedParameterList(SeparatedSyntaxList<ParameterSyntax> parameters)
            => SyntaxFactory.BracketedParameterList(parameters);

        public ParameterSyntax Parameter(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax type, SyntaxToken identifier, EqualsValueClauseSyntax? @default)
            => SyntaxFactory.Parameter(attributeLists, modifiers, type, identifier, @default);

        public ParameterSyntax Parameter(SyntaxToken identifier)
            => SyntaxFactory.Parameter(identifier);

        public ParameterSyntax Parameter(TypeSyntax type, string identifier)
            => Parameter(type, Identifier(identifier));

        public ParameterSyntax Parameter(TypeSyntax type, SyntaxToken identifier)
            => Parameter(identifier).WithType(type);

        public FunctionPointerParameterSyntax FunctionPointerParameter(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax type)
            => SyntaxFactory.FunctionPointerParameter(attributeLists, modifiers, type);

        public FunctionPointerParameterSyntax FunctionPointerParameter(TypeSyntax type)
            => SyntaxFactory.FunctionPointerParameter(type);

        public IncompleteMemberSyntax IncompleteMember(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax type)
            => SyntaxFactory.IncompleteMember(attributeLists, modifiers, type);

        public IncompleteMemberSyntax IncompleteMember(TypeSyntax type)
            => SyntaxFactory.IncompleteMember(type);

        public SkippedTokensTriviaSyntax SkippedTokensTrivia(SyntaxTokenList tokens)
            => SyntaxFactory.SkippedTokensTrivia(tokens);

        public SkippedTokensTriviaSyntax SkippedTokensTrivia()
            => SyntaxFactory.SkippedTokensTrivia();

        public DocumentationCommentTriviaSyntax DocumentationCommentTrivia(SyntaxKind kind, SyntaxList<XmlNodeSyntax> content, SyntaxToken endOfComment)
            => SyntaxFactory.DocumentationCommentTrivia(kind, content, endOfComment);

        public DocumentationCommentTriviaSyntax DocumentationCommentTrivia(SyntaxKind kind, SyntaxList<XmlNodeSyntax> content)
            => SyntaxFactory.DocumentationCommentTrivia(kind, content);

        public TypeCrefSyntax TypeCref(TypeSyntax type)
            => SyntaxFactory.TypeCref(type);

        public QualifiedCrefSyntax QualifiedCref(TypeSyntax container, SyntaxToken dotToken, MemberCrefSyntax member)
            => SyntaxFactory.QualifiedCref(container, dotToken, member);

        public QualifiedCrefSyntax QualifiedCref(TypeSyntax container, MemberCrefSyntax member)
            => SyntaxFactory.QualifiedCref(container, member);

        public NameMemberCrefSyntax NameMemberCref(TypeSyntax name, CrefParameterListSyntax parameters)
            => SyntaxFactory.NameMemberCref(name, parameters);

        public NameMemberCrefSyntax NameMemberCref(TypeSyntax name)
            => SyntaxFactory.NameMemberCref(name);

        public IndexerMemberCrefSyntax IndexerMemberCref(SyntaxToken thisKeyword, CrefBracketedParameterListSyntax parameters)
            => SyntaxFactory.IndexerMemberCref(thisKeyword, parameters);

        public IndexerMemberCrefSyntax IndexerMemberCref(CrefBracketedParameterListSyntax parameters)
            => SyntaxFactory.IndexerMemberCref(parameters);

        public OperatorMemberCrefSyntax OperatorMemberCref(SyntaxToken operatorKeyword, SyntaxToken checkedKeyword, SyntaxToken operatorToken, CrefParameterListSyntax parameters)
            => SyntaxFactory.OperatorMemberCref(operatorKeyword, checkedKeyword, operatorToken, parameters);

        public OperatorMemberCrefSyntax OperatorMemberCref(SyntaxToken operatorToken, CrefParameterListSyntax parameters)
            => SyntaxFactory.OperatorMemberCref(operatorToken, parameters);

        public OperatorMemberCrefSyntax OperatorMemberCref(SyntaxToken operatorToken)
            => SyntaxFactory.OperatorMemberCref(operatorToken);

        public ConversionOperatorMemberCrefSyntax ConversionOperatorMemberCref(SyntaxToken implicitOrExplicitKeyword, SyntaxToken operatorKeyword, SyntaxToken checkedKeyword, TypeSyntax type, CrefParameterListSyntax parameters)
            => SyntaxFactory.ConversionOperatorMemberCref(implicitOrExplicitKeyword, operatorKeyword, checkedKeyword, type, parameters);

        public ConversionOperatorMemberCrefSyntax ConversionOperatorMemberCref(SyntaxToken implicitOrExplicitKeyword, TypeSyntax type, CrefParameterListSyntax parameters)
            => SyntaxFactory.ConversionOperatorMemberCref(implicitOrExplicitKeyword, type, parameters);

        public ConversionOperatorMemberCrefSyntax ConversionOperatorMemberCref(SyntaxToken implicitOrExplicitKeyword, TypeSyntax type)
            => SyntaxFactory.ConversionOperatorMemberCref(implicitOrExplicitKeyword, type);

        public CrefParameterListSyntax CrefParameterList(SyntaxToken openParenToken, SeparatedSyntaxList<CrefParameterSyntax> parameters, SyntaxToken closeParenToken)
            => SyntaxFactory.CrefParameterList(openParenToken, parameters, closeParenToken);

        public CrefParameterListSyntax CrefParameterList(SeparatedSyntaxList<CrefParameterSyntax> parameters)
            => SyntaxFactory.CrefParameterList(parameters);

        public CrefBracketedParameterListSyntax CrefBracketedParameterList(SyntaxToken openBracketToken, SeparatedSyntaxList<CrefParameterSyntax> parameters, SyntaxToken closeBracketToken)
            => SyntaxFactory.CrefBracketedParameterList(openBracketToken, parameters, closeBracketToken);

        public CrefBracketedParameterListSyntax CrefBracketedParameterList(SeparatedSyntaxList<CrefParameterSyntax> parameters)
            => SyntaxFactory.CrefBracketedParameterList(parameters);

        public CrefParameterSyntax CrefParameter(SyntaxToken refKindKeyword, SyntaxToken readOnlyKeyword, TypeSyntax type)
            => SyntaxFactory.CrefParameter(refKindKeyword, readOnlyKeyword, type);

        public CrefParameterSyntax CrefParameter(SyntaxToken refKindKeyword, TypeSyntax type)
            => SyntaxFactory.CrefParameter(refKindKeyword, type);

        public CrefParameterSyntax CrefParameter(TypeSyntax type)
            => SyntaxFactory.CrefParameter(type);

        public XmlElementSyntax XmlElement(XmlElementStartTagSyntax startTag, SyntaxList<XmlNodeSyntax> content, XmlElementEndTagSyntax endTag)
            => SyntaxFactory.XmlElement(startTag, content, endTag);

        public XmlElementSyntax XmlElement(XmlElementStartTagSyntax startTag, XmlElementEndTagSyntax endTag)
            => SyntaxFactory.XmlElement(startTag, endTag);

        public XmlElementStartTagSyntax XmlElementStartTag(SyntaxToken lessThanToken, XmlNameSyntax name, SyntaxList<XmlAttributeSyntax> attributes, SyntaxToken greaterThanToken)
            => SyntaxFactory.XmlElementStartTag(lessThanToken, name, attributes, greaterThanToken);

        public XmlElementStartTagSyntax XmlElementStartTag(XmlNameSyntax name, SyntaxList<XmlAttributeSyntax> attributes)
            => SyntaxFactory.XmlElementStartTag(name, attributes);

        public XmlElementStartTagSyntax XmlElementStartTag(XmlNameSyntax name)
            => SyntaxFactory.XmlElementStartTag(name);

        public XmlElementEndTagSyntax XmlElementEndTag(SyntaxToken lessThanSlashToken, XmlNameSyntax name, SyntaxToken greaterThanToken)
            => SyntaxFactory.XmlElementEndTag(lessThanSlashToken, name, greaterThanToken);

        public XmlElementEndTagSyntax XmlElementEndTag(XmlNameSyntax name)
            => SyntaxFactory.XmlElementEndTag(name);

        public XmlEmptyElementSyntax XmlEmptyElement(SyntaxToken lessThanToken, XmlNameSyntax name, SyntaxList<XmlAttributeSyntax> attributes, SyntaxToken slashGreaterThanToken)
            => SyntaxFactory.XmlEmptyElement(lessThanToken, name, attributes, slashGreaterThanToken);

        public XmlEmptyElementSyntax XmlEmptyElement(XmlNameSyntax name, SyntaxList<XmlAttributeSyntax> attributes)
            => SyntaxFactory.XmlEmptyElement(name, attributes);

        public XmlEmptyElementSyntax XmlEmptyElement(XmlNameSyntax name)
            => SyntaxFactory.XmlEmptyElement(name);

        public XmlNameSyntax XmlName(XmlPrefixSyntax prefix, SyntaxToken localName)
            => SyntaxFactory.XmlName(prefix, localName);

        public XmlNameSyntax XmlName(SyntaxToken localName)
            => SyntaxFactory.XmlName(localName);

        public XmlNameSyntax XmlName(string localName)
            => SyntaxFactory.XmlName(localName);

        public XmlPrefixSyntax XmlPrefix(SyntaxToken prefix, SyntaxToken colonToken)
            => SyntaxFactory.XmlPrefix(prefix, colonToken);

        public XmlPrefixSyntax XmlPrefix(SyntaxToken prefix)
            => SyntaxFactory.XmlPrefix(prefix);

        public XmlPrefixSyntax XmlPrefix(string prefix)
            => SyntaxFactory.XmlPrefix(prefix);

        public XmlTextAttributeSyntax XmlTextAttribute(XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, SyntaxTokenList textTokens, SyntaxToken endQuoteToken)
            => SyntaxFactory.XmlTextAttribute(name, equalsToken, startQuoteToken, textTokens, endQuoteToken);

        public XmlTextAttributeSyntax XmlTextAttribute(XmlNameSyntax name, SyntaxToken startQuoteToken, SyntaxTokenList textTokens, SyntaxToken endQuoteToken)
            => SyntaxFactory.XmlTextAttribute(name, startQuoteToken, textTokens, endQuoteToken);

        public XmlTextAttributeSyntax XmlTextAttribute(XmlNameSyntax name, SyntaxToken startQuoteToken, SyntaxToken endQuoteToken)
            => SyntaxFactory.XmlTextAttribute(name, startQuoteToken, endQuoteToken);

        public XmlCrefAttributeSyntax XmlCrefAttribute(XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, CrefSyntax cref, SyntaxToken endQuoteToken)
            => SyntaxFactory.XmlCrefAttribute(name, equalsToken, startQuoteToken, cref, endQuoteToken);

        public XmlCrefAttributeSyntax XmlCrefAttribute(XmlNameSyntax name, SyntaxToken startQuoteToken, CrefSyntax cref, SyntaxToken endQuoteToken)
            => SyntaxFactory.XmlCrefAttribute(name, startQuoteToken, cref, endQuoteToken);

        public XmlNameAttributeSyntax XmlNameAttribute(XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, IdentifierNameSyntax identifier, SyntaxToken endQuoteToken)
            => SyntaxFactory.XmlNameAttribute(name, equalsToken, startQuoteToken, identifier, endQuoteToken);

        public XmlNameAttributeSyntax XmlNameAttribute(XmlNameSyntax name, SyntaxToken startQuoteToken, IdentifierNameSyntax identifier, SyntaxToken endQuoteToken)
            => SyntaxFactory.XmlNameAttribute(name, startQuoteToken, identifier, endQuoteToken);

        public XmlNameAttributeSyntax XmlNameAttribute(XmlNameSyntax name, SyntaxToken startQuoteToken, string identifier, SyntaxToken endQuoteToken)
            => SyntaxFactory.XmlNameAttribute(name, startQuoteToken, identifier, endQuoteToken);

        public XmlTextSyntax XmlText(SyntaxTokenList textTokens)
            => SyntaxFactory.XmlText(textTokens);

        public XmlTextSyntax XmlText()
            => SyntaxFactory.XmlText();

        public XmlCDataSectionSyntax XmlCDataSection(SyntaxToken startCDataToken, SyntaxTokenList textTokens, SyntaxToken endCDataToken)
            => SyntaxFactory.XmlCDataSection(startCDataToken, textTokens, endCDataToken);

        public XmlCDataSectionSyntax XmlCDataSection(SyntaxTokenList textTokens)
            => SyntaxFactory.XmlCDataSection(textTokens);

        public XmlProcessingInstructionSyntax XmlProcessingInstruction(SyntaxToken startProcessingInstructionToken, XmlNameSyntax name, SyntaxTokenList textTokens, SyntaxToken endProcessingInstructionToken)
            => SyntaxFactory.XmlProcessingInstruction(startProcessingInstructionToken, name, textTokens, endProcessingInstructionToken);

        public XmlProcessingInstructionSyntax XmlProcessingInstruction(XmlNameSyntax name, SyntaxTokenList textTokens)
            => SyntaxFactory.XmlProcessingInstruction(name, textTokens);

        public XmlProcessingInstructionSyntax XmlProcessingInstruction(XmlNameSyntax name)
            => SyntaxFactory.XmlProcessingInstruction(name);

        public XmlCommentSyntax XmlComment(SyntaxToken lessThanExclamationMinusMinusToken, SyntaxTokenList textTokens, SyntaxToken minusMinusGreaterThanToken)
            => SyntaxFactory.XmlComment(lessThanExclamationMinusMinusToken, textTokens, minusMinusGreaterThanToken);

        public XmlCommentSyntax XmlComment(SyntaxTokenList textTokens)
            => SyntaxFactory.XmlComment(textTokens);

        public IfDirectiveTriviaSyntax IfDirectiveTrivia(SyntaxToken hashToken, SyntaxToken ifKeyword, ExpressionSyntax condition, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken, bool conditionValue)
            => SyntaxFactory.IfDirectiveTrivia(hashToken, ifKeyword, condition, endOfDirectiveToken, isActive, branchTaken, conditionValue);

        public IfDirectiveTriviaSyntax IfDirectiveTrivia(ExpressionSyntax condition, bool isActive, bool branchTaken, bool conditionValue)
            => SyntaxFactory.IfDirectiveTrivia(condition, isActive, branchTaken, conditionValue);

        public ElifDirectiveTriviaSyntax ElifDirectiveTrivia(SyntaxToken hashToken, SyntaxToken elifKeyword, ExpressionSyntax condition, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken, bool conditionValue)
            => SyntaxFactory.ElifDirectiveTrivia(hashToken, elifKeyword, condition, endOfDirectiveToken, isActive, branchTaken, conditionValue);

        public ElifDirectiveTriviaSyntax ElifDirectiveTrivia(ExpressionSyntax condition, bool isActive, bool branchTaken, bool conditionValue)
            => SyntaxFactory.ElifDirectiveTrivia(condition, isActive, branchTaken, conditionValue);

        public ElseDirectiveTriviaSyntax ElseDirectiveTrivia(SyntaxToken hashToken, SyntaxToken elseKeyword, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken)
            => SyntaxFactory.ElseDirectiveTrivia(hashToken, elseKeyword, endOfDirectiveToken, isActive, branchTaken);

        public ElseDirectiveTriviaSyntax ElseDirectiveTrivia(bool isActive, bool branchTaken)
            => SyntaxFactory.ElseDirectiveTrivia(isActive, branchTaken);

        public EndIfDirectiveTriviaSyntax EndIfDirectiveTrivia(SyntaxToken hashToken, SyntaxToken endIfKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
            => SyntaxFactory.EndIfDirectiveTrivia(hashToken, endIfKeyword, endOfDirectiveToken, isActive);

        public EndIfDirectiveTriviaSyntax EndIfDirectiveTrivia(bool isActive)
            => SyntaxFactory.EndIfDirectiveTrivia(isActive);

        public RegionDirectiveTriviaSyntax RegionDirectiveTrivia(SyntaxToken hashToken, SyntaxToken regionKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
            => SyntaxFactory.RegionDirectiveTrivia(hashToken, regionKeyword, endOfDirectiveToken, isActive);

        public RegionDirectiveTriviaSyntax RegionDirectiveTrivia(bool isActive)
            => SyntaxFactory.RegionDirectiveTrivia(isActive);

        public EndRegionDirectiveTriviaSyntax EndRegionDirectiveTrivia(SyntaxToken hashToken, SyntaxToken endRegionKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
            => SyntaxFactory.EndRegionDirectiveTrivia(hashToken, endRegionKeyword, endOfDirectiveToken, isActive);

        public EndRegionDirectiveTriviaSyntax EndRegionDirectiveTrivia(bool isActive)
            => SyntaxFactory.EndRegionDirectiveTrivia(isActive);

        public ErrorDirectiveTriviaSyntax ErrorDirectiveTrivia(SyntaxToken hashToken, SyntaxToken errorKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
            => SyntaxFactory.ErrorDirectiveTrivia(hashToken, errorKeyword, endOfDirectiveToken, isActive);

        public ErrorDirectiveTriviaSyntax ErrorDirectiveTrivia(bool isActive)
            => SyntaxFactory.ErrorDirectiveTrivia(isActive);

        public WarningDirectiveTriviaSyntax WarningDirectiveTrivia(SyntaxToken hashToken, SyntaxToken warningKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
            => SyntaxFactory.WarningDirectiveTrivia(hashToken, warningKeyword, endOfDirectiveToken, isActive);

        public WarningDirectiveTriviaSyntax WarningDirectiveTrivia(bool isActive)
            => SyntaxFactory.WarningDirectiveTrivia(isActive);

        public BadDirectiveTriviaSyntax BadDirectiveTrivia(SyntaxToken hashToken, SyntaxToken identifier, SyntaxToken endOfDirectiveToken, bool isActive)
            => SyntaxFactory.BadDirectiveTrivia(hashToken, identifier, endOfDirectiveToken, isActive);

        public BadDirectiveTriviaSyntax BadDirectiveTrivia(SyntaxToken identifier, bool isActive)
            => SyntaxFactory.BadDirectiveTrivia(identifier, isActive);

        public DefineDirectiveTriviaSyntax DefineDirectiveTrivia(SyntaxToken hashToken, SyntaxToken defineKeyword, SyntaxToken name, SyntaxToken endOfDirectiveToken, bool isActive)
            => SyntaxFactory.DefineDirectiveTrivia(hashToken, defineKeyword, name, endOfDirectiveToken, isActive);

        public DefineDirectiveTriviaSyntax DefineDirectiveTrivia(SyntaxToken name, bool isActive)
            => SyntaxFactory.DefineDirectiveTrivia(name, isActive);

        public DefineDirectiveTriviaSyntax DefineDirectiveTrivia(string name, bool isActive)
            => SyntaxFactory.DefineDirectiveTrivia(name, isActive);

        public UndefDirectiveTriviaSyntax UndefDirectiveTrivia(SyntaxToken hashToken, SyntaxToken undefKeyword, SyntaxToken name, SyntaxToken endOfDirectiveToken, bool isActive)
            => SyntaxFactory.UndefDirectiveTrivia(hashToken, undefKeyword, name, endOfDirectiveToken, isActive);

        public UndefDirectiveTriviaSyntax UndefDirectiveTrivia(SyntaxToken name, bool isActive)
            => SyntaxFactory.UndefDirectiveTrivia(name, isActive);

        public UndefDirectiveTriviaSyntax UndefDirectiveTrivia(string name, bool isActive)
            => SyntaxFactory.UndefDirectiveTrivia(name, isActive);

        public LineDirectiveTriviaSyntax LineDirectiveTrivia(SyntaxToken hashToken, SyntaxToken lineKeyword, SyntaxToken line, SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive)
            => SyntaxFactory.LineDirectiveTrivia(hashToken, lineKeyword, line, file, endOfDirectiveToken, isActive);

        public LineDirectiveTriviaSyntax LineDirectiveTrivia(SyntaxToken line, SyntaxToken file, bool isActive)
            => SyntaxFactory.LineDirectiveTrivia(line, file, isActive);

        public LineDirectiveTriviaSyntax LineDirectiveTrivia(SyntaxToken line, bool isActive)
            => SyntaxFactory.LineDirectiveTrivia(line, isActive);

        public LineDirectivePositionSyntax LineDirectivePosition(SyntaxToken openParenToken, SyntaxToken line, SyntaxToken commaToken, SyntaxToken character, SyntaxToken closeParenToken)
            => SyntaxFactory.LineDirectivePosition(openParenToken, line, commaToken, character, closeParenToken);

        public LineDirectivePositionSyntax LineDirectivePosition(SyntaxToken line, SyntaxToken character)
            => SyntaxFactory.LineDirectivePosition(line, character);

        public LineSpanDirectiveTriviaSyntax LineSpanDirectiveTrivia(SyntaxToken hashToken, SyntaxToken lineKeyword, LineDirectivePositionSyntax start, SyntaxToken minusToken, LineDirectivePositionSyntax end, SyntaxToken characterOffset, SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive)
            => SyntaxFactory.LineSpanDirectiveTrivia(hashToken, lineKeyword, start, minusToken, end, characterOffset, file, endOfDirectiveToken, isActive);

        public LineSpanDirectiveTriviaSyntax LineSpanDirectiveTrivia(LineDirectivePositionSyntax start, LineDirectivePositionSyntax end, SyntaxToken characterOffset, SyntaxToken file, bool isActive)
            => SyntaxFactory.LineSpanDirectiveTrivia(start, end, characterOffset, file, isActive);

        public LineSpanDirectiveTriviaSyntax LineSpanDirectiveTrivia(LineDirectivePositionSyntax start, LineDirectivePositionSyntax end, SyntaxToken file, bool isActive)
            => SyntaxFactory.LineSpanDirectiveTrivia(start, end, file, isActive);

        public PragmaWarningDirectiveTriviaSyntax PragmaWarningDirectiveTrivia(SyntaxToken hashToken, SyntaxToken pragmaKeyword, SyntaxToken warningKeyword, SyntaxToken disableOrRestoreKeyword, SeparatedSyntaxList<ExpressionSyntax> errorCodes, SyntaxToken endOfDirectiveToken, bool isActive)
            => SyntaxFactory.PragmaWarningDirectiveTrivia(hashToken, pragmaKeyword, warningKeyword, disableOrRestoreKeyword, errorCodes, endOfDirectiveToken, isActive);

        public PragmaWarningDirectiveTriviaSyntax PragmaWarningDirectiveTrivia(SyntaxToken disableOrRestoreKeyword, SeparatedSyntaxList<ExpressionSyntax> errorCodes, bool isActive)
            => SyntaxFactory.PragmaWarningDirectiveTrivia(disableOrRestoreKeyword, errorCodes, isActive);

        public PragmaWarningDirectiveTriviaSyntax PragmaWarningDirectiveTrivia(SyntaxToken disableOrRestoreKeyword, bool isActive)
            => SyntaxFactory.PragmaWarningDirectiveTrivia(disableOrRestoreKeyword, isActive);

        public PragmaChecksumDirectiveTriviaSyntax PragmaChecksumDirectiveTrivia(SyntaxToken hashToken, SyntaxToken pragmaKeyword, SyntaxToken checksumKeyword, SyntaxToken file, SyntaxToken guid, SyntaxToken bytes, SyntaxToken endOfDirectiveToken, bool isActive)
            => SyntaxFactory.PragmaChecksumDirectiveTrivia(hashToken, pragmaKeyword, checksumKeyword, file, guid, bytes, endOfDirectiveToken, isActive);

        public PragmaChecksumDirectiveTriviaSyntax PragmaChecksumDirectiveTrivia(SyntaxToken file, SyntaxToken guid, SyntaxToken bytes, bool isActive)
            => SyntaxFactory.PragmaChecksumDirectiveTrivia(file, guid, bytes, isActive);

        public ReferenceDirectiveTriviaSyntax ReferenceDirectiveTrivia(SyntaxToken hashToken, SyntaxToken referenceKeyword, SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive)
            => SyntaxFactory.ReferenceDirectiveTrivia(hashToken, referenceKeyword, file, endOfDirectiveToken, isActive);

        public ReferenceDirectiveTriviaSyntax ReferenceDirectiveTrivia(SyntaxToken file, bool isActive)
            => SyntaxFactory.ReferenceDirectiveTrivia(file, isActive);

        public LoadDirectiveTriviaSyntax LoadDirectiveTrivia(SyntaxToken hashToken, SyntaxToken loadKeyword, SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive)
            => SyntaxFactory.LoadDirectiveTrivia(hashToken, loadKeyword, file, endOfDirectiveToken, isActive);

        public LoadDirectiveTriviaSyntax LoadDirectiveTrivia(SyntaxToken file, bool isActive)
            => SyntaxFactory.LoadDirectiveTrivia(file, isActive);

        public ShebangDirectiveTriviaSyntax ShebangDirectiveTrivia(SyntaxToken hashToken, SyntaxToken exclamationToken, SyntaxToken endOfDirectiveToken, bool isActive)
            => SyntaxFactory.ShebangDirectiveTrivia(hashToken, exclamationToken, endOfDirectiveToken, isActive);

        public ShebangDirectiveTriviaSyntax ShebangDirectiveTrivia(bool isActive)
            => SyntaxFactory.ShebangDirectiveTrivia(isActive);

        public NullableDirectiveTriviaSyntax NullableDirectiveTrivia(SyntaxToken hashToken, SyntaxToken nullableKeyword, SyntaxToken settingToken, SyntaxToken targetToken, SyntaxToken endOfDirectiveToken, bool isActive)
            => SyntaxFactory.NullableDirectiveTrivia(hashToken, nullableKeyword, settingToken, targetToken, endOfDirectiveToken, isActive);

        public NullableDirectiveTriviaSyntax NullableDirectiveTrivia(SyntaxToken settingToken, SyntaxToken targetToken, bool isActive)
            => SyntaxFactory.NullableDirectiveTrivia(settingToken, targetToken, isActive);

        public NullableDirectiveTriviaSyntax NullableDirectiveTrivia(SyntaxToken settingToken, bool isActive)
            => SyntaxFactory.NullableDirectiveTrivia(settingToken, isActive);
    }
}
