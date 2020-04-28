using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Managers
{
    /// <summary>
    /// Менеджер сервисов, которые работают с наборами данных из БД.
    /// В данном менеджере можно реализовать логику выбора необходимого сервиса.
    /// В реализации данного ТЗ менеджер будет работать только с одним сервисом БД
    /// Можно было реализовать вместо данного менеджера SingleTone  в сервисе <see cref="SQLiteServiceDTO"/>
    /// </summary>
    public static class MenagerServices
    {
        #region Поля
        private static IServiceDTO _currentService = new SQLiteServiceDTO();
        #endregion

        #region Методы
        /// <summary>
        /// Возвращает текущий активный сервис <see cref="IServiceDTO"/>
        /// </summary>
        /// <returns></returns>
        public static IServiceDTO GetCurrentService()
        {
            return _currentService;
        }
        #endregion
    }
}
