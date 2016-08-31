using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;
using TooksCms.ServiceLayer.Models.Lookup;
using TooksCms.ServiceLayer.Objects.Account;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using TooksCms.Core.Interfaces.Repository;

namespace TooksCms.ServiceLayer.Models.Account
{
    public class ContactInfoModel : ModelBase
    {
        [Dependency]
        private IAccountRepository _accountRepository { get; set; }

        public ContactInfoModel() { }

        public ContactInfoModel(IContactInfo data)
        {
            this.Id = data.ContactInfoId;
            this.Uid = data.ContactInfoUid;
            this.Title = data.Title;
            this.FirstName = data.FirstName;
            this.LastName = data.LastName;
            this.City = data.City;
            this.Country = CountryModel.Load(data.CountryId);
            this.DateCreated = data.DateCreated;
            this.Addresses = data.Addresses.Select(a_ => new AddressModel(a_)).ToList();
            this.EmailAddresses = data.EmailAddresses.Select(e_ => new EmailModel(e_)).ToList();
            this.PhoneNumbers = data.PhoneNumbers.Select(p_ => new PhoneNumberModel(p_)).ToList();
        }

        #region Properties

        [Required]
        [Display(Name = "Title", Prompt = "Please enter title:")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "First Name", Prompt = "Please enter first name:")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name", Prompt = "Please enter last name:")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        public List<AddressModel> Addresses { get; set; }

        public List<EmailModel> EmailAddresses { get; set; }

        public List<PhoneNumberModel> PhoneNumbers { get; set; }

        [Required]
        [Display(Name = "City", Prompt = "Please enter title:")]
        [DataType(DataType.Text)]
        public string City { get; set; }

        public CountryModel Country { get; set; }

        [Required]
        [Display(Name = "Country", Prompt = "Please select country:")]
        public int CountryId { get; set; }

        public DateTime DateCreated { get; set; }

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
                        _accountRepository.InsertContact(BuildInteface());
                    }
                    else if (!IsNew & IsDirty)
                    {
                        /* [Update] a existing, but changed object to be saved */
                        _accountRepository.UpdateContact(BuildInteface());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ContactInfo BuildInteface()
        {
            return ContactInfo.CreateContactInfo(this.Id, this.Uid, this.FirstName, this.LastName, this.Title, this.DateCreated, this.City, this.CountryId,
                this.Addresses.Select(a_ => a_.BuildInteface()), this.PhoneNumbers.Select(pn_ => pn_.BuildInteface()), this.EmailAddresses.Select(e_ => e_.BuildInteface()));
        }

        #region Static Members

        public static ContactInfoModel CreateNewContactInfo()
        {
            var contactInfo = new ContactInfoModel
            {
                DateCreated = DateTime.Now
            };
            contactInfo.Addresses = new List<AddressModel>{
                AddressModel.CreateNewAddress()
            };
            contactInfo.EmailAddresses = new List<EmailModel>{
                EmailModel.CreateNewEmail()
            };
            contactInfo.PhoneNumbers = new List<PhoneNumberModel>{
                PhoneNumberModel.CreateNewPhoneNumber()
            };
            return contactInfo;
        }

        #endregion
    }
}
