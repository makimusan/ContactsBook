using ContactsBook.Helpers.Validation;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ContactsBook.Domain.Models
{
    public abstract class ModelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        #region Свойства

        private int _id;
        /// <summary>
        /// ИД модели
        /// </summary>
        public int ID
        {
            get { return _id; }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged(nameof(ID));
                }
            }
        }

        private bool wasChanged;
        /// <summary>
        /// Флаг изменения данных
        /// </summary>
        public bool WasModelChanged
        {
            get
            {
                return wasChanged;
            }
            set
            {
                wasChanged = value;
            }
        }

        private Dictionary<string, string> errorCollection;
        /// <summary>
        /// Коллекция со списком ошибок
        /// </summary>
        public Dictionary<string, string> ErrorCollection 
        {
            get { return errorCollection ?? (errorCollection = new Dictionary<string, string>()); } 
            private set
            {
                if(value != errorCollection)
                {
                    OnPropertyChanged(nameof(ErrorCollection));
                    errorCollection = value;
                }
            } 
        }

        #endregion

        #region Реализация интерфейса INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод на событие <see cref="PropertyChanged"/>
        /// </summary>
        /// <param name="propName"></param>
        public void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        protected void OnDataChanged([CallerMemberName] string propertyName = null)
        {
            WasModelChanged = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Валидация IDataErrorInfo

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

        #endregion
    }
}
