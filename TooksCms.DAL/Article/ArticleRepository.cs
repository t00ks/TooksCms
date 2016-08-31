using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Exceptions;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Interfaces.Repository;

namespace TooksCms.DAL
{
    public class ArticleRepository : IArticleRepository
    {
        #region Article

        public IArticle Fetch(int id)
        {
            var db = new TooksCmsDAL();

            if (!_exists(id, db))
            {
                throw new DataNotFoundException("Article does not exits", "id");
            }

            return db.Articles.SingleOrDefault(a_ => a_.ArticleId == id);
        }

        public IEnumerable<IArticle> FetchList(int count, string type)
        {
            var db = new TooksCmsDAL();
            return db.Articles.Where(a_ => a_.ArticleType.Name == type).OrderByDescending(a_ => a_.Date).Take(count);
        }

        public IEnumerable<IArticle> Search(string search)
        {
            var db = new TooksCmsDAL();
            return db.SearchArticles(search);
        }

        public IEnumerable<IArticle> FetchList(int count, DateTime from)
        {
            var db = new TooksCmsDAL();
            return db.Articles.Where(a_ => a_.Date >= from).OrderByDescending(a_ => a_.Date).Take(count);
        }

        public IEnumerable<IArticle> FetchList(int count, int skip)
        {
            var db = new TooksCmsDAL();
            return db.Articles.OrderByDescending(a_ => a_.Date).Skip(skip).Take(count);
        }

        public int Insert(IArticle data)
        {
            var db = new TooksCmsDAL();

            var a = Article.CreateArticle(data);
            db.Articles.Add(a);

            db.SaveChanges();

            return a.ArticleId;
        }

        public IArticle InsertContent(IArticle data)
        {
            var db = new TooksCmsDAL();
            var a = db.Articles.SingleOrDefault(a_ => a_.ArticleId == data.ArticleId);
            a.UpdateContent(data);

            db.SaveChanges();
            return a;
        }

        public int Update(IArticle data)
        {
            var db = new TooksCmsDAL();

            if (!_exists(data.ArticleId, db))
            {
                throw new DataNotFoundException("Article does not exits", "id");
            }

            var a = db.Articles.First(a_ => a_.ArticleId == data.ArticleId);
            a.Update(data);
            a.UpdateContent(data);

            db.SaveChanges();

            return a.ArticleId;
        }

        public void Delete(int id)
        {
            var db = new TooksCmsDAL();
            db.DeleteArticle(id);
        }

        public bool Exists(int id)
        {
            var db = new TooksCmsDAL();
            return _exists(id, db);
        }

        private bool _exists(int id, TooksCmsDAL db)
        {
            return db.Articles.Any(a_ => a_.ArticleId == id);
        }

        #endregion Article

        #region ArticleType

        public IArticleType FetchType(int id)
        {
            var db = new TooksCmsDAL();

            if (!_typeExists(id, db))
            {
                throw new DataNotFoundException("ArticleType does not exits", "id");
            }

            return db.ArticleTypes.FirstOrDefault(at_ => at_.ArticleTypeId == id);
        }

        public IArticleType FetchType(string name)
        {
            var db = new TooksCmsDAL();

            if (!_typeExists(name, db))
            {
                throw new DataNotFoundException("ArticleType does not exits", "name");
            }

            return db.ArticleTypes.FirstOrDefault(at_ => at_.Name.ToLower() == name.ToLower());
        }

        public bool TypeExists(int id)
        {
            var db = new TooksCmsDAL();
            return _typeExists(id, db);
        }

        public bool TypeExists(string name)
        {
            var db = new TooksCmsDAL();
            return _typeExists(name, db);
        }

        private bool _typeExists(int id, TooksCmsDAL db)
        {
            return db.ArticleTypes.Any(a_ => a_.ArticleTypeId == id);
        }

        private bool _typeExists(string name, TooksCmsDAL db)
        {
            return db.ArticleTypes.Any(a_ => a_.Name.ToLower() == name.ToLower());
        }

        #endregion ArticleType

        #region ArticleImage

        public IEnumerable<IArticleImage> InsertAllImages(IEnumerable<IArticleImage> data)
        {
            var db = new TooksCmsDAL();

            var returnCollection = new List<IArticleImage>();

            foreach (IArticleImage image in data)
            {
                returnCollection.Add(_insert(image, db));
            }

            db.SaveChanges();

            return returnCollection;
        }

        private IArticleImage _insert(IArticleImage data, TooksCmsDAL db)
        {
            var i = ArticleImage.CreateArticleImage(data);
            db.ArticleImages.Add(i);
            return i;
        }

