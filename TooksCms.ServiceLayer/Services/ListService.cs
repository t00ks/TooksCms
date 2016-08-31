using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TooksCms.Core.Enums;
using TooksCms.Core.Interfaces.Repository;
using TooksCms.ServiceLayer.Models;
using TooksCms.ServiceLayer.Objects;
using TooksCms.ServiceLayer.Objects.Lookup;

namespace TooksCms.ServiceLayer.Services
{
    public static class ListService
    {
        public static List<SelectListItem> GetGadgets(IConfigRepository rep)
        {
            var gadgetList = new List<SelectListItem>();
            var list = rep.FetchGadgets().ToList();
            list.ForEach(l => gadgetList.Add(new SelectListItem
            {
                Text = l.Name,
                Value = l.GadgetId.ToString()
            }));
            return gadgetList;
        }

        public static List<SelectListItem> GetRoles(ISecurityRepository rep)
        {
            var roleList = new List<SelectListItem>();
            var list = rep.FetchRoles().ToList();
            list.ForEach(l => roleList.Add(new SelectListItem
            {
                Text = l.RoleName,
                Value = l.RoleId.ToString()
            }));
            return roleList;
        }

        public static List<SelectListItem> GetAreaTypes()
        {
            var areaTypeList = new List<SelectListItem>();
            var list = Enum.GetNames(typeof(AreaType)).ToList();
            list.ForEach(l => areaTypeList.Add(new SelectListItem
            {
                Text = l,
                Value = ((int)Enum.Parse(typeof(AreaType), l)).ToString()
            }));
            return areaTypeList;
        }

        public static List<SelectListItem> GetGalleries(IGalleryRepository rep)
        {
            var galleryList = new List<SelectListItem>();
            var galleries = rep.FetchGalleries().Select(g => new Gallery(g)).ToList();
            galleries.ForEach(g => galleryList.Add(new SelectListItem
            {
                Text = g.GalleryId.ToString() + "-" + g.Title,
                Value = g.GalleryId.ToString()
            }));
            return galleryList;
        }

        public static List<SelectListItem> GetArticles(int articleTypeId, IArticleRepository rep)
        {
            var articleList = new List<SelectListItem>();
            var articleInfos = rep.FetchArticleInfos(articleTypeId).Select(ai => new ArticleInfo(ai)).ToList();
            articleInfos.ForEach(ai => articleList.Add(new SelectListItem
            {
                Text = ai.ArticleId.ToString() + "-" + ai.Title,
                Value = ai.ArticleId.ToString()
            }));
            return articleList;
        }

        public static List<SelectListItem> GetArticleTypes(int? selected, IConfigRepository rep)
        {
            var articleTypeList = new List<SelectListItem>();
            var articleTypes = rep.FetchArticleTypes().Select(at_ => new ArticleType(at_)).ToList();
            articleTypes.ForEach(at => articleTypeList.Add(new SelectListItem
            {
                Text = at.Name,
                Value = at.ArticleTypeId.ToString(),
                Selected = selected.HasValue &&
                    (at.ArticleTypeId == selected.Value)
            }));
            return articleTypeList;
        }

        public static List<SelectListItem> GetParentCategories(int? selected, ILookupRepository rep)
        {
            var categoryList = new List<SelectListItem>();
            var categories = rep.FetchParentCategories().Select(c_ => new Category(c_)).ToList();
            categories.ForEach(c => categoryList.Add(new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.CategoryId.ToString(),
                Selected = selected.HasValue && (c.CategoryId == selected.Value)
            }));
            return categoryList;
        }

        public static List<SelectListItem> GetCategories(int? selected, ILookupRepository repository)
        {
            var categoryList = new List<SelectListItem>();
            var categories = repository.FetchCategoryInfos().Select(c_ => new CategoryInfo(c_)).ToList();
            categories.ForEach(c => categoryList.Add(new SelectListItem
            {
                Text = c.FullCategoryName,
                Value = c.CategoryId.ToString(),
                Selected = selected.HasValue && (c.CategoryId == selected.Value)
            }));
            return categoryList;
        }

        public static List<ImageListItem> GetFlags(string selected)
        {
            var flagList = new List<ImageListItem>();
            flagList.Add(new ImageListItem());
            var path = HttpContext.Current.Server.MapPath("~/Content/images/flags");
            var files = new List<string>(Directory.GetFiles(path));
            var flags = new List<string>();
            files.ForEach(f => flags.Add(f.Replace(path + "\\", "")));
            flags.ForEach(f => flagList.Add(new ImageListItem
            {
                Text = f,
                Value = f,
                Selected = f == selected,
                BackgroundImage = "/Content/images/flags/" + f
            }));
            return flagList;
        }

        public static List<SelectListItem> GetPhoneTypes(PhoneType? type)
        {
            var phoneTypes = new List<SelectListItem>();
            phoneTypes.Add(GetFirstSelectListItem(""));
            phoneTypes.Add(new SelectListItem
            {
                Text = PhoneType.Home.ToString(),
                Value = ((int)PhoneType.Home).ToString(),
                Selected = type.HasValue && type.Value == PhoneType.Home
            });
            phoneTypes.Add(new SelectListItem
            {
                Text = PhoneType.Mobile.ToString(),
                Value = ((int)PhoneType.Mobile).ToString(),
                Selected = type.HasValue && type.Value == PhoneType.Mobile
            });
            phoneTypes.Add(new SelectListItem
            {
                Text = PhoneType.Work.ToString(),
                Value = ((int)PhoneType.Work).ToString(),
                Selected = type.HasValue && type.Value == PhoneType.Work
            });
            return phoneTypes;
        }

        public static List<SelectListItem> GetCountries(int? selected, ILookupRepository repository)
        {
            var countryList = new List<SelectListItem>();
            var countries = repository.FetchCountries().Select(c_ => new Country(c_)).ToList();
            countries.ForEach(c => countryList.Add(new SelectListItem
            {
                Text = c.Name,
                Value = c.CountryId.ToString(),
                Selected = selected.HasValue && (c.CountryId == selected.Value)
            }));
            return countryList;
        }

        private static SelectListItem GetFirstSelectListItem(string s)
        {
            return new SelectListItem
            {
                Text = String.Format("-- select {0} --", s),
                Value = "-1"
            };
        }
    }
}
