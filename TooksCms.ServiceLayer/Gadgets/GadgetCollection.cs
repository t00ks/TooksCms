using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Enums;
using TooksCms.ServiceLayer.Objects.Security;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Interfaces.Repository;

namespace TooksCms.ServiceLayer.Gadgets
{
    public class GadgetCollection : Dictionary<KeyValuePair<string, AreaType>, List<Gadget>>
    {
        public List<Gadget> GetGadgets(IEnumerable<IRole> roles, AreaType area, ISecurityRepository sRep, IConfigRepository cRep)
        {
            var unionizedList = new List<Gadget>();
            foreach(var role in roles)
            {
                if (this.Contains(role.RoleName, area))
                {
                    unionizedList = unionizedList.Union(this.Single(p_ => p_.Key.Key == role.RoleName && p_.Key.Value == area).Value, new GadgetEqualityComparer()).ToList();
                }
                else
                {
                    unionizedList = unionizedList.Union(LoadGadget(role.RoleName, area, sRep, cRep), new GadgetEqualityComparer()).ToList();
                }
            }
            return unionizedList;
        }

        public bool Contains(string role, AreaType area)
        {
            return this.Any(p_ => p_.Key.Key == role && p_.Key.Value == area);
        }

        private IEnumerable<Gadget> LoadGadget(string roleName, AreaType area, ISecurityRepository sRep, IConfigRepository cRep)
        {
            Role role = new Role(sRep.FetchRole(roleName));
            List<Gadget> list = cRep.FetchGadgets(role.RoleId, (int)area).Select(g_ => new Gadget(g_)).ToList();
            this.Add(new KeyValuePair<string, AreaType>(roleName, area), list);
            return list;
        }
    }

    public class GadgetEqualityComparer : IEqualityComparer<Gadget>
    {
        public bool Equals(Gadget x, Gadget y)
        {
            return x.View == y.View;
        }

        public int GetHashCode(Gadget obj)
        {
            return obj.GadgetId;
        }
    }
}
