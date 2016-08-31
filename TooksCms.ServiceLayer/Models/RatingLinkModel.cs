using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TooksCms.ServiceLayer.Objects;
using TooksCms.ServiceLayer.Objects.Lookup;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Interfaces.Repository;

namespace TooksCms.ServiceLayer.Models
{
    public class RatingLinkModelLite
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }

        public List<RatingModel> Ratings { get; set; }

        public string CategoryName { get; set; }
        public int CategoryId { get; set; }

        public string ArticleTypeName { get; set; }
        public int ArticleTypeId { get; set; }

        public bool IsDirty { get; set; }
        public bool IsNew { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class RatingLinkModel : ModelBase
    {
        private ILookupRepository _lookupRepository;
        private IArticleRepository _articleRepository;

        public RatingLinkModel()
        {
            _lookupRepository = DependencyResolver.Current.GetService<ILookupRepository>();
        }

        public RatingLinkModel(IRatingLink data)
        {
            _lookupRepository = DependencyResolver.Current.GetService<ILookupRepository>();
            _articleRepository = DependencyResolver.Current.GetService<IArticleRepository>();

            this.Category = new CategoryInfo(_lookupRepository.FetchCategoryInfo(data.CategoryId));
            this.ArticleType = new ArticleType(_articleRepository.FetchType(data.ArticleTypeId));
            this.Ratings = data.RatingIds.Select(r => new RatingModel(r.Value)).ToList();
        }

        public RatingLinkModel(RatingLinkModelLite lite)
        {
            _lookupRepository = DependencyResolver.Current.GetService<ILookupRepository>();
            _articleRepository = DependencyResolver.Current.GetService<IArticleRepository>();

            this.Category = new CategoryInfo(_lookupRepository.FetchCategoryInfo(lite.CategoryId));
            this.ArticleType = new ArticleType(_articleRepository.FetchType(lite.ArticleTypeId));
            this.Ratings = lite.Ratings;

            this._isDeleted = lite.IsDeleted;
            this._isDirty = lite.IsDirty;
            this._isNew = lite.IsNew;
        }

        public CategoryInfo Category { get; set; }

        public List<RatingModel> Ratings { get; set; }

        public ArticleType ArticleType { get; set; }


        #region CRUD

        public void Save()
        {
            var cRep = DependencyResolver.Current.GetService<IConfigRepository>();
            cRep.CreateRatingLink(BuildInterface());
        }

        public RatingLink BuildInterface()
        {
            var dic = new Dictionary<int, IRating>();
            var count = 1;
            Ratings.ForEach(r =>
            {
                dic.Add(count, r.BuildInterface());
                count++;
            });
            return RatingLink.CreateRatingLink(dic, ArticleType.ArticleTypeId, Category.CategoryId);
        }

        #endregion

        #region Static Methods

        public static List<RatingLinkModel> GetList()
        {
            var cRep = DependencyResolver.Current.GetService<IConfigRepository>();
            return cRep.FetchRatingLinks().Select(rl => new RatingLinkModel(rl)).ToList();
        }

        public static RatingLinkModel CreateNew(int articleTypeId, int categoryId)
        {
            var lRep = DependencyResolver.Current.GetService<ILookupRepository>();
            var aRep = DependencyResolver.Current.GetService<IArticleRepository>();

            return new RatingLinkModel
                       {
                           Category = new CategoryInfo(lRep.FetchCategoryInfo(categoryId)),
                           ArticleType = new ArticleType(aRep.FetchType(articleTypeId)),
                           Ratings = new List<RatingModel>()
                       };
        }

        public static bool CheckExists(int articleTypeId, int categoryId)
        {
            var cRep = DependencyResolver.Current.GetService<IConfigRepository>();
            return cRep.CheckRatingExists(articleTypeId, categoryId);
        }

        #endregion

        public RatingLinkModelLite GetLite()
        {
            return new RatingLinkModelLite
            {
                Id = this.Id,
                Uid = this.Uid,
                Ratings = this.Ratings,
                CategoryName = this.Category.FullCategoryName,
                CategoryId = this.Category.CategoryId,
                ArticleTypeName = this.ArticleType.Name,
                ArticleTypeId = this.ArticleType.ArticleTypeId,
                IsDirty = this.IsDirty,
                IsDeleted = this.IsDeleted,
                IsNew = this.IsNew
            };
        }
    }
}
