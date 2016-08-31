using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooksCms.Core.Enums;
using TooksCms.Core.Interfaces;

namespace TooksCms.DAL
{
    public partial class Guest1 : IWeddingGuest
    {
        WeddingGuestType IWeddingGuest.GuestType
        {
            get
            {
                return (WeddingGuestType)this.GuestType;
            }
            set
            {
                this.GuestType = (byte)value;
            }
        }

        WeddingGuestSide IWeddingGuest.GuestSide
        {
            get
            {
                return (WeddingGuestSide)this.GuestSide;
            }
            set
            {
                this.GuestSide = (byte)value;
            }
        }

        public static Guest1 CreatGuest(IWeddingGuest guest)
        {
            return new Guest1
            {
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                Address = guest.Address,
                Attending = guest.Attending,
                Code = guest.Code,
                GuestGroupId = guest.GuestGroupId,
                GuestSide = (byte)guest.GuestSide,
                GuestType = (byte)guest.GuestType,
                InvitationSent = guest.InvitationSent,
                DietaryRequirements = guest.DietaryRequirements
            };
        }

        public void Update(IWeddingGuest guest)
        {
            FirstName = guest.FirstName;
            LastName = guest.LastName;
            Address = guest.Address;
            Attending = guest.Attending;
            Code = guest.Code;
            GuestGroupId = guest.GuestGroupId;
            GuestSide = (byte)guest.GuestSide;
            GuestType = (byte)guest.GuestType;
            InvitationSent = guest.InvitationSent;
            DietaryRequirements = guest.DietaryRequirements;
        }

        public void RSVP(IRsvp rsvp)
        {
            RSVPDate = rsvp.Date;
            RSVPIpAddress = rsvp.IpAddress;
            DietaryRequirements = rsvp.DietaryRequirements;
            Attending = rsvp.Attending;
        }
    }

    public partial class GuestGroup : IWeddingGuestGroup
    {

    }
}
