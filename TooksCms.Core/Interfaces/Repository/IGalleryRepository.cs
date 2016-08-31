using System;
using System.Collections.Generic;
using TooksCms.Core.Interfaces;

namespace TooksCms.Core.Interfaces.Repository
{
    public interface IGalleryRepository
    {
        bool CheckGalleryExists(int galleryId);
        bool CheckGalleryImageExists(int galleryImageId);
        IEnumerable<IGallery> FetchGalleries();
        IGallery FetchGallery(int galleryId);
        IGalleryImage FetchGalleryImage(int galleryImageId);
        IEnumerable<IGalleryImage> FetchGalleryImages();
        IEnumerable<IGalleryImage> FetchGalleryImages(int galleryId);
        IEnumerable<IGalleryInfo> FetchGalleryInfos();
        IEnumerable<IGalleryInfo> FetchGalleryInfos(int? count);
        IGallery InsertGallery(IGallery data);
        IGalleryImage InsertGalleryImage(IGalleryImage data);
        IGallery UpdateGallery(IGallery data);
        IGalleryImage UpdateGalleryImage(IGalleryImage data);
    }
}
