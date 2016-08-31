using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;

namespace TooksCms.ServiceLayer.Objects
{
    public class Snapshot : InterfacingBase, ISnapshot
    {
        public Snapshot() { }

        public Snapshot(ISnapshot data) :
            base(data, typeof(ISnapshot))
        {

        }

        #region ISnapshot

        public int SnapshotId { get; set; }

        public string Url { get; set; }

        public string Html { get; set; }

        public DateTime Date { get; set; }

        #endregion
    }
}
