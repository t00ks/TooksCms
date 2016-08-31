using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;
using TooksCms.ServiceLayer.Objects.Account;
using TooksCms.ServiceLayer.Models.Lookup;
using System.ComponentModel.DataAnnotations;
using TooksCms.Core.Interfaces.Repository;
using Microsoft.Practices.Unity;

namespace TooksCms.ServiceLayer.Models.Account
{
    public class AddressModel : ModelBase
    {
        [Dependency]
        private IAccountRepository _accountRepository { get; set; }

        public AddressModel() { }

        public AddressModel(IAddress data)
        {
            this.Id = data.AddressId;
            this.Uid = data.AddressUid;
            this.HouseNumber = data.HouseNumber;
            this.HouseName = data.HouseName;
            this.AddressLine1 = data.AddressLine1;
            this.AddressLine2 = data.AddressLine2;
            this.AddressLine3 = data.AddressLine3;
            this.City = data.City;
            this.County = data.County;
            this.Country = new CountryModel(data.Country);
            this.PostCode = data.PostCode;
        }

        #region Properties

        [Display(Name = "House Number", Prompt = "Please enter house number:")]
        public int? HouseNumber { get;set; }

        [Display(Name = "House Name", Prompt = "Please enter house name:")]
        [DataType(DataType.Text)]
        public string HouseName { get;set; }
        
        [Required]
        [Display(Name = "Address Line 1", Prompt = "Please enter address:")]
        [DataType(DataType.Text)]
        public string AddressLine1 { get;set; }

        [Display(Name = "Address Line 2", Prompt = "Please enter address:")]
        [DataType(DataType.Text)]
        public string AddressLine2 { get;set; }

        [Display(Name = "Address Line 3", Prompt = "Please enter address:")]
        [DataType(DataType.Text)]
        public string AddressLine3 { get;set; }

        [Required]
        [Display(Name = "City", Prompt = "Please enter city:")]
        [DataType(DataType.Text)]
        public string City { get;set; }

        [Display(Name = "County", Prompt = "Please enter county:")]
        [DataType(DataType.Text)]
        public string County { get;set; }

        public CountryModel Country { get;set; }

        [Required]
        [Display(Name = "Country", Prompt = "Please select country:")]
        public int CountryId { get; set; }

        [Required]
        [Display(Name = "Post Code", Prompt = "Please enter post code:")]
        public string PostCode { get;set; }

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
                        _accountRepository.InsertAddress(BuildInteface());
                    }
                    else if (!IsNew & IsDirty)
                    {
                        /* [Update] a existing, but changed object to be saved */
                        _accountRepository.UpdateAddress(BuildInteface());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Address BuildInteface()
        {
            return Address.CreateAddress(this.Id, this.Uid, this.HouseNumber, this.HouseName, this.AddressLine1, this.AddressLine2,
                this.AddressLine3, this.City, this.County, this.Country.BuildInteface(), this.PostCode);
        }

        #region Static Members

        public static AddressModel CreateNewAddress()
        {
            return new AddressModel();
        }

        #endregion
    }
}
