using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;

namespace TooksCms.ServiceLayer.Objects
{
    public class CategoryInfo: InterfacingBase, ICategoryInfo
    {
        public CategoryInfo() { }

        public CategoryInfo(ICategoryInfo data) :
            base(data, typeof(ICategoryInfo)) { }

        #region Implementation of ICategoryInfo

        public string CategoryName { get; set; }
        public string FullCategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string ParentDescription { get; set; }
        public int CategoryId { get; set; }
        public int? ParentCategoryId { get; set; }
        public string ImageName { get; set; }

        #endregion
    }
}
