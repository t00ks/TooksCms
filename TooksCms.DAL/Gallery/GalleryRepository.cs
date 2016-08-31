using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Exceptions;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Interfaces.Repository;

namespace TooksCms.DAL
{
    public class GalleryRepository : IGalleryRepository
    {
        #region Gallery

        public IGallery UpdateGallery(IGallery data)
        {
            var db = new TooksCmsDAL();

            if (!CheckGalleryExists(db, data.GalleryId))
            {
                throw new DataNotFoundException("Database does not contain Gallery with id:" + data.GalleryId.ToString());
            }

            var g = db.Galleries.Single(g_ => g_.GalleryId == data.GalleryId);
            g.Update(data);

            db.SaveChanges();

            return g;
        }

        public IGallery InsertGallery(IGallery data)
        {
            var db = new TooksCmsDAL();

            var g = Gallery.CreateGallery(data);
            db.Galleries.Add(g);

            db.SaveChanges();

            return g;

        }

        public IEnumerable<IGallery> FetchGalleries()
        {
            var db = new TooksCmsDAL();

            return db.Galleries;
        }

        public IGallery FetchGallery(int galleryId)
        {
            var db = new TooksCmsDAL();

            if (!CheckGalleryExists(db, galleryId))
            {
                throw new DataNotFoundException("Database does not contain Gallery with id:" + galleryId.ToString());
            }

            return db.Galleries.Single(g_ => g_.GalleryId == galleryId);
        }

        public bool CheckGalleryExists(int galleryId)
        {
            var db = new TooksCmsDAL();
            return CheckGalleryExists(db, galleryId);
        }

        private bool CheckGalleryExists(TooksCmsDAL db, int galleryId)
        {
            return db.Galleries.Any(g_ => g_.GalleryId == galleryId);
        }

        #endregion

        #region GalleryImage

        public IGalleryImage UpdateGalleryImage(IGalleryImage data)
        {
            var db = new TooksCmsDAL();

            if (!CheckGalleryImageExists(db, data.GalleryImageId))
            {
                throw new DataNotFoundException("Database does not contain GalleryImage with id:" + data.GalleryId.ToString());
            }

            var g = db.GalleryImages.Single(g_ => g_.GalleryImageId == data.GalleryImageId);
            g.Update(data);

            db.SaveChanges();

            return g;
        }

        public IGalleryImage InsertGalleryImage(IGalleryImage data)
        {
            var db = new TooksCmsDAL();

            var g = GalleryImage.CreateGalleryImage(data);
            db.GalleryImages.Add(g);

            db.SaveChanges();

            return g;

        }

        public IEnumerable<IGalleryImage> FetchGalleryImages()
        {
            var db = new TooksCmsDAL();

            return db.GalleryImages;
        }

        public IEnumerable<IGalleryImage> FetchGalleryImages(int galleryId)
        {
            var db = new TooksCmsDAL();

            return db.GalleryImages.Where(g_ => g_.GalleryId == galleryId);
        }

        public IGalleryImage FetchGalleryImage(int galleryImageId)
        {
            var db = new TooksCmsDAL();

            if (!CheckGalleryExists(db, galleryImageId))
            {
                throw new DataNotFoundException("Database does not contain Gallery with id:" + galleryImageId.ToString());
            }

            return db.GalleryImages.Single(g_ => g_.GalleryImageId == galleryImageId);
        }

        public bool CheckGalleryImageExists(int galleryImageId)
        {
            var db = new TooksCmsDAL();
            return CheckGalleryExists(db, galleryImageId);
        }

        private bool CheckGalleryImageExists(TooksCmsDAL db, int galleryImageId)
        {
            return db.GalleryImages.Any(g_ => g_.GalleryImageId == galleryImageId);
        }

        #endregion

        #region GalleryInfo

        public IEnumerable<IGalleryInfo> FetchGalleryInfos()
        {
            var db = new TooksCmsDAL();
            return db.GetGalleryInfo(null).OrderByDescending(gi => gi.CreatedDate);
        }

        public IEnumerable<IGalleryInfo> FetchGalleryInfos(int? count)
        {
            var db = new TooksCmsDAL();
            return db.GetGalleryInfo(null).Take(count.HasValue ? count.Value : 10).OrderByDescending(gi => gi.CreatedDate);
        }

        #endregion
    }
}
