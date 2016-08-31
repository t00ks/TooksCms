using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Exceptions;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Interfaces.Repository;

namespace TooksCms.DAL
{
    public class LookupRepository : ILookupRepository
    {
        #region Categories

        /// <summary>
        /// Fetch a category from the DAL.
        /// </summary>
        /// <param name="id">ID of the category</param>
        /// <returns>A Category DAL object</returns>
        /// <exception cref="TooksCms.Core.Exceptions.DataNotFoundException">Category does not exist</exception>
        public ICategory FetchCategory(int id)
        {
            var db = new TooksCmsDAL();

            if (!_categoryExists(id, db))
            {
                throw new DataNotFoundException("Category does not exits", "id");
            }

            return db.Categories.FirstOrDefault(c_ => c_.CategoryId == id);
        }

        public ICategoryInfo FetchCategoryInfo(int id)
        {
            var db = new TooksCmsDAL();

            if (!_categoryExists(id, db))
            {
                throw new DataNotFoundException("Category does not exits", "id");
            }

            return db.SelectCategoryInfo(id).FirstOrDefault();
        }

        public IEnumerable<ICategoryInfo> FetchCategoryInfos()
        {
            var db = new TooksCmsDAL();

            return db.SelectCategoryInfo(null);
        }

        public IEnumerable<ICategory> FetchCategories()
        {
            var db = new TooksCmsDAL();
            return db.Categories.ToList();
        }

        public IEnumerable<ICategory> FetchParentCategories()
        {
            var db = new TooksCmsDAL();
            return db.Categories.Where(c_ => !c_.ParentCategoryId.HasValue);
        }

        public IEnumerable<ICategory> FetchChildCategories()
        {
            var db = new TooksCmsDAL();
            return db.Categories.Where(c_ => c_.ParentCategoryId.HasValue);
        }

        public ICategory InsertCategory(ICategory data)
        {
            var db = new TooksCmsDAL();
            var c = Category.CreateCategory(data);
            db.Categories.Add(c);
            db.SaveChanges();
            return c;
        }

        public ICategory UpdateCategory(ICategory data)
        {
            var db = new TooksCmsDAL();

            if (!_categoryExists(data.CategoryId, db))
            {
                throw new DataNotFoundException("Category does not exist in the DAL", "id");
            }

            var c = db.Categories.FirstOrDefault(c_ => c_.CategoryId == data.CategoryId);
            c.Update(data);

            db.SaveChanges();

            return c;
        }

        public bool CategoryExists(int id)
        {
            var db = new TooksCmsDAL();
            return _categoryExists(id, db);
        }

        private bool _categoryExists(int id, TooksCmsDAL db)
        {
            return db.Categories.Any(c_ => c_.CategoryId == id);
        }

        #endregion

        #region Country

        /// <summary>
        /// Fetch a country from the DAL.
        /// </summary>
        /// <param name="id">ID of the country</param>
        /// <returns>A Country DAL object</returns>
        /// <exception cref="TooksCms.Core.Exceptions.DataNotFoundException">Country does not exist</exception>
        public ICountry FetchCountry(int id)
        {
            var db = new TooksCmsDAL();

            if (!_countryExists(id, db))
            {
                throw new DataNotFoundException("Country does not exits", "id");
            }

            return db.Countries.FirstOrDefault(c_ => c_.CountryId == id);
        }

        public IEnumerable<ICountry> FetchCountries()
        {
            var db = new TooksCmsDAL();
            return db.Countries.ToList();
        }

        public ICountry InsertCountry(ICountry data)
        {
            var db = new TooksCmsDAL();
            var c = Country.CreateCountry(data);
            db.Countries.Add(c);
            db.SaveChanges();
            return c;
        }

        public ICountry UpdateCountry(ICountry data)
        {
            var db = new TooksCmsDAL();

            if (!_countryExists(data.CountryId, db))
            {
                throw new DataNotFoundException("Country does not exits", "id");
            }

            var c = db.Countries.FirstOrDefault(c_ => c_.CountryId == data.CountryId);
            c.Update(data);
            db.SaveChanges();
            return c;
        }

        public bool CountryExists(int id)
        {
            var db = new TooksCmsDAL();
            return _countryExists(id, db);
        }

