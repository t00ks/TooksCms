define('models/ko/galleryeditor', function () {
    "use strict";

    var ImageModel = function (data) {
        this.id = data.id;
        this.uid = data.uid;
        this.image = ko.observable(data.image);
        this.thumbnail = ko.observable(data.thumbnail);
        this.holding = data.holding;
    }

    var UploadedImageModel = function (data, holding) {
        this.id = 0;
        this.uid = "";
        this.image = ko.observable(data.imagePath);
        this.thumbnail = ko.observable(data.thumbnail);
        this.holding = holding;

        this.value = data.value;
        this.thumbValue = data.thumbValue;
    }

    var GalleryModel = function (data) {
        var self = this,
            holdingImage = "/Content/Images/grey.png";

        this.id = data.id;
        this.uid = data.uid;
        this.userId = data.userId;
        this.title = ko.observable(data.title);
        this.author = data.author;
        this.date = data.date;
        this.images = ko.observableArray();
        this.categoryName = data.categoryName;
        this.categoryId = data.categoryId;

        for (var i = 0; i < data.images.length; i++) {
            this.images.push(new ImageModel(data.images[i]));
        }

        this.addImage = function (dto) {
            self.images.push(new UploadedImageModel(dto, holdingImage));
        }



        this.parseModel = function () {
            var gallery = this;

            var o = {
                images: gallery.images().map(function (i) {
                    return {
                        id: i.id,
                        uid: i.uid,
                        thumbnail: i.thumbValue,
                        image: i.value,
                        isNew: i.value !== undefined
                    }
                }),
                title: gallery.title(),
                userId: gallery.userId,
                categoryId: gallery.categoryId,
                date: gallery.date,
                id: gallery.id,
                uid: gallery.uid
            }

            return o;
        }

    }

    return GalleryModel;
});