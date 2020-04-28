using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Base
{
    public interface IViewBase
    {
        #region Свойства
        /// <summary>
        /// Заголовок окна
        /// </summary>
        string ViewTitle { get; }
        #endregion

        #region Методы

        /// <summary>
        /// Инициализирует модель-представление
        /// </summary>
        void InitializeViewModel();

        #endregion
    }
}
