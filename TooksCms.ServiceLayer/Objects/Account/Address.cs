using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;

namespace TooksCms.ServiceLayer.Objects.Account
{
    public class Address : IAddress
    {
        private Address() { }

        public Address(IAddress data)
        {
            AddressUid = data.AddressUid;
            AddressLine1 = data.AddressLine1;
            AddressLine2 = data.AddressLine2;
            AddressLine3 = data.AddressLine3;
            City = data.City;
            HouseName = data.HouseName;
            HouseNumber = data.HouseNumber;
            PostCode = data.PostCode;
            County = data.County;
            Country = data.Country;
        }

        #region IAddress Members

        public int AddressId { get; private set; }

        public Guid AddressUid { get; private set; }

        public int? HouseNumber { get; private set; }

        public string HouseName { get; private set; }

        public string AddressLine1 { get; private set; }

        public string AddressLine2 { get; private set; }

        public string AddressLine3 { get; private set; }

        public string City { get; private set; }

        public string County { get; private set; }

        public ICountry Country { get; private set; }

        public string PostCode { get; private set; }

        #endregion

        public static Address CreateAddress(int id, Guid uid, int? houseNumber, string houseName, string addressLine1, string addressLine2,
            string addressLine3, string city, string county, ICountry country, string postcode)
        {
            return new Address
            {
                AddressId = id,
                AddressUid = uid,
                AddressLine1 = addressLine1,
                AddressLine2 = addressLine2,
                AddressLine3 = addressLine3,
                City = city,
                County = county,
                Country = country,
                PostCode = postcode
            };
        }
    }
}
