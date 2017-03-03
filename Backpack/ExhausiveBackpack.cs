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
    /// <seealso cref="Backpack.Backpack" />
    public class ExhausiveSearchBackpack: Backpack
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
            this.items = items;
            SolveZeroOneRec();
            return result;
        }
        public override uint[] SolveUnlimited(Item[] items)
        {
            resultUnl = new uint[items.Length];
            this.items = items;
            return resultUnl;
        }
        /// <summary>
        /// Решает задачу 0-1 рекурсивно перебирая все варианты.
        /// </summary>
        /// <param name="i">Номер рассматриваемого предмета.</param>
        /// <param name="w">Вес рюкзака к данному моменту.</param>
        /// <returns>Возвращает итоговый вес рюкзака.</returns>
        private double SolveZeroOneRec(int i = 0, double w = 0)
        {
            if (i >= result.Length) return w; //выход из рекурсии
            double includeResult = SolveZeroOneRec(i + 1, w + items[i].Weight);
            double excludeResult = SolveZeroOneRec(i + 1, w);
            //сначала вычисляются возможные альтернативы, затем происходит разбор полётов
            //если наборот, то метод ветвей и границ
            if (includeResult < 0 && excludeResult < 0) //оба решения плохи
                return -1;
            if(includeResult > excludeResult) //включить предмет лучше
            {
                if(includeResult <= maxWeight) //не перебрали?
                {
                    result[i] = true;
                    return includeResult;
                }
            }
            if (excludeResult <= maxWeight) //если не включать предмет, то перебираем?
                return excludeResult;
            return -1; //перебрали в обоих случаях
        }
        /// <summary>
        /// Решает неограниченную задачу рекурсивно перебирая все варианты.
        /// </summary>
        /// <param name="i">Номер рассматриваемого предмета.</param>
        /// <param name="w">Вес рюкзака к данному моменту.</param>
        /// <returns>Возвращает итоговый вес рюкзака.</returns>
        private double SolveUnlimitedRec(int i = 0, double w = 0)
        {
            if (i >= resultUnl.Length) return w; //выход из рекурсии
            double enoughResult = SolveUnlimitedRec(i + 1, w); //достаточно таких предметов
            double oneMoreResult = SolveUnlimitedRec(i, w + items[i].Cost); //положим ещё один
            if (enoughResult < 0 && oneMoreResult < 0) //оба решения плохи?
                return -1;
            //если положить ещё, то получаем больше
            if(oneMoreResult > 0 && oneMoreResult > enoughResult) 
            {
                if (oneMoreResult <= maxWeight) //и вес меньше максимального
                {
                    resultUnl[i]++;
                    return oneMoreResult;
                }
            }
            //если не класть, то получаем больше
            if(enoughResult > 0 && enoughResult <= maxWeight) 
                return enoughResult;
            return -1; //оба решения - перебор
        }
    }
}
