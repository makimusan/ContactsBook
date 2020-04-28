using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Base;
using ContactsBook.Domain.Models;
using System.Windows.Input;

namespace ViewModels.Interfaces
{
    public interface IContactsViewModel : IViewBase
    {
        #region Свойства

        /// <summary>
        /// Коллекция контактов
        /// </summary>
        IList<ContactModel> Contacts { get; }

        #endregion

        #region Команды

        ICommand CreateContactCommand { get; }

        ICommand EditContactCommand{ get; }

        ICommand DeleteContactCommand { get; }
        #endregion
    }
}
