using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;

namespace TooksCms.DAL
{
    public partial class Email : IEmail
    {
        public static Email CreateEmail(IEmail data)
        {
            var email = new Email
            {
                Address = data.Address,
                EmailUid = data.EmailUid,
                IsPrimary = data.IsPrimary
            };
            return email;
        }

        public void Update(IEmail data)
        {
            this.Address = data.Address;
            this.IsPrimary = data.IsPrimary;
        }
    }
}
