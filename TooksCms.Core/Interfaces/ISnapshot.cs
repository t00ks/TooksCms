using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TooksCms.Core.Interfaces
{
    public interface ISnapshot : IInterfacingBase
    {
        int SnapshotId { get; set; }

        string Url { get; set; }

        string Html { get; set; }

        DateTime Date { get; set; }
    }
}
