using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;

namespace TooksCms.DAL
{
    public partial class ContactInfo : IContactInfo
    {
        public static ContactInfo CreateContactInfo(IContactInfo data)
        {
            var contact = new ContactInfo
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                Title = data.Title,
                ContactInfoUid = data.ContactInfoUid,
                DateCreated = data.DateCreated,
                City = data.City,
                CountryId = data.CountryId
            };
            data.Addresses.ToList().ForEach(a_ => contact.Addresses.Add(Address.CreateAddress(a_)));
            data.PhoneNumbers.ToList().ForEach(p_ => contact.PhoneNumbers.Add(PhoneNumber.CreatePhoneNumber(p_)));
            data.EmailAddresses.ToList().ForEach(e_ => contact.Emails.Add(Email.CreateEmail(e_)));
            return contact;
        }

        #region IContactInfo Members

        IEnumerable<IAddress> IContactInfo.Addresses
        {
            get { return this.Addresses.ToList(); }
        }

        public IEnumerable<IEmail> EmailAddresses
        {
            get { return this.Emails.ToList(); }
        }

        IEnumerable<IPhoneNumber> IContactInfo.PhoneNumbers
        {
            get { return this.PhoneNumbers.ToList(); }
        }

        #endregion

        public void Update(IContactInfo data)
        {
            this.Title = data.Title;
            this.FirstName = data.FirstName;
            this.LastName = data.LastName;
            this.City = data.City;
            this.CountryId = data.CountryId;
        }
    }
}
