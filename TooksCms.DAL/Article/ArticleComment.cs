using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;

namespace TooksCms.DAL
{
    public partial class ArticleComment : IArticleComment
    {
        public static ArticleComment CreateArticleComment(IArticleComment data)
        {
            return new ArticleComment
            {
                ArticleCommentUid = data.ArticleCommentUid,
                ArticleId = data.ArticleId,
                UserId = data.User == null ? null : (int?)data.User.UserId,
                GuestId = data.Guest == null ? null : (int?)data.Guest.GuestId,
                Title = data.Title,
                Content = data.Content,
                Date = data.Date,
                ParentCommentId = data.ParentCommentId
            };
        }

        public void Update(IArticleComment data)
        {
            this.Title = data.Title;
            this.Content = data.Content;
        }
        
        IUser IArticleComment.User
        {
            get
            {
                return this.User;
            }
        }

        IGuest IArticleComment.Guest
        {
            get
            {
                return this.Guest;
            }
        }
    }
}
