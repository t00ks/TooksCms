using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Interfaces
{
    public interface ICategory : IInterfacingBase
    {
        int CategoryId { get; set; }
        Guid CategoryUid { get; set; }
        string CategoryName { get; set; }
        string CategoryDescription { get; set; }
        int? ParentCategoryId { get; set; }
        string ImageName { get; set; }
    }
}
