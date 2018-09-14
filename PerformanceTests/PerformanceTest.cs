using NBench;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceTests
{
    class PerformanceTest: PerformanceTestSuite<PerformanceTest>
    {
        private readonly Dictionary<int, int> dictionary = new Dictionary<int, int>();

        private const string AddCounterName = "AddCounter";
        private Counter addCounter;
        private int key;
        private const int AcceptableMinAddThroughput = 200;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            addCounter = context.GetCounter(AddCounterName);
            key = 0;
        }

        [PerfBenchmark(RunMode = RunMode.Iterations, TestMode = TestMode.Test)]
        [CounterThroughputAssertion(AddCounterName, MustBe.GreaterThan, AcceptableMinAddThroughput)]
        public void AddThroughput_IterationsMode(BenchmarkContext context)
        {
            for (var i = 0; i < AcceptableMinAddThroughput; i++)
            {
                dictionary.Add(i, i);
                addCounter.Increment();
            }
        }
    }
}
