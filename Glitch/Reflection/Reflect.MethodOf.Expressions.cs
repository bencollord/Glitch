using Glitch.Functional;
using Glitch.Functional.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Glitch.Reflection
{
    public static partial class Reflect
    {
        public static MethodInfo MethodOf(Expression<Action> expression) => GetMethod(expression);
        public static MethodInfo MethodOf(Expression<Func<object>> expression) => GetMethod(expression);
        public static MethodInfo MethodOf<T>(Expression<Action<T>> expression) => GetMethod(expression);
        public static MethodInfo MethodOf<T>(Expression<Func<T, object>> expression) => GetMethod(expression);

        public static Expected<MethodInfo> TryMethodOf(Expression<Action> expression) => TryGetMethod(expression);
        public static Expected<MethodInfo> TryMethodOf(Expression<Func<object>> expression) => TryGetMethod(expression);
        public static Expected<MethodInfo> TryMethodOf<T>(Expression<Action<T>> expression) => TryGetMethod(expression);
        public static Expected<MethodInfo> TryMethodOf<T>(Expression<Func<T, object>> expression) => TryGetMethod(expression);

        private static MethodInfo GetMethod(LambdaExpression expression)
            => TryGetMethod(expression)
                   .UnwrapOrElse(e => throw new ArgumentException(e.Message));

        private static Expected<MethodInfo> TryGetMethod(LambdaExpression expression)
        {
            var body = new UnwrapMethodCallVisitor().Visit(expression.Body);

            if (body is MethodCallExpression call)
            {
                return call.Method;
            }

            return Expected.Fail($"Expression was not a valid method. Expression: {expression}");
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
