using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsBook.Domain.DataStructs;
using ContactsBook.Domain.Models;

namespace Infrastructure.Factories
{
    /// <summary>
    /// Интерфейс фабрики для сборки сущностей (вместо маппера)
    /// </summary>
    public interface IFactory
    {
        #region Методы
        /// <summary>
        /// Возвращает список сущностей контактов
        /// </summary>
        /// <param name="contacts"></param>
        /// <returns></returns>
        IList<ContactModel> CreateContacts(IList<Contact> contacts);

        /// <summary>
        /// Возвращает список сущностей контактов для БД
        /// </summary>
        /// <param name="contacts"></param>
        /// <returns></returns>
        IList<Contact> CreateContacts(IList<ContactModel> contacts);

        /// <summary>
        /// Возвращает <see cref="ContactModel"/>
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        ContactModel CreateContact(Contact contact);

        /// <summary>
        /// Возвращает <see cref="Contact"/> для БД
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        Contact CreateContact(ContactModel contact);

        /// <summary>
        /// Возвращает список сущностей <see cref="MailModel"/> контактов
        /// </summary>
        /// <param name="contactMails"></param>
        /// <returns></returns>
        IList<MailModel> CreateContactMails(IList<EMail> contactMails);

        /// <summary>
        /// Возвращает список сущностей <see cref="EMail"/> контактов для БД
        /// </summary>
        /// <param name="contactMails"></param>
        /// <returns></returns>
        IList<EMail> CreateContactMails(IList<MailModel> contactMails);

        /// <summary>
        /// Возвращает <see cref="MailModel"/> контакта
        /// </summary>
        /// <param name="contacts"></param>
        /// <returns></returns>
        MailModel CreateEMail(EMail contactEMail);

        /// <summary>
        /// Возвращает <see cref="EMail"/> контакта для БД
        /// </summary>
        /// <param name="contacts"></param>
        /// <returns></returns>
        EMail CreateEMail(MailModel contactEMail);

        /// <summary>
        /// Возвращает список сущностей <see cref="PhoneNumberModel"/> контактов
        /// </summary>
        /// <param name="contactPhoneNumbers"></param>
        /// <returns></returns>
        IList<PhoneNumberModel> CreateContactPhoneNumbers(IList<PhoneNumber> contactPhoneNumbers);

        /// <summary>
        /// Возвращает список сущностей <see cref="PhoneNumber"/> контактов для БД
        /// </summary>
        /// <param name="contactPhoneNumbers"></param>
        /// <returns></returns>
        IList<PhoneNumber> CreateContactPhoneNumbers(IList<PhoneNumberModel> contactPhoneNumbers);

        /// <summary>
        /// Возвращает <see cref="PhoneNumberModel"/> контакта
        /// </summary>
        /// <param name="contactPhoneNumber"></param>
        /// <returns></returns>
        PhoneNumberModel CreatePhoneNumber(PhoneNumber contactPhoneNumber);

        /// <summary>
        /// Возвращает <see cref="PhoneNumber"/> контакта для БД
        /// </summary>
        /// <param name="contactPhoneNumber"></param>
        /// <returns></returns>
        PhoneNumber CreatePhoneNumber(PhoneNumberModel contactPhoneNumber);

        #endregion
    }
}
