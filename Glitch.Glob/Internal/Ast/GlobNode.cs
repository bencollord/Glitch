namespace Glitch.Glob.Internal.Ast
{
    internal abstract class GlobNode
    {
        internal GlobMatch Match(string input) => Match(input, 0);

        internal abstract GlobMatch Match(string input, int pos);

        public abstract override string ToString();

        private protected static StringComparison MapComparison(GlobOptions options)
        {
            return options switch
            {
                _ when options.HasFlag(GlobOptions.LocaleAware)
                    && options.HasFlag(GlobOptions.IgnoreCase)
                        => StringComparison.CurrentCultureIgnoreCase,
                _ when options.HasFlag(GlobOptions.LocaleAware)
                        => StringComparison.CurrentCulture,
                _ when options.HasFlag(GlobOptions.IgnoreCase)
                        => StringComparison.OrdinalIgnoreCase,
                _ => StringComparison.Ordinal
            };
        }
    }
}
