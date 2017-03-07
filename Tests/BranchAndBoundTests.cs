using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Backpack;
using System.Diagnostics;

namespace Tests
{
    [TestClass]
    public class BranchAndBoundTests
    {
        [TestMethod]
        public void TestZeroOneCorrectness()
        {
            var task = TaskGenerator.GetPredictableTask();
            AbstractBackpack b = new BranchAndBoundBackpack(task.Item2);
            bool[] result = b.SolveZeroOne(task.Item1);
            Assert.IsTrue(b.TotalCost(task.Item1, result) == 36);
            Assert.IsTrue(b.TotalWeight(task.Item1, result) <= task.Item2);
        }
        [TestMethod]
        public void TestUnlimitedCorrectness()
        {
            var task = TaskGenerator.GetPredictableTask();
            AbstractBackpack b = new BranchAndBoundBackpack(task.Item2);
            uint[] result = b.SolveUnlimited(task.Item1);
            Assert.IsTrue(b.TotalCost(task.Item1, result) == 44);
            Assert.IsTrue(b.TotalWeight(task.Item1, result) <= task.Item2);
        }

        [TestMethod]
        public void TestElapsedTime()
        {
            Stopwatch sw = new Stopwatch();
            var task = TaskGenerator.GetRandomTask(30);
            AbstractBackpack b = new BranchAndBoundBackpack(task.Item2);
            sw.Start();
            bool[] result = b.SolveZeroOne(task.Item1);
            sw.Stop();
            Trace.WriteLine(string.Format("{0} c", (double)sw.ElapsedMilliseconds / 1000));
        }
    }
}
