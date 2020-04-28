using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsBook.Domain.Models;

namespace Infrastructure.Services
{
    /// <summary>
    /// Интерфейс сервиса для управления моделями
    /// </summary>
    public interface IServiceDTO
    {
        #region Методы
        /// <summary>
        /// Возвращает список всех контактов
        /// </summary>
        /// <returns></returns>
        IList<ContactModel> GetContacts();
        #endregion
    }
}
