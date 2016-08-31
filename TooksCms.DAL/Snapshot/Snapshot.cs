using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooksCms.Core.Interfaces;

namespace TooksCms.DAL
{
    public partial class Snapshot : ISnapshot
    {
        public static Snapshot CreateSnapshot(ISnapshot data)
        {
            return new Snapshot
            {
                SnapshotId = data.SnapshotId,
                Url = data.Url,
                Html = data.Html,
                Date = data.Date
            };
        }

        public void Update(ISnapshot data)
        {
            this.Url = data.Url;
            this.Html = data.Html;
            this.Date = data.Date;
        }
    }
}
