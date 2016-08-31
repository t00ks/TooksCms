using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Interfaces
{
    public interface IUser : IInterfacingBase
    {
        int UserId { get; set; }
        Guid UserUid { get; set; }
        string LoginName { get; set; }
        string ScreenName { get; set; }
        IContactInfo ContactInfo { get; set; }
        string Password { get; set; }
        string Salt { get; set; }
        DateTime DateCreated { get; set; }
        string CreationIP { get; set; }
        DateTime LastLogin { get; set; }
        string LastLoginIP { get; set; }
        IEnumerable<IRole> Roles { get; set; }
    }
}

