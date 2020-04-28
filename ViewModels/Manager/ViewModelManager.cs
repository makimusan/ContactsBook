using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Implementations;
using ViewModels.Interfaces;

namespace ViewModels.Manager
{
    public static class ViewModelManager
    {
        public static IContactsViewModel GetContactsViewModel()
        {
            return new ContactsViewModel();
        }

        public static IContactPopupViewModel GetContactViewModel()
        {
            return new ContactPopupViewModel();
        }
    }
}
