define('models/article', function () {
    "use strict";
    var EditableContent = function (data) {
        this.value = data.value,
        this.type = data.type.toLowerCase();
        this.cssClass = data.cssClass;
    }

    var NewsArticle = function (data) {
        var self = this;

        this.uid = data ? data.uid : undefined;
        this.id = data ? data.id : undefined;
        this.title = data ? data.title.value : undefined;
        this.type = data ? data.type : undefined;

        this.state = data ? data.state : undefined;
        this.siteId = data ? data.siteId : undefined;
        this.articleTypeId = data ? data.articleTypeId : undefined;

        this.categoryName = data ? data.categoryInfo.fullCategoryName : undefined;
        this.categoryId = data ? data.categoryInfo.categoryId : undefined;

        this.date = data ? new Date(data.date) : undefined;
        this.images = data ? data.images : undefined;

        this.content = [];
        if (data) {
            data.editableContent.forEach(function (c) {
                self.content.push(new EditableContent(c));
            });
        }
    };

    var RatingItem = function (data) {
        this.text = data.text;
        this.rating = data.rating;
        this.name = data.name;
    };

    var ReviewArticle = function (data) {
        NewsArticle.call(this, data);

        var self = this;

        this.ratings = [];
        this.summary = null;

        if (data) {
            data.ratings.forEach(function (r) {
                if (r.type === "Rating") {
                    self.ratings.push(new RatingItem(r));
                } else {
                    self.summary = new RatingItem(r)
                }
            });

        }
    };

    ReviewArticle.prototype = new NewsArticle();
    ReviewArticle.prototype.constructor = ReviewArticle;

    var ArticleInfo = function (data) {
        this.articleId = data.articleId;
        this.articleUid = data.articleUid;
        this.status = data.status;
        this.categoryId = data.categoryId;
        this.categoryName = data.categoryName;
        this.categoryImage = data.categoryImage;
        this.date = new Date(data.date);
        this.typeName = data.typeName;
        this.articleTypeId = data.articleTypeId;
        this.version = data.version;
        this.title = data.title;
        this.hasImages = data.hasImages;
        this.imageThumbnail = data.imageThumbnail;
    }

    return {
        newsArticle: NewsArticle,
        reviewArticle: ReviewArticle,
        articleInfo: ArticleInfo
    };
});