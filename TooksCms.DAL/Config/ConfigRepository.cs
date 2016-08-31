using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Interfaces.Repository;

namespace TooksCms.DAL
{
    public class ConfigRepository : IConfigRepository
    {
        public IEnumerable<IGadget> FetchGadgets(int roleId, int areaTypeId)
        {
            var db = new TooksCmsDAL();

            return db.Gadget2Role2AreaType.Where(s_ => s_.RoleId == roleId && s_.AreaTypeId == areaTypeId).Select(s_ => s_.Gadget);
        }

        public IEnumerable<IGadget> FetchGadgets()
        {
            var db = new TooksCmsDAL();

            return db.Gadgets;
        }

        public IEnumerable<IGadgetInfo> FetchGagetInfo()
        {
            var db = new TooksCmsDAL();

            return db.GetGadgetInfo(null, null, null);
        }

        public IEnumerable<IArticleType> FetchArticleTypes()
        {
            var db = new TooksCmsDAL();

            return db.ArticleTypes;
        }

        public bool GadgetLinkExists(int gadgetId, int areaType, int roleId)
        {
            var db = new TooksCmsDAL();
            return db.Gadget2Role2AreaType.Any(g2a => g2a.AreaTypeId == areaType && g2a.GadgetId == gadgetId && g2a.RoleId == roleId);
        }

        public IGadgetInfo AddGadgetLink(int gadgetId, int areaType, int roleId)
        {
            var db = new TooksCmsDAL();
            var gl = new Gadget2Role2AreaType { GadgetId = gadgetId, RoleId = roleId, AreaTypeId = areaType };
            db.Gadget2Role2AreaType.Add(gl);
            db.SaveChanges();

            return db.GetGadgetInfo(roleId, gadgetId, areaType).First();
        }

        public void RemoveGadgetLink(IGadgetInfo data)
        {
            var db = new TooksCmsDAL();
            var gl = db.Gadget2Role2AreaType.Single(gl_ => gl_.AreaType.AreaType1 == data.AreaType &&
                gl_.GadgetId == data.GadgetId &&
                gl_.Role.RoleName == data.RoleName);
            db.Gadget2Role2AreaType.Remove(gl);
            db.SaveChanges();
        }

        #region Rating

        public IEnumerable<IRating> FetchRatings()
        {
            var db = new TooksCmsDAL();
            return db.Ratings;
        }

        public IEnumerable<IRating> FetchRatings(int articleTypeId, int categoryId)
        {
            var db = new TooksCmsDAL();

            return db.Ratings
                        .Join(db.Rating2ArticleType2Category,
                                r => r.RatingId,
                                rac => rac.RatingId,
                                (r, rac) => new { r, rac })
                        .Where(z => z.rac.ArticleTypeId == articleTypeId && z.rac.CategoryId == categoryId)
                        .OrderBy(z => z.rac.Ordinal)
                        .Select(z => z.r);
        }

        public IRating InsertRating(IRating data)
        {
            var db = new TooksCmsDAL();

            var r = Rating.CreateRating(data);
            db.Ratings.Add(r);
            db.SaveChanges();

            return r;
        }

        public IRating UpdateRating(IRating data)
        {
            var db = new TooksCmsDAL();

            var r = db.Ratings.First(r_ => r_.RatingId == data.RatingId);
            r.Update(data);
            db.SaveChanges();

            return r;
        }

        public IEnumerable<IRatingLink> FetchRatingLinks()
        {
            var db = new TooksCmsDAL();
            var dlinks = db.Rating2ArticleType2Category
                            .Join(db.Ratings,
                                    rac => rac.RatingId,
                                    r => r.RatingId,
                                    (rac, r) => new { rac, r })
                            .OrderBy(z => z.rac.Ordinal).ToList();
            var links = new List<RatingLink>();
            dlinks.ForEach(e =>
            {
                if (links.Any(l => l.CategoryId == e.rac.CategoryId && l.ArticleTypeId == e.rac.ArticleTypeId))
                {
                    links.Single(l => l.CategoryId == e.rac.CategoryId && l.ArticleTypeId == e.rac.ArticleTypeId).AddLink(e.r, e.rac.Ordinal);
                }
                else
                {
                    links.Add(new RatingLink(e.rac, e.r));
                }
            });
            return links;
        }

        public void CreateRatingLink(IRatingLink link)
        {
            var db = new TooksCmsDAL();

            db.DeleteRatingLinks(link.ArticleTypeId, link.CategoryId);
            foreach (var rating in link.RatingIds)
            {
                CreateRatingLink(db, rating.Value.RatingId, link.ArticleTypeId, link.CategoryId, rating.Key);
            }

            db.SaveChanges();
        }

        private void CreateRatingLink(TooksCmsDAL db, int ratingId, int articleTypeId, int categoryid, int ordinal)
        {
            var link = new Rating2ArticleType2Category { RatingId = ratingId, ArticleTypeId = articleTypeId, CategoryId = categoryid, Ordinal = ordinal };

            db.Rating2ArticleType2Category.Add(link);
        }

        public bool CheckRatingExists(int articleTypeId, int categoryId)
        {
            var db = new TooksCmsDAL();
            return db.Rating2ArticleType2Category.Any(rac => rac.ArticleTypeId == articleTypeId && rac.CategoryId == categoryId);
        }

        #endregion

        #region Routes

        public bool CheckRouteExists(string route)
        {
            var db = new TooksCmsDAL();
            return db.StaticRoutes.Any(r_ => r_.StaticRoute1 == route);
        }

        public IStaticRoute CreateRoute(IStaticRoute data)
        {
            var db = new TooksCmsDAL();

            var r = StaticRoute.CreateStaticRoute(data);
            db.StaticRoutes.Add(r);

            db.SaveChanges();

            return r;
        }

        public IEnumerable<IStaticRoute> FetchRoutes()
        {
            var db = new TooksCmsDAL();
            return db.StaticRoutes;
        }

        public void DeleteRoute(int id)
        {
            var db = new TooksCmsDAL();
            db.StaticRoutes.Remove(db.StaticRoutes.Single(r => r.Id == id));
            db.SaveChanges();
        }

        #endregion
    }
}
