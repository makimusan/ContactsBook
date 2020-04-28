using ContactsBook.Domain.Models;
using Infrastructure.Services;
using Infrastructure.Commands;
using Infrastructure.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.Base;
using ViewModels.Interfaces;

namespace ViewModels.Implementations
{
    class ContactsViewModel : ViewModelBase, IContactsViewModel
    {
        #region Поля
        private IServiceDTO _serviceDTO;
        #endregion

        #region Конструктор
        public ContactsViewModel() : base()
        {
            InitializeViewModel();
        }
        #endregion

        #region Реализация IContactsViewModel

        #region Свойства
        private IList<ContactModel> contacts;
        public IList<ContactModel> Contacts 
        {
            get { return contacts ?? new List<ContactModel>(); }
            private set
            {
                if(contacts != value)
                {
                    contacts = value;
                    OnDataChanged(nameof(Contacts));
                }
            }
        }

        public string ViewTitle => "Справочник контактов";

        #endregion

        #region Методы
        public void InitializeViewModel()
        {
            _serviceDTO = GetService();
            Contacts = _serviceDTO.GetContacts();
            WasModelChanged = false;

            CreateContactCommand = new UICommand((x) => ShowCreateContactWindow(x));
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

        #endregion

        #endregion

        #region Методы

        private void ShowCreateContactWindow(object modalWindow)
        {
            DialogManager.ShowUserDialog(modalWindow);
        }

        #endregion


    }
}
