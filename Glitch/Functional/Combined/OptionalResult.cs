using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glitch.Functional
{
    public class OptionalResult<T>
    {
        public static readonly OptionalResult<T> None = new(Option<Result<T>>.None);

        private Option<Result<T>> option;

        private OptionalResult(Option<Result<T>> result)
        {
            this.option = result;
        }

        public bool IsOkay => option.IsSomeAnd(r => r.IsOkay);

        public bool IsError => option.IsSomeAnd(r => r.IsFail);

        public bool IsNone => option.IsNone;

        public bool IsFaulted => option.IsNoneOr(r => r.IsFail);

        public static OptionalResult<T> Okay(T value) => new(Some(Result<T>.Okay(value)));

        public static OptionalResult<T> Fail(Error error) => new(Some(Result<T>.Fail(error)));

        public OptionalResult<TResult> Map<TResult>(Func<T, TResult> map)
            => new(option.Map(r => r.Map(map)));

        public OptionalResult<TResult> AndThen<TResult>(Func<T, OptionalResult<TResult>> bind)
            => option.Match(
                r => r.Match(
                    v => bind(v), 
                    e => OptionalResult<TResult>.Fail(e)), 
                _ => OptionalResult<TResult>.None);

        public TResult Match<TResult>(Func<T, TResult> ifOkay, Func<Error, TResult> ifFail, Func<Terminal, TResult> ifNone)
            => option.Match(r => r.Match(ifOkay, ifFail), ifNone);
    }
}
