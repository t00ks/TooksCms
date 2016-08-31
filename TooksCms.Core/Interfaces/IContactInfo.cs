using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Interfaces
{
    public interface IContactInfo
    {
        int ContactInfoId { get; }
        Guid ContactInfoUid { get; }
        string Title { get; }
        string FirstName { get; }
        string LastName { get; }
        IEnumerable<IAddress> Addresses { get; }
        IEnumerable<IEmail> EmailAddresses { get; }
        IEnumerable<IPhoneNumber> PhoneNumbers { get; }
        DateTime DateCreated { get; }
        int CountryId { get; }
        string City { get; }
    }
}
