define('models/bulletin', function () {
    "use strict";

    var Bulletin = function (data) {
        this.uid = data ? data.uid : undefined;
        this.id = data ? data.id : undefined;
        this.title = data ? data.title.value : undefined;
        this.commentCount = data ? data.commentCount : undefined;
        this.categoryName = data ? data.categoryInfo.fullCategoryName : undefined;
        this.categoryImage = data ? data.categoryInfo.imageName : undefined;
        this.bulletinType = data ? data.bulletinType.toLowerCase() : undefined;
        this.date = data ? new Date(data.date) : undefined;
        this.link = data ? data.link.link : undefined;
    };

    var NewsBulletin = function (data) {
        Bulletin.call(this, data);

        this.articleId = data ? data.articleId : undefined;
        this.image = data ? data.image : undefined;
        this.content = data ? data.viewContent.value : undefined;
        this.contentCss = data ? data.viewContent.cssClass.toLowerCase() : undefined;
        this.contentType = data ? data.viewContent.type.toLowerCase() : undefined;
    };

    NewsBulletin.prototype = new Bulletin();
    NewsBulletin.prototype.constructor = NewsBulletin;

    var ReviewSummary = function (data) {
        this.text = data.text;
        this.rating = data.rating;
    };

    var ReviewBulletin = function (data) {
        NewsBulletin.call(this, data);

        this.summary = new ReviewSummary(data.summary);
    };

    ReviewBulletin.prototype = new NewsBulletin();
    ReviewBulletin.prototype.constructor = ReviewBulletin;

    var GalleryBulletin = function (data) {
        Bulletin.call(this, data);

        this.images = data.images;
        this.galleryId = data.galleryId;
    };

    GalleryBulletin.prototype = new Bulletin();
    GalleryBulletin.prototype.constructor = GalleryBulletin;

    return {
        newsBulletin: NewsBulletin,
        reviewBulletin: ReviewBulletin,
        galleryBulletin: GalleryBulletin
    };
});