using Backpack;

BranchAndBoundBackpack backpack = new BranchAndBoundBackpack(15);
var items = TaskGenerator.GetPredictableTask().Item1;

var result = backpack.SolveZeroOne(items);

for (int i = 0; i < result.Length; i++)
{
    Console.WriteLine(i + " " + items[i].Weight + " " + result[i]);
}