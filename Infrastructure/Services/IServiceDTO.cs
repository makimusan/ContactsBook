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

        /// <summary>
        /// Сохраняет список контактов
        /// </summary>
        /// <param name="contactModels"></param>
        void SaveContacts(IList<ContactModel> contactModels);
        /// <summary>
        /// Добавляет контакты в БД
        /// </summary>
        /// <param name="contactModels"></param>
        void CreateContacts(IList<ContactModel> contactModels);
        /// <summary>
        /// Обновляет контакты в БД
        /// </summary>
        /// <param name="contactModels"></param>
        void UpdateContacts(IList<ContactModel> contactModels);
        /// <summary>
        /// Удаляет контакты из БД
        /// </summary>
        /// <param name="contactModels"></param>
        void DeleteContacts(IList<ContactModel> contactModels);

        /// <summary>
        /// Удаляет @ адреса из БД
        /// </summary>
        /// <param name="eMails"></param>
        void DeleteEMails(IList<MailModel> eMails);

        /// <summary>
        /// Удаляет телефонные номера из БД
        /// </summary>
        /// <param name="phoneNumbers"></param>
        void DeletePhoneNumbers(IList<PhoneNumberModel> phoneNumbers);
        #endregion
    }
}
