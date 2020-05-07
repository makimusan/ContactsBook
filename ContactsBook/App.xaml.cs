using System.Windows;
using ContactsBook.Locator.Services;
using ViewModels.Manager;

namespace ContactsBook
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            IServiceLocator serviceLocator = new ServiceLocator();
            serviceLocator.ActivateMainWindow(ViewModelManager.GetContactsViewModel());
            //ContactsView view = new ContactsView();
            ////view.DataContext = new ViewModels.MainViewModel();
            //view.Show();
            
        }
    }
}
