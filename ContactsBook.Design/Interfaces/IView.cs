using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
