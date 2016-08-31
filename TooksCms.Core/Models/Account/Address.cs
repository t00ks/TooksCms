using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooksCms.Core.Interfaces;

namespace TooksCms.Core.Models.Account
{
    public class Address : IAddress
    {
        public int AddressId { get; set; }

        public Guid AddressUid { get; set; }

        public int? HouseNumber { get; set; }

        public string HouseName { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string AddressLine3 { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public ICountry Country { get; set; }

        public string PostCode { get; set; }
    }
}
