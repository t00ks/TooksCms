using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Interfaces
{
    public interface IAddress
    {
        int AddressId { get; }
        Guid AddressUid { get; }
        int? HouseNumber { get; }
        string HouseName { get; }
        string AddressLine1 { get; }
        string AddressLine2 { get; }
        string AddressLine3 { get; }
        string City { get; }
        string County { get; }
        ICountry Country { get; }
        string PostCode { get; }
    }
}
