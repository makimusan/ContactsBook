using ContactsBook.Domain.Models;
using Infrastructure.Services;
using Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.Base;
using ViewModels.Interfaces;
using ContactsBook.Locator.Services;
using ViewModels.Manager;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ViewModels.Implementations
{
    class ContactsViewModel : ViewModelBase, IContactsViewModel
    {
        #region Поля
        private IServiceDTO _serviceDTO;
        private IServiceLocator _serviceLocator;
        #endregion

        #region Конструктор
        public ContactsViewModel() : base()
        {
            InitializeViewModel();
        }
        #endregion

        #region Реализация IContactsViewModel

        #region Свойства
        private ObservableCollection<ContactModel> contacts;
        public ObservableCollection<ContactModel> Contacts 
        {
            get { return contacts ?? new ObservableCollection<ContactModel>(); }
            private set
            {
                if(contacts != value)
                {
                    contacts = value;
                    OnDataChanged(nameof(Contacts));
                }
            }
        }

        private ContactModel contact;
        public ContactModel Contact
        {
            get { return contact; }
            set 
            { 
                if(contact != value)
                {
                    contact = value;
                    OnPropertyChanged(nameof(Contact));
                }
            }
        }


        public string ViewTitle => "Справочник контактов";

        #endregion

        #region Методы
        public void InitializeViewModel()
        {
            _serviceDTO = GetService();
            _serviceLocator = new ServiceLocator();

            Contacts = new ObservableCollection<ContactModel>(_serviceDTO.GetContacts());
            WasModelChanged = false;

            CreateContactCommand = new UICommand(obj => ShowCreateContactWindow());
            EditContactCommand = new UICommand(obj => ShowEditContactWindow(), ced => HasSelectedContact());
            DeleteContactCommand = new UICommand(obj => DeleteContact(), cd => HasSelectedContact());

            SubscribeToEvent();
        }

        private bool HasSelectedContact()
        {
            return contact != null ? true : false;
        }

        private void DeleteContact()
        {
            if (contact == null) return;

            Contacts.Remove(Contact);
        }

        public void OnClosingWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = false;
        }

        #endregion

        #region Команды

        public ICommand CreateContactCommand
        {
            get; private set;
        }

        public ICommand EditContactCommand
        {
            get; private set;
        }

        public ICommand DeleteContactCommand
        {
            get; private set;
        }

        public ICommand CloseWindowCommand
        {
            get; private set;
        }

        #endregion

        #endregion

        #region Методы

        private void ShowCreateContactWindow()
        {
            var vm = ViewModelManager.GetContactViewModel();
            if (_serviceLocator.ActivateContactWindowDialog(vm) == true)
            {
                var newContact = vm.GetNewContact();
                if (!CheckEMails(newContact.MailsOfContact.ToList()) || !CheckPhoneNumbers(newContact.PhoneNumbers.ToList())) return;

                Contacts.Add(newContact);
            }
        }

        private void ShowEditContactWindow()
        {
            var vm = ViewModelManager.GetContactViewModel(Contact);
            if (_serviceLocator.ActivateContactWindowDialog(vm) == true)
            {
                Contact = vm.GetChangedContact();
            }

        }

        private bool CheckEMails(List<MailModel> mailModels)
        {
            var mailAddresses = mailModels.Select(m => m.MailOfContact);

            foreach (var item in Contacts)
            {
                var res = item.MailsOfContact.FirstOrDefault(m => mailAddresses.Contains(m.MailOfContact));
                if (res != null) return false;
            }

            return true;
        }

        private bool CheckPhoneNumbers(List<PhoneNumberModel> phoneNumbers)
        {
            var onlyPhoneNumbers = phoneNumbers.Select(p => p.PhoneNumber);

            foreach (var item in Contacts)
            {
                var res = item.PhoneNumbers.FirstOrDefault(phn => onlyPhoneNumbers.Contains(phn.PhoneNumber));
                if (res != null) return false;
            }

            return true;
        }

        private void SubscribeToEvent()
        {
            
        }
        #endregion
    }
}
