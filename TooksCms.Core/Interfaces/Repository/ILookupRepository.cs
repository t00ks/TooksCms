using System.Collections.Generic;

namespace TooksCms.Core.Interfaces.Repository
{
    public interface ILookupRepository
    {
        /// <summary>
        /// Fetch a category from the DAL.
        /// </summary>
        /// <param name="id">ID of the category</param>
        /// <returns>A Category DAL object</returns>
        /// <exception cref="TooksCms.Core.Exceptions.DataNotFoundException">Category does not exist</exception>
        ICategory FetchCategory(int id);

        ICategoryInfo FetchCategoryInfo(int id);
        IEnumerable<ICategoryInfo> FetchCategoryInfos();
        IEnumerable<ICategory> FetchCategories();
        IEnumerable<ICategory> FetchParentCategories();
        IEnumerable<ICategory> FetchChildCategories();
        ICategory InsertCategory(ICategory data);
        ICategory UpdateCategory(ICategory data);
        bool CategoryExists(int id);

        /// <summary>
        /// Fetch a country from the DAL.
        /// </summary>
        /// <param name="id">ID of the country</param>
        /// <returns>A Country DAL object</returns>
        /// <exception cref="TooksCms.Core.Exceptions.DataNotFoundException">Country does not exist</exception>
        ICountry FetchCountry(int id);

        IEnumerable<ICountry> FetchCountries();
        ICountry InsertCountry(ICountry data);
        ICountry UpdateCountry(ICountry data);
        bool CountryExists(int id);
        IEnumerable<ITag> FetchTags();
        IEnumerable<ITag> FetchCommonTags();
        IEnumerable<ITag> FetchCommonTagsNotInArticle(int articleId);
        IEnumerable<ITag> FetchCommonTagsNotInGallery(int galleryId);
        IEnumerable<ITag> FetchTagsForArticle(int articleId);
        IEnumerable<ITag> FetchTagsForGallery(int galleryId);
        IEnumerable<IRankedTag> FetchRankedTags(int count);
        ITag FetchTag(string name);
        ITag FetchTag(int id);
        ITag InsertTag(ITag tag);
        void InsertArticleTagLink(ITag tag, int articleId);
        void InsertGalleryTagLink(ITag tag, int galleryId);
        void RemoveArticleTagLink(ITag tag, int articleId);
        void RemoveGalleryTagLink(ITag tag, int galleryId);
        bool TagExists(string name);
    }
}
