using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Interfaces.Repository;

namespace TooksCms.ServiceLayer.Objects.Account
{
    public class Guest : InterfacingBase, IGuest
    {
        public Guest() { IsNew = true; GuestUid = Guid.NewGuid(); LoadRoles(); }

        public Guest(IGuest data)
            : base(data, typeof(IGuest))
        {
            LoadRoles();
        }

        public bool IsNew { get; set; }

        #region Implementation of IGuest

        public int GuestId { get; set; }
        public Guid GuestUid { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string IpAddress { get; set; }
        public DateTime Date { get; set; }
        public bool IsArchived { get; set; }

        #endregion

        public IEnumerable<IRole> Roles { get; set; }

        private void LoadRoles()
        {
            var sRep = DependencyResolver.Current.GetService<ISecurityRepository>();
            this.Roles = new List<IRole> { sRep.FetchRole("Everyone") };
        }

        public static Guest CreateGuest(Guid uid, string name, string email, string website, DateTime date, string ipAddress, bool isArchived)
        {
            return new Guest
            {
                GuestUid = uid,
                Name = name,
                Email = email,
                Website = website,
                Date = date,
                IpAddress = ipAddress,
                IsArchived = isArchived
            };
        }

        public void Save()
        {
            var aRep = DependencyResolver.Current.GetService<IAccountRepository>();
            this.GuestId = aRep.InsertGuest(this).GuestId;
        }

        public void Archive()
        {
            var aRep = DependencyResolver.Current.GetService<IAccountRepository>();
            this.IsArchived = true;
            aRep.UpdateGuest(this);
        }

        public static Guest Load(string ipAddress)
        {
            var aRep = DependencyResolver.Current.GetService<IAccountRepository>();

            var dto = aRep.FetchGuest(ipAddress);
            if (dto != null)
            {
                return new Guest(dto);
            }
            return new Guest() { IpAddress = ipAddress };
        }

        public bool IsInRole(string roles)
        {
            string[] rolesarray = roles.Split(',');
            return this.Roles.Any(a_ => rolesarray.Contains(a_.RoleName));
        }

        public object GetJSONModel()
        {
            return new
            {
                Name = Name,
                Email = Email,
                Website = Website,
                Date = Date,
                IpAddress = IpAddress,
                IsNew = IsNew
            };
        }
    }
}
