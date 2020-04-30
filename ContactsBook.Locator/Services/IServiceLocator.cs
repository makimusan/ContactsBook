using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ContactsBook.Design.Interfaces;

namespace ContactsBook.Locator.Services
{
    public interface IServiceLocator
    {
        void ActivateMainWindow<T>(T dataContext);

        void ActivateContactWindow<T>(T dataContext);
    }
}