        private bool _countryExists(int id, TooksCmsDAL db)
        {
            return db.Countries.Any(c_ => c_.CountryId == id);
        }

        #endregion

        #region Tags

        public IEnumerable<ITag> FetchTags()
        {
            var db = new TooksCmsDAL();

            return db.Tags;
        }

        public IEnumerable<ITag> FetchCommonTags()
        {
            var db = new TooksCmsDAL();

            return db.Tags.OrderByDescending(t => (t.Articles.Count + t.Galleries.Count)).Take(5);
        }

        public IEnumerable<ITag> FetchCommonTagsNotInArticle(int articleId)
        {
            var db = new TooksCmsDAL();

            return db.Tags.Where(t => !t.Articles.Any(a => a.ArticleId == articleId)).OrderByDescending(t => (t.Articles.Count + t.Galleries.Count)).Take(5);
        }

        public IEnumerable<ITag> FetchCommonTagsNotInGallery(int galleryId)
        {
            var db = new TooksCmsDAL();

            return db.Tags.Where(t => !t.Galleries.Any(g => g.GalleryId == galleryId)).OrderByDescending(t => (t.Articles.Count + t.Galleries.Count)).Take(5);
        }

        public IEnumerable<ITag> FetchTagsForArticle(int articleId)
        {
            var db = new TooksCmsDAL();

            return db.Tags.Where(t => t.Articles.Any(a => a.ArticleId == articleId));
        }

        public IEnumerable<ITag> FetchTagsForGallery(int galleryId)
        {
            var db = new TooksCmsDAL();

            return db.Tags.Where(t => t.Galleries.Any(g => g.GalleryId == galleryId));
        }
        
        public IEnumerable<IRankedTag> FetchRankedTags(int count)
        {
            var db = new TooksCmsDAL();

            return db.Tags.OrderByDescending(t => (t.Articles.Count + t.Galleries.Count)).Take(count).Select(t => new RankedTag {Tag = t, Rank = (t.Articles.Count + t.Galleries.Count)});
        }

        public ITag FetchTag(string name)
        {
            var db = new TooksCmsDAL();

            return db.Tags.Single(t => t.Name == name);
        }

        public ITag FetchTag(int id)
        {
            var db = new TooksCmsDAL();

            return db.Tags.Single(t => t.TagId == id);
        }

        public ITag InsertTag(ITag tag)
        {
            var db = new TooksCmsDAL();

            var t = Tag.CreateTag(tag);

            db.Tags.Add(t);

            db.SaveChanges();

            return t;
        }

        public void InsertArticleTagLink(ITag tag, int articleId)
        {
            var db = new TooksCmsDAL();
            var t = db.Tags.FirstOrDefault(t_ => t_.TagId == tag.TagId);
            if (t != null)
            {
                t.Articles.Add(db.Articles.First(a => a.ArticleId == articleId));
            }
            db.SaveChanges();
        }

        public void InsertGalleryTagLink(ITag tag, int galleryId)
        {
            var db = new TooksCmsDAL();
            var t = db.Tags.FirstOrDefault(t_ => t_.TagId == tag.TagId);
            if (t != null)
            {
                t.Galleries.Add(db.Galleries.First(g => g.GalleryId == galleryId));
            }
            db.SaveChanges();
        }

        public void RemoveArticleTagLink(ITag tag, int articleId)
        {
            var db = new TooksCmsDAL();
            var t = db.Tags.FirstOrDefault(t_ => t_.TagId == tag.TagId);
            if (t != null)
            {
                t.Articles.Remove(db.Articles.First(a => a.ArticleId == articleId));
            }
            db.SaveChanges();
        }

        public void RemoveGalleryTagLink(ITag tag, int galleryId)
        {
            var db = new TooksCmsDAL();
            var t = db.Tags.FirstOrDefault(t_ => t_.TagId == tag.TagId);
            if (t != null)
            {
                t.Galleries.Remove(db.Galleries.First(g => g.GalleryId == galleryId));
            }
            db.SaveChanges();
        }

        public bool TagExists(string name)
        {
            var db = new TooksCmsDAL();
            return _tagExists(name, db);
        }

        private bool _tagExists(string name, TooksCmsDAL db)
        {
            return db.Tags.Any(t => t.Name == name);
        }

        #endregion
    }
}
