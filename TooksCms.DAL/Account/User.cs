using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;

namespace TooksCms.DAL
{
    public partial class User : IUser
    {
        public static User CreateUser(IUser data)
        {
            var user = new User
            {
                UserUid = data.UserUid,
                LoginName = data.LoginName,
                CreationIP = data.CreationIP,
                LastLogin = data.LastLogin,
                DateCreated = data.DateCreated,
                LastLoginIP = data.LastLoginIP,
                Password = data.Password,
                Salt = data.Salt,
                ScreenName = data.ScreenName,
                SiteId = 1
            };
            user.ContactInfoes.Add(ContactInfo.CreateContactInfo(data.ContactInfo));
            return user;
        }

        IContactInfo IUser.ContactInfo
        {
            get { return this.ContactInfoes.FirstOrDefault(); }
            set { }
        }

        IEnumerable<IRole> IUser.Roles
        {
            get { return this.Roles.ToList(); }
            set { }
        }

        public void Update(IUser data)
        {
            this.LoginName = data.LoginName;
            this.LastLogin = data.LastLogin;
            this.LastLoginIP = data.LastLoginIP;
            this.Password = data.Password;
        }
    }
}
