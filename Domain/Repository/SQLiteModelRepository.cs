using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ContactsBook.Domain.DataStructs;
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

        public void CreateContacts(IList<Contact> contacts)
        {
            using (ContactsBookEntities contextDB = new ContactsBookEntities(_connectionManger.GetConnectionString()))
            {
                contextDB.Contacts.AddRange(contacts.ToList());
                contextDB.SaveChanges();
            }
        }
        public void UpdateContacts(IList<Contact> contacts)
        {
            using (ContactsBookEntities contextDB = new ContactsBookEntities(_connectionManger.GetConnectionString()))
            {
                foreach (var item in contacts)
                {
                    foreach (var phone in item.PhoneNumbers)
                    {
                        contextDB.Entry(phone).State = EntityState.Modified;
                    }
                    foreach (var eMail in item.EMails)
                    {
                        contextDB.Entry(eMail).State = EntityState.Modified;
                    }

                    contextDB.Entry(item).State = EntityState.Modified;

                }
                contextDB.SaveChanges();
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

        public void CreatePhoneNumbers(IList<PhoneNumber> phoneNumbers)
        {
            using (ContactsBookEntities contextDB = new ContactsBookEntities(_connectionManger.GetConnectionString()))
            {
                foreach (var item in phoneNumbers)
                {
                    item.Contact = null;
                    contextDB.PhoneNumbers.Add(item);
                }
                contextDB.SaveChanges();
            }
        }
        public void DeletePhoneNumbers(IList<PhoneNumber> phoneNumbers)
        {
            using (ContactsBookEntities contextDB = new ContactsBookEntities(_connectionManger.GetConnectionString()))
            {
                foreach (var item in phoneNumbers)
                {
                    contextDB.PhoneNumbers.Attach(item);
                }
                contextDB.PhoneNumbers.RemoveRange(phoneNumbers);
                contextDB.SaveChanges();
            }
        }

        public void CreateEMails(IList<EMail> eMails)
        {
            using (ContactsBookEntities contextDB = new ContactsBookEntities(_connectionManger.GetConnectionString()))
            {
                foreach (var item in eMails)
                {
                    item.Contact = null;
                    contextDB.EMails.Add(item);
                }
                contextDB.SaveChanges();
            }
        }
        public void DeleteEMails(IList<EMail> eMails)
        {
            using (ContactsBookEntities contextDB = new ContactsBookEntities(_connectionManger.GetConnectionString()))
            {
                foreach (var item in eMails)
                {
                    contextDB.EMails.Attach(item);
                }
                contextDB.EMails.RemoveRange(eMails);
                contextDB.SaveChanges();
            }
        }

        #endregion
    }
}
