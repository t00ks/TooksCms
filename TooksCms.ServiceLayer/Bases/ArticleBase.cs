using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Serialization;
using TooksCms.ServiceLayer.Objects;
using TooksCms.Core.Bases;
using TooksCms.Core.Enums;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Interfaces.Repository;
using TooksCms.Core.Objects.Xml;
using TooksCms.Core.Reflection;
using System.Web;
using Microsoft.Practices.Unity;

namespace TooksCms.ServiceLayer.Bases
{
    public class ArticleEventArgs : EventArgs
    {
        public ArticleEventArgs()
            : base()
        {
        }
    }


    public abstract class ArticleBase : ModelBase
    {
        #region Delegates

        public delegate void ArticleEventHandler(object sender, ArticleEventArgs e);

        #endregion

        #region Events

        public event ArticleEventHandler OnSaveComplete;
        public event ArticleEventHandler OnBeforeContentSave;
        public event ArticleEventHandler OnBeforeSave;

        #endregion

        public ArticleBase()
        {
            OnBeforeSave += new ArticleEventHandler(Article_OnBeforeSave);
            OnBeforeContentSave += SaveImages;
        }

        public List<ImageProperty> Images { get; set; }

        public TitleTextBoxProperty Title { get; set; }

        public List<EditableDivProperty> EditableContent { get; set; }

        [XmlIgnore]
        public abstract string ArticleTypeName { get; }

        [XmlIgnore]
        public abstract string EditAction { get; }

        [XmlIgnore]
        public abstract string SaveAction { get; }

        [XmlAttribute("tkfDisplayType")]
        public DisplayType Display { get; set; }

        [XmlAttribute("siteId")]
        public int SiteId { get; set; }

        [XmlAttribute("categoryId")]
        public int CategoryId { get; set; }

        [XmlAttribute("state")]
        public ArticleState State { get; set; }

        [XmlAttribute("articleTypeId")]
        public int ArticleTypeId { get; set; }

        public DateTime Date { get; set; }

        public new ArticleBase Desrialize(string xml)
        {
            var xsr = new XmlSerializer(GetType());
            var sr = new StringReader(xml);
            var obj = xsr.Deserialize(sr);
            ((ArticleBase)obj).MarkOld();
            return (ArticleBase)obj;
        }

        public static CollectionBase<ArticleBase> GetList(int count, string type, IArticleRepository rep)
        {
            var articleBases = new CollectionBase<ArticleBase>();
            articleBases.AddRange(rep.FetchList(count, type).Select(article => ((ArticleBase)Reflector.CreateObject(article.ArticleType.Assembly, article.ArticleType.Class)).
                    Desrialize(article.Content.ToString())));
            articleBases.ForEach(ab => ab.MarkOld());
            return articleBases;
        }

        public static CollectionBase<ArticleBase> Search(string search, IArticleRepository rep)
        {
            search = search.ToLower();
            var articleBases = new CollectionBase<ArticleBase>();
            articleBases.AddRange(rep.Search(search).Select(article => ((ArticleBase)Reflector.CreateObject(article.ArticleType.Assembly, article.ArticleType.Class)).
                    Desrialize(article.Content.ToString())));
            articleBases.ForEach(ab => ab.MarkOld());
            return articleBases;
        }

        public void Article_OnBeforeSave(object sender, ArticleEventArgs e)
        {
            int count = 0;
            var tempList = EditableContent;
            tempList.ForEach(ec =>
            {
                if (ec.Deleted)
                {
                    EditableContent.Remove(ec);
                    if (Images != null)
                    {
                        Images.ForEach(i =>
                        {
                            if (i.Position.Contains(string.Format("{0}-", (count))))
                            {
                                i.Position = "gal";
                            }
                        });
                    }
                }
                count++;
            });
            EditableContent = tempList;
        }

