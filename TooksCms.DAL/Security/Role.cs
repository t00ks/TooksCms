using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;

namespace TooksCms.DAL
{
    public partial class Role : IRole
    {
        public static Role CreateRole(IRole data)
        {
            return new Role
            {
                RoleUid = data.RoleUid,
                RoleName = data.RoleName,
                Description = data.Description
            };
        }

        public void Update(IRole data)
        {
            this.RoleName = data.RoleName;
            this.Description = data.Description;
        }
    }
}
