using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Base;
using ContactsBook.Domain.Models;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ViewModels.Interfaces
{
    public interface IContactsViewModel : IViewModelBase
    {
        #region Свойства

        /// <summary>
        /// Коллекция контактов
        /// </summary>
        ObservableCollection<ContactModel> Contacts { get; }

        /// <summary>
        /// Возвращает/задает теущий контакт
        /// </summary>
        ContactModel Contact { get; set; }

        #endregion

        #region Методы

        /// <summary>
        /// Инициализирует модель-представление
        /// </summary>
        void InitializeViewModel();

        /// <summary>
        /// Вызывается при попытке закрыть окно
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnClosingWindow(object sender, CancelEventArgs e);

        #endregion

        #region Команды

        ICommand CreateContactCommand { get; }

        ICommand EditContactCommand{ get; }

        ICommand DeleteContactCommand { get; }
        #endregion
    }
}
