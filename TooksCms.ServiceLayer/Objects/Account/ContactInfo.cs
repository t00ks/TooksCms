using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;

namespace TooksCms.ServiceLayer.Objects.Account
{
    public class ContactInfo : IContactInfo
    {
        public ContactInfo() { }

        public ContactInfo(IContactInfo data)
        {
            ContactInfoId = data.ContactInfoId;
            ContactInfoUid = data.ContactInfoUid;
            FirstName = data.FirstName;
            LastName = data.LastName;
            Title = data.Title;
            DateCreated = data.DateCreated;
            City = data.City;
            CountryId = data.CountryId;
            Addresses = data.Addresses;
            PhoneNumbers = data.PhoneNumbers;
            EmailAddresses = data.EmailAddresses;
        }

        #region IContactInfo Members

        public int ContactInfoId { get; private set; }

        public Guid ContactInfoUid { get; private set; }

        public string Title { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public IEnumerable<IAddress> Addresses { get; private set; }

        public IEnumerable<IEmail> EmailAddresses { get; private set; }

        public IEnumerable<IPhoneNumber> PhoneNumbers { get; private set; }

        public DateTime DateCreated { get; private set; }

        public int CountryId { get; private set; }

        public string City { get; private set; }

        #endregion

        public static ContactInfo CreateContactInfo(int id, Guid uid, string firstName, string lastName, string title, DateTime dateCreated,
            string city, int countryId, IEnumerable<IAddress> addresses, IEnumerable<IPhoneNumber> phoneNumbers, IEnumerable<IEmail> emailAddresses)
        {
            return new ContactInfo
            {
                ContactInfoId = id,
                ContactInfoUid = uid,
                FirstName = firstName,
                LastName = lastName,
                Title = title,
                DateCreated = dateCreated,
                City = city,
                CountryId = countryId,
                Addresses = addresses,
                PhoneNumbers = phoneNumbers,
                EmailAddresses = emailAddresses
            };
        }
    }
}