        public IEnumerable<IArticleImage> UpdateAllImages(IEnumerable<IArticleImage> data)
        {
            var db = new TooksCmsDAL();

            var returnCollection = new List<IArticleImage>();

            foreach (IArticleImage image in data)
            {
                if (!_imageExists(image.ArticleImageId, db))
                {
                    returnCollection.Add(_insert(image, db));
                }
                else
                {
                    var i = db.ArticleImages.Single(i_ => i_.ArticleImageId == image.ArticleImageId);
                    i.Update(image);
                    returnCollection.Add(i);
                }
            }

            db.SaveChanges();

            return returnCollection;

        }

        public IEnumerable<IArticleImage> FetchArticleImages(int parentId)
        {
            var db = new TooksCmsDAL();

            return db.ArticleImages.Where(i => i.ArticleId == parentId);
        }

        public bool ImageExists(int id)
        {
            var db = new TooksCmsDAL();
            return _imageExists(id, db);
        }

        private bool _imageExists(int id, TooksCmsDAL db)
        {
            return db.ArticleImages.Any(i_ => i_.ArticleImageId == id);
        }

        #endregion

        #region ArticleComment

        public IArticleComment FetchComment(int id)
        {
            var db = new TooksCmsDAL();

            if (!db.ArticleComments.Any(ac_ => ac_.ArticleCommentId == id))
            {
                throw new DataNotFoundException("Article does not exits", "id");
            }

            return db.ArticleComments.SingleOrDefault(a_ => a_.ArticleCommentId == id);
        }

        public IEnumerable<IArticleComment> FetchComments()
        {
            var db = new TooksCmsDAL();

            return db.ArticleComments;
        }

        public IEnumerable<IArticleComment> FetchComments(int articleId)
        {
            var db = new TooksCmsDAL();

            return db.ArticleComments.Where(ac_ => ac_.ArticleId == articleId && !ac_.ParentCommentId.HasValue);
        }

        public IEnumerable<IArticleComment> FetchChildComments(int parentId)
        {
            var db = new TooksCmsDAL();

            return db.ArticleComments.Where(ac_ => ac_.ParentCommentId == parentId);
        }

        public IArticleComment InsertComment(IArticleComment data)
        {
            var db = new TooksCmsDAL();

            var ac = ArticleComment.CreateArticleComment(data);
            db.ArticleComments.Add(ac);

            db.SaveChanges();

            return ac;
        }

        public IArticleComment UpdateComment(IArticleComment data)
        {
            var db = new TooksCmsDAL();

            if (!db.ArticleComments.Any(ac_ => ac_.ArticleCommentId == data.ArticleCommentId))
            {
                throw new DataNotFoundException("Comment does not exits", "id");
            }

            var ac = db.ArticleComments.First(ac_ => ac_.ArticleCommentId == data.ArticleCommentId);
            ac.Update(data);

            db.SaveChanges();

            return ac;
        }

        public void DeleteComment(int commentId)
        {
            var db = new TooksCmsDAL();

            db.ArticleComments.Remove(db.ArticleComments.Single(c => c.ArticleCommentId == commentId));
            db.SaveChanges();
        }

        #endregion

        #region ArticleInfo

        public IArticleInfo FetchArticleInfo(int articleId)
        {
            var db = new TooksCmsDAL();
            return db.GetArticleInfo(articleId).Single();
        }

        public IEnumerable<IArticleInfo> FetchArticleInfos()
        {
            var db = new TooksCmsDAL();
            return db.GetArticleInfo(null).OrderByDescending(ai => ai.Date);
        }

        public IEnumerable<IArticleInfo> FetchArticleInfos(int? count)
        {
            var db = new TooksCmsDAL();
            return db.GetArticleInfo(null).OrderByDescending(ai => ai.Date).Take(count.HasValue ? count.Value : 10);
        }

        public IEnumerable<IArticleInfo> FetchArticleInfos(int articleTypeId)
        {
            var db = new TooksCmsDAL();
            return db.GetArticleInfo(null).Where(ai => ai.ArticleTypeId == articleTypeId);
        }

        public IEnumerable<IArticleInfo> FetchArticleInfos(string typeName)
        {
            var db = new TooksCmsDAL();
            return db.GetArticleInfo(null).Where(ai => ai.TypeName.ToLower() == typeName.ToLower());
        }

        public IEnumerable<IArticleInfo> SearchArticleInfos(string term)
        {
            var db = new TooksCmsDAL();
            return db.ArticleInfoSearch(term);
        }

        #endregion
    }
}
