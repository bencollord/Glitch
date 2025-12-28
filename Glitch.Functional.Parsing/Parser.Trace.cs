using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public static partial class Parser
{
    extension<TToken, T>(IParser<TToken, T> source)
    {
        public IParser<TToken, T> Trace() => source.Trace(v => v!.ToString() ?? v.GetType().ToString());

        public IParser<TToken, T> Trace(string message) => source.Trace(_ => message);

        public IParser<TToken, T> Trace(Func<T, string> value) => source.Trace(value, err => err.ToString());

        public IParser<TToken, T> Trace(Func<T, string> value, Func<ParseError, string> error)
            => source.Match(ok =>
               {
                   Console.WriteLine("Success {0}, Remaining: {1}", value(ok.Value), ok.Remaining);
                   return ok;
               },
               err =>
               {
                   Console.WriteLine("Error {0}, Remaining: {1}", error(err.Error), err.Remaining);
                   return err;
               });
    }
}
