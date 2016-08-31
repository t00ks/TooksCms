using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;

namespace TooksCms.DAL
{
    public partial class Category : ICategory
    {
        public static Category CreateCategory(ICategory data)
        {
            return new Category
            {
                CategoryName = data.CategoryName,
                CategoryDescription = data.CategoryDescription,
                CategoryUid = data.CategoryUid,
                ParentCategoryId = data.ParentCategoryId
            };
        }

        public void Update(ICategory data)
        {
            this.CategoryName = data.CategoryName;
            this.CategoryDescription = data.CategoryDescription;
            this.ParentCategoryId = data.ParentCategoryId;
        }
    }
}
