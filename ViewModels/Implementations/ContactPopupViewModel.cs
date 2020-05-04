﻿using ContactsBook.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Interfaces;
using ViewModels.Base;
using Infrastructure.Services;
using System.Windows.Input;
using Infrastructure.Commands;
using System.ComponentModel;
using System.Collections.ObjectModel;
using ContactsBook.Domain.DataStructs;
using ViewModels.Manager;
using ContactsBook.Helpers.Validation;

namespace ViewModels.Implementations
{
    class ContactPopupViewModel : ViewModelBase, IContactPopupViewModel, IDataErrorInfo
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

        public string ViewTitle => "Редактирование контакта";

        public bool? ModalDialogResult { get; set; }

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

            CloseWindowCommand = new UICommand(obj => CloseWindow());
            AddNumberCommand = new UICommand(obj => AddNumber(), cex => CanAddNumber());
            EditNumberCommand = new UICommand(obj => EditNumber(), cex => CanEditNumber());
            DeleteNumberCommand = new UICommand(obj => { PhoneNumbers.Remove(PhoneNumber); }, cex => PhoneNumber != null);
            AddEMailCommand = new UICommand(obj => AddEMail(), cex => CanAddEMail());
            EditEMailCommand = new UICommand(obj => EditEMail(), cex => CanEditEMail());
            DeleteEMailCommand = new UICommand(obj => { EMails.Remove(EMail); }, cex => EMail != null);


            Contact = contactModel != null ? CloneContact(contactModel) : new ContactModel();
            Contact.PropertyChanged += Model_PropertyChanged;
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
            ModalDialogResult = false;
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
            newContact.WasModelChanged = true;

            return _cloneService.CloneObject(Contact);
        }

        private void AddNumber()
        {
            PhoneNumbers.Add(new PhoneNumberModel() { ID = 0, PhoneNumber = EditablePhoneNumber });
        }

        private bool CanAddNumber()
        {
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
            if (string.IsNullOrWhiteSpace(editablePhoneNumber)) return false;

            if (PhoneNumber == null) return false;

            if (PhoneNumbers.FirstOrDefault(phnm => phnm.PhoneNumber.Equals(editablePhoneNumber)) == null) return true;

            return false;
        }

        private void AddEMail()
        {
            EMails.Add(new MailModel() { ID = 0, MailOfContact = EditableEMail });
        }

        private bool CanAddEMail()
        {
            if (string.IsNullOrWhiteSpace(EditableEMail)) return false;

            if (EMails.Count == 0) return true;

            if (EMails.FirstOrDefault(m => m.MailOfContact.Equals(EditableEMail)) == null) return true;

            return false;
        }

        private void EditEMail()
        {
            EMail.MailOfContact = EditableEMail;
        }

        private bool CanEditEMail()
        {
            if (string.IsNullOrWhiteSpace(EditableEMail)) return false;

            if (EMail == null) return false;

            if (EMails.FirstOrDefault(m => m.MailOfContact.Equals(EditableEMail)) == null) return true;

            return false;
        }

        private void OnOptionalServicesListChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                    item.PropertyChanged -= Model_PropertyChanged;
            }
            if (e.NewItems != null)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                    item.PropertyChanged += Model_PropertyChanged;
            }
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnDataChanged(e.PropertyName);
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

        #region

        public string Error => null;

        public string this[string propertyName]
        {
            get
            {
                string result = null;

                if (propertyName == nameof(EditablePhoneNumber))
                {
                    if (!ValidationManager.ContainsOnlyNumbers(EditablePhoneNumber)) return "Номер должен содержать только цифры";
                }

                return result;
            }
        }

        #endregion
    }
}
