using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases;
using TooksCms.ServiceLayer.Objects.Account;
using TooksCms.Core.Interfaces;
using System.ComponentModel.DataAnnotations;
using TooksCms.Core.Interfaces.Repository;

namespace TooksCms.ServiceLayer.Models.Account
{
    public class EmailModel : ModelBase
    {
        public EmailModel() { }

        public EmailModel(IEmail data)
        {
            this.Id = data.EmailId;
            this.Uid = data.EmailUid;
            this.Address = data.Address;
            this.IsPrimary = data.IsPrimary;
            this.ContactInfoId = data.ContactInfoId;
        }

        #region Properties

        [Required]
        [Display(Name = "E-mail Address", Prompt = "Please enter e-mail address")]
        [DataType(DataType.EmailAddress)]
        public string Address { get; set; }

        public bool IsPrimary { get; set; }

        public int ContactInfoId { get; set; }

        #endregion

        public void Save(IAccountRepository rep)
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
                        rep.InsertEmail(BuildInteface());
                    }
                    else if (!IsNew & IsDirty)
                    {
                        /* [Update] a existing, but changed object to be saved */
                        rep.UpdateEmail(BuildInteface());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Email BuildInteface()
        {
            return Email.CreateEmail(this.Id, this.Uid, this.ContactInfoId, this.Address, this.IsPrimary);
        }

        #region Static Members

        public static EmailModel CreateNewEmail()
        {
            return new EmailModel();
        }

        #endregion
    }
}
