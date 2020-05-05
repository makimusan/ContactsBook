using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Infrastructure.Managers;
using Infrastructure.Services;

namespace ViewModels.Base
{
    public abstract class ViewModelBase : INotifyPropertyChanged
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

        /// <summary>
        /// Флаг изменения данных
        /// </summary>
        public bool WasModelChanged { get; set; }

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
    }
}
