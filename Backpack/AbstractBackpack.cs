using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backpack
{
    /// <summary>
    /// Задача о рюкзаке.
    /// <see cref="https://ru.wikipedia.org/wiki/Задача_о_рюкзаке"/>
    /// </summary>
    public abstract class AbstractBackpack
    {
        protected double maxWeight;

        /// <summary>
        /// Получает максимальный вес рюкзака.
        /// </summary>
        /// <exception cref="ArgumentException">Значение должно быть больше 0. - value</exception>
        public double MaxWeight
        {
            get
            {
                return maxWeight;
            }
            private set
            {
                if (value <= 0)
                    throw new ArgumentException("Значение должно быть больше 0.", "value");
                maxWeight = value;
            }
        }
        /// <summary>
        /// Получает тип метода, использующегося для решения задачи.
        /// </summary>
        public abstract MethodType Method
        {
            get;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AbstractBackpack"/>.
        /// </summary>
        /// <param name="maxCost">Максимальный вес.</param>
        public AbstractBackpack(double maxWeight)
        {
            this.MaxWeight = maxWeight;
        }

        /// <summary>
        /// Решает задачу о рюкзаке 0-1.
        /// </summary>
        /// <param name="items">Предметы.</param>
        /// <returns>Возвращает массив, каждый элемент которого показывает, 
        /// содержится ли соответствющий предмет в рюкзаке. </returns>
        public abstract bool[] SolveZeroOne(Item[] items);
        /// <summary>
        /// Расчитывает стоимость предметов в рюкзаке.
        /// </summary>
        /// <param name="items">Предметы.</param>
        /// <param name="inBackpack">Массив, каждый элемент которого показывает, 
        /// содержится ли соответствющий предмет в рюкзаке.</param>
        /// <returns>Возвращает стоимость предметов в рюкзаке.</returns>
        public double TotalCost(Item[] items, bool[] inBackpack)
        {
            double result = 0;
            for(int i = 0; i < items.Length; i++)
            {
                if (inBackpack[i])
                    result += items[i].Cost;
            }
            return result;
        }
        /// <summary>
        /// Расчитывает стоимость предметов в рюкзаке.
        /// </summary>
        /// <param name="items">Предметы.</param>
        /// <param name="inBackpack">Массив, каждый элемент которого показывает, 
        /// сколько соответствующих предметов содержится в рюкзаке.</param>
        /// <returns>Возвращает стоимость предметов в рюкзаке.</returns>
        public double TotalCost(Item[] items, uint[] inBackpack)
        {
            double result = 0;
            for (int i = 0; i < items.Length; i++)
                result += inBackpack[i] * items[i].Cost;
            return result;
        }
        /// <summary>
        /// Решает задачу о неограниченном рюкзаке.
        /// </summary>
        /// <param name="items">Предметы.</param>
        /// <returns>Возвращает массив, каждый элемент которого показывает, 
        /// сколько соответствующих предметов содержится в рюкзаке.</returns>
        public abstract uint[] SolveUnlimited(Item[] items);
        /// <summary>
        /// Вычисляет общий вес предметов.
        /// </summary>
        /// <param name="items">Предметы.</param>
        /// <param name="inBackpack">Массив, каждый элемент которого показывает, 
        /// содержится ли соответствющий предмет в рюкзаке.</param>
        /// <returns>Возвращает общий вес предметов в рюкзаке.</returns>
        public double TotalWeight(Item[] items, bool[] inBackpack)
        {
            double result = 0;
            for (int i = 0; i < items.Length; i++)
            {
                if (inBackpack[i])
                    result += items[i].Weight;
            }
            return result;
        }
        /// <summary>
        /// Вычисляет общий вес предметов.
        /// </summary>
        /// <param name="items">Предметы.</param>
        /// <param name="inBackpack">Массив, каждый элемент которого показывает, 
        /// сколько соответствующих предметов содержится в рюкзаке.</param>
        /// <returns>Возвращает общий вес предметов в рюкзаке.</returns>
        public double TotalWeight(Item[] items, uint[] inBackpack)
        {
            double result = 0;
            for (int i = 0; i < items.Length; i++)
                result += inBackpack[i] * items[i].Weight;
            return result;
        }
    }

    /// <summary>
    /// Предмет рюкзака.
    /// </summary>
    public class Item
    {
        protected double weight, cost;

        /// <summary>
        /// Получает или задаёт стоимость предмета.
        /// </summary>
        public double Cost
        {
            get { return cost; }
            set 
            {
                if (value <= 0)
                    throw new ArgumentException("Значение должно быть больше 0.", "value");
                cost = value;
            }
        }
        /// <summary>
        /// Получает или задаёт вес предмета.
        /// </summary>
        public double Weight
        {
            get { return weight; }
            set 
            {
                if (value <= 0)
                    throw new ArgumentException("Значение должно быть больше 0.", "value");
                weight = value;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Item"/>.
        /// </summary>
        /// <param name="weight">Вес предмета</param>
        /// <param name="cost">Стоиомость предмета.</param>
        public Item(double weight = 1, double cost = 1)
        {
            this.Weight = weight;
            this.Cost = cost;
        }
        
    }
}
