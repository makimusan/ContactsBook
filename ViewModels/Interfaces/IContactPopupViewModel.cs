using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Base;
using ContactsBook.Domain.Models;

namespace ViewModels.Interfaces
{
    /// <summary>
    /// Интерфейс для модели-представления всплывающего окна "Контакт"
    /// </summary>
    public interface IContactPopupViewModel : IViewBase
    {
        #region Свойства

        /// <summary>
        /// Новый или редактируемый контакт
        /// </summary>
        ContactModel Contact { get; }

        #endregion
    }
}
