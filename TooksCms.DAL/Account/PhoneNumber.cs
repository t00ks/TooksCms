using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Enums;

namespace TooksCms.DAL
{
    public partial class PhoneNumber : IPhoneNumber
    {
        public static PhoneNumber CreatePhoneNumber(IPhoneNumber data)
        {
            var phoneNumber = new PhoneNumber
            {
                Number = data.Number,
                PhoneNumberUid = data.PhoneNumberUid,
                Type = (int)data.Type
            };
            return phoneNumber;
        }

        PhoneType IPhoneNumber.Type
        {
            get { return (PhoneType)this.Type; }
        }

        public void Update(IPhoneNumber data)
        {
            Number = data.Number;
            Type = (int)data.Type;
        }
    }
}
