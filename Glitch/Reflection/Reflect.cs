using System.Linq.Expressions;
using System.Reflection;

namespace Glitch.Reflection;

public static class Reflect
{
    public static FieldInfo FieldOf(Expression<Action> expression) => GetMember<FieldInfo>(expression);
    public static FieldInfo FieldOf(Expression<Func<object>> expression) => GetMember<FieldInfo>(expression);
    public static FieldInfo FieldOf<T>(Expression<Action<T>> expression) => GetMember<FieldInfo>(expression);
    public static FieldInfo FieldOf<T>(Expression<Func<T, object>> expression) => GetMember<FieldInfo>(expression);

    public static PropertyInfo PropertyOf(Expression<Action> expression) => GetMember<PropertyInfo>(expression);
    public static PropertyInfo PropertyOf(Expression<Func<object>> expression) => GetMember<PropertyInfo>(expression);
    public static PropertyInfo PropertyOf<T>(Expression<Action<T>> expression) => GetMember<PropertyInfo>(expression);
    public static PropertyInfo PropertyOf<T>(Expression<Func<T, object>> expression) => GetMember<PropertyInfo>(expression);

    public static MethodInfo MethodOf(Expression<Action> expression) => GetMember<MethodInfo>(expression);
    public static MethodInfo MethodOf(Expression<Func<object>> expression) => GetMember<MethodInfo>(expression);
    public static MethodInfo MethodOf<T>(Expression<Action<T>> expression) => GetMember<MethodInfo>(expression);
    public static MethodInfo MethodOf<T>(Expression<Func<T, object>> expression) => GetMember<MethodInfo>(expression);

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
