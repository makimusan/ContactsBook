using ContactsBook.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Interfaces;
using ViewModels.Base;
using Infrastructure.Services;

namespace ViewModels.Implementations
{
    class ContactPopupViewModel : ViewModelBase, IContactPopupViewModel
    {
        #region Поля
        private IServiceDTO _serviceDTO;
        #endregion

        #region Конструктор
        public ContactPopupViewModel() : base()
        {

        }
        #endregion

        #region Свойства
        public ContactModel Contact => new ContactModel();

        public string ViewTitle => "Редактирование контакта";
        #endregion

        #region Методы
        public void InitializeViewModel()
        {
            _serviceDTO = GetService();
            //Contacts = _serviceDTO.GetContacts();
        }

        #endregion
    }
}
