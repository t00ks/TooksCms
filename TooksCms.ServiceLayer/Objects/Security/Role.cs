using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Bases;

namespace TooksCms.ServiceLayer.Objects.Security
{
    public class Role : InterfacingBase, IRole
    {
        public Role(IRole data) :
            base(data, typeof(IRole))
        {
            
        }

        #region IRole Members

        public int RoleId { get; set; }

        public Guid RoleUid { get; set; }

        public string RoleName { get; set; }

        public string Description { get; set; }

        #endregion
    }
}
