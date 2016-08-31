using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Interfaces
{
    public interface IArticleComment : IInterfacingBase
    {
        int ArticleCommentId { get; }
        Guid ArticleCommentUid { get; }
        int ArticleId { get; }
        IUser User { get; }
        IGuest Guest { get; }
        string Title { get; }
        string Content { get; }
        DateTime Date { get; }
        int? ParentCommentId { get; }
    }
}
