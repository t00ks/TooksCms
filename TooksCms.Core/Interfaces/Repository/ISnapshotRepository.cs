using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TooksCms.Core.Interfaces.Repository
{
    public interface ISnapshotRepository
    {
        IEnumerable<ISnapshot> Fetch(bool includeHtml = false); 
        ISnapshot Fetch(string url);
        void UpdateOrInsert(ISnapshot data);
    }
}
