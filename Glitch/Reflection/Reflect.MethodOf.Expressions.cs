using System.Linq.Expressions;
using System.Reflection;

namespace Glitch.Reflection
{
    public static partial class Reflect
    {
        public static MethodInfo MethodOf(Expression<Action> expression) => GetMethod(expression);
        public static MethodInfo MethodOf(Expression<Func<object>> expression) => GetMethod(expression);
        public static MethodInfo MethodOf<T>(Expression<Action<T>> expression) => GetMethod(expression);
        public static MethodInfo MethodOf<T>(Expression<Func<T, object>> expression) => GetMethod(expression);

        private static MethodInfo GetMethod(LambdaExpression expression)
        {
            var body = new UnwrapMethodCallVisitor().Visit(expression.Body);

            if (body is MethodCallExpression call)
            {
                return call.Method;
            }

            throw new ArgumentException($"Expression was not a valid method. Expression: {expression}");
        }

        private class UnwrapMethodCallVisitor : ExpressionVisitor
        {
            protected override Expression VisitMethodCall(MethodCallExpression node)
            {
                // Short circuit traversal after we see the first call
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
}
