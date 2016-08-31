using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Interfaces
{
    public interface ICountry
    {
        int CountryId { get; }
        Guid CountryUid { get; }
        string Name { get; }
        string ISO3166 { get; }
        string ImageName { get; }
    }
}
