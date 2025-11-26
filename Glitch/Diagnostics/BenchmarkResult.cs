using System.Collections;

namespace Glitch.Diagnostics;

public class BenchmarkResult : IEnumerable<BenchmarkCaseResult>
{
    private IEnumerable<BenchmarkCaseResult> cases;

    internal BenchmarkResult(IEnumerable<BenchmarkCaseResult> cases)
    {
        this.cases = cases;
    }

    public IEnumerator<BenchmarkCaseResult> GetEnumerator() => cases.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
