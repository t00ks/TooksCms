using System.Reflection;
using TooksCms.Core.Attributes;
namespace TooksCms.Core.Enums
{
    public enum AreaType
    {
        [JSONDescription("a")]
        Article = 1,
        ArticleEdit = 2,
        [JSONDescription("h")]
        Home = 3,
        ArticleAdmin = 4,
        [JSONDescription("ab")]
        About = 5,
        [JSONDescription("c")]
        Contact = 6,
        [JSONDescription("l")]
        ArticleList = 7,
        ContactAdmin = 8,
        RatingAdmin = 9,
        RouteAdmin = 10,
        GalleryEdit = 11,
        [JSONDescription("g")]
        GalleryView = 12,
        GadgetAdmin = 13,
        LookupAdmin = 14,
        Search = 15,
        Statistics = 16,
        [JSONDescription("l")]
        GalleryList = 17
    }

    public static class AreaTypeExtentions
    {
        public static string GetJSONDescription(this AreaType area)
        {
            //Using reflection to get the field info
            FieldInfo info = area.GetType().GetField(area.ToString());

            //Get the Description Attributes
            JSONDescriptionAttribute[] attributes = (JSONDescriptionAttribute[])info.GetCustomAttributes(typeof(JSONDescriptionAttribute), false);
            
            //Only capture the description attribute if it is a concrete result (i.e. 1 entry)
            if (attributes.Length == 1)
            {
                return attributes[0].Description;
            }
            else //Use the value for display if not concrete result
            {
                throw (new CustomAttributeFormatException("Missing JSON Description"));
            }

        }
    }
}