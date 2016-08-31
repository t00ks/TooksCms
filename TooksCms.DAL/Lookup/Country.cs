using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;

namespace TooksCms.DAL
{
    public partial class Country : ICountry
    {
        public static Country CreateCountry(ICountry data)
        {
            var country = new Country
            {
                CountryUid = data.CountryUid,
                Name = data.Name,
                ImageName = data.ImageName,
                ISO3166 = data.ISO3166
            };
            return country;
        }

        public void Update(ICountry data)
        {
            this.Name = data.Name;
            this.ImageName = data.ImageName;
            this.ISO3166 = data.ISO3166;
        }
    }
}
