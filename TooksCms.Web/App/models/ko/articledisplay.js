define('models/ko/articledisplay', ['utils', 'knockout'],  function (utils, ko) {
    "use strict";
    function _getImageClass(image) {
        var s = image.size;
        var sclass = "";
        switch (s) {
            case "s":
                sclass = " small";
                break;
            case "l":
                sclass = " large";
                break;
            case "x":
                return "image x-large";
        }
        var p = image.position.split('-');
        if (p[1] == "1") {
            return "image-left" + sclass;
        }
        return "image-right" + sclass;
    }

    function DisplayModel(data) {
        var self = this;

        this.article = data;
        this.tags = ko.observableArray([]);

        this.imageExists = function (position) {
            return self.article.images.some(function (i) {
                return i.position === position;
            });
        }

        this.getImage = function (position) {
            var image = self.article.images.filter(function (i) {
                return i.position === position;
            })[0];

            if (!image) {
                return null;
            }

            image.getImageClass = _getImageClass;

            return image;
        }

        this.getVideoAttributes = function (item, index, id) {
            return {
                'class': item.cssClass,
                id: id + '_' + index,
                'data-url': utils.getVideoUrl(item.cssClass, item.value)
            };
        }

        this.getRatingClass = function (rating) {
            var cssClass = 'total';
            if (rating % 1 != 0 || rating > 9) {
                cssClass += " decimal";
            }
            return cssClass;
        }

        this.updateTags = function(tags) {
            for (var i = 0; i < tags.length; i++) {
                self.tags().push(tags[i]);
            }
            self.tags.notifySubscribers(self.tags());
        }

        this.isAuthenticated = ko.observable(false);

        this.authClass = ko.computed(function () {
            return self.isAuthenticated() ? 'authenticated' : '';
        });
    }

    return DisplayModel;
});