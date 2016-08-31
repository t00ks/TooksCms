using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Interfaces
{
    public interface ICategoryInfo : IInterfacingBase
    {
        string CategoryName { get; set; }
        string FullCategoryName { get; set; }
        string CategoryDescription { get; set; }
        string ParentDescription { get; set; }
        int CategoryId { get; set; }
        int? ParentCategoryId { get; set; }
        string ImageName { get; set; }
    }
}
