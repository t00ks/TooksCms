using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;
using TooksCms.ServiceLayer.Objects.Security;
using TooksCms.Core.Bases;

namespace TooksCms.ServiceLayer.Objects.Account
{
    public class User : InterfacingBase, IUser
    {
        private User() { }

        public User(IUser data)
            : base(data, typeof(IUser))
        {

        }

        #region IUser Members

        public int UserId { get; set; }

        public Guid UserUid { get; set; }

        public string LoginName { get; set; }

        public string ScreenName { get; set; }

        public IContactInfo ContactInfo { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public DateTime DateCreated { get; set; }

        public string CreationIP { get; set; }

        public DateTime LastLogin { get; set; }

        public string LastLoginIP { get; set; }

        public IEnumerable<IRole> Roles { get; set; }

        #endregion

        public static User CreateUser(int id, Guid uid, string loginName, string screenName, IContactInfo contactInfo, string password, string salt, DateTime dateCreated,
            string creationIP, DateTime lastLogin, string lastLoginIP)
        {
            return new User
            {
                UserId = id,
                UserUid = uid,
                LoginName = loginName,
                ScreenName = screenName,
                ContactInfo = contactInfo,
                Password = password,
                Salt = salt,
                DateCreated = dateCreated,
                CreationIP = creationIP,
                LastLogin = lastLogin,
                LastLoginIP = lastLoginIP
            };
        }

        public bool IsInRole(string roles)
        {
            string[] rolesarray = roles.Split(',');
            return this.Roles.Any(a_ => rolesarray.Contains(a_.RoleName));
        }
    }
}
