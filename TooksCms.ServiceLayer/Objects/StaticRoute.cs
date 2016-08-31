using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces.Repository;
using System.Web.Mvc;

namespace TooksCms.ServiceLayer.Objects
{
    public class StaticRoute : InterfacingBase, IStaticRoute
    {
        private StaticRoute() { }

        public StaticRoute (IStaticRoute data) :
         base(data, typeof(IStaticRoute)) { }

        #region IStaticRoutes

        public string StaticRouteUrl { get; set; }

        public string Area { get; set; }

        public string Action { get; set; }

        public int Id { get; set; }

        #endregion

        public static StaticRoute Create(string staticRoute, string area, string action, int id)
        {
            return new StaticRoute
            {
                StaticRouteUrl = staticRoute,
                Area = area,
                Action = action,
                Id = id
            };
        }

        public static List<StaticRoute> List()
        {
            var rep = DependencyResolver.Current.GetService<IConfigRepository>();
            var rs = rep.FetchRoutes();
            foreach (var r in rs)
            {
                var nr = new StaticRoute(r);
            }

            return rep.FetchRoutes().Select(sr_ => new Objects.StaticRoute(sr_)).ToList();
        }
    }
}
