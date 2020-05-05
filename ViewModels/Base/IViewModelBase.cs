using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Base
{
    public interface IViewModelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        #region Свойства
        /// <summary>
        /// Заголовок окна
        /// </summary>
        string ViewTitle { get; }

        /// <summary>
        /// Флаг изменения данных
        /// </summary>
        bool WasModelChanged { get; set; }

        /// <summary>
        /// Коллекция со списком ошибок
        /// </summary>
        Dictionary<string, string> ErrorCollection { get; }

        /// <summary>
        /// Флаг наличия ошибок в моделе
        /// </summary>
        bool HasErrors { get; }
        #endregion

        #region Методы

        /// <summary>
        /// Очищает список ошибок для определённого свойства
        /// </summary>
        /// <param name="propName">Имя свойства</param>
        void ClearErrors(string propName);

        /// <summary>
        /// Добавляет ошибки в словарь
        /// </summary>
        /// <param name="propName">Имя свойства</param>
        /// <param name="errorMessage">Сообщение об ошибке</param>
        void AddErrorToCollection(string propName, string errorMessage);

        #endregion
    }
}
