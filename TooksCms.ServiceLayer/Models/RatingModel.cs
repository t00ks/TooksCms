using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;
using System.ComponentModel.DataAnnotations;
using TooksCms.ServiceLayer.Objects;
using TooksCms.Core.Interfaces.Repository;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace TooksCms.ServiceLayer.Models
{
    public class RatingModel : ModelBase
    {
        public RatingModel() { }

        public RatingModel(IRating data)
        {
            this.Id = data.RatingId;
            this.Uid = data.RatingUid;
            this.Name = data.Name;
        }

        #region CRUD

        public void Save()
        {
            var cRep = DependencyResolver.Current.GetService<IConfigRepository>();
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
                        cRep.InsertRating(BuildInterface());
                    }
                    else if (!IsNew & IsDirty)
                    {
                        /* [Update] a existing, but changed object to be saved */
                        cRep.UpdateRating(BuildInterface());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Rating BuildInterface()
        {
            return Rating.CreateRating(this.Id, this.Uid, this.Name);
        }

        #endregion

        #region Properties

        [Required]
        [Display(Name = "Rating Name", Prompt = "Please enter a name:")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        #endregion

        #region StaticMemebers

        public static List<RatingModel> GetList()
        {
            var cRep = DependencyResolver.Current.GetService<IConfigRepository>();
            return cRep.FetchRatings().Select(r_ => new RatingModel(r_)).ToList();
        }

        #endregion

        public static RatingModel Create(string name)
        {
            return new RatingModel
                       {
                           Name = name
                       };
        }
    }
}
