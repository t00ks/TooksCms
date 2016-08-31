using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;

namespace TooksCms.ServiceLayer.Objects.Lookup
{
    public class Category : InterfacingBase, ICategory
    {
        public Category() { }

        public Category(ICategory data) : base(data, typeof(ICategory)){}

        #region ICategory Members

        public int CategoryId { get; set; }

        public Guid CategoryUid { get; set; }

        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }

        public int? ParentCategoryId { get; set; }

        public string ImageName { get; set; }

        #endregion

        public static Category CreateCategory(int id, Guid uid, string categoryName, string categoryDescription, int? parentCategoryId)
        {
            if (parentCategoryId.HasValue && parentCategoryId.Value < 0) { parentCategoryId = null; }
            return new Category
            {
                CategoryId = id,
                CategoryUid = uid,
                CategoryName = categoryName,
                CategoryDescription = categoryDescription,
                ParentCategoryId = parentCategoryId
            };
        }
    }
}
