namespace Glitch.Functional.Parsing
{
    public abstract partial class Parser<TToken, T>
    {
        public virtual Parser<TToken, T> Trace()
           => Trace(v => v!.ToString() ?? v.GetType().ToString());

        public virtual Parser<TToken, T> Trace(string message)
            => Trace(_ => message);

        public virtual Parser<TToken, T> Trace(Func<T, string> formatter)
            => Match(ok =>
               {
                   Console.WriteLine("Success {0} {1}, Remaining: {2}", formatter(ok.Value), ok.Expectation, ok.Remaining);
                   return ok;
               },
               err =>
               {
                   Console.WriteLine("Error {0} {1}, Remaining: {2}", err.Message, err.Expectation, err.Remaining);
                   return err;
               });
    }
}
