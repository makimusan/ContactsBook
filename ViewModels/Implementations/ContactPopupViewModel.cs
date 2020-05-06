using ContactsBook.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using ViewModels.Interfaces;
using ViewModels.Base;
using Infrastructure.Services;
using System.Windows.Input;
using Infrastructure.Commands;
using System.ComponentModel;
using System.Collections.ObjectModel;
using ContactsBook.Helpers.Validation;

namespace ViewModels.Implementations
{
    class ContactPopupViewModel : ViewModelBase, IContactPopupViewModel
    {
        #region Поля
        private IServiceDTO _serviceDTO;
        private ICloneService<ContactModel> _cloneService;
        private ContactModel _contactModelBackUp;
        #endregion

        #region Конструктор
        public ContactPopupViewModel() : base() { }
        #endregion

        #region Свойства

        ContactModel contact;
        public ContactModel Contact {
            get 
            {
                return contact ?? new ContactModel();
            }
            set
            {
                contact = value;
                OnPropertyChanged(nameof(Contact));
            }
        }

        PhoneNumberModel phoneNumber;
        public PhoneNumberModel PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                if(value != phoneNumber)
                {
                    phoneNumber = value;
                    EditablePhoneNumber = value?.PhoneNumber;
                    OnPropertyChanged(nameof(PhoneNumber));
                }
            }
        }

        private string editablePhoneNumber;
        /// <summary>
        /// Возвращает/задаёт новый или редактируемый телефонный номер
        /// </summary>
        public string EditablePhoneNumber
        {
            get { return editablePhoneNumber; }
            set 
            { 
                if(editablePhoneNumber != value)
                {
                    editablePhoneNumber = value;
                    OnPropertyChanged(nameof(EditablePhoneNumber));
                }
            }
        }

        MailModel eMail;
        public MailModel EMail
        {
            get { return eMail; }
            set
            {
                if (value != eMail)
                {
                    eMail = value;
                    EditableEMail = value?.MailOfContact;
                    OnPropertyChanged(nameof(EMail));
                }
            }
        }

        private string editableEMail;
        /// <summary>
        /// Возвращает/задаёт новый или редактируемый @
        /// </summary>
        public string EditableEMail
        {
            get { return editableEMail; }
            set
            {
                if (editableEMail != value)
                {
                    editableEMail = value;
                    OnPropertyChanged(nameof(EditableEMail));
                }
            }
        }

        public override string ViewTitle => "Редактирование контакта";

        #region Обертки над свойствами

        /// <summary>
        /// Свойство-обертка над <see cref="ContactModel.PhoneNumbers"/>.
        /// Список телефонных номеров контакта
        /// </summary>
        public ObservableCollection<PhoneNumberModel> PhoneNumbers
        {
            get { return phoneNumbers ?? (phoneNumbers = new ObservableCollection<PhoneNumberModel>()); }
            set 
            { 
                if(value != phoneNumbers)
                {
                    phoneNumbers = value;
                    OnPropertyChanged(nameof(PhoneNumbers));
                }
            }
        }
        private ObservableCollection<PhoneNumberModel> phoneNumbers;

        /// <summary>
        /// Свойство-обертка над <see cref="ContactModel.MailsOfContact"/>.
        /// Список электронных адресов контакта
        /// </summary>
        public ObservableCollection<MailModel> EMails
        {
            get { return eMails ?? (eMails = new ObservableCollection<MailModel>()); }
            set
            {
                if (value != eMails)
                {
                    eMails = value;
                    OnPropertyChanged(nameof(EMails));
                }
            }
        }
        private ObservableCollection<MailModel> eMails;

        #endregion

        #endregion

        #region Методы
        public void InitializeViewModel(ContactModel contactModel = null)
        {
            _serviceDTO = GetService();
            _cloneService = new ContactCloneService();

            SaveCommand = new UICommand(obj => Save(), cex => CanSave());
            CloseWindowCommand = new UICommand(obj => CloseWindow());
            AddNumberCommand = new UICommand(obj => AddNumber(), cex => CanAddNumber());
            EditNumberCommand = new UICommand(obj => EditNumber(), cex => CanEditNumber());
            DeleteNumberCommand = new UICommand(obj => DelNumber(), cex => CanDelNumber());
            AddEMailCommand = new UICommand(obj => AddEMail(), cex => CanAddEMail());
            EditEMailCommand = new UICommand(obj => EditEMail(), cex => CanEditEMail());
            DeleteEMailCommand = new UICommand(obj => DelEMail(), cex => CanDelEMail());

            Contact = contactModel != null ? CloneContact(contactModel) : new ContactModel();
        }

        public void OnClosingWindow(object sender, CancelEventArgs e)
        {
            _contactModelBackUp = null;
            PhoneNumbers = null;
            EMails = null;
            e.Cancel = false;
        }

        private void CloseWindow()
        {
            
        }

        /// <summary>
        /// Возвращает глубоко клонированную модель <see cref="ContactModel"/>
        /// </summary>
        private ContactModel CloneContact(ContactModel contactModel)
        {
            _contactModelBackUp = contactModel;
            ContactModel contact = _cloneService.CloneObject(contactModel);
            PhoneNumbers = new ObservableCollection<PhoneNumberModel>(contact.PhoneNumbers);
            EMails = new ObservableCollection<MailModel>(contact.MailsOfContact);

            return contact;
        }

        public ContactModel GetChangedContact()
        {
            _contactModelBackUp.Name = Contact.Name;
            _contactModelBackUp.SurName = Contact.SurName;
            _contactModelBackUp.PhoneNumbers = new List<PhoneNumberModel>(PhoneNumbers.ToList());
            _contactModelBackUp.MailsOfContact = new List<MailModel>(EMails.ToList());
            _contactModelBackUp.WasModelChanged = true;

            return _contactModelBackUp;
        }

        public ContactModel GetNewContact()
        {
            Contact.PhoneNumbers = new List<PhoneNumberModel>(PhoneNumbers.ToList());
            Contact.MailsOfContact = new List<MailModel>(EMails.ToList());
            var newContact = _cloneService.CloneObject(Contact);

            return _cloneService.CloneObject(Contact);
        }

        private void Save()
        {

        }

        private bool CanSave()
        {
            return !Contact.HasErrors;
        }

        private void AddNumber()
        {
            PhoneNumbers.Add(new PhoneNumberModel() { ID = 0, PhoneNumber = EditablePhoneNumber });
        }

        private bool CanAddNumber()
        {
            if(ErrorCollection.ContainsKey(nameof(EditablePhoneNumber))) return false;

            if (string.IsNullOrWhiteSpace(EditablePhoneNumber)) return false;

            if (PhoneNumbers.Count == 0) return true;

            if (PhoneNumbers.FirstOrDefault(phnm => phnm.PhoneNumber.Equals(EditablePhoneNumber)) == null) return true;

            return false;
        }

        private void EditNumber()
        {
            PhoneNumber.PhoneNumber = EditablePhoneNumber;
        }

        private bool CanEditNumber()
        {
            if (ErrorCollection.ContainsKey(nameof(EditablePhoneNumber))) return false;

            if (string.IsNullOrWhiteSpace(editablePhoneNumber)) return false;

            if (PhoneNumber == null) return false;

            if (PhoneNumbers.FirstOrDefault(phnm => phnm.PhoneNumber.Equals(editablePhoneNumber)) == null) return true;

            return false;
        }

        private void DelNumber()
        {
            PhoneNumber.IsDeleted = true;
            PhoneNumber = null;
        }

        private bool CanDelNumber()
        {
            return PhoneNumber != null;
        }

        private void AddEMail()
        {
            EMails.Add(new MailModel() { ID = 0, MailOfContact = EditableEMail });
        }

        private bool CanAddEMail()
        {
            if (ErrorCollection.ContainsKey(nameof(EditableEMail))) return false;

            if (string.IsNullOrWhiteSpace(EditableEMail)) return false;

            if (EMails.Count == 0) return true;

            if (EMails.FirstOrDefault(m => m.MailOfContact.Equals(EditableEMail)) == null) return true;

            return false;
        }

        private void EditEMail()
        {
            EMail.MailOfContact = EditableEMail;
        }

        private void DelEMail()
        {
            EMail.IsDeleted = true;
            EMail = null;
        }

        private bool CanDelEMail()
        {
            return EMail != null;
        }

        private bool CanEditEMail()
        {
            if (ErrorCollection.ContainsKey(nameof(EditableEMail))) return false;

            if (string.IsNullOrWhiteSpace(EditableEMail)) return false;

            if (EMail == null) return false;

            if (EMails.FirstOrDefault(m => m.MailOfContact.Equals(EditableEMail)) == null) return true;

            return false;
        }

        #endregion

        #region Команды

        public ICommand CloseWindowCommand
        {
            get; private set;
        }

        public ICommand SaveCommand
        {
            get; private set;
        }
        
        public ICommand AddNumberCommand
        {
            get; private set;
        }
        
        public ICommand EditNumberCommand
        {
            get; private set;
        }
        
        public ICommand DeleteNumberCommand
        {
            get; private set;
        }

        public ICommand AddEMailCommand
        {
            get; private set;
        }

        public ICommand EditEMailCommand
        {
            get; private set;
        }

        public ICommand DeleteEMailCommand
        {
            get; private set;
        }
        #endregion

        #region Валидация

        public override string this[string propertyName]
        {
            get
            {
                string result = null;

                switch (propertyName)
                {
                    case "EditablePhoneNumber":
                        ClearErrors(propertyName);
                        if (!ValidationManager.ContainsOnlyNumbers(EditablePhoneNumber)) result = "Номер должен содержать только цифры";
                        break;
                    case "EditableEMail":
                        ClearErrors(propertyName);
                        if (!ValidationManager.IsEMail(EditableEMail)) result = "Запись не является адресом @";
                        break;
                    default:
                        break;
                }

                AddErrorToCollection(propertyName, result);

                return result;
            }
        }

        #endregion
    }
}
