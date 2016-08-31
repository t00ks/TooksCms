using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooksCms.Core.Enums;

namespace TooksCms.Core.Interfaces
{
    public interface IWeddingGuest : IInterfacingBase
    {
        int GuestId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Address { get; set; }
        WeddingGuestType GuestType { get; set; }
        WeddingGuestSide GuestSide { get; set; }
        bool InvitationSent { get; set; }
        string Code { get; set; }
        bool? Attending { get; set; }
        int? GuestGroupId { get; set; }
        string DietaryRequirements { get; set; }
        Nullable<DateTime> RSVPDate { get; set; }
        string RSVPIpAddress { get; set; }
    }
}
