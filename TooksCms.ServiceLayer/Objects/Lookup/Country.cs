using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;

namespace TooksCms.ServiceLayer.Objects.Lookup
{
    public class Country : ICountry
    {
        private Country() { }

        public Country(ICountry data)
        {
            CountryId = data.CountryId;
            CountryUid = data.CountryUid;
            Name = data.Name;
            ISO3166 = data.ISO3166;
            ImageName = data.ImageName;
        }

        #region ICountry Members

        public int CountryId { get; private set; }

        public Guid CountryUid { get; private set; }

        public string Name { get; private set; }

        public string ISO3166 { get; private set; }

        public string ImageName { get; private set; }

        #endregion

        public static Country CreateCountry(int id, Guid uid, string name, string ISO3166, string imageName)
        {
            return new Country
            {
                CountryId = id,
                CountryUid = uid,
                Name = name,
                ISO3166 = ISO3166,
                ImageName = imageName
            };
        }
    }
}
