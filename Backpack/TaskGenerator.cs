using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backpack
{
    public static class TaskGenerator
    {
        public static Tuple<Item[], double> GetPredictableTask()
        {
            Item[] items = new Item[5];
            items[0] = new Item(5, 10);
            items[1] = new Item(10, 12);
            items[2] = new Item(15, 22);
            items[3] = new Item(7, 8);
            items[4] = new Item(6, 14);
            return new Tuple<Item[], double>(items, 21);
        }
        public static Tuple<Item[], double> GetRandomTask(int number)
        {
            Random rnd = new Random();
            double totalWeight = 0;
            Item[] items = new Item[number];
            for (int i = 0; i < number; i++)
            {
                double w = rnd.Next(1, number);
                totalWeight += w;
                items[i] = new Item(w, rnd.Next(1, number));
            }
            return new Tuple<Item[], double>(items, totalWeight / 2);
        }
    }
}
