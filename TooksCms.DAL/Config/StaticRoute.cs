using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;

namespace TooksCms.DAL
{
    public partial class StaticRoute : IStaticRoute
    {
        public static StaticRoute CreateStaticRoute(IStaticRoute data)
        {
            return new StaticRoute
            {
                StaticRoute1 = data.StaticRouteUrl,
                Area = data.Area,
                Action = data.Action,
                Id = data.Id
            };

        }

        public string StaticRouteUrl
        {
            get { return this.StaticRoute1; }
            set { this.StaticRoute1 = value; }
        }
    }
}
