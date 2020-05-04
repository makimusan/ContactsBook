using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ContactsBook.Domain.Models
{
    public abstract class ModelBase : INotifyPropertyChanged
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
    }
}
