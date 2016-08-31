using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooksCms.Core.Models.Account;

namespace TooksCms.Data
{
    public class Context: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<Address> Addresss { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
