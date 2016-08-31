using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;
using TooksCms.ServiceLayer.Objects;
using TooksCms.ServiceLayer.Objects.Account;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Interfaces.Repository;
using System.Web.Mvc;
using TooksCms.ServiceLayer.Utilities;
using TooksCms.Core.Mail;
using System.Net.Mail;

namespace TooksCms.ServiceLayer.Models
{
    public class CommentModel : ModelBase
    {
        public CommentModel() { }

        public CommentModel(IArticleComment data)
        {
            this.Id = data.ArticleCommentId;
            this.Uid = data.ArticleCommentUid;
            this.ArticleId = data.ArticleId;
            this.Name = data.User == null ? data.Guest.Name : data.User.ScreenName;
            this.Website = data.User == null ? data.Guest.Website : ""; //TODO Users website
            this.Title = data.Title;
            this.Comment = data.Content;
            this.AutherId = data.User == null ? data.Guest.GuestId : data.User.UserId;
            this.IsGuest = data.User == null;
            this.Date = data.Date;
            this.ParentId = data.ParentCommentId;
            this.Children = new List<CommentModel>();
        }

        #region Public Properties

        public int ArticleId { get; set; }

        [Required]
        [Display(Name = "Name", Prompt = "Please enter a name.")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "Website")]
        [DataType(DataType.Text)]
        public string Website { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.Text)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Subject", Prompt = "Please give your comment a subject.")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Comment", Prompt = "Please enter your comment.")]
        [DataType(DataType.Text)]
        public string Comment { get; set; }

        public int AutherId { get; set; }

        public bool IsGuest { get; set; }

        public DateTime Date { get; set; }

        public int? ParentId { get; set; }

        public List<CommentModel> Children { get; set; }

        #endregion

        #region CRUD

        public void Save(User user = null, Guest guest = null)
        {
            var aRep = DependencyResolver.Current.GetService<IArticleRepository>();
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
                        this.Id = aRep.InsertComment(BuildInterface(user, guest)).ArticleCommentId;
                        this.Name = user == null ? guest.Name : user.ScreenName;
                        this.Website = user == null ? guest.Website : "";
                        this.AutherId = user == null ? guest.GuestId : user.UserId;

                    }
                    else if (!IsNew & IsDirty)
                    {
                        /* [Update] a existing, but changed object to be saved */
                        aRep.UpdateComment(BuildInterface(user, guest));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Notifier.SendNotification(this);
            SendReplyNotification();
        }

        public void SendReplyNotification()
        {
            if (this.ParentId.HasValue)
            {
                var aRep = DependencyResolver.Current.GetService<IArticleRepository>();

                var comment = aRep.FetchComment(this.ParentId.Value);

                var body = Notifier.GetEmailTemplate("TooksCms.ServiceLayer.EmailTemplates.ReplyCommentMail.txt");

                var subject = "Somebody Replied to Your Comment";

                body = body.Replace("##COMMENT##", this.Comment).Replace("##TITLE##", comment.Title).Replace("##NAME##", this.Name).Replace("##ID##", this.ArticleId.ToString()); ;

                string recipientName = string.Empty, recipientEmail = string.Empty;

                if (comment.Guest != null)
                {
                    recipientName = comment.Guest.Name;
                    recipientEmail = comment.Guest.Email;
                }
                else if (comment.User != null)
                {
                    recipientName = comment.User.ScreenName;
                    recipientEmail = comment.User.ContactInfo.EmailAddresses.First().Address;
                }

                if (!string.IsNullOrWhiteSpace(recipientEmail) && !string.IsNullOrWhiteSpace(recipientName))
                {
                    Notifier.SendReplyNotification(subject, body, new MailAddress(recipientEmail, recipientName));
                }
            }
        }

        public ArticleComment BuildInterface(User user = null, Guest guest = null)
        {
            return ArticleComment.CreateArticleComment(this.Id, this.Uid, this.ArticleId, user, guest, this.Title, this.Comment, this.Date, this.ParentId);
        }

        #endregion

        #region Factory

        public static List<CommentModel> GetList(int articleId, IArticleRepository rep)
        {
            var list = rep.FetchComments(articleId).Select(c_ => new CommentModel(c_)).ToList();
            list.ForEach(c =>
            {
                c.Children = rep.FetchChildComments(c.Id).Select(c_ => new CommentModel(c_)).ToList();
            });
            return list;
        }

        public static List<CommentModel> GetAll(IArticleRepository rep)
        {
            var list = rep.FetchComments().Select(c_ => new CommentModel(c_)).ToList();
            return list;
        }

        public static CommentModel Create(User user)
        {
            return new CommentModel
            {
                AutherId = user.UserId,
                IsGuest = false
            };
        }

        public static CommentModel Create(Guest guest = null)
        {
            var model = new CommentModel
            {
                IsGuest = true
            };
            if (guest != null)
            {
                model.Name = guest.Name;
            }
            return model;
        }

        #endregion
    }
}
