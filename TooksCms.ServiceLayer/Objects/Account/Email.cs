using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;

namespace TooksCms.ServiceLayer.Objects.Account
{
    public class Email : IEmail
    {
        public Email() { }

        public Email(IEmail data)
        {
            EmailId = data.EmailId;
            EmailUid = data.EmailUid;
            ContactInfoId = data.ContactInfoId;
            Address = data.Address;
            IsPrimary = data.IsPrimary;
        }

        #region IEmail Members

        public int EmailId { get; private set; }

        public Guid EmailUid { get; private set; }

        public int ContactInfoId { get; private set; }

        public string Address { get; private set; }

        public bool IsPrimary { get; private set; }

        #endregion

        public static Email CreateEmail(int id, Guid uid, int contactInfoId, string address, bool isPrimary)
        {
            return new Email
            {
                EmailId = id,
                EmailUid = uid,
                ContactInfoId = contactInfoId,
                Address = address,
                IsPrimary = isPrimary
            };
        }
    }
}