        public void SaveImages(object sender, ArticleEventArgs e)
        {
            var aRep = DependencyResolver.Current.GetService<IArticleRepository>();
            if (Images != null)
            {
                try
                {
                    string newPath = "~/Uploads/Images/" + ArticleTypeName + "/" + Uid + "/";
                    newPath = HttpContext.Current.Request.MapPath(newPath);
                    string oldPath = "~/Uploads/Images/Temp/";
                    oldPath = HttpContext.Current.Request.MapPath(oldPath);
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }

                    List<ArticleImage> images = new List<ArticleImage>();

                    foreach (ImageProperty imageProperty in Images)
                    {
                        if (imageProperty.Id == 0)
                        {
                            File.Move(oldPath + imageProperty.Value, newPath + imageProperty.Value);
                            File.Move(oldPath + imageProperty.Thumbnail, newPath + imageProperty.Thumbnail);
                        }

                        images.Add(this.BuildImageInterface(imageProperty.Id,
                                                            imageProperty.Value,
                                                            imageProperty.Thumbnail,
                                                            imageProperty.Position,
                                                            imageProperty.Size));
                    }

                    Images = ImageProperty.BuildXmlProperty(aRep.UpdateAllImages(images)).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void Save(IArticleRepository rep)
        {
            if (OnBeforeSave != null) { OnBeforeSave(this, new ArticleEventArgs()); }

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
                        this.Id = rep.Insert(BuildInteface(false, rep));

                        if (OnBeforeContentSave != null) { OnBeforeContentSave(this, new ArticleEventArgs()); }

                        rep.InsertContent(BuildInteface(true, rep));
                    }
                    else if (!IsNew & IsDirty)
                    {
                        /* [Update] a existing, but changed object to be saved */
                        if (OnBeforeContentSave != null) { OnBeforeContentSave(this, new ArticleEventArgs()); }

                        rep.Update(BuildInteface(true, rep));
                    }
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
            if (OnSaveComplete != null) { OnSaveComplete(this, new ArticleEventArgs()); }
        }

        public static void Delete(int id, IArticleRepository rep)
        {
            try
            {
                var article = rep.Fetch(id);

                rep.Delete(id);

                string newPath = "~/Uploads/Images/" + (article.ArticleType.Name == "News" ? "NewsArticle" : article.ArticleType.Name) + "/" + article.ArticleUid + "/";
                newPath = HttpContext.Current.Request.MapPath(newPath);

                if (Directory.Exists(newPath))
                {
                    Directory.Delete(newPath, true);
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static ArticleBase Load(int id, IArticleRepository rep)
        {
            var article = rep.Fetch(id);
            var obj = ((ArticleBase)Reflector.CreateObject(article.ArticleType.Assembly, article.ArticleType.Class)).Desrialize(article.Content.ToString());
            obj.MarkOld();
            return obj;
        }

        public abstract override object GetJSONModel();

        internal abstract void CreateBulletin();

        internal abstract void UpdateBulletin();

        internal Article BuildInteface(bool parseContent, IArticleRepository rep)
        {
            var content = parseContent ? XElement.Parse(this.Serialize()) : XElement.Parse("<temp></temp>");
            return Article.CreateArticle(this._id, this._uid, this.ArticleTypeId, this.State, this.SiteId, content, this.CategoryId, this.Date, rep);
        }

        #region Image Methods / Properties

        public bool ImageExists(string position)
        {
            if (Images == null) { return false; }
            return Images.Any(i_ => i_.Position.ToLower() == position.ToLower());
        }

        public virtual bool IsLarge(string position)
        {
            if (Images == null) { return false; }
            return Images.Single(i_ => i_.Position.ToLower() == position.ToLower()).Size == "l";
        }

        public virtual string GetImageValue(string position)
        {
            if (Images == null) { return "gal"; }
            return Images.First(i_ => i_.Position.ToLower() == position.ToLower()).Value;
        }

        public virtual string GetImageLink(string position)
        {
            var link = Images.First(i_ => i_.Position.ToLower() == position.ToLower()).Value;
            return VirtualPathUtility.ToAbsolute("~/Uploads/Images/" + ArticleTypeName + "/" + Uid + "/" + link);
        }

        public virtual string GetImageClass(string position)
        {
            if (Images == null) { return ""; }
            var s = Images.First(i_ => i_.Position.ToLower() == position.ToLower()).Size;
            var sclass = string.Empty;
            switch (s)
            {
                case "s":
                    sclass = " small";
                    break;
                case "l":
                    sclass = " large";
                    break;
                case "x":
                    return "image x-large";
            }
            var p = position.Split('-');
            if (p[1] == "1")
            {
                return "image-left" + sclass;
            }
            return "image-right" + sclass;
        }

        public virtual string GetImageThumbnail(string position)
        {
            var thumbnail = Images.First(i_ => i_.Position.ToLower() == position.ToLower()).Thumbnail;
            return VirtualPathUtility.ToAbsolute("~/Uploads/Images/" + ArticleTypeName + "/" + Uid + "/" + thumbnail);
        }

        public virtual ImageProperty GetBulletinImage()
        {
            if (ImageExists("0-1"))
            {
                return Images.First(i_ => i_.Position == "0-1");
            }
            else if (ImageExists("0-2"))
            {
                return Images.First(i_ => i_.Position == "0-2");
            }
            return null;
        }

        #endregion

        internal ArticleImage BuildImageInterface(int imageId, string image, string thumbnail, string position, string size)
        {
            if (string.IsNullOrEmpty(size)) { size = "s"; }
            return TooksCms.ServiceLayer.Objects.ArticleImage.CreateArticleImage(imageId, this._id, image, thumbnail, position, size);
        }

        private CategoryInfo _categoryInfo;
        public CategoryInfo CategoryInfo
        {
            get
            {
                if (this._categoryInfo == null)
                {
                    this._categoryInfo = new Objects.CategoryInfo(LookupRepository.FetchCategoryInfo(this.CategoryId));
                }
                return _categoryInfo;
            }
        }

        public virtual string GetCategoryName()
        {
            return this.CategoryInfo.FullCategoryName;
        }

        private ILookupRepository _lookupRepository;
        [XmlIgnore]
        public virtual ILookupRepository LookupRepository
        {
            get { return _lookupRepository ?? (_lookupRepository = DependencyResolver.Current.GetService<ILookupRepository>()); }
            set { _lookupRepository = value; }
        }
    }
}
