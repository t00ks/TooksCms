using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Exceptions;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Interfaces.Repository;

namespace TooksCms.DAL
{
    public class BulletinRepository : IBulletinRepository
    {
        #region Bulletin

        public IBulletin Fetch(int id)
        {
            var db = new TooksCmsDAL();

            if (!_exists(id, db))
            {
                throw new DataNotFoundException("Bulletin does not exits", "id");
            }
            return db.Bulletins.SingleOrDefault(b_ => b_.BulletinId == id);
        }

        public IBulletin FetchOnArticleId(int articleId)
        {
            var db = new TooksCmsDAL();

            if (!db.Bulletins.Any(b_ => b_.ArticleId == articleId))
            {
                throw new DataNotFoundException("Article Bulletin does not exits", "id");
            }

            return db.Bulletins.Single(b_ => b_.ArticleId == articleId);
        }

        public IBulletin FetchOnGalleryId(int galleryId)
        {
            var db = new TooksCmsDAL();

            if (!db.Bulletins.Any(b_ => b_.GalleryId == galleryId))
            {
                throw new DataNotFoundException("Gallery Bulletin does not exits", "id");
            }

            return db.Bulletins.Single(b_ => b_.GalleryId == galleryId);
        }

        public IEnumerable<IBulletin> FetchList()
        {
            var db = new TooksCmsDAL();
            return db.Bulletins.OrderByDescending(b_ => b_.Date)
                .ThenBy(b_ => b_.BulletinId);
        }

        public IEnumerable<IBulletin> FetchList(int count)
        {
            var db = new TooksCmsDAL();
            return db.Bulletins.OrderByDescending(b_ => b_.Date)
                .ThenBy(b_ => b_.BulletinId)
                .Take(count);
        }

        public IEnumerable<IBulletin> FetchList(int count, DateTime from)
        {
            var db = new TooksCmsDAL();
            return db.Bulletins.Where(b_ => b_.Date >= from)
                .OrderByDescending(b_ => b_.Date)
                .ThenBy(b_ => b_.BulletinId)
                .Take(count);
        }

        public IEnumerable<IBulletin> FetchList(int count, int skip)
        {
            var db = new TooksCmsDAL();
            return db.Bulletins.OrderByDescending(b_ => b_.Date)
                .ThenBy(b_ => b_.BulletinId)
                .Skip(skip)
                .Take(count);
        }

        public int Insert(IBulletin data)
        {
            var db = new TooksCmsDAL();

            var b = Bulletin.CreateBulletin(data);

            db.Bulletins.Add(b);
            db.SaveChanges();

            return b.BulletinId;
        }

        public IBulletin InsertContent(IBulletin data)
        {
            var db = new TooksCmsDAL();
            var b = db.Bulletins.SingleOrDefault(b_ => b_.BulletinId == data.BulletinId);
            b.BulletinContents.Add(BulletinContent.CreateBulletinContent(data));
            db.SaveChanges();
            return b;
        }

        public IBulletin Update(IBulletin data)
        {
            var db = new TooksCmsDAL();

            if (!_exists(data.BulletinId, db))
            {
                throw new DataNotFoundException("Bulletin does not exits", "id");
            }

            var b = db.Bulletins.First(b_ => b_.BulletinId == data.BulletinId);
            b.Update(data);
            b.UpdateContent(data);

            db.SaveChanges();
            return b;
        }

        public bool Exists(int id)
        {
            var db = new TooksCmsDAL();
            return _exists(id, db);
        }

        private bool _exists(int id, TooksCmsDAL db)
        {
            return db.Bulletins.Any(b_ => b_.BulletinId == id);
        }

        #endregion Bulletin

        #region BulletinType

        public IBulletinType FetchType(int id)
        {
            var db = new TooksCmsDAL();

            if (!_typeExists(id, db))
            {
                throw new DataNotFoundException("BulletinType does not exits", "id");
            }

            return db.BulletinTypes.FirstOrDefault(bt_ => bt_.BulletinTypeId == id);
        }

        public IBulletinType FetchType(string name)
        {
            var db = new TooksCmsDAL();

            if (!_typeExists(name, db))
            {
                throw new DataNotFoundException("BulletinType does not exits", "name");
            }

            return db.BulletinTypes.FirstOrDefault(at_ => at_.Name.ToLower() == name.ToLower());
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
            return db.BulletinTypes.Any(bt_ => bt_.BulletinTypeId == id);
        }

        private bool _typeExists(string name, TooksCmsDAL db)
        {
            return db.BulletinTypes.Any(bt_ => bt_.Name.ToLower() == name.ToLower());
        }

        #endregion BulletinType
    }
}
