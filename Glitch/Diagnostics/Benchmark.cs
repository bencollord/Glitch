using System.Diagnostics;

namespace Glitch.Diagnostics;

public class Benchmark
{
    private const uint DefaultIterations = 1000;

    private uint iterations;
    private Dictionary<string, BenchmarkCase> cases = new();

    public Benchmark() : this(DefaultIterations) { }

    public Benchmark(uint iterations)
    {
        this.iterations = iterations;
    }

    public Benchmark Add<T>(Func<T> func) => Add(func.Method.Name, func);

    public Benchmark Add<T>(string name, Func<T> func) => Add(name, ToAction(func));

    public Benchmark Add(Action action) => Add(action.Method.Name, action);

    public Benchmark Add(string name, Action action)
    {
        cases.Add(name, new BenchmarkCase(name, action));
        return this;
    }

    public Benchmark Remove(string name)
    {
        cases.Remove(name);
        return this;
    }

    public Benchmark Clear()
    {
        cases.Clear();
        return this;
    }

    public BenchmarkResult Run() => Run(iterations);

    public BenchmarkResult Run(uint iterationOverride) 
        => new BenchmarkResult(cases.Values.Select(c => c.Run(iterationOverride)).ToList());

    public TimeSpan Run<T>(Func<T> func)
        => Run(ToAction(func));

    public TimeSpan Run(Action action)
    {
        var timer = Stopwatch.StartNew();

        for (int i = 0; i < iterations; i++)
        {
            action();
        }

        timer.Stop();

        return timer.Elapsed;
    }

    internal static Action ToAction<T>(Func<T> func) => () => func();

    internal static IEnumerable<TimeSpan> RunRepeatedly(Action action)
    {
        var timer = new Stopwatch();

        while (true)
        {
            timer.Restart();
            action();
            timer.Stop();
            yield return timer.Elapsed;
        }
    }
}
