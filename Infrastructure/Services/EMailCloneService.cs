﻿using ContactsBook.Domain.Models;
using System.Collections.Generic;

namespace Infrastructure.Services
{
    class EMailCloneService : AbstactCloneService<MailModel>
    {
        public override IList<MailModel> CloneCollection(IList<MailModel> clonableCollection)
        {
            List<MailModel> eMails = new List<MailModel>();
            foreach (var item in clonableCollection)
            {
                eMails.Add(CloneObject(item));
            }
            return eMails;
        }

        public override MailModel CloneObject(MailModel clonableObject)
        {
            return new MailModel()
            {
                ID = clonableObject.ID,
                MailOfContact = clonableObject.MailOfContact,
                IsDeleted = clonableObject.IsDeleted
            };
        }
    }
}
