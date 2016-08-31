using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooksCms.Core.Interfaces;

namespace TooksCms.Core.Models.Account
{
    public class User : IUser
    {
        public int UserId { get; set; }

        public Guid UserUid { get; set; }

        public string LoginName { get; set; }

        public string ScreenName { get; set; }

        public ContactInfo ContactInfo { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public DateTime DateCreated { get; set; }

        public string CreationIP { get; set; }

        public DateTime LastLogin { get; set; }

        public string LastLoginIP { get; set; }

        public List<Role> Roles { get; set; }


        IContactInfo IUser.ContactInfo
        {
            get
            {
                return (IContactInfo)this.ContactInfo;
            }
            set
            {
                this.ContactInfo = (ContactInfo)value;
            }
        }

        IEnumerable<IRole> IUser.Roles
        {
            get
            {
                return this.Roles.Select(r => (IRole)r);
            }
            set
            {
                this.Roles = value.Select(r => (Role)r).ToList();
            }
        }
    }
}
