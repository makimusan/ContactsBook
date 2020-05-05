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
            List<Contact> entContacts = new List<Contact>();

            foreach (var item in contacts)
            {
                entContacts.Add(CreateContact(item));
            }

            return entContacts;
        }

        public ContactModel CreateContact(Contact contact)
        {
            ContactModel contactModel = new ContactModel() { ID = (int)contact.ID, Name = contact.Name, SurName = contact.Surname };

            contactModel.PhoneNumbers = CreateContactPhoneNumbers(contact.PhoneNumbers.ToList());

            contactModel.MailsOfContact = CreateContactMails(contact.EMails.ToList());

            return contactModel;
        }

        public Contact CreateContact(ContactModel contact)
        {
            Contact entContact = new Contact() { ID = contact.ID, Name = contact.Name, Surname = contact.SurName };

            entContact.PhoneNumbers = CreateContactPhoneNumbers(contact.PhoneNumbers.ToList(), contact.ID, entContact);

            entContact.EMails = CreateContactMails(contact.MailsOfContact.ToList(), contact.ID, entContact);

            return entContact;
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

        public IList<EMail> CreateContactMails(IList<MailModel> contactMails, int contactID, Contact contact)
        {
            List<EMail> eMailModels = new List<EMail>();
            foreach (var item in contactMails)
            {
                eMailModels.Add(CreateEMail(item, contactID, contact));
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

        public IList<PhoneNumber> CreateContactPhoneNumbers(IList<PhoneNumberModel> contactPhoneNumbers, int contactID, Contact contact)
        {
            List<PhoneNumber> phoneNumberModels = new List<PhoneNumber>();
            foreach (var item in contactPhoneNumbers)
            {
                phoneNumberModels.Add(CreatePhoneNumber(item, contactID, contact));
            }
            return phoneNumberModels;
        }

        public MailModel CreateEMail(EMail contactEMail)
        {
            return new MailModel() { ID = (int)contactEMail.ID, MailOfContact = contactEMail.EMailAddress };
        }

        public EMail CreateEMail(MailModel contactEMail, int contactID, Contact contact)
        {
            return new EMail() { ID = contactEMail.ID, EMailAddress = contactEMail.MailOfContact, ContactID = contactID, Contact = contact };
        }

        public PhoneNumberModel CreatePhoneNumber(PhoneNumber contactPhoneNumber)
        {
            return new PhoneNumberModel() { ID = (int)contactPhoneNumber.ID, PhoneNumber = contactPhoneNumber.Number };
        }

        public PhoneNumber CreatePhoneNumber(PhoneNumberModel contactPhoneNumber, int contactID, Contact contact)
        {
            return new PhoneNumber() { ID = contactPhoneNumber.ID, Number = contactPhoneNumber.PhoneNumber, ContactID = contactID, Contact = contact };
        }
        #endregion

        #endregion
    }
}
