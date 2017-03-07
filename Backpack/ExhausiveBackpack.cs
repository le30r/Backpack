using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backpack
{
    /// <summary>
    /// Задача о рюкзаке. Метод полного перебора.
    /// </summary>
    /// <seealso cref="AbstractBackpack.AbstractBackpack" />
    public class ExhausiveSearchBackpack: AbstractBackpack
    {
        /// <summary>
        /// Получает тип метода, использующегося для решения задачи.
        /// </summary>
        public override MethodType Method
        {
            get { return MethodType.ExhausiveSearchRec; }
        }

        protected bool[] result;
        protected Item[] items;
        protected uint[] resultUnl;
        protected double maxCost;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ExhausiveSearchBackpack"/>.
        /// </summary>
        /// <param name="maxWeight">Максимальный вес</param>
        public ExhausiveSearchBackpack(double maxWeight = 100): base(maxWeight)
        {

        }

        /// <summary>
        /// Решает задачу о рюкзаке 0-1.
        /// </summary>
        /// <param name="items">Предметы.</param>
        /// <returns>
        /// Возвращает массив, каждый элемент которого показывает,
        /// содержится ли соответствющий предмет в рюкзаке.
        /// </returns>
        public override bool[] SolveZeroOne(Item[] items)
        {
            result = new bool[items.Length];
            bool[] testVector = new bool[items.Length];
            maxCost = 0;
            this.items = items;
            SolveZeroOneRec(testVector);
            return result;
        }
        /// <summary>
        /// Решает задачу о неограниченном рюкзаке.
        /// </summary>
        /// <param name="items">Предметы.</param>
        /// <returns>
        /// Возвращает массив, каждый элемент которого показывает,
        /// сколько соответствующих предметов содержится в рюкзаке.
        /// </returns>
        public override uint[] SolveUnlimited(Item[] items)
        {
            resultUnl = new uint[items.Length];
            uint[] testVector = new uint[items.Length];
            maxCost = 0;
            this.items = items;
            SolveUnlimitedRec(testVector);
            return resultUnl;
        }
        /// <summary>
        /// Решает задачу 0-1 рекурсивно перебирая все варианты.
        /// </summary>
        /// <param name="vector">Текущий вариант решения.</param>
        /// <param name="i">Номер проверяемого предмета.</param>
        private void SolveZeroOneRec(bool[] vector, int i = 0, double w = 0)
        {
            if (i == vector.Length) //надо принять решение
            {
                if (maxWeight >= TotalWeight(items, vector))
                {
                    double cost = TotalCost(items, vector);
                    if (cost > maxCost)
                    {
                        maxCost = cost;
                        vector.CopyTo(result, 0);
                    }
                }
            }
            else
            {
                double inclWeight = w + items[i].Weight;
                if (inclWeight <= maxWeight) //если не переберём, то пробуем включить элемент
                {
                    vector[i] = true;
                    SolveZeroOneRec(vector, i + 1);
                }
                vector[i] = false;
                SolveZeroOneRec(vector, i + 1, w);
            }
        }
        /// <summary>
        /// Решает неограниченную задачу рекурсивно перебирая все варианты.
        /// </summary>
        /// <param name="i">Номер рассматриваемого предмета.</param>
        /// <param name="w">Вес рюкзака к данному моменту.</param>
        /// <returns>Возвращает итоговый вес рюкзака.</returns>
        private void SolveUnlimitedRec(uint[] vector, int i = 0, double w = 0)
        {
            if (i == vector.Length) //надо принять решение
            {
                if (maxWeight >= TotalWeight(items, vector))
                {
                    double cost = TotalCost(items, vector);
                    if (cost > maxCost)
                    {
                        maxCost = cost;
                        vector.CopyTo(resultUnl, 0);
                    }
                }
            }
            else
            {
                SolveUnlimitedRec(vector, i + 1, w);
                double oneMoreW = w + items[i].Weight;
                while(oneMoreW <= maxWeight)
                {
                    vector[i]++;
                    SolveUnlimitedRec(vector, i, oneMoreW);
                    oneMoreW += items[i].Weight;
                }
                vector[i] = 0;
            }
        }
    }
}

