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
    
    public partial class Category
    {
        public Category()
        {
            this.Rating2ArticleType2Category = new HashSet<Rating2ArticleType2Category>();
            this.Articles = new HashSet<Article>();
            this.Galleries = new HashSet<Gallery>();
        }
    
        public int CategoryId { get; set; }
        public System.Guid CategoryUid { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public Nullable<int> ParentCategoryId { get; set; }
        public string ImageName { get; set; }
    
        public virtual ICollection<Rating2ArticleType2Category> Rating2ArticleType2Category { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Gallery> Galleries { get; set; }
    }
}
