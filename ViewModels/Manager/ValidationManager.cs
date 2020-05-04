using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Manager
{
    public static class ValidationManager
    {
        /// <summary>
        /// Проверяет корректность введённого логина.
        /// Возвращает положительный результат, если логин некорректен
        /// </summary>
        /// <param name="_login"></param>
        /// <returns></returns>
        public static bool IsInvalidLoginName(string _login)
        {
            foreach (char _c in _login)
            {
                if ((_c >= 'a' && _c <= 'z') ||
                    (_c >= 'A' && _c <= 'Z') ||
                    (_c >= 'а' && _c <= 'я') ||
                    (_c >= 'А' && _c <= 'Я') ||
                    _c.ToString().Equals(".") || _c.ToString().Equals("_")) continue;

                return true;
            }

            return false;
        }
    }
}
