using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Enums;

namespace TooksCms.ServiceLayer.Objects.Account
{
    public class PhoneNumber : IPhoneNumber
    {
        private PhoneNumber() { }

        public PhoneNumber(IPhoneNumber data)
        {
            PhoneNumberId = data.PhoneNumberId;
            PhoneNumberUid = data.PhoneNumberUid;
            Number = data.Number;
            Type = data.Type;
        }

        #region IPhoneNumber Members

        public int PhoneNumberId { get; private set; }

        public Guid PhoneNumberUid { get; private set; }

        public string Number { get; private set; }

        public PhoneType Type { get; private set; }

        #endregion

        public static PhoneNumber CreatePhoneNumber(int id, Guid uid, string number, PhoneType type)
        {
            return new PhoneNumber
            {
                PhoneNumberId = id,
                PhoneNumberUid = uid,
                Number = number,
                Type = type
            };
        }
    }
}
