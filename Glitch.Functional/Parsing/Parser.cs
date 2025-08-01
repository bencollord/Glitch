using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;
using System.Collections.Immutable;

namespace Glitch.Functional.Parsing
{
    using static Parse;

    public partial class Parser<TToken, T>
    {
        private Func<TokenSequence<TToken>, ParseResult<TToken, T>> parser;

        internal Parser(Func<TokenSequence<TToken>, ParseResult<TToken, T>> parser)
        {
            this.parser = parser;
        }

        public virtual Parser<TToken, TResult> Map<TResult>(Func<T, TResult> selector)
            => new(input => parser(input).Map(selector));

        public virtual Parser<TToken, T> Filter(Func<T, bool> predicate)
           => new(input => parser(input).Filter(predicate));

        public virtual Parser<TToken, T> Guard(Func<T, bool> predicate, Func<T, Expectation<TToken>> ifFail)
            => new(input => parser(input).Guard(predicate, ifFail));

        public virtual Parser<TToken, T> Or(Parser<TToken, T> other)
            => new(input =>
            {
                var result = parser(input);

                return result.WasSuccessful ? result : other.parser(input);
            });

        public virtual Parser<TToken, Unit> Not() => new(input =>
        {
            var result = parser(input);

            return result.WasSuccessful
                 ? ParseResult.Fail<TToken, Unit>("Negated parser was successful", input)
                 : ParseResult.Okay(Unit.Value, result.Remaining);
        });

        /// <summary>
        /// Returns a new parser that returns an <see cref="Option{T}"/>
        /// on success and a successful result containing
        /// <see cref="Option{T}.None"/> on failure.
        /// </summary>
        /// <returns></returns>
        public virtual Parser<TToken, Option<T>> Maybe()
            => new(input => parser(input).Match(ok => ok.Map(Some),
                                                err => ParseResult.Okay(Option<T>.None, input)));

        /// <summary>
        /// Returns a new parser that returns an <see cref="Option{T}"/>
        /// on success without consuming any input and a successful result
        /// containing <see cref="Option{T}.None"/> on failure.
        /// </summary>
        /// <remarks>
        /// Like <see cref="Maybe()"/>, but does not advance the input further
        /// </remarks>
        /// <returns></returns>
        public virtual Parser<TToken, Option<T>> Lookahead()
            => new(input => parser(input).Match(ok => ParseResult.Okay(Some(ok.Value), input),
                                                err => ParseResult.Okay(Option<T>.None, input)));

        public virtual Parser<TToken, TResult> Match<TResult>(Func<ParseSuccess<TToken, T>, TResult> ifOkay, Func<ParseError<TToken, T>, TResult> ifFail)
            => new(input => parser(input).Match(ifOkay, ifFail));

        public virtual Parser<TToken, TResult> Match<TResult>(Func<ParseSuccess<TToken, T>, ParseResult<TToken, TResult>> ifOkay, Func<ParseError<TToken, T>, ParseResult<TToken, TResult>> ifFail)
            => new(input => parser(input).Match(ifOkay, ifFail));

        public virtual Parser<TToken, T> WithRemaining(TokenSequence<TToken> remaining)
            => Match(ok => ok with { Remaining = remaining },
                     err => err with { Remaining = remaining });

        public virtual Parser<TToken, T> Except(TToken token)
            => Except(Token(token));

        public virtual Parser<TToken, T> Except<TOther>(Parser<TToken, TOther> other)
            => new(input => other.parser(input) is ParseSuccess<TToken, TOther>(var result, _)
                          ? ParseResult.Fail<TToken, T>($"Excluded parser succeeded with {result}", input)
                          : parser(input));

        // TODO Move regions to separate partial class files

        #region Then, Before, After
        public virtual Parser<TToken, TOther> Then<TOther>(Parser<TToken, TOther> other)
            => Then(_ => other);

        public virtual Parser<TToken, TResult> Then<TElement, TResult>(Parser<TToken, TElement> next, Func<T, TElement, TResult> projection)
            => Then(x => next.Map(y => projection(x, y)));

        public virtual Parser<TToken, TResult> Then<TResult>(Func<T, Parser<TToken, TResult>> next)
            => new(input => parser(input).Match(ok => next(ok.Value).parser(ok.Remaining),
                                                err => err.Cast<TResult>() with { Remaining = input }));

        public virtual Parser<TToken, TResult> Then<TElement, TResult>(Func<T, Parser<TToken, TElement>> selector, Func<T, TElement, TResult> projection)
            => Then(x => selector(x).Map(y => projection(x, y)));

        public virtual Parser<TToken, T> Before<TOther>(Parser<TToken, TOther> parser)
            => Then(parser, (me, _) => me);

        public virtual Parser<TToken, T> After<TOther>(Parser<TToken, TOther> parser)
            => parser.Then(this, (_, me) => me);

        #endregion

        #region Between
        public virtual Parser<TToken, T> Between<TSeparator>(TToken separator)
           => Between(separator, separator);

