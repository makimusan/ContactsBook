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

        public void UpdateContacts(IList<Contact> contacts)
        {
            using (ContactsBookEntities contextDB = new ContactsBookEntities(_connectionManger.GetConnectionString()))
            {
                //contacts = contextDB.Contacts.Include(c => c.EMails).Include(c => c.PhoneNumbers).ToList();
            }
        }
        
        public void DeleteContacts(IList<Contact> contacts)
        {
            using (ContactsBookEntities contextDB = new ContactsBookEntities(_connectionManger.GetConnectionString()))
            {
                foreach (var item in contacts)
                {
                    contextDB.Contacts.Attach(item);
                }
                contextDB.Contacts.RemoveRange(contacts.ToList());
                contextDB.SaveChanges();
            }
        }

        public void CreateContacts(IList<Contact> contacts)
        {
            using (ContactsBookEntities contextDB = new ContactsBookEntities(_connectionManger.GetConnectionString()))
            {
                foreach (var item in contacts)
                {
                    contextDB.Entry(item);
                }
                contextDB.Contacts.AddRange(contacts.ToList());
                contextDB.SaveChanges();
            }
        }

        #endregion
    }
}
