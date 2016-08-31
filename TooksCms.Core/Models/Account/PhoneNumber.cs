using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooksCms.Core.Enums;
using TooksCms.Core.Interfaces;

namespace TooksCms.Core.Models.Account
{
    public class PhoneNumber
    {
        public int PhoneNumberId { get; set; }

        public Guid PhoneNumberUid { get; set; }

        public string Number { get; set; }

        public PhoneType Type { get; set; }
    }
}
