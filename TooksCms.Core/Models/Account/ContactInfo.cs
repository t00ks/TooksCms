using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooksCms.Core.Interfaces;

namespace TooksCms.Core.Models.Account
{
    public class ContactInfo
    {
        public int ContactInfoId { get; set; }

        public Guid ContactInfoUid { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Address> Addresses { get; set; }

        public List<Email> EmailAddresses { get; set; }

        public List<PhoneNumber> PhoneNumbers { get; set; }

        public DateTime DateCreated { get; set; }

        public int CountryId { get; set; }

        public string City { get; set; }
    }
}
