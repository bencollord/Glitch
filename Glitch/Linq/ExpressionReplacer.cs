using Glitch.Functional;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Glitch.Linq
{
    public class ExpressionReplacer : ExpressionVisitor
    {
        private readonly Dictionary<Expression, Expression> replacementMap = new();

        public ExpressionReplacer Replace(Expression original, Expression replacement)
        {
            replacementMap[original] = replacement;
            return this;
        }

        [return: NotNullIfNotNull(nameof(node))]
        public override Expression? Visit(Expression? node)
        {
            return Maybe(node)
                .AndThen(expr => replacementMap.TryGetValue(expr))
                .DefaultIfNone(() => base.Visit(node));
        }
    }
}
