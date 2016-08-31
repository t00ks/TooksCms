using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooksCms.Core.Interfaces;

namespace TooksCms.Core.Models.Account
{
    public class Email : IEmail
    {
        public int EmailId { get; set; }

        public Guid EmailUid { get; set; }

        public int ContactInfoId { get; set; }

        public string Address { get; set; }

        public bool IsPrimary { get; set; }
    }
}
