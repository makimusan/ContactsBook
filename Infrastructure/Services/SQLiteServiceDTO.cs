using ContactsBook.Domain.Models;
using ContactsBook.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Factories;

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
            _repository.DeleteContacts(_factory.CreateContacts(contactModels.Where(c => c.IsDeleted).ToList()));
            //_repository.UpdateContacts(_factory.CreateContacts(contactModels.Where(c => c.ID > 0 && !c.IsDeleted).ToList()));
            _repository.CreateContacts(_factory.CreateContacts(contactModels.Where(c => c.ID == 0).ToList()));
        }
        #endregion

        #endregion
    }
}
