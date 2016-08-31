using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;

namespace TooksCms.ServiceLayer.Objects
{
    [Serializable]
    public class ArticleType : IArticleType
    {
        /// <summary>
        /// Paramertless constructor allows xml serialisation
        /// </summary>
        public ArticleType() { }

        /// <summary>
        /// Constructor taking in IArticleType DTO object
        /// </summary>
        /// <param name="data">Article Type DTO object</param>
        public ArticleType(IArticleType data)
        {
            ArticleTypeId = data.ArticleTypeId;
            ArticleTypeUid = data.ArticleTypeUid;
            Name = data.Name;
            Description = data.Description;
            Class = data.Class;
            Assembly = data.Assembly;
        }

        public int ArticleTypeId { get; set; }
        public Guid ArticleTypeUid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Class { get; set; }
        public string Assembly { get; set; }
        public string Action { get; set; }
    }
}
