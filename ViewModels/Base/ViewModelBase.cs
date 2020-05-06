using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Infrastructure.Managers;
using Infrastructure.Services;
using ContactsBook.Helpers.Interfaces;

namespace ViewModels.Base
{
    public abstract class ViewModelBase : IViewModelBase
    {
        #region Поля
        private IServiceDTO _serviceDTO;
        #endregion

        #region Конструктор
        public ViewModelBase()
        {
            _serviceDTO = MenagerServices.GetCurrentService();

        }
        #endregion

        #region Свойства

        public virtual string ViewTitle { get { return "BaseViewModel"; } }

        public bool WasModelChanged { get; set; }

        private Dictionary<string, string> errorCollection;
        public Dictionary<string, string> ErrorCollection
        {
            get { return errorCollection ?? (errorCollection = new Dictionary<string, string>()); }
            private set
            {
                if (value != errorCollection)
                {
                    OnPropertyChanged(nameof(ErrorCollection));
                    errorCollection = value;
                }
            }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Возвращает текущий сервис <see cref="IServiceDTO"/> для модели представления
        /// </summary>
        /// <returns></returns>
        public IServiceDTO GetService()
        {
            return _serviceDTO;
        }

        #endregion

        #region Реализация интерфейса INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод на событие <see cref="PropertyChanged"/>
        /// </summary>
        /// <param name="_propName"></param>
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnDataChanged([CallerMemberName] string propertyName = null)
        {
            WasModelChanged = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region IDataErrorInfo

        public string Error => null;

        /// <summary>
        /// Вызывается при изменении свойства
        /// </summary>
        /// <param name="propName"></param>
        /// <returns></returns>
        public virtual string this[string propName]
        {
            get
            {
                string result = null;

                switch (propName)
                {
                    default:
                        ClearErrors(propName);
                        break;
                }

                AddErrorToCollection(propName, result);

                return result;
            }
        }

        /// <summary>
        /// Флаг наличия ошибок в моделе
        /// </summary>
        public virtual bool HasErrors => ErrorCollection.Any();

        /// <summary>
        /// Очищает список ошибок для определённого свойства
        /// </summary>
        /// <param name="propName">Имя свойства</param>
        public virtual void ClearErrors(string propName)
        {
            if (ErrorCollection.ContainsKey(propName))
            {
                ErrorCollection.Remove(propName);
            }
        }

        /// <summary>
        /// Добавляет ошибки в словарь
        /// </summary>
        /// <param name="propName">Имя свойства</param>
        /// <param name="errorMessage">Сообщение об ошибке</param>
        public virtual void AddErrorToCollection(string propName, string errorMessage)
        {
            if (ErrorCollection.ContainsKey(propName)) ErrorCollection[propName] = errorMessage;
            else if (errorMessage != null) ErrorCollection.Add(propName, errorMessage);
        }

        #endregion
    }
}
