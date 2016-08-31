using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;
using TooksCms.ServiceLayer.Objects.Account;
using TooksCms.ServiceLayer.Objects.Lookup;
using TooksCms.Core.Interfaces.Repository;
using Microsoft.Practices.Unity;

namespace TooksCms.ServiceLayer.Models.Lookup
{
    public class CountryModel : ModelBase
    {
        [Dependency]
        private ILookupRepository _lookupRepository { get; set; }

        public CountryModel()
        {
            _lookupRepository = DependencyResolver.Current.GetService<ILookupRepository>();
        }

        public CountryModel(ICountry data)
        {
            this.Id = data.CountryId;
            this.Uid = data.CountryUid;
            this.Name = data.Name;
            this.ISO3166 = data.ISO3166;
            this.ImageName = data.ImageName;
            _lookupRepository = DependencyResolver.Current.GetService<ILookupRepository>();
            MarkOld();
        }

        #region Properties

        [Required]
        public string Name { get; set; }

        [Required]
        public string ISO3166 { get; set; }

        [Required]
        public string ImageName { get; set; }

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
                        _lookupRepository.InsertCountry(BuildInteface());
                    }
                    else if (!IsNew & IsDirty)
                    {
                        /* [Update] a existing, but changed object to be saved */
                        _lookupRepository.UpdateCountry(BuildInteface());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Country BuildInteface()
        {
            return Country.CreateCountry(this.Id, this.Uid, this.Name, this.ISO3166, this.ImageName);
        }

        #region Static Methods

        public static CountryModel Load(int id)
        {
            var lookupRepository = DependencyResolver.Current.GetService<ILookupRepository>();
            return new CountryModel(lookupRepository.FetchCountry(id));
        }

        public static CountryModel NewCountryModel()
        {
            return new CountryModel();
        }

        public static List<CountryModel> GetList()
        {
            var lookupRepository = DependencyResolver.Current.GetService<ILookupRepository>();
            return lookupRepository.FetchCountries().Select(C_ => new CountryModel(C_)).ToList();
        }

        #endregion


        #region Tracking Overrides

        public new bool IsNew
        {
            get
            {
                return base.IsNew;
            }
            set
            {
                base._isNew = value;
            }
        }

        public new bool IsDirty
        {
            get
            {
                return base.IsDirty;
            }
            set
            {
                base._isDirty = value;
            }
        }

        public new bool IsDeleted
        {
            get
            {
                return base.IsDeleted;
            }
            set
            {
                base._isDeleted = value;
            }
        }

        #endregion
    }
}
