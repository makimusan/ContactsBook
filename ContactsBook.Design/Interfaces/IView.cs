using System.ComponentModel;
using System.Windows;

namespace ContactsBook.Design.Interfaces
{
    public interface IView
    {
        object DataContext { get; set; }
        Window Owner { get; set; }

        void Show();
        bool? ShowDialog();
        void Close();

        event CancelEventHandler Closing;
    }
}
