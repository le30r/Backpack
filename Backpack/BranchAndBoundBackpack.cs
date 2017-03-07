using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backpack
{
    /// <summary>
    /// Решение методом ветвей и границ.
    /// </summary>
    /// <seealso cref="Backpack.AbstractBackpack" />
    public class BranchAndBoundBackpack: AbstractBackpack
    {
        protected bool[] result;
        protected Item[] items;
        protected uint[] resultUnl;
        protected double maxCost;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BranchAndBoundBackpack"/>.
        /// </summary>
        /// <param name="maxWeight">Максимальный вес рюкзака.</param>
        public BranchAndBoundBackpack(double maxWeight = 100): base(maxWeight)
        {

        }

        /// <summary>
        /// Получает тип метода, использующегося для решения задачи.
        /// </summary>
        public override MethodType Method
        {
            get { return MethodType.BranchAndBound; }
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
            var defaultOrder = GetDefaultOrder(items);
            var ordered = defaultOrder.OrderByDescending<OrderedItem, double>(
                (i) => i.Cost / i.Weight).ToArray();
            this.items = ordered;
            maxCost = 0;
            result = new bool[items.Length];
            var testVector = new bool[items.Length];
            SolveZeroOneRec(testVector);
            return RestoreSolution(ordered);
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
            var defaultOrder = GetDefaultOrder(items);
            var ordered = defaultOrder.OrderByDescending<OrderedItem, double>(
                (i) => i.Cost / i.Weight).ToArray();
            this.items = ordered;
            maxCost = 0;
            resultUnl = new uint[items.Length];
            var testVector = new uint[items.Length];
            SolveUnlimitedRec(testVector);
            return RestoreUnlimitedSolution(ordered);
        }

        /// <summary>
        /// Получает массив упорядоченных элементов.
        /// </summary>
        /// <param name="items">Массив неупорядоченных элементов.</param>
        /// <returns>Возвращает массив упорядоченных элементов.</returns>
        private OrderedItem[] GetDefaultOrder(Item[] items)
        {
            var temp = new OrderedItem[items.Length];
            for(int i = 0; i < items.Length; i++)
            {
                temp[i] = new OrderedItem(items[i].Weight, items[i].Cost, i);
            }
            return temp;
        }
        /// <summary>
        /// Восстанавливает порядок решения, используя упорядоченную последовательность.
        /// </summary>
        /// <param name="items">Массив упорядоченных элементов.</param>
        /// <returns>Возвращает решене задачи.</returns>
        private bool[] RestoreSolution(OrderedItem[] items)
        {
            bool[] res = new bool[items.Length];
            for(int i = 0; i < items.Length; i++)
            {
                res[items[i].Number] = result[i];
            }
            return res;
        }
        /// <summary>
        /// Восстанавливает порядок решения, используя упорядоченную последовательность.
        /// </summary>
        /// <param name="items">Массив упорядоченных элементов.</param>
        /// <returns>Возвращает решене задачи.</returns>
        private uint[] RestoreUnlimitedSolution(OrderedItem[] items)
        {
            uint[] res = new uint[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                res[items[i].Number] = resultUnl[i];
            }
            return res;
        }


        /// <summary>
        /// Рекурсивная процедура решения задачи 0-1.
        /// </summary>
        /// <param name="vector">Проверяемое решение.</param>
        /// <param name="i">Номер проверяемого элемента решения.</param>
        /// <param name="c">Общая стоимость решения к текущему моменту.</param>
        /// <param name="w">Общий вес решения к текущему моменту.</param>
        private void SolveZeroOneRec(bool[] vector, int i = 0, double c = 0, double w = 0)
        {
            if (i == vector.Length) //надо принять решение
            {
                //к этому шагу мы дойдём, только если вес рюкзака будет меньше допустимого, 
                //т.к. проверки выполняются до запуска ветви
                if (c > maxCost)
                {
                    maxCost = c;
                    vector.CopyTo(result, 0);
                }
            }
            else
            {
                double includeC = c + items[i].Cost;
                double includeW = w + items[i].Weight;
                if(includeW <= maxWeight) //не перебираем по весу
                {
                    vector[i] = true;
                    //если оценка сверху больше масимальной стоимости
                    if (CalculateUpperBound(includeC, includeW, i) > maxCost)
                        SolveZeroOneRec(vector, i + 1, includeC, includeW);
                }
                vector[i] = false;
                //если оценка сверху больше масимальной стоимости
                if (CalculateUpperBound(c, w, i) > maxCost)
                    SolveZeroOneRec(vector, i + 1, c, w);
            }
        }
        /// <summary>
        /// Рекурсивная процедура решения задачи о неограниченном рюкзаке .
        /// </summary>
        /// <param name="vector">Проверяемое решение.</param>
        /// <param name="i">Номер проверяемого элемента решения.</param>
        /// <param name="c">Общая стоимость решения к текущему моменту.</param>
        /// <param name="w">Общий вес решения к текущему моменту.</param>
        private void SolveUnlimitedRec(uint[] vector, int i = 0, double c = 0, double w = 0)
        {
            if (i == vector.Length) //надо принять решение
            {
                //к этому шагу мы дойдём, только если вес рюкзака будет меньше допустимого, 
                //т.к. проверки выполняются до запуска ветви
                if (c > maxCost)
                {
                    maxCost = c;
                    vector.CopyTo(resultUnl, 0);
                }
            }
            else
            {
                double oneMoreC = c + items[i].Cost;
                double oneMoreW = w + items[i].Weight;
                if (oneMoreW <= maxWeight) //не перебираем по весу
                {
                    vector[i]++;
                    //если оценка сверху больше масимальной стоимости
                    if (CalculateUpperBound(oneMoreC, oneMoreW, i - 1) > maxCost)
                        SolveUnlimitedRec(vector, i, oneMoreC, oneMoreW);
                }
                //если оценка сверху больше масимальной стоимости
                if (CalculateUpperBound(c, w, i) > maxCost)
                    SolveUnlimitedRec(vector, i + 1, c, w);
                vector[i] = vector[i] > 0? vector[i] - 1 : 0;
            }
        }
        /// <summary>
        /// Вычисляет верхнюю границу решения для методы ветвей и границ.
        /// </summary>
        /// <param name="c">Текущая стоимость решения.</param>
        /// <param name="w">Текущий вес решения.</param>
        /// <param name="i">Номер тестируемого элемента.</param>
        /// <returns>Возвращает верхнюю границу решения.</returns>
        private double CalculateUpperBound(double c, double w, int i)
        {
            double ub = c + (maxWeight - w) * ((i + 1 < items.Length) ?
                (items[i + 1].Cost / items[i + 1].Weight) : 0);
            return ub;
        }

        /// <summary>
        /// Расширяет класс <seealso cref="Backpack.Item" />, добавляя переменную, показывающую 
        /// порядок в коллекции. Используется для того, чтобы восстановить исходный порядок после 
        /// сортировки.
        /// </summary>
        /// <seealso cref="Backpack.Item" />
        private class OrderedItem: Item
        {
            protected int number;

            /// <summary>
            /// Порядок элемента в коллекции.
            /// </summary>
            public int Number
            {
                get { return number; }
            }

            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="OrderedItem"/>.
            /// </summary>
            /// <param name="w">Вес предмета.</param>
            /// <param name="c">Стоимость предмета.</param>
            /// <param name="i">Порядок элемента в коллекции.</param>
            public OrderedItem(double w, double c, int i): base(w, c)
            {
                this.number = i;
            }
        }
    }
}
