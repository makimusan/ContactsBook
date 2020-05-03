using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ContactsBook.Design.Dialogs;
using ContactsBook.Design.Interfaces;
using ContactsBook.Design.Views;

namespace ContactsBook.Locator.Services
{
    public class ServiceLocator : IServiceLocator
    {

        public void ActivateMainWindow<T>(T dataContext)
        {
            IView view = new ContactsView();
            view.DataContext = dataContext;
            view.Show();
        }

        public bool? ActivateContactWindowDialog<T>(T dataContext)
        {
            IView view = BaseDialogWindow.GetWindowDialog(new ContactsBook.Design.UserControls.ContactPopupView());
            view.DataContext = dataContext;
            view.Owner = Application.Current.MainWindow;
            
            return view.ShowDialog();
        }

    }
}
