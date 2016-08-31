using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Interfaces
{
    public interface IRole : IInterfacingBase
    {
        int RoleId { get; set; }
        Guid RoleUid { get; set; }
        string RoleName { get; set; }
        string Description { get; set; }
    }
}
