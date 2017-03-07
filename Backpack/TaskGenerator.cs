using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backpack
{
    /// <summary>
    /// Генератор задач.
    /// </summary>
    public static class TaskGenerator
    {
        /// <summary>
        /// Получает задачу, для которой известно решение для обоих формулировок: 
        /// задача 0-1: решение - 36
        /// неограниченная задача: решение - 44
        /// </summary>
        /// <returns>Возвращает <see cref="System.Tuple<Item[], double>"/>, первым элементом 
        /// которого является массив предметов, формирующих задачу, а вторым - максимальный вес 
        /// рюкзака.</returns>
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
        /// <summary>
        /// Генерирует случайню задачу.
        /// </summary>
        /// <param name="number">Количество предметов.</param>
        /// <returns>Возвращает <see cref="System.Tuple<Item[], double>"/>, первым элементом 
        /// которого является массив предметов, формирующих задачу, а вторым - максимальный вес 
        /// рюкзака.</returns>
        public static Tuple<Item[], double> GetRandomTask(int number)
        {
            Random rnd = new Random();
            double totalWeight = 0;
            Item[] items = new Item[number];
            for (int i = 0; i < number; i++)
            {
                double w = rnd.Next(1, number * 10);
                totalWeight += w;
                items[i] = new Item(w, rnd.Next(1, number));
            }
            return new Tuple<Item[], double>(items, totalWeight / 2);
        }
    }
}
