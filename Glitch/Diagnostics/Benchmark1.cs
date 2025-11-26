namespace Glitch.Diagnostics;

public class Benchmark<T>
{
    private const int DefaultIterations = 1000;

    private int iterations;
    private Dictionary<string, Action<T>> cases = new();

    public Benchmark() : this(DefaultIterations) { }

    public Benchmark(int iterations)
    {
        this.iterations = iterations;
    }

    public Benchmark<T> Add<TResult>(Func<T, TResult> func) => Add(func.Method.Name, func);

    public Benchmark<T> Add<TResult>(string name, Func<T, TResult> func) 
        => Add(name, new Action<T>(arg => _ = func(arg)));

    public Benchmark<T> Add(Action<T> action) => Add(action.Method.Name, action);

    public Benchmark<T> Add(string name, Action<T> action)
    {
        cases.Add(name, action);
        return this;
    }

    public Benchmark<T> Remove(string name)
    {
        cases.Remove(name);
        return this;
    }

    public Benchmark<T> Clear()
    {
        cases.Clear();
        return this;
    }

    public BenchmarkResult Run(T arg)
    {
        var results = from pair in cases
                      let name = pair.Key
                      let action = pair.Value
                      let times = Benchmark.RunRepeatedly(() => action(arg))
                        .Take(iterations)
                        .ToList()
                      select new BenchmarkCaseResult(name, times);

        return new BenchmarkResult(results.ToList());
    }
}
