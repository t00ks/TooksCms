define('models/lookup', function () {
    "use strict";

    var Country = function (data) {
        this.name = data.name;
        this.isO3166 = data.isO3166;
        this.imageName = data.imageName;
        this.isNew = data.isNew;
        this.isDirty = data.isDirty;
        this.isDeleted = data.isDeleted;
        this.id = data.id;
        this.uid = data.uid;
    };

    var Category = function (data) {
        this.categoryName = data.categoryName;
        this.categoryDescription = data.categoryDescription;
        this.parentCategoryId = data.parentCategoryId;
        this.isNew = data.isNew;
        this.isDirty = data.isDirty;
        this.isDeleted = data.isDeleted;
        this.id = data.id;
        this.uid = data.uid;
    }

    return {
        country: Country,
        category: Category
    };
});