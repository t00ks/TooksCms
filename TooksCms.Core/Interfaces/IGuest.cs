using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Interfaces
{
    public interface IGuest : IInterfacingBase
    {
        int GuestId { get; set; }
        Guid GuestUid { get; set; }
        string Name { get; set; }
        string Website { get; set; }
        string Email { get; set; }
        string IpAddress { get; set; }
        DateTime Date { get; set; }
        bool IsArchived { get; set; }
    }
}
