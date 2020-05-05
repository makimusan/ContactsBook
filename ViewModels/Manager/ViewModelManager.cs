using ContactsBook.Domain.Models;
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
            IContactsViewModel contactsViewModel = new ContactsViewModel();
            contactsViewModel.InitializeViewModel();
            return contactsViewModel;
        }

        public static IContactPopupViewModel GetContactViewModel(ContactModel contactModel = null)
        {
            IContactPopupViewModel contactPopupViewModel = new ContactPopupViewModel();
            contactPopupViewModel.InitializeViewModel(contactModel);
            return contactPopupViewModel;
        }
    }
}
