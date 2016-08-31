using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases;

namespace TooksCms.Core.Objects.Responses
{
    public class LookupResponse<T> : ResponseBase
    {
        public List<T> List { get; set; }
    }
}
