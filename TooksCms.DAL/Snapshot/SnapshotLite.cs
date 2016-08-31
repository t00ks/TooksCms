using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooksCms.Core.Interfaces;

namespace TooksCms.DAL
{
    public partial class SnapshotLite : ISnapshot
    {
        public string Html
        {
            get
            {
                return string.Empty;
            }
            set
            {
                //Do Nothing
            }
        }
    }
}
