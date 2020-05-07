using ContactsBook.Domain.Models;
using System.Collections.Generic;

namespace Infrastructure.Services
{
    class PhoneNumberCloneService : AbstactCloneService<PhoneNumberModel>
    {
        public override IList<PhoneNumberModel> CloneCollection(IList<PhoneNumberModel> clonableCollection)
        {
            List<PhoneNumberModel> phoneNumbers = new List<PhoneNumberModel>();
            foreach (var item in clonableCollection)
            {
                phoneNumbers.Add(CloneObject(item));
            }
            return phoneNumbers;
        }

        public override PhoneNumberModel CloneObject(PhoneNumberModel clonableObject)
        {
            return new PhoneNumberModel()
            {
                ID = clonableObject.ID,
                PhoneNumber = clonableObject.PhoneNumber,
                IsDeleted = clonableObject.IsDeleted
            };
        }
    }
}
