using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;

namespace TooksCms.ServiceLayer.Objects
{
    public class BulletinType : IBulletinType
    {
        public BulletinType(IBulletinType data)
        {
            BulletinTypeId = data.BulletinTypeId;
            BulletinTypeUid = data.BulletinTypeUid;
            Name = data.Name;
            Description = data.Description;
            Class = data.Class;
            Assembly = data.Assembly;
        }

        public int BulletinTypeId { get; private set; }

        public Guid BulletinTypeUid { get; private set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Class { get; private set; }

        public string Assembly { get; private set; }
    }
}
