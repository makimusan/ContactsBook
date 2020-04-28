using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Infrastructure.Services
{
    public interface IDialogService
    {
        #region Методы

        Window GetCreateContactDialog();

        #endregion
    }
}
