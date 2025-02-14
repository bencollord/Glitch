namespace Glitch.Functional
{
    public abstract record IO<T>
    {
        public abstract IO<TResult> Map<TResult>(Func<T, TResult> map);

        public IO<TResult> Apply<TResult>(IO<Func<T, TResult>> apply)
            => AndThen(val => apply.Map(fn => fn(val)));

        public abstract IO<TResult> AndThen<TResult>(Func<T, IO<TResult>> bind);

        public IO<TResult> AndThen<TElement, TResult>(Func<T, IO<TElement>> bind, Func<T, TElement, TResult> projection)
            => AndThen(x => bind(x).Map(y => projection(x, y)));
    }

    internal record PureIO<T>(T Value) : IO<T>
    {
        public override IO<TResult> Map<TResult>(Func<T, TResult> map)
        {
            return new LiftedIO<TResult>(() => map(Value));
        }

        public override IO<TResult> AndThen<TResult>(Func<T, IO<TResult>> bind)
        {
            throw new NotImplementedException();
        }
    }

    internal record LiftedIO<T> : IO<T>
    {
        private readonly Func<T> thunk;

        internal LiftedIO(Func<T> thunk)
        {
            this.thunk = thunk;
        }

        public override IO<TResult> Map<TResult>(Func<T, TResult> map)
        {
            return new LiftedIO<TResult>(thunk.Then(map));
        }

        public override IO<TResult> AndThen<TResult>(Func<T, IO<TResult>> bind)
        {
            return new BindIO<T, TResult>(this, bind);
        }
    }

    internal record MapIO<T, TNext> : IO<TNext>
    {
        private IO<T> source;
        private Func<T, TNext> map;

        internal MapIO(IO<T> source, Func<T, TNext> map)
        {
            this.source = source;
            this.map = map;
        }

        public override IO<TResult> Map<TResult>(Func<TNext, TResult> map)
        {
            return new MapIO<T, TResult>(source, this.map.Then(map));
        }

        public override IO<TResult> AndThen<TResult>(Func<TNext, IO<TResult>> bind)
        {
            return new BindIO<TNext, TResult>(this, bind);
        }
    }

    internal record BindIO<T, TNext> : IO<TNext>
    {
        private IO<T> source;
        private Func<T, IO<TNext>> next;

        internal BindIO(IO<T> source, Func<T, IO<TNext>> next)
        {
            this.source = source;
            this.next = next;
        }

        public override IO<TResult> Map<TResult>(Func<TNext, TResult> map)
        {
            throw new NotImplementedException();
        }

        public override IO<TResult> AndThen<TResult>(Func<TNext, IO<TResult>> bind)
        {
            throw new NotImplementedException();
        }
    }
}