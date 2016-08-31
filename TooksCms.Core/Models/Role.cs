using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooksCms.Core.Interfaces;

namespace TooksCms.Core.Models
{
    public class Role : IRole
    {
        public int RoleId { get; set; }

        public Guid RoleUid { get; set; }

        public string RoleName { get; set; }

        public string Description { get; set; }
    }
}
