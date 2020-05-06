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
            IView view = new ContactPopupView();
            view.DataContext = dataContext;
            view.Owner = Application.Current.MainWindow;
            
            return view.ShowDialog();
        }

        public bool? ShowQuestionDialog(string title, string question)
        {
            IView view = BaseDialog.ShowQuestionDialog(title, question);
            view.Owner = Application.Current.MainWindow;

            return view.ShowDialog();
        }

        public bool? ShowInfoDialog(string title, string question)
        {
            IView view = BaseDialog.ShowInfoDialog(title, question);
            view.Owner = Application.Current.MainWindow;

            return view.ShowDialog();
        }

    }
}
