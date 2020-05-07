using ContactsBook.Domain.Models;
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
