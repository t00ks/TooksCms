define('models/gallery', function () {
    "use strict";
    var Image = function (data) {
        this.id = data.id,
        this.uid = data.uid,
        this.image = data.image,
        this.thumbnail = data.thumbnail;
        this.holding = data.holding;
    }

    var Gallery = function (data) {
        this.id = data.id;
        this.uid = data.uid;
        this.title = data.title;
        this.author = data.author;
        this.userId = data.userId;
        this.date = new Date(data.date);
        this.images = [];
        this.categoryName = data.categoryInfo.fullCategoryName;
        this.categoryId = data.categoryInfo.categoryId;

        for (var i = 0; i < data.images.length; i++) {
            this.images.push(new Image(data.images[i]));
        }
    }

    var GalleryInfo = function (data) {
        this.galleryId = data.galleryId;
        this.galleryUid = data.galleryUid;
        this.title = data.title;
        this.categoryId = data.categoryId;
        this.categoryName = data.categoryName;
        this.createdDate = data.createdDate;
        this.imageThumbnail = data.imageThumbnail;
    }

    var GalleryListItem = function (data) {
        this.id = data.id;
        this.title = data.title;
        this.imageCount = data.imageCount;
        this.createdDate = data.createdDate;
        this.usersName = data.usersName;
        this.thumbnail = data.thumbnail;
    }

    return {
        gallery: Gallery,
        galleryInfo: GalleryInfo,
        galleryListItem: GalleryListItem
    };
});