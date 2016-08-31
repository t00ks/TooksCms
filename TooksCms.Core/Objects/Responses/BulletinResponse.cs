using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases;

namespace TooksCms.Core.Objects.Responses
{
    public class BulletinResponse : ResponseBase
    {
        public BulletinResponse()
        {
            this.area = Enums.AreaType.Home;
        }
    }
}
