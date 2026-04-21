using System.Linq.Expressions;
using System.Reflection;

namespace Glitch.Reflection;

public static class Reflect
{
    public static FieldInfo FieldOf(LambdaExpression expression) => GetMember<FieldInfo>(expression);
    public static FieldInfo FieldOf(Expression<Action> expression) => GetMember<FieldInfo>(expression);
    public static FieldInfo FieldOf(Expression<Func<object>> expression) => GetMember<FieldInfo>(expression);
    public static FieldInfo FieldOf<T>(Expression<Action<T>> expression) => GetMember<FieldInfo>(expression);
    public static FieldInfo FieldOf<TArg, T>(Expression<Func<TArg, T>> expression) => GetMember<FieldInfo>(expression);
    public static FieldInfo FieldOf<TArg>(Expression<Func<TArg, object?>> expression) => GetMember<FieldInfo>(expression);

    public static PropertyInfo PropertyOf(LambdaExpression expression) => GetMember<PropertyInfo>(expression);
    public static PropertyInfo PropertyOf(Expression<Action> expression) => GetMember<PropertyInfo>(expression);
    public static PropertyInfo PropertyOf<T>(Expression<Func<T>> expression) => GetMember<PropertyInfo>(expression);
    public static PropertyInfo PropertyOf<T>(Expression<Action<T>> expression) => GetMember<PropertyInfo>(expression);
    public static PropertyInfo PropertyOf<TArg, T>(Expression<Func<TArg, T>> expression) => GetMember<PropertyInfo>(expression);
    public static PropertyInfo PropertyOf<TArg>(Expression<Func<TArg, object?>> expression) => GetMember<PropertyInfo>(expression);

    public static MethodInfo MethodOf(LambdaExpression expression) => GetMember<MethodInfo>(expression);
    public static MethodInfo MethodOf(Expression<Action> expression) => GetMember<MethodInfo>(expression);
    public static MethodInfo MethodOf<T>(Expression<Func<T>> expression) => GetMember<MethodInfo>(expression);
    public static MethodInfo MethodOf<TArg>(Expression<Action<TArg>> expression) => GetMember<MethodInfo>(expression);
    public static MethodInfo MethodOf<TArg, T>(Expression<Func<TArg, T>> expression) => GetMember<MethodInfo>(expression);
    public static MethodInfo MethodOf<TArg>(Expression<Func<TArg, object?>> expression) => GetMember<MethodInfo>(expression);

    private static TMember GetMember<TMember>(LambdaExpression expression)
        where TMember : MemberInfo =>
        GetMember(expression) as TMember ?? throw InvalidMember(expression, nameof(TMember));

    private static MemberInfo GetMember(LambdaExpression expression)
    {
        return new StripQuotesAndConversionsVisitor().Visit(expression.Body) switch
        {
            MemberExpression member => member.Member,

            MethodCallExpression call => call.Method,

            _ => throw InvalidMember(expression)
        };
    }

    private static Exception InvalidMember(Expression expression, string memberType = nameof(MemberInfo)) =>
        new ArgumentException($"Expression was not a valid {memberType}. Expression: {expression}");

    private class StripQuotesAndConversionsVisitor : ExpressionVisitor
    {
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            // Short circuit traversal after we see the first call
            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            // Short circuit traversal after we see the first member
            return node;
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Quote:
                case ExpressionType.Convert when node.Type == typeof(object):
                    return node.Operand;

                default:
                    return base.VisitUnary(node);
            }
        }
    }
}
