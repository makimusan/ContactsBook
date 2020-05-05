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

            IList<MailModel> delEMailModels = GetDeletedEMails(contactModels);

            if(delEMailModels.Count > 0)
            {
                foreach (var item in contactModels)
                {
                    item.MailsOfContact = item.MailsOfContact.Except(delEMailModels).ToList();
                }
                DeleteEMails(delEMailModels);

            }

            _repository.UpdateContacts(_factory.CreateContacts(contactModels));
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

        #endregion

        #endregion

        #region Методы

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

        #endregion
    }
}
