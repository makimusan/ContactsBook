using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsBook.Domain.DataStructs;
using ContactsBook.Domain.Models;
using ContactsBook.Domain.Managers;

namespace ContactsBook.Domain.Repository
{
    public class SQLiteModelRepository
    {
        #region Поля
        private IConnectionManager _connectionManger;
        #endregion

        #region Конструктор

        public SQLiteModelRepository()
        {
            _connectionManger = new SQLiteConnectionManager();
        }

        #endregion

        #region Методы

        public IList<Contact> GetContacts()
        {
            List<Contact> contacts = new List<Contact>();
            using (ContactsBookEntities contextDB = new ContactsBookEntities(_connectionManger.GetConnectionString()))
            {
                contacts = contextDB.Contacts.Include(c => c.EMails).Include(c => c.PhoneNumbers).ToList();
            }

            return contacts;
        }

        #endregion
    }
}
