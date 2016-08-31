using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TooksCms.Core.Interfaces
{
    public interface IMailRecipient
    {
        string DisplayName { get; set; }
        string Address { get; set; }
    }
}
