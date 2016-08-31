using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using TooksCms.ServiceLayer.Objects.Account;
using TooksCms.Core.Interfaces.Repository;
using Microsoft.Practices.Unity;

namespace TooksCms.ServiceLayer.Authentication
{
    public class GuestPrincipal : IPrincipal
    {
        public GuestPrincipal(string ipAddress)
        {
            this.Guest = Guest.Load(ipAddress);
        }

        #region IPrincipal Members

        public IIdentity Identity
        {
            get { return new GuestIdentity(Guest.IpAddress); }
        }

        public bool IsInRole(string roles)
        {
            return this.Guest.IsInRole(roles);
        }

        #endregion

        public Guest Guest { get; private set; }
    }
}
