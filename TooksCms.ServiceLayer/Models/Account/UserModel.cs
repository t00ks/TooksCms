using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Bases;
using TooksCms.ServiceLayer.Objects.Account;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Security.Cryptography;
using TooksCms.ServiceLayer.Authentication;
using TooksCms.Core.Interfaces.Repository;
using Microsoft.Practices.Unity;

namespace TooksCms.ServiceLayer.Models.Account
{
    public class UserModel : ModelBase
    {
        [Dependency]
        private IAccountRepository _accountRepository { get; set; }

        public UserModel() { }

        public UserModel(IUser data)
        {
            this.Id = data.UserId;
            this.Uid = data.UserUid;
            this.LoginName = data.LoginName;
            this.ScreenName = data.ScreenName;
            this.Password = data.Password;
            this.DateCreated = data.DateCreated;
            this.CreationIP = data.CreationIP;
            this.LastLogin = data.LastLogin;
            this.LastLoginIP = data.LastLoginIP;
            this.ContactInfo = new ContactInfoModel(data.ContactInfo);
        }

        #region Properties
        
        [Required]
        [Display(Name = "User Name", Prompt = "Please enter a screen name:")]
        [DataType(DataType.Text)]
        public string ScreenName { get; set; }

        [Required]
        [Display(Name = "Email Address", Prompt = "Please enter email address:")]
        [DataType(DataType.EmailAddress)]
        public string LoginName { get; set; }

        [Required]
        [Display(Name = "Confirm Email Address", Prompt = "Please confirm email address:")]
        [DataType(DataType.EmailAddress)]
        [System.Web.Mvc.Compare("LoginName", ErrorMessage = "Email addresses must match")]
        public string ConfirmLoginName { get; set; }

        public ContactInfoModel ContactInfo { get; set; }

        [Required]
        [Display(Name = "Password", Prompt = "Please enter password:")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password", Prompt = "Please confirm password:")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string Salt { get; private set; }

        public DateTime DateCreated { get; set; }

        public string CreationIP { get; set; }

        public DateTime LastLogin { get; set; }

        public string LastLoginIP { get; set; }

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
                        this.Salt = Guid.NewGuid().ToString();
                        this.Password = this.ConfirmPassword = UserPrincipal.EncodePassword(this.Password, this.Salt);
                        _accountRepository.InsertUser(BuildInterface());
                    }
                    else if (!IsNew & IsDirty)
                    {
                        /* [Update] a existing, but changed object to be saved */
                        _accountRepository.UpdateUser(BuildInterface());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User BuildInterface()
        {
            return User.CreateUser(this.Id, this.Uid, this.LoginName, this.ScreenName, this.ContactInfo.BuildInteface(), this.Password, this.Salt, 
                this.DateCreated, this.CreationIP, this.LastLogin, this.LastLoginIP);
        }


        #region Static Methods

        public static UserModel CreateNewUser()
        {
            var user = new UserModel
            {
                DateCreated = DateTime.Now,
                LastLogin = DateTime.Now
            };
            user.ContactInfo = ContactInfoModel.CreateNewContactInfo();
            return user;
        }

        #endregion

    }
}
