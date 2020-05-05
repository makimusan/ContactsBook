using ContactsBook.Helpers.Validation;
using System.Collections.Generic;

namespace ContactsBook.Domain.Models
{
    /// <summary>
    /// Сущность контакта
    /// </summary>
    public class ContactModel : ModelBase
    {
        #region Свойства

        private string name;
        /// <summary>
        /// Имя контакта
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                    OnDataChanged(nameof(Name));
                }
            }
        }

        private string surName;
        /// <summary>
        /// Имя контакта
        /// </summary>
        public string SurName
        {
            get { return surName; }
            set
            {
                if (value != surName)
                {
                    surName = value;
                    OnDataChanged(nameof(SurName));
                }
            }
        }

        private IList<PhoneNumberModel> phoneNumbers;
        /// <summary>
        /// Список телефонных номеров контакта
        /// </summary>
        public IList<PhoneNumberModel> PhoneNumbers
        {
            get { return phoneNumbers ?? new List<PhoneNumberModel>(); }
            set
            {
                if(phoneNumbers != value)
                {
                    phoneNumbers = value;
                    OnPropertyChanged(nameof(PhoneNumbers));
                }
            }
        }

        private IList<MailModel> mailsOfContact;
        /// <summary>
        /// Список электронных адресов контакта
        /// </summary>
        public IList<MailModel> MailsOfContact
        {
            get { return mailsOfContact ?? new List<MailModel>(); }
            set
            {
                if (mailsOfContact != value)
                {
                    mailsOfContact = value;
                    OnPropertyChanged(nameof(MailsOfContact));
                }
            }
        }

        #endregion

        #region Валидация IDataErrorInfo

        public override string this[string propName] 
        {
            get
            {
                string result = null;

                switch (propName)
                {
                    case "Name":
                        ClearErrors(propName);
                        if (!ValidationManager.ContainsOnlyLetters(Name)) result = "Имя должно содержать только буквы"; 
                        break;
                    case "SurName":
                        ClearErrors(propName);
                        if(!ValidationManager.ContainsOnlyLetters(SurName)) result = "Фамилия должна содержать только буквы"; 
                        break;
                }

                AddErrorToCollection(propName, result);

                return result;
            }
        }

        #endregion
    }
}
