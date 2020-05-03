using ContactsBook.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ContactCloneService : AbstactCloneService<ContactModel>
    {
        ICloneService<PhoneNumberModel> _cloneServicePhoneNambers;
        ICloneService<MailModel> _cloneServiceEMails;

        public ContactCloneService()
        {
            _cloneServicePhoneNambers = new PhoneNumberCloneService();
            _cloneServiceEMails = new EMailCloneService();
        }

        public override ContactModel CloneObject(ContactModel clonableObject)
        {
            return new ContactModel() 
            { 
                ID = clonableObject.ID,
                Name = clonableObject.Name,
                SurName = clonableObject.SurName,
                PhoneNumbers = _cloneServicePhoneNambers.CloneCollection(clonableObject.PhoneNumbers),
                MailsOfContact = _cloneServiceEMails.CloneCollection(clonableObject.MailsOfContact)
            };
        }
    }
}
