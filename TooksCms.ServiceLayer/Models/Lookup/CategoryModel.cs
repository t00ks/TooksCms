using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;
using TooksCms.ServiceLayer.Objects.Lookup;
using System.ComponentModel.DataAnnotations;
using TooksCms.Core.Interfaces.Repository;

namespace TooksCms.ServiceLayer.Models.Lookup
{
    public class CategoryModel : ModelBase
    {
        [Dependency]
        private ILookupRepository _lookupRepository { get; set; }

        #region Private Constructors

        public CategoryModel()
        {
            _lookupRepository = DependencyResolver.Current.GetService<ILookupRepository>();
        }

        public CategoryModel(ICategory data)
        {
            this.Id = data.CategoryId;
            this.Uid = data.CategoryUid;
            this.CategoryName = data.CategoryName;
            this.CategoryDescription = data.CategoryDescription;
            this.ParentCategoryId = data.ParentCategoryId;
            _lookupRepository = DependencyResolver.Current.GetService<ILookupRepository>();
            MarkOld();
        }

        #endregion
        
        #region Properties

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string CategoryDescription { get; set; }

        public int? ParentCategoryId { get; set; }

        #endregion

        #region CRUD

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
                        this.Id = _lookupRepository.InsertCategory(BuildInteface()).CategoryId;
                    }
                    else if (!IsNew & IsDirty)
                    {
                        /* [Update] a existing, but changed object to be saved */
                        _lookupRepository.UpdateCategory(BuildInteface());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Category BuildInteface()
        {
            return Category.CreateCategory(this.Id, this.Uid, this.CategoryName, this.CategoryDescription, this.ParentCategoryId);
        }

        #endregion

        #region Static Members

        public static CategoryModel Load(int id)
        {
            var lookupRepository = DependencyResolver.Current.GetService<ILookupRepository>();
            return new CategoryModel(lookupRepository.FetchCategory(id));
        }

        public static List<CategoryModel> GetList()
        {
            var lookupRepository = DependencyResolver.Current.GetService<ILookupRepository>();
            return lookupRepository.FetchCategories().Select(c_ => new CategoryModel(c_)).ToList();
        }

        public static CategoryModel NewCategoryModel()
        {
            return new CategoryModel();
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
