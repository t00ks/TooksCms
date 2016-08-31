using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;

namespace TooksCms.DAL
{
    public partial class Guest : IGuest
    {
        public static Guest CreateGuest(IGuest data)
        {
            return new Guest
            {
                GuestUid = data.GuestUid,
                Name = data.Name,
                Email = data.Email,
                Website = data.Website,
                IpAddress = data.IpAddress,
                Date = data.Date,
                IsArchived = data.IsArchived
            };
        }

        public void Update(IGuest data)
        {
            this.Name = data.Name;
            this.Website = data.Website;
            this.Email = data.Email;
            this.IpAddress = data.IpAddress;
            this.Date = data.Date;
            this.IsArchived = data.IsArchived;
        }
    }
}
