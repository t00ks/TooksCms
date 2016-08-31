using System;
using System.Collections.Generic;
using TooksCms.Core.Interfaces;

namespace TooksCms.Core.Interfaces.Repository
{
    public interface IArticleRepository
    {
        bool Exists(int id);
        void Delete(int id);
        IArticle Fetch(int id);
        IEnumerable<IArticleImage> FetchArticleImages(int parentId);
        IArticleInfo FetchArticleInfo(int articleId);
        IEnumerable<IArticleInfo> FetchArticleInfos();
        IEnumerable<IArticleInfo> FetchArticleInfos(int articleTypeId);
        IEnumerable<IArticleInfo> FetchArticleInfos(string typeName);
        IEnumerable<IArticleInfo> FetchArticleInfos(int? count);
        IEnumerable<IArticleComment> FetchComments();
        IEnumerable<IArticleComment> FetchChildComments(int parentId);
        IArticleComment FetchComment(int id);
        IEnumerable<IArticleComment> FetchComments(int articleId);
        IEnumerable<IArticle> FetchList(int count, DateTime from);
        IEnumerable<IArticle> FetchList(int count, int skip);
        IEnumerable<IArticle> FetchList(int count, string type);
        IArticleType FetchType(int id);
        IArticleType FetchType(string name);
        bool ImageExists(int id);
        int Insert(IArticle data);
        IEnumerable<IArticleImage> InsertAllImages(IEnumerable<IArticleImage> data);
        IArticleComment InsertComment(IArticleComment data);
        void DeleteComment(int commentId);
        IArticle InsertContent(IArticle data);
        IEnumerable<IArticle> Search(string search);
        bool TypeExists(int id);
        bool TypeExists(string name);
        int Update(IArticle data);
        IEnumerable<IArticleImage> UpdateAllImages(IEnumerable<IArticleImage> data);
        IArticleComment UpdateComment(IArticleComment data);
        IEnumerable<IArticleInfo> SearchArticleInfos(string term);
    }
}
