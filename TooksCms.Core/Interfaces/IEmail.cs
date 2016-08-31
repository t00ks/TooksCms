using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Interfaces
{
    public interface IEmail
    {
        int EmailId { get; }
        Guid EmailUid { get; }
        int ContactInfoId { get; }
        string Address { get; }
        bool IsPrimary { get; }
    }
}
