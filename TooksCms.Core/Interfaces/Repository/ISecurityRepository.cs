using System;
using System.Collections.Generic;
using TooksCms.Core.Interfaces;
namespace TooksCms.Core.Interfaces.Repository
{
    public interface ISecurityRepository
    {
        IRole FetchRole(int id);
        IRole FetchRole(string name);
        IEnumerable<IRole> FetchRoles();
        IEnumerable<ISite> FetchSites();
        bool RoleExists(int id);
        bool RoleExists(string name);
    }
}
