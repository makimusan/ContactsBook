using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsBook.Domain.Managers
{
    public interface IConnectionManager
    {
        /// <summary>
        /// Возвращает строку подключения к БД
        /// </summary>
        /// <returns>Строка подключения</returns>
        string GetConnectionString();
    }
}
