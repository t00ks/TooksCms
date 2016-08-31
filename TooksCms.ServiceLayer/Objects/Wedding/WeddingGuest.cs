using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooksCms.Core.Bases;
using TooksCms.Core.Enums;
using TooksCms.Core.Interfaces;

namespace TooksCms.ServiceLayer.Objects.Wedding
{
    public class WeddingGuest : InterfacingBase, IWeddingGuest
    {
        public WeddingGuest() { }

        public WeddingGuest(IWeddingGuest data) :
            base(data, typeof(IWeddingGuest))
        {

        }

        public int GuestId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public WeddingGuestType GuestType { get; set; }

        public WeddingGuestSide GuestSide { get; set; }

        public bool InvitationSent { get; set; }

        public string Code { get; set; }

        public bool? Attending { get; set; }

        public int? GuestGroupId { get; set; }

        public string DietaryRequirements { get; set; }

        public DateTime? RSVPDate { get; set; }

        public string RSVPIpAddress { get; set; }
    }

    public class WeddingGuestGroup : InterfacingBase, IWeddingGuestGroup
    {
        public WeddingGuestGroup() { }

        public WeddingGuestGroup(IWeddingGuestGroup data) :
            base(data, typeof(IWeddingGuestGroup))
        {

        }

        public int GuestGroupId { get; set; }

        public string Name { get; set; }

        public string Message { get; set; }
    }
}
