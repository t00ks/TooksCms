using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Interfaces.Repository;
using TooksCms.ServiceLayer.Objects;

namespace TooksCms.ServiceLayer.Models
{
    public class TagModel : ModelBase
    {
        public TagModel() { }

        public TagModel(ITag data)
        {
            this.Id = data.TagId;
            this.Uid = data.TagUid;
            this.Name = data.Name;
        }

        public string Name { get; set; }

        public static CollectionBase<TagModel> ListTags()
        {
            var rep = DependencyResolver.Current.GetService<ILookupRepository>();
            return new CollectionBase<TagModel>(rep.FetchTags().Select(t => new TagModel(t)));
        }

        public static CollectionBase<TagModel> ListCommon()
        {
            var rep = DependencyResolver.Current.GetService<ILookupRepository>();
            return new CollectionBase<TagModel>(rep.FetchCommonTags().Select(t => new TagModel(t)));
        }

        public static CollectionBase<TagModel> ListCommon(int id, string type)
        {
            var rep = DependencyResolver.Current.GetService<ILookupRepository>();
            switch (type)
            {
                case "gallery":
                    return new CollectionBase<TagModel>(rep.FetchCommonTagsNotInGallery(id).Select(t => new TagModel(t)));
                default:
                    return new CollectionBase<TagModel>(rep.FetchCommonTagsNotInArticle(id).Select(t => new TagModel(t)));
            }
        }

        public static CollectionBase<TagModel> ListTags(int id, string type)
        {
            var rep = DependencyResolver.Current.GetService<ILookupRepository>();
            switch (type)
            {
                case "gallery":
                    return new CollectionBase<TagModel>(rep.FetchTagsForGallery(id).Select(t => new TagModel(t)));
                default:
                    return new CollectionBase<TagModel>(rep.FetchTagsForArticle(id).Select(t => new TagModel(t)));
            }
        }

        public static void AddTag(string name, int id, string type)
        {
            var rep = DependencyResolver.Current.GetService<ILookupRepository>();
            ITag tag;
            if (!rep.TagExists(name))
            {
                var model = new TagModel { Name = name };
                tag = rep.InsertTag(model.BuildInterface());
            }
            else
            {
                tag = rep.FetchTag(name);
            }
            switch (type)
            {
                case "article":
                    rep.InsertArticleTagLink(tag, id);
                    break;
            }
        }

        public static void RegisterTag(int id, int tagId, string type)
        {
            var rep = DependencyResolver.Current.GetService<ILookupRepository>();
            var tag = rep.FetchTag(tagId);
            switch (type)
            {
                case "article":
                    rep.InsertArticleTagLink(tag, id);
                    break;
                case "gallery":
                    rep.InsertGalleryTagLink(tag, id);
                    break;
            }
        }

        public static void UnRegisterTag(int id, int tagId, string type)
        {
            var rep = DependencyResolver.Current.GetService<ILookupRepository>();
            var tag = rep.FetchTag(tagId);
            switch (type)
            {
                case "article":
                    rep.RemoveArticleTagLink(tag, id);
                    break;
                case "gallery":
                    rep.RemoveGalleryTagLink(tag, id);
                    break;
            }
        }

        public Tag BuildInterface()
        {
            return Tag.CreateRating(this.Id, this.Uid, this.Name);
        }
    }
}
