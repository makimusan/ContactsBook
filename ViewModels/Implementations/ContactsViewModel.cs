using ContactsBook.Domain.Models;
using Infrastructure.Services;
using Infrastructure.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ViewModels.Base;
using ViewModels.Interfaces;
using ContactsBook.Locator.Services;
using ViewModels.Manager;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace ViewModels.Implementations
{
    class ContactsViewModel : ViewModelBase, IContactsViewModel
    {
        #region Поля
        private IServiceDTO _serviceDTO;
        private IServiceLocator _serviceLocator;
        private ContactModel _contactModelBackUp;
        #endregion

        #region Конструктор
        public ContactsViewModel() : base()
        {
        }
        #endregion

        #region Реализация IContactsViewModel

        #region Свойства

        private ICollectionView contactCollection;
        public ICollectionView ContactCollection 
        { 
            get { return contactCollection; } 
            private set 
            { 
                contactCollection = value; 
                OnPropertyChanged(nameof(ContactCollection)); 
            } 
        }

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

        public override string ViewTitle => "Справочник контактов";

        private string searchByNameString;

        public string SearchByNameString
        {
            get { return searchByNameString; }
            set 
            { 
                if(searchByNameString != value)
                {
                    searchByNameString = value;
                    OnPropertyChanged(nameof(SearchByNameString));
                    RefreshCollectionView();
                }
            }
        }
        

        private string searchBySurNameString;

        public string SearchBySurNameString
        {
            get { return searchBySurNameString; }
            set 
            { 
                if(searchBySurNameString != value)
                {
                    searchBySurNameString = value;
                    OnPropertyChanged(nameof(SearchByNameString));
                    RefreshCollectionView();
                }
            }
        }
        #endregion

        #region Методы
        public void InitializeViewModel()
        {
            _serviceDTO = GetService();
            _serviceLocator = new ServiceLocator();

            UpdateContacts();
            InitCollectionView();

            SaveCommand = new UICommand(obj => Save(), cex => HasChanges());
            CreateContactCommand = new UICommand(obj => ShowCreateContactWindow());
            EditContactCommand = new UICommand(obj => ShowEditContactWindow(), ced => HasSelectedContact());
            DeleteContactCommand = new UICommand(obj => DeleteContact(), cd => HasSelectedContact());

        }

        private void Save()
        {
            _serviceDTO.SaveContacts(Contacts.Where(c => c.WasModelChanged).ToList());

            ContactCollection = null;
            UpdateContacts();
        }

        private bool HasChanges()
        {
            return Contacts.Any(c => c.WasModelChanged);
        }

        /// <summary>
        /// Обновляет список контактов
        /// </summary>
        private void UpdateContacts()
        {
            Contacts.Clear();
            Contacts = new ObservableCollection<ContactModel>(_serviceDTO.GetContacts());
            InitContacts();
            if (ContactCollection != null) RefreshCollectionView();
            else InitCollectionView();
        }

        private void InitCollectionView()
        {
            // Инициализация ICollectionView
            BindingOperations.EnableCollectionSynchronization(Contacts, new object());
            ContactCollection = CollectionViewSource.GetDefaultView(Contacts);
            // Инициализация фильра ICollectionView
            ContactCollection.Filter = ContactsFilter;

            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Name");
            ContactCollection.GroupDescriptions.Add(groupDescription);

            OnPropertyChanged("ContactCollection");
        }

        private void RefreshCollectionView()
        {
            ContactCollection.Refresh();
        }

        private bool HasSelectedContact()
        {
            return contact != null ? true : false;
        }

        private void DeleteContact()
        {
            if (contact == null) return;
            Contact.IsDeleted = true;
            Contact = null;
        }

        public void OnClosingWindow(object sender, CancelEventArgs e)
        {
            if (HasChanges())
            {
                if (_serviceLocator.ShowQuestionDialog("ВНИМАНИЕ!!!", "Имеются несохранённые изменения! При выходе все данные будут потеряны!\nВы уверены?") == true) 
                {
                    e.Cancel = false;
                    return;
                }
                e.Cancel = true;
                return;
            }
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
        
        public ICommand SaveCommand
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
                var newContact = vm.GetContact();
                if (!CheckEMails(newContact.MailsOfContact.ToList()) || !CheckPhoneNumbers(newContact.PhoneNumbers.ToList()))
                {
                    _serviceLocator.ShowInfoDialog("ВНИМАНИЕ!!!", "Значения телефонного номера и @ должны быть уникальны в рамках одного контакта!");
                    return;
                }

                Contacts.Add(newContact);
            }
        }

        private void ShowEditContactWindow()
        {
            _contactModelBackUp = Contact;
            var vm = ViewModelManager.GetContactViewModel(Contact);
            if (_serviceLocator.ActivateContactWindowDialog(vm) == true)
            {
                var changedContact = vm.GetContact();
                if (!CheckEMails(changedContact.MailsOfContact.ToList()) || !CheckPhoneNumbers(changedContact.PhoneNumbers.ToList()))
                {
                    _serviceLocator.ShowInfoDialog("ВНИМАНИЕ!!!", "Значения телефонного номера и @ должны быть уникальны в рамках одного контакта!");
                    _contactModelBackUp = null;
                    return;
                }
                var index = Contacts.IndexOf(_contactModelBackUp);
                Contacts.Remove(Contact);
                _contactModelBackUp = null;
                Contacts.Insert(index, changedContact);
            }
        }

        private bool CheckEMails(List<MailModel> mailModels)
        {
            var mailAddresses = mailModels.Select(m => m.MailOfContact);

            foreach (var item in Contacts)
            {
                if(_contactModelBackUp == null || item.ID != _contactModelBackUp.ID)
                {
                    var res = item.MailsOfContact.FirstOrDefault(m => mailAddresses.Contains(m.MailOfContact));
                    if (res != null) return false;
                }
                
            }

            return true;
        }

        private bool CheckPhoneNumbers(List<PhoneNumberModel> phoneNumbers)
        {
            var onlyPhoneNumbers = phoneNumbers.Select(p => p.PhoneNumber);

            foreach (var item in Contacts)
            {
                if (_contactModelBackUp == null || item.ID != _contactModelBackUp.ID)
                {
                    var res = item.PhoneNumbers.FirstOrDefault(phn => onlyPhoneNumbers.Contains(phn.PhoneNumber));
                    if (res != null) return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Инициализация новых контактов после добавления
        /// </summary>
        private void InitContacts()
        {
            foreach (var item in Contacts)
            {
                item.WasModelChanged = false;
            }
        }

        /// <summary>
        /// Враппер фильтра контактов
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool ContactsFilter(object item)
        {
            ContactModel tContact = item as ContactModel;

            if (!string.IsNullOrWhiteSpace(SearchByNameString) && !tContact.Name.Contains(SearchByNameString)) return false;
            
            if (!string.IsNullOrWhiteSpace(searchBySurNameString) && !tContact.SurName.Contains(searchBySurNameString)) return false;

            return true;
        }
        #endregion
    }
}
