using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Interfaces
{
    public interface IBulletinType
    {
        int BulletinTypeId { get; }
        Guid BulletinTypeUid { get; }
        string Name { get; set; }
        string Description { get; set; }
        string Class { get; }
        string Assembly { get; }
    }
}
