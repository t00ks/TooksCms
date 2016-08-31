using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using TooksCms.ServiceLayer.Objects.Account;
using TooksCms.Core.Bases;
using System.Web;
using System.IO;
using TooksCms.ServiceLayer.Objects;
using TooksCms.Core.Interfaces.Repository;
using TooksCms.Core.Interfaces;

namespace TooksCms.ServiceLayer.Models
{
    public class GalleryDTO
    {
        public int Id { get; set; }

        public Guid Uid { get; set; }

        public List<GalleryImageModel> Images { get; set; }

        public string Title { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public int CategoryId { get; set; }

    }

    public class GalleryModel : ModelBase
    {
        public GalleryModel() { }

        public GalleryModel(IGallery data)
        {
            this.Id = data.GalleryId;
            this.Uid = data.GalleryUid;
            this.Title = data.Title;
            this.UserId = data.CreatedByUserId;
            var user = new User(DependencyResolver.Current.GetService<IAccountRepository>().FetchUser(this.UserId));
            this.UsersName = user.ContactInfo.FirstName + " " + user.ContactInfo.LastName;
            this.CreatedDate = data.CreatedDate;
            this.CategoryId = data.CategoryId;
            this.Images = DependencyResolver.Current.GetService<IGalleryRepository>().FetchGalleryImages(data.GalleryId).Select(gi_ => new GalleryImageModel(gi_)).ToList();
        }

        public GalleryModel(GalleryDTO dto)
        {
            this.Id = dto.Id;
            this.Uid = dto.Uid;
            this.Title = dto.Title;
            this.UserId = dto.UserId;
            this.CreatedDate = dto.Date;
            this.CategoryId = dto.CategoryId;
            this.Images = dto.Images;
        }

        #region Public Properties

        public List<GalleryImageModel> Images { get; set; }

        public string Title { get; set; }

        public int UserId { get; set; }

        public string UsersName { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CategoryId { get; set; }

        #endregion

        #region CRUD

        public void SaveImages(IGalleryRepository gRep)
        {
            if (Images != null)
            {
                try
                {
                    string newPath = "~/Uploads/Images/Galleries/" + Uid + "/";
                    newPath = HttpContext.Current.Request.MapPath(newPath);
                    string oldPath = "~/Uploads/Images/Temp/";
                    oldPath = HttpContext.Current.Request.MapPath(oldPath);
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }


                    foreach (var image in Images)
                    {
                        if (image.Id == 0)
                        {
                            File.Move(oldPath + image.Image, newPath + image.Image);
                            File.Move(oldPath + image.Thumbnail, newPath + image.Thumbnail);
                            image.Id = gRep.InsertGalleryImage(image.BuildInterface(this.Id)).GalleryImageId;
                        }
                    }


                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void Save()
        {
            var gRep = DependencyResolver.Current.GetService<IGalleryRepository>();
            try
            {
                if (!IsNew & IsDeleted)
                {
                    /* [Delete] an existing object marked for deletion */
                    //dc.ArticleDelete(Id);
                }
                else
                {
                    /* Exception will cause the transaction to rollback */
                    if (IsNew)
                    {
                        /* [Insert] a new and valid object to be saved */
                        this.Id = gRep.InsertGallery(BuildInteface()).GalleryId;
                    }
                    else if (!IsNew & IsDirty)
                    {
                        /* [Update] a existing, but changed object to be saved */
                        gRep.UpdateGallery(BuildInteface());
                    }
                    SaveImages(gRep);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (!IsNew & IsDeleted)
            {
            }
            else
            {
                if (IsNew)
                {
                    CreateBulletin();
                }
                else if (!IsNew & IsDirty)
                {
                    UpdateBulletin();
                }
            }
            MarkOld();
        }

        private void CreateBulletin()
        {
            GalleryBulletin.Create(Id, Title, "Gallery/View/" + Id, "Read More", CreatedDate, Images.Take(3));
        }

        private void UpdateBulletin()
        {
            GalleryBulletin.Update(Id, Title, "Gallery/View/" + Id, "Read More", CreatedDate, Images.Take(3));
        }

        #endregion

        private Gallery BuildInteface()
        {
            return Gallery.Create(this.Id, this.Uid, this.Title, this.UserId, this.CreatedDate, this.CategoryId);
        }

        public static List<GalleryModel> List(IGalleryRepository gRep)
        {
            return gRep.FetchGalleries().Select(g_ => new GalleryModel(g_)).ToList();
        }

        public static GalleryModel Load(int id, IGalleryRepository gRep)
        {
            return new GalleryModel(gRep.FetchGallery(id));
        }

        public static GalleryModel New(int userId, int categoryId)
        {
            return new GalleryModel
            {
                UserId = userId,
                Images = new List<GalleryImageModel>(),
                CategoryId = categoryId,
                CreatedDate = DateTime.Now
            };
        }

        private CategoryInfo _categoryInfo;
        public CategoryInfo CategoryInfo
        {
            get
            {
                if (this._categoryInfo == null)
                {
                    var lookupRepository = DependencyResolver.Current.GetService<ILookupRepository>();
                    this._categoryInfo = new Objects.CategoryInfo(lookupRepository.FetchCategoryInfo(this.CategoryId));
                }
                return _categoryInfo;
            }
        }

        public object GetJSONModel()
        {
            return new
            {
                Id = Id,
                Uid = Uid,
                Title = Title,
                Author = UsersName,
                Date = CreatedDate,
                UserId = UserId,
                Images = Images.Select(i => i.GetJSONModel()),
                CategoryInfo = this.CategoryInfo
            };
        }
    }
}
