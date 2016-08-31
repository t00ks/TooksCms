using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Interfaces.Repository;

namespace TooksCms.DAL
{
    public partial class Address : IAddress
    {
        public static Address CreateAddress(IAddress data)
        {
            var address = new Address
            {
                AddressUid = data.AddressUid,
                AddressLine1 = data.AddressLine1,
                AddressLine2 = data.AddressLine2,
                AddressLine3 = data.AddressLine3,
                City = data.City,
                HouseName = data.HouseName,
                HouseNumber = data.HouseNumber,
                PostCode = data.PostCode,
                County = data.County,
                Country = Country.CreateCountry(data.Country)
            };
            return address;
        }

        ICountry IAddress.Country
        {
            get { return new LookupRepository().FetchCountry(this.CountryId); }
        }

        public void Update(IAddress data)
        {
            AddressLine1 = data.AddressLine1;
            AddressLine2 = data.AddressLine2;
            AddressLine3 = data.AddressLine3;
            City = data.City;
            HouseName = data.HouseName;
            HouseNumber = data.HouseNumber;
            PostCode = data.PostCode;
            County = data.County;
            CountryId = data.Country.CountryId;
        }
    }
}