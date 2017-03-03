using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backpack
{
    /// <summary>
    /// Аттрибут, задающий имя метода для интерфейсных целей.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class MethodFriendlyName : Attribute
    {
        protected string name; //имя аттрибута

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="MethodFriendlyName"/>.
        /// </summary>
        /// <param name="name">Имя метода.</param>
        public MethodFriendlyName(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Получает или задаёт имя метода.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
