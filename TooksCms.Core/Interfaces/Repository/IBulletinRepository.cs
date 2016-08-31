using System;
using System.Collections.Generic;
using TooksCms.Core.Interfaces;

namespace TooksCms.Core.Interfaces.Repository
{
    public interface IBulletinRepository
    {
        bool Exists(int id);
        IBulletin Fetch(int id);
        IEnumerable<IBulletin> FetchList();
        IEnumerable<IBulletin> FetchList(int count);
        IEnumerable<IBulletin> FetchList(int count, DateTime from);
        IEnumerable<IBulletin> FetchList(int count, int skip);
        IBulletin FetchOnArticleId(int articleId);
        IBulletin FetchOnGalleryId(int galleryId);
        IBulletinType FetchType(int id);
        IBulletinType FetchType(string name);
        int Insert(IBulletin data);
        IBulletin InsertContent(IBulletin data);
        bool TypeExists(int id);
        bool TypeExists(string name);
        IBulletin Update(IBulletin data);
    }
}
