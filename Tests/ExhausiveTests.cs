using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Backpack;

namespace Tests
{
    [TestClass]
    public class ExhausiveTests
    {
        [TestMethod]
        public void TestZeroOneCorrectness()
        {
            var task = TaskGenerator.GetPredictableTask();
            AbstractBackpack b = new ExhausiveSearchBackpack(task.Item2);
            bool[] result = b.SolveZeroOne(task.Item1);
            Assert.IsTrue(b.TotalCost(task.Item1, result) == 36);
            Assert.IsTrue(b.TotalWeight(task.Item1, result) <= task.Item2);
        }
        [TestMethod]
        public void TestUnlimitedCorrectness()
        {
            var task = TaskGenerator.GetPredictableTask();
            AbstractBackpack b = new ExhausiveSearchBackpack(task.Item2);
            uint[] result = b.SolveUnlimited(task.Item1);
            Assert.IsTrue(b.TotalCost(task.Item1, result) == 44);
            Assert.IsTrue(b.TotalWeight(task.Item1, result) <= task.Item2);
        }
    }
}
