//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TooksCms.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ArticleType
    {
        public ArticleType()
        {
            this.Articles = new HashSet<Article>();
            this.Rating2ArticleType2Category = new HashSet<Rating2ArticleType2Category>();
        }
    
        public int ArticleTypeId { get; set; }
        public System.Guid ArticleTypeUid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Class { get; set; }
        public string Assembly { get; set; }
        public string Action { get; set; }
    
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Rating2ArticleType2Category> Rating2ArticleType2Category { get; set; }
    }
}