using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using TooksCms.ServiceLayer.Objects.Account;
using System.Security.Cryptography;
using TooksCms.Core.Interfaces.Repository;

namespace TooksCms.ServiceLayer.Authentication
{
    public class UserPrincipal : IPrincipal
    {
        public UserPrincipal(string userName, IAccountRepository rep)
        {
            this.User = new User(rep.FetchUser(userName));
        }

        #region IPrincipal Members

        public IIdentity Identity
        {
            get { return new UserIdentity(true, User.LoginName); }
        }

        public bool IsInRole(string roles)
        {
            return this.User.IsInRole(roles);
        }

        #endregion

        public User User { get; private set; }

        public bool AuthenticatePassword(string password)
        {
            string hashedPassword = EncodePassword(password, User.Salt);
            return hashedPassword == User.Password;
        }

        #region Static Members

        public static string EncodePassword(string pass, string salt)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(pass);
            byte[] src = Encoding.Unicode.GetBytes(salt);
            byte[] dst = new byte[src.Length + bytes.Length];
            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }
        
        #endregion

    }
}
