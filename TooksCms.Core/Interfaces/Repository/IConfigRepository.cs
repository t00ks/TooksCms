using System;
using System.Collections.Generic;
using TooksCms.Core.Interfaces;

namespace TooksCms.Core.Interfaces.Repository
{
    public interface IConfigRepository
    {
        IGadgetInfo AddGadgetLink(int gadgetId, int areaType, int roleId);
        void RemoveGadgetLink(IGadgetInfo data);
        bool CheckRatingExists(int articleTypeId, int categoryId);
        bool CheckRouteExists(string route);
        void CreateRatingLink(IRatingLink link);
        IStaticRoute CreateRoute(IStaticRoute data);
        void DeleteRoute(int id);
        IEnumerable<IArticleType> FetchArticleTypes();
        IEnumerable<IGadget> FetchGadgets();
        IEnumerable<IGadget> FetchGadgets(int roleId, int areaTypeId);
        IEnumerable<IGadgetInfo> FetchGagetInfo();
        IEnumerable<IRatingLink> FetchRatingLinks();
        IEnumerable<IRating> FetchRatings();
        IEnumerable<IRating> FetchRatings(int articleTypeId, int categoryId);
        IEnumerable<IStaticRoute> FetchRoutes();
        bool GadgetLinkExists(int gadgetId, int areaType, int roleId);
        IRating InsertRating(IRating data);
        IRating UpdateRating(IRating data);
    }
}
