using System.Diagnostics;

namespace Glitch.Diagnostics
{
    public class BenchmarkCase
    {
        private string name;
        private Action action;

        public BenchmarkCase(string name, Action action)
        {
            this.name = name;
            this.action = action;
        }

        public string Name => name;

        public BenchmarkCaseResult Run(uint iterations)
        {
            var timer = new Stopwatch();
            var times = new List<TimeSpan>();

            for (int i = 0; i < iterations; i++)
            {
                timer.Restart();
                action();
                timer.Stop();
                times.Add(timer.Elapsed);
            }

            return new BenchmarkCaseResult(Name, times);
        }
    }
}
