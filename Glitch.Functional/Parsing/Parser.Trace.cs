namespace Glitch.Functional.Parsing
{
    public partial class Parser<TToken, T>
    {
        public Parser<TToken, T> Trace()
            => new(input =>
            {
                var r = parser(input);

                Console.WriteLine(r.ToString());

                return r;
            });

        public Parser<TToken, T> Trace(string message)
            => Trace(_ => message);

        public Parser<TToken, T> Trace(Func<T, string> formatter)
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
