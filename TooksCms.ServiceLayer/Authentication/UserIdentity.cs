using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace TooksCms.ServiceLayer.Authentication
{
    public class UserIdentity : IIdentity
    {
        private bool _isAuthenticated;
        private string _name;

        public UserIdentity(bool isAuthenticated, string name)
        {
            this._isAuthenticated = isAuthenticated;
            this._name = name;
        }

        #region IIdentity Members

        public string AuthenticationType
        {
            get { return "Forms"; }
        }

        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
        }

        public string Name
        {
            get { return _name; }
        }

        #endregion
    }
}
