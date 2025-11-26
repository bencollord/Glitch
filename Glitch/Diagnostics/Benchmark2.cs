namespace Glitch.Diagnostics;

public class Benchmark<T1, T2>
{
    private const int DefaultIterations = 1000;

    private int iterations;
    private Dictionary<string, Action<T1, T2>> cases = new();

    public Benchmark() : this(DefaultIterations) { }

    public Benchmark(int iterations)
    {
        this.iterations = iterations;
    }

    public Benchmark<T1, T2> Add<TResult>(Func<T1, T2, TResult> func) => Add(func.Method.Name, func);

    public Benchmark<T1, T2> Add<TResult>(string name, Func<T1, T2, TResult> func) 
        => Add(name, new Action<T1, T2>((arg1, arg2) => _ = func(arg1, arg2)));

    public Benchmark<T1, T2> Add(Action<T1, T2> action) => Add(action.Method.Name, action);

    public Benchmark<T1, T2> Add(string name, Action<T1, T2> action)
    {
        cases.Add(name, action);
        return this;
    }

    public Benchmark<T1, T2> Remove(string name)
    {
        cases.Remove(name);
        return this;
    }

    public Benchmark<T1, T2> Clear()
    {
        cases.Clear();
        return this;
    }

    public BenchmarkResult Run(T1 arg1, T2 arg2)
    {
        var results = from pair in cases
                      let name = pair.Key
                      let action = pair.Value
                      let times = Benchmark.RunRepeatedly(() => action(arg1, arg2))
                        .Take(iterations)
                        .ToList()
                      select new BenchmarkCaseResult(name, times);

        return new BenchmarkResult(results.ToList());
    }
}
