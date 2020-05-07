using ViewModels.Base;
using ContactsBook.Domain.Models;
using System.ComponentModel;

namespace ViewModels.Interfaces
{
    /// <summary>
    /// Интерфейс для модели-представления всплывающего окна "Контакт"
    /// </summary>
    public interface IContactPopupViewModel : IViewModelBase
    {
        #region Свойства

        /// <summary>
        /// Новый или редактируемый контакт
        /// </summary>
        ContactModel Contact { get; }

        #endregion

        #region Методы

        void InitializeViewModel(ContactModel contactModel = null);

        void OnClosingWindow(object sender, CancelEventArgs e);

        /// <summary>
        /// Возвращает новый или изменённый контакт
        /// </summary>
        /// <returns></returns>
        ContactModel GetContact();

        #endregion
    }
}
