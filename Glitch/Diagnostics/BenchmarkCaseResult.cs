namespace Glitch.Diagnostics
{
    public class BenchmarkCaseResult
    {
        private string caseName;
        private IReadOnlyList<TimeSpan> times;
        private Lazy<TimeSpan> best;
        private Lazy<TimeSpan> worst;
        private Lazy<TimeSpan> average;
        private Lazy<TimeSpan> median;
        private Lazy<TimeSpan> total;

        internal BenchmarkCaseResult(string caseName, IReadOnlyList<TimeSpan> times)
        {
            this.caseName = caseName;
            this.times = times;
            best = new Lazy<TimeSpan>(ComputeBest);
            worst = new Lazy<TimeSpan>(ComputeWorst);
            average = new Lazy<TimeSpan>(ComputeAverage);
            median = new Lazy<TimeSpan>(ComputeMedian);
            total = new Lazy<TimeSpan>(ComputeTotal);
        }

        public string Name => caseName;
        public int Iterations => times.Count;
        public TimeSpan Best => best.Value;
        public TimeSpan Worst => worst.Value;
        public TimeSpan Average => average.Value;
        public TimeSpan Median => median.Value;
        public TimeSpan Total => total.Value;

        private TimeSpan ComputeBest() => times.Min();
        private TimeSpan ComputeWorst() => times.Max();
        private TimeSpan ComputeAverage() => times.Average(t => t.Ticks).Convert(t => new TimeSpan((long)t));
        private TimeSpan ComputeTotal() => times.Sum(t => t.Ticks).Convert(t => new TimeSpan(t));
        private TimeSpan ComputeMedian()
        {
            var array = times.Order().ToArray();

            int midpoint = array.Length / 2;

            // Count is even. Get middle two items, add them together, and halve the result.
            if (array.Length % 2 == 0)
            {
                var x = array[midpoint - 1];
                var y = array[midpoint];

                return new TimeSpan((x.Ticks + y.Ticks) / 2);
            }

            // Count is odd. Just return the middle element
            return array[midpoint];
        }
    }
}
