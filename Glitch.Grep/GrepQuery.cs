using Glitch.Grep.Internal;
using System.Collections;

namespace Glitch.Grep
{
    public class GrepQuery : IEnumerable<FileLine>
    {
        private readonly GrepTraversal traversal;
        private readonly GrepFilter filter;

        internal GrepQuery(GrepTraversal traversal, GrepFilter filter)
        {
            this.traversal = traversal;
            this.filter = filter;
        }

        public GrepQuery FixedString() => WithFilter(filter.AsFixedString());
        public GrepQuery RegularExpression() => WithFilter(filter.AsRegularExpression());
        public GrepQuery IgnoreCase() => WithFilter(filter.IgnoreCase());
        public GrepQuery MatchCase() => WithFilter(filter.AsCaseSensitive());
        public GrepQuery MatchWholeLine() => WithFilter(filter.MatchWholeLine());
        public GrepQuery AllowPartialMatch() => WithFilter(filter.AllowPartialMatch());
        public GrepQuery LocaleAware() => WithFilter(filter.LocaleAware());
        public GrepQuery LocaleAgnostic() => WithFilter(filter.LocaleAgnostic());

        public IEnumerator<FileLine> GetEnumerator()
        {
            //return traversal.Execute();
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private GrepQuery WithFilter(GrepFilter filter) 
            => filter == this.filter ? this : new GrepQuery(traversal, filter);

        private GrepQuery WithTraversal(GrepTraversal traversal)
            => traversal == this.traversal ? this : new GrepQuery(traversal, filter);
    }
}