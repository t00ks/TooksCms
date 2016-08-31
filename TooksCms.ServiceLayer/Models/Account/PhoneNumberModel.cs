using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases;
using TooksCms.Core.Enums;
using TooksCms.ServiceLayer.Objects.Account;
using TooksCms.Core.Interfaces;
using System.ComponentModel.DataAnnotations;
using TooksCms.Core.Interfaces.Repository;
using Microsoft.Practices.Unity;

namespace TooksCms.ServiceLayer.Models.Account
{
    public class PhoneNumberModel : ModelBase
    {
        [Dependency]
        private IAccountRepository _accountRepository { get; set; }

        public PhoneNumberModel() { }

        public PhoneNumberModel(IPhoneNumber data)
        {
            this.Id = data.PhoneNumberId;
            this.Uid = data.PhoneNumberUid;
            this.Number = data.Number;
            this.Type = data.Type;
        }

        #region Properties

        [Display(Name = "Phone Number", Prompt = "Please select phone number")]
        [DataType(DataType.PhoneNumber)]
        public string Number { get; set; }

        [Display(Name = "Number Type", Prompt = "Please select phone number")]
        public PhoneType Type { get; set; }

        #endregion

        public void Save()
        {
            try
            {
                if (!IsNew & IsDeleted)
                {
                    /* [Delete] an existing object marked for deletion */
                    //dc.ArticleDelete(Id);
                }
                else
                {
                    /* Exception will cause the transaction to rollback */
                    if (IsNew)
                    {
                        /* [Insert] a new and valid object to be saved */
                        _accountRepository.InsertPhoneNumber(BuildInteface());
                    }
                    else if (!IsNew & IsDirty)
                    {
                        /* [Update] a existing, but changed object to be saved */
                        _accountRepository.UpdatePhoneNumber(BuildInteface());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PhoneNumber BuildInteface()
        {
            return PhoneNumber.CreatePhoneNumber(this.Id, this.Uid, this.Number, this.Type);
        }

        #region Static Members

        public static PhoneNumberModel CreateNewPhoneNumber()
        {
            return new PhoneNumberModel();
        }

        #endregion
    }
}
