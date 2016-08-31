using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Exceptions;
using TooksCms.Core.Interfaces.Repository;

namespace TooksCms.DAL
{
    public class SecurityRepository : ISecurityRepository
    {
        /// <summary>
        /// Fetch a role from the DAL.
        /// </summary>
        /// <param name="id">ID of the role</param>
        /// <returns>A Role DTO object</returns>
        /// <exception cref="TooksCms.Core.Exceptions.DataNotFoundException">Role does not exist</exception>
        public IRole FetchRole(int id)
        {
            var db = new TooksCmsDAL();

            if (!_roleExists(id, db))
            {
                throw new DataNotFoundException("Role does not exist", "id");
            }

            return db.Roles.FirstOrDefault(r_ => r_.RoleId == id);
        }

        /// <summary>
        /// Fetch a role from the DAL.
        /// </summary>
        /// <param name="name">Name of the role</param>
        /// <returns>A Role DTO object</returns>
        /// <exception cref="TooksCms.Core.Exceptions.DataNotFoundException">Role does not exist</exception>
        public IRole FetchRole(string name)
        {
            var db = new TooksCmsDAL();

            if (!_roleExists(name, db))
            {
                throw new DataNotFoundException("Role does not exist", "id");
            }

            return db.Roles.FirstOrDefault(r_ => r_.RoleName == name);
        }

        /// <summary>
        /// Fetch list of roles from the DAL
        /// </summary>
        /// <returns>An enumerable collection of Role DTO objects</returns>
        public IEnumerable<IRole> FetchRoles()
        {
            var db = new TooksCmsDAL();

            return db.Roles;
        }

        /// <summary>
        /// Checks the DAL to see if Role exists
        /// </summary>
        /// <param name="id">ID of Role</param>
        /// <returns>True or False</returns>
        public bool RoleExists(int id)
        {
            var db = new TooksCmsDAL();
            return _roleExists(id, db);
        }

        private bool _roleExists(int id, TooksCmsDAL db)
        {
            return db.Roles.Any(r_ => r_.RoleId == id);
        }

        /// <summary>
        /// Checks the DAL to see if Role exists
        /// </summary>
        /// <param name="name">Name of Role</param>
        /// <returns>True or False</returns>
        public bool RoleExists(string name)
        {
            var db = new TooksCmsDAL();
            return _roleExists(name, db);
        }

        private bool _roleExists(string name, TooksCmsDAL db)
        {
            return db.Roles.Any(r_ => r_.RoleName == name);
        }

        public IEnumerable<ISite> FetchSites()
        {
            var db = new TooksCmsDAL();
            return db.Sites;
        }
    }
}
