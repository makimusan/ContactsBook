using ContactsBook.Domain.Models;
using ContactsBook.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Factories;
using ContactsBook.Domain.DataStructs;

namespace Infrastructure.Services
{
    /// <summary>
    /// Сервис для работы с наборами данных из SQLite БД
    /// </summary>
    public class SQLiteServiceDTO : IServiceDTO
    {
        #region Поля
        SQLiteModelRepository _repository;
        IFactory _factory;
        #endregion

        #region Конструктор

        public SQLiteServiceDTO()
        {
            _repository = new SQLiteModelRepository();
            _factory = new SQLiteFactory();
        }

        #endregion

        #region Реализация интерфейса IServiceDTO

        #region Методы
        public IList<ContactModel> GetContacts()
        {
          return _factory.CreateContacts(_repository.GetContacts());
        }

        public void SaveContacts(IList<ContactModel> contactModels)
        {
            CreateContacts(contactModels.Where(c => c.ID == 0).ToList());
            UpdateContacts(contactModels.Where(c => c.ID > 0 && !c.IsDeleted).ToList());
            DeleteContacts(contactModels.Where(c => c.IsDeleted).ToList());

        }

        public void CreateContacts(IList<ContactModel> contactModels)
        {
            if (contactModels.Count == 0) return;
            _repository.CreateContacts(_factory.CreateContacts(contactModels));
        }
        public void UpdateContacts(IList<ContactModel> contactModels)
        {
            if (contactModels.Count == 0) return;

            #region Удаление телефонных номеров и @
            IList<MailModel> delEMailModels = GetDeletedEMails(contactModels);
            if(delEMailModels.Count > 0)
            {
                DeleteEMails(delEMailModels);
                foreach (var item in contactModels)
                {
                    item.MailsOfContact = item.MailsOfContact.Except(delEMailModels).ToList();
                }
            }
            IList<PhoneNumberModel> delPhoneNumberModels = GetDeletedPhoneNumbers(contactModels);
            if (delPhoneNumberModels.Count > 0)
            {
                DeletePhoneNumbers(delPhoneNumberModels);
                foreach (var item in contactModels)
                {
                    item.PhoneNumbers = item.PhoneNumbers.Except(delPhoneNumberModels).ToList();
                }
            }
            #endregion

            #region Сохранение новых телефонных номеров и @
            var contacts = _factory.CreateContacts(contactModels);
            IList<PhoneNumber> newPhoneNumbers = GetNewPhoneNumbers(contacts);
            if (newPhoneNumbers.Count > 0)
            {
                _repository.CreatePhoneNumbers(newPhoneNumbers);
                foreach (var item in contacts)
                {
                    item.PhoneNumbers = item.PhoneNumbers.Except(newPhoneNumbers).ToList();
                }
            }
            IList<EMail> newEMails = GetNewEMails(contacts);
            if (newEMails.Count > 0)
            {
                _repository.CreateEMails(newEMails);
                foreach (var item in contacts)
                {
                    item.EMails = item.EMails.Except(newEMails).ToList();
                }
            }
            #endregion

            _repository.UpdateContacts(contacts);
        }
        public void DeleteContacts(IList<ContactModel> contactModels)
        {
            if (contactModels.Count == 0) return;
            _repository.DeleteContacts(_factory.CreateContacts(contactModels));
        }

        public void DeleteEMails(IList<MailModel> eMails)
        {
            _repository.DeleteEMails(_factory.CreateContactMails(eMails));
        }
        
        public void DeletePhoneNumbers(IList<PhoneNumberModel> phoneNumbers)
        {
            _repository.DeletePhoneNumbers(_factory.CreateContactPhoneNumbers(phoneNumbers));
        }

        #endregion

        #endregion

        #region Методы

        /// <summary>
        /// Возвращает список @ адресов помеченных на удаление
        /// </summary>
        /// <param name="contactModels"></param>
        /// <returns></returns>
        private IList<MailModel> GetDeletedEMails(IList<ContactModel> contactModels)
        {
            List<MailModel> delEMailModels = new List<MailModel>();

            foreach (var item in contactModels)
            {
                var delItems = (item.MailsOfContact.Where(m => m.IsDeleted).ToList());
                foreach (var delMail in delItems)
                {
                    delEMailModels.Add(delMail);
                }
            }
            
            return delEMailModels;
        }

        /// <summary>
        /// Возвращает список телефонных номеров помеченных на удаление
        /// </summary>
        /// <param name="contactModels"></param>
        /// <returns></returns>
        private IList<PhoneNumberModel> GetDeletedPhoneNumbers(IList<ContactModel> contactModels)
        {
            List<PhoneNumberModel> delPhoneNumberModels = new List<PhoneNumberModel>();

            foreach (var item in contactModels)
            {
                var delItems = (item.PhoneNumbers.Where(m => m.IsDeleted).ToList());
                foreach (var delPhoneNumber in delItems)
                {
                    delPhoneNumberModels.Add(delPhoneNumber);
                }
            }

            return delPhoneNumberModels;
        }

        private IList<PhoneNumber> GetNewPhoneNumbers(IList<Contact> contactModels)
        {
            List<PhoneNumber> newPhoneNumberModels = new List<PhoneNumber>();

            foreach (var item in contactModels)
            {
                var newItems = (item.PhoneNumbers.Where(m => m.ID == 0).ToList());
                foreach (var newPhoneNumber in newItems)
                {
                    newPhoneNumberModels.Add(newPhoneNumber);
                }
            }

            return newPhoneNumberModels;
        }
        private IList<EMail> GetNewEMails(IList<Contact> contactModels)
        {
            List<EMail> newEMails = new List<EMail>();

            foreach (var item in contactModels)
            {
                var newItems = (item.EMails.Where(m => m.ID == 0).ToList());
                foreach (var newEmail in newItems)
                {
                    newEMails.Add(newEmail);
                }
            }

            return newEMails;
        }
    }
        #endregion
}
