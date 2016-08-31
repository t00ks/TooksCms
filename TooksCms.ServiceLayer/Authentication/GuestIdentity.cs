using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace TooksCms.ServiceLayer.Authentication
{
    public class GuestIdentity : IIdentity
    {
        private string _ipAddress;

        public GuestIdentity(string ipAddress)
        {
            this._ipAddress = ipAddress;
        }

        #region IIdentity Members

        public string AuthenticationType
        {
            get { return "Forms"; }
        }

        public bool IsAuthenticated
        {
            get { return false; }
        }

        public string Name
        {
            get { return _ipAddress; }
        }

        #endregion
    }
}
