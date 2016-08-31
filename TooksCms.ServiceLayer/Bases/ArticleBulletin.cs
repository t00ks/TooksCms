using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml.Serialization;
using TooksCms.Core.Interfaces.Repository;
using TooksCms.Core.Objects.Xml;
using System.Xml.Linq;
using TooksCms.Core.Interfaces;
namespace TooksCms.ServiceLayer.Bases
{
    public abstract class ArticleBulletin : BulletinBase
    {
        public ArticleBulletin() { }
        public ArticleBulletin(IBulletin data) : base(data) { }

        [XmlAttribute("articleId")]
        public int ArticleId { get; set; }

        public ImageProperty Image { get; set; }

        public StandardTextProperty ViewContent { get; set; }

        #region Image Methods

        public bool ImageExists()
        {
            return Image != null;
        }

        public abstract string GetImageLink();

        public virtual string GetImageClass()
        {
            var sclass = string.Empty;
            switch (Image.Size)
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
            var p = Image.Position.Split('-');
            if (p[1] == "1")
            {
                return "image-left" + sclass;
            }
            return "image-right" + sclass;
        }

        public abstract string GetImageThumbnail();

        public abstract string GetImage();

        internal override Objects.Bulletin BuildInteface(bool parseContent)
        {
            var content = parseContent ? XElement.Parse(this.Serialize()) : XElement.Parse("<temp></temp>");
            return TooksCms.ServiceLayer.Objects.Bulletin.CreateBulletin(this._id, this._uid, this.ArticleId,null, this.BulletinType, this.SiteId, content, this.Date);
        }

        #endregion

        public override void LoadCategoryInfo(ILookupRepository lookupRepository)
        {
            var articleRepository = DependencyResolver.Current.GetService<IArticleRepository>();

            var article = articleRepository.FetchArticleInfo(this.ArticleId);
            this.CategoryInfo = new Objects.CategoryInfo(lookupRepository.FetchCategoryInfo(article.CategoryId));
        }
    }
}
