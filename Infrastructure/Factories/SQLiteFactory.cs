using ContactsBook.Domain.DataStructs;
using ContactsBook.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Factories
{
    /// <summary>
    /// Фабрика-маппер для прямых и обратных сборок БД-Модель-БД
    /// </summary>
    class SQLiteFactory : IFactory
    {
        #region Реализация IFactory

        #region Методы

        public IList<ContactModel> CreateContacts(IList<Contact> contacts)
        {
            List<ContactModel> contactsModels = new List<ContactModel>();

            foreach (var item in contacts)
            {
                contactsModels.Add(CreateContact(item));
            }

            return contactsModels;
        }

        public IList<Contact> CreateContacts(IList<ContactModel> contacts)
        {
            throw new NotImplementedException();
        }

        public ContactModel CreateContact(Contact contact)
        {
            ContactModel contactModel = new ContactModel() { ID = (int)contact.ID, Name = contact.Name, SurName = contact.Surname };

            contactModel.PhoneNumbers = CreateContactPhoneNumbers(contact.PhoneNumbers.ToList());//contact.PhoneNumbers != null ? CreateContactPhoneNumbers(contact.PhoneNumbers.ToList()) : null;

            contactModel.MailsOfContact = CreateContactMails(contact.EMails.ToList());//contact.EMails != null ? CreateContactMails(contact.EMails.ToList()) : null;

            return contactModel;
        }

        public MailModel CreateContactMail(EMail contactMail)
        {
            throw new NotImplementedException();
        }

        public PhoneNumberModel CreateContactPhoneNumber(PhoneNumber contactPhoneNumber)
        {
            throw new NotImplementedException();
        }

        public Contact CreateContact(ContactModel contact)
        {
            throw new NotImplementedException();
        }

        public EMail CreateContactMail(MailModel contactMail)
        {
            throw new NotImplementedException();
        }

        public PhoneNumber CreateContactPhoneNumber(PhoneNumberModel contactPhoneNumber)
        {
            throw new NotImplementedException();
        }

        public IList<MailModel> CreateContactMails(IList<EMail> contactMails)
        {
            List<MailModel> eMailModels = new List<MailModel>();
            foreach (var item in contactMails)
            {
                eMailModels.Add(CreateEMail(item));
            }
            return eMailModels;
        }

        public IList<PhoneNumberModel> CreateContactPhoneNumbers(IList<PhoneNumber> contactPhoneNumbers)
        {
            List<PhoneNumberModel> phoneNumberModels = new List<PhoneNumberModel>();
            foreach (var item in contactPhoneNumbers)
            {
                phoneNumberModels.Add(CreatePhoneNumber(item));
            }
            return phoneNumberModels;
        }

        public IList<EMail> CreateContactMails(IList<MailModel> contactMails)
        {
            throw new NotImplementedException();
        }



        public EMail CreateEMail(MailModel contactEMail)
        {
            throw new NotImplementedException();
        }



        public IList<PhoneNumber> CreateContactPhoneNumbers(IList<PhoneNumberModel> contactPhoneNumbers)
        {
            throw new NotImplementedException();
        }

        public MailModel CreateEMail(EMail contactEMail)
        {
            return new MailModel() { ID = (int)contactEMail.ID, MailOfContact = contactEMail.EMailAddress };
        }

        public PhoneNumberModel CreatePhoneNumber(PhoneNumber contactPhoneNumber)
        {
            return new PhoneNumberModel() { ID = (int)contactPhoneNumber.ID, PhoneNumber = contactPhoneNumber.Number };
        }

        public PhoneNumber CreatePhoneNumber(PhoneNumberModel contactPhoneNumber)
        {
            throw new NotImplementedException();
        }
        #endregion

        #endregion
    }
}
