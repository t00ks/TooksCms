define('models/rating', function () {
    "use strict";

    var Rating = function (data) {
        this.name = data.name;
        this.id = data.id;
        this.uid = data.uid;
        this.isDirty = data.isDirty;
        this.isNew = data.isNew;
        this.isDeleted = data.isDeleted;
    }

    var RatingLink = function (data) {
        var self = this;

        this.id = data.id;
        this.uid = data.uid;

        this.ratings = [];
        this.categoryName = data.categoryName;
        this.categoryId = data.categoryId;
        this.articleTypeName = data.articleTypeName;
        this.articleTypeId = data.articleTypeId;

        this.isDirty = data.isDirty;
        this.isNew = data.isNew;
        this.isDeleted = data.isDeleted;

        data.ratings.forEach(function (r) {
            self.ratings.push(new Rating(r));
        });
    }

    return {
        rating: Rating,
        ratingLink: RatingLink
    };
});