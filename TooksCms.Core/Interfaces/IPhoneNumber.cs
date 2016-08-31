using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Enums;

namespace TooksCms.Core.Interfaces
{
    public interface IPhoneNumber
    {
        int PhoneNumberId { get; }
        Guid PhoneNumberUid { get; }
        string Number { get; }
        PhoneType Type { get; }
    }
}
