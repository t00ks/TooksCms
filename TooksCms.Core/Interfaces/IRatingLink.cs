using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Interfaces
{
    public interface IRatingLink : IInterfacingBase
    {
        Dictionary<int, IRating> RatingIds { get; }
        int ArticleTypeId { get; }
        int CategoryId { get; }
    }
}
