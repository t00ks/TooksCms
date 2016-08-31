using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;

namespace TooksCms.ServiceLayer.Objects
{
    public class ArticleComment : InterfacingBase, IArticleComment
    {
        private ArticleComment() { }

        public ArticleComment(IArticleComment data)
            : base(data, typeof(IArticleComment))
        { }

        #region IArticleComment

        public int ArticleCommentId { get; private set; }

        public Guid ArticleCommentUid { get; private set; }

        public int ArticleId { get; private set; }

        public IUser User { get; private set; }

        public IGuest Guest { get; private set; }

        public string Title { get; private set; }

        public string Content { get; private set; }

        public DateTime Date { get; private set; }

        public int? ParentCommentId { get; private set; }

        #endregion

        public static ArticleComment CreateArticleComment(int id, Guid uid, int articleId, IUser user, IGuest guest, string title, string content, DateTime date, int? parentId)
        {
            return new ArticleComment
            {
                ArticleCommentId = id,
                ArticleCommentUid = uid,
                ArticleId = articleId,
                User = user,
                Guest = guest,
                Title = title,
                Content = content,
                Date = date,
                ParentCommentId = parentId
            };
        }
    }
}
