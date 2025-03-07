using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glitch.Functional
{
    public class EmptyError : Error
    {
        public static readonly EmptyError Value = new();

        private EmptyError() : base() { }

        public override string Message => "No error";

        public override Option<Error> Inner => None;

        public override Exception AsException() => throw new NullReferenceException("There's no actual error here");

        public override bool IsException<T>() => false;

        public override Error Combine(Error other) => other;

        public override IEnumerable<Error> Iterate()
        {
            yield break;
        }
    }
}
