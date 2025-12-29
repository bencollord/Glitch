namespace Glitch.Functional.Effects;

[Monad]
public delegate T Reader<in TEnv, out T>(TEnv env) ;

public static partial class ReaderExtensions
{
    extension<TEnv, T>(Reader<TEnv, T>)
    {
        public static Reader<TEnv, T> Return(T value) => new(_ => value);

        public static Reader<TEnv, T> Lift(Func<T> runner) => new(_ => runner());

        public static Reader<TEnv, T> Asks(Func<TEnv, T> runner) => new(runner);
    }

    extension<TEnv, T>(Reader<TEnv, T> self)
    {
        public Func<TEnv, T> AsFunc() => env => self(env);

        /// <summary>
        /// Maps the reader's input value such that the Run method now
        /// takes a new environment type as input.
        /// </summary>
        /// <typeparam name="TNewEnv"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        public Reader<TNewEnv, T> With<TNewEnv>(Func<TNewEnv, TEnv> map) => newEnv => self(map(newEnv));

        /// <summary>
        /// Returns a new reader that maps its input value before
        /// running the current reader.
        /// </summary>
        /// <remarks>
        /// I'm not sure why the naming convention I've seen in other code uses
        /// Local rather than overloading With for environment mappings that don't
        /// take a type.
        /// </remarks>
        /// <param name="map"></param>
        /// <returns></returns>
        public Reader<TEnv, T> Local(Func<TEnv, TEnv> map) => self.With(map);

        public Reader<TEnv, TResult> Select<TResult>(Func<T, TResult> map) => 
            env => map(self(env));

        public Reader<TEnv, TOther> Then<TOther>(Reader<TEnv, TOther> other) => 
            self.AndThen(_ => other);

        public Reader<TEnv, TResult> Then<TOther, TResult>(Reader<TEnv, TOther> other, Func<T, TOther, TResult> project) => 
            self.AndThen(_ => other, project);

        public Reader<TEnv, T> Then(Reader<TEnv, Unit> other) =>
             env =>
             {
                 var value = self(env);
                 _ = other(env);
                 return value;
             };

        public Reader<TEnv, TResult> Apply<TResult>(Reader<TEnv, Func<T, TResult>> apply) =>
            apply.AndThen(fn => self.Select(fn));

        public Reader<TEnv, TResult> AndThen<TResult>(Func<T, Reader<TEnv, TResult>> bind) =>
             env => bind(self(env))(env);

        public Reader<TEnv, TResult> AndThen<TElement, TResult>(Func<T, Reader<TEnv, TElement>> bind, Func<T, TElement, TResult> project) =>
            self.AndThen(x => bind(x).Select(project.Curry(x)));

        public Reader<TEnv, TResult> SelectMany<TElement, TResult>(Func<T, Reader<TEnv, TElement>> bind, Func<T, TElement, TResult> project) =>
            self.AndThen(bind, project);
    }
}
