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
        /// <param name="_propName"></param>
        public void OnPropertyChanged([CallerMemberName] string _propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propName));
        }

        protected void OnDataChanged(string propertyName)
        {
            WasModelChanged = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