        public virtual Parser<TToken, T> Between<TStart, TStop>(TStart start, TStop stop)
            => Between(Token(start), Token(stop));

        public virtual Parser<TToken, T> Between<TSeparator>(Parser<TToken, TSeparator> separator)
            => Between(separator, separator);

        public virtual Parser<TToken, T> Between<TStart, TStop>(Parser<TToken, TStart> start, Parser<TToken, TStop> stop)
            => from s in start
               from x in this
               from e in stop
               select x;
        #endregion

        #region Sequence

        public virtual Parser<TToken, IEnumerable<T>> AtLeastOnce()
        {
            return from once in Map(ImmutableList.Create)
                   from tail in ZeroOrMoreTimes()
                   select once.Concat(tail);
        }

        public virtual Parser<TToken, IEnumerable<T>> ZeroOrMoreTimes()
        {
            return new(input =>
            {
                var remaining = input;
                var items = new List<T>();

                while (!remaining.IsEnd)
                {
                    var result = parser(remaining);

                    if (!result.WasSuccessful)
                    {
                        break;
                    }

                    items.Add((T)result);
                    remaining = result.Remaining;
                }

                return ParseResult.Okay(items.AsEnumerable(), remaining);
            });
        }

        public virtual Parser<TToken, IEnumerable<T>> Times(int count)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(count);

            return AtLeastOnce()
                .Guard(items => items.Count() == count,
                       // TODO Extract expected tokens
                       // UNDONE 
                       items => new Expectation<TToken>($"Expected {count} times, found {items.Count()} times"));
        }

        public virtual Parser<TToken, IEnumerable<T>> Until<TOther>(Parser<TToken, TOther> other)
        {
            ArgumentNullException.ThrowIfNull(other); // TODO Add this check to other methods

            return new Parser<TToken, IEnumerable<T>>(input =>
            {
                // TODO Remove duplication with ZeroOrMoreTimes and replace with internal iterator
                var remaining = input;
                var items = new List<T>();

                while (!other.Execute(remaining) && !remaining.IsEnd)
                {
                    var result = parser(remaining);

                    if (!result.WasSuccessful)
                    {
                        return result.Cast<IEnumerable<T>>();
                    }

                    items.Add((T)result);
                    remaining = result.Remaining;
                }

                return ParseResult.Okay(items.AsEnumerable(), remaining);
            });
        }

        #endregion

        #region Expectations
        private static readonly Prism<ParseResult<TToken, T>, Expectation<TToken>> ExpectationPrism = new(e => e.Expectation, (e, x) => e with { Expectation = x });

        private static readonly Prism<ParseResult<TToken, T>, string> LabelPrism = ExpectationPrism.Compose<string>(new(e => e.Label, (e, l) => e with { Label = l }));

        public virtual Parser<TToken, T> WithLabel(string label)
            => WithExpectation(e => e with { Label = label });

        public virtual Parser<TToken, T> WithExpected(TToken expected)
            => WithExpected([expected]);

        public virtual Parser<TToken, T> WithExpected(params IEnumerable<TToken> expected)
            => WithExpectation(e => e with { Expected = expected });

        private Parser<TToken, T> WithExpectation(Func<Expectation<TToken>, Expectation<TToken>> update)
            => new(input => parser(input).Match(
                                ExpectationPrism.Update(update),
                                ExpectationPrism.Update(update)));
        #endregion

        #region Execute
        public virtual ParseResult<TToken, T> Execute(TokenSequence<TToken> input)
            => parser(input);

        public virtual Result<T, ParseError> TryParse(TokenSequence<TToken> input)
            => Execute(input).Match(ok => Result.Okay<T, ParseError>(ok.Value),
                                    err => Result.Fail<T, ParseError>(new ParseError(err.Message, typeof(T)))); // TODO Unify ParseError types

        public virtual T Parse(TokenSequence<TToken> input)
            => Execute(input).Match(ok => ok.Value,
                                    err => throw ParseException.FromError(err));
        #endregion

        #region Trace
        public virtual Parser<TToken, T> Trace()
           => new(input =>
           {
               var r = parser(input);

               Console.WriteLine(r.ToString());

               return r;
           });

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
        #endregion

        #region Operators
        public static Parser<TToken, T> operator |(Parser<TToken, T> x, Parser<TToken, T> y) => x.Or(y);

        public static Parser<TToken, IEnumerable<T>> operator +(Parser<TToken, T> x, Parser<TToken, T> y) => x.Then<T, IEnumerable<T>>(y, (a, b) => [a, b]);

        public static Parser<TToken, T> operator >>(Parser<TToken, T> x, Parser<TToken, T> y) => x.Then(y);
        public static Parser<TToken, T> operator >>(Parser<TToken, T> x, Parser<TToken, Unit> y) => x.Then(v => y.Map(_ => v));

        public static implicit operator Parser<TToken, T>(Func<TokenSequence<TToken>, ParseResult<TToken, T>> parser) => new(parser);
        #endregion
    }
}
