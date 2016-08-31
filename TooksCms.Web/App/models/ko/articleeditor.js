define('models/ko/articleeditor', ['utils', 'knockout', 'controls'], function (utils, ko, controls) {
    "use strict";

    function _getImageStyle(image) {
        if (_isLarge(image)) {
            return "width:450px;"
        }
        return "width:165px;";
    };

    function _isLarge(image) {
        return image.size() === 'l';
    }

    function ContentModel(data) {
        this.value = ko.observable(data.value),
        this.type = ko.observable(data.type);
        this.cssClass = ko.observable(data.cssClass);
    }

    var RatingModel = function (data) {
        this.text = ko.observable(data.text);
        this.rating = ko.observable(data.rating);
        this.name = ko.observable(data.name);
    };

    var ImageModel = function (data) {
        this.id = data.id;
        this.imagePath = ko.observable(data.imagePath);
        this.position = ko.observable(data.position);
        this.size = ko.observable(data.size);
        this.thumbnail = ko.observable(data.thumbnail);
        this.value = ko.observable(data.value);
        this.thumbValue = data.thumbValue;
    }

    function EditorModel(data) {
        var self = this;

        this.id = data.id;
        this.type = data.type;
        this.uid = data.uid;
        this.title = ko.observable(data.title);
        this.content = ko.observableArray();
        this.images = ko.observableArray();
        this.categoryId = data.categoryId;
        this.state = data.state;
        this.siteId = data.siteId;
        this.articleTypeId = data.articleTypeId;
        this.date = ko.observable(data.date);

        this.categoryName = data.categoryName;
        this.categoryId = data.categoryId;

        if (data.ratings) {
            this.ratings = ko.observableArray();
            for (var i = 0; i < data.ratings.length; i++) {
                this.ratings.push(new RatingModel(data.ratings[i]));
            }

            this.summary = ko.observable(new RatingModel(data.summary));
        }

        data.content.forEach(function (c) {
            self.content.push(new ContentModel(c));
        });

        data.images.forEach(function (i) {
            self.images.push(new ImageModel(i));
        });

        this.imageExists = function (index, side) {
            var position = index + '-' + side;
            return self.images().some(function (i) {
                return i.position === position;
            });
        };

        this.isLarge = function (index, side) {
            var position = index + '-' + side;
            return self.images().some(function (i) {
                if (i.position === position) {
                    return i.size === 'l';
                }
                return false;
            });
        };

        this.getContainerStyle = function (index, side) {
            if (self.imageExists(index, side) && self.isLarge(index, side)) {
                return "height:337px;"
            }
            return "";
        };

        this.getImageValue = function (position) {
            if (self.images().length == 0) { return 'gal'; }
            for (var i = 0; i < self.images().length; i++) {
                if (self.images()[i].position().toLowerCase() == position.toLowerCase()) {
                    return self.images()[i].value;
                }
            }
            return 'gal';
        };

        this.getEditorStyle = function (index) {
            if (self.imageExists(index + '-1') && self.isLarge(index + '-1') ||
                self.imageExists(index + '-2') && self.isLarge(index + '-2')) {
                return 'min-height: 337px;';
            }
            return '';
        };

        this.getCodeTypeText = function (brush) {
            switch (brush) {
                case "brush: cpp":
                    return "C++";
                case "brush: css":
                    return "CSS";
                case "brush: java":
                    return "Java";
                case "brush: js":
                    return "JavaScript";
                case "brush: php":
                    return "PHP";
                case "brush: text":
                    return "Plain Text";
                case "brush: py":
                    return "Python";
                case "brush: sql":
                    return "SQL";
                case "brush: vb":
                    return "VB";
                case "brush: xml":
                    return "XML/HTML";
                default:
                    return "C#";
            }
        };

        this.getVideoTypeText = function (video) {
            switch (video) {
                case "video vimeo":
                    return "Vimeo";
                default:
                    return "YouTube";
            }
        }

        this.getImage = function (index, side) {
            var position = index + '-' + side;

            var image = self.images().filter(function (i) {
                return i.position() === position;
            })[0];

            if (!image) {
                return null;
            }

            image.getImageStyle = _getImageStyle;

            image.isLarge = _isLarge;

            return image;
        }

        this.getRatingClass = function (rating) {
            var cssClass = 'total';
            if (rating % 1 != 0 || rating > 9) {
                cssClass += " decimal";
            }
            return cssClass;
        }

        this.addVideo = function () {
            var count = self.content().length;
            self.content.push(new ContentModel({
                value: '',
                type: 'embededvideo',
                cssClass: 'video youtube'
            }));

            setTimeout(function () {
                controls.embededVideo.initNew(count);
            }, 20);
        }

        this.addSingleImage = function () {
            var count = self.content().length;
            self.content.push(new ContentModel({
                value: '',
                type: 'singleimage',
                cssClass: 'single-image'
            }));

            setTimeout(function () {
                controls.singleImage.initNew(count);
            }, 20);
        }

        this.addSnippet = function () {
            var count = self.content().length;
            self.content.push(new ContentModel({
                value: '',
                type: 'codesnippet',
                cssClass: 'brush: c#'
            }));

            setTimeout(function () {
                controls.codeSnippet.initNew(count);
            }, 20);
        }

        this.addContent = function () {
            var count = self.content().length;
            self.content.push(new ContentModel({
                value: '',
                type: 'editablediv',
                cssClass: 'rich-content'
            }));

            setTimeout(function () {
                controls.editor.init();
                controls.richEditor.initNew(count);
            }, 20);
        }

        this.removeItem = function (position) {
            var images = self.images();
            for (var i in images) {
                if (images[i].position().indexOf(position + '-') > -1) {
                    images[i].position('gal')
                }
            }
            for (var j = position; j < self.content().length; j++) {
                for (var i in images) {
                    if (images[i].position().indexOf(j + '-') > -1) {
                        images[i].position((j - 1) + images[i].position().substring(1));
                    }
                }
            }
            self.content.remove(self.content()[position]);
        }

        this.addImage = function (dto) {
            self.images.push(new ImageModel(dto));
        }

        this.parseModel = function () {
            var article = this;

            var o = {
                images: article.images().map(function (i) {
                    return {
                        id: i.id,
                        position: i.position(),
                        size: i.size(),
                        thumbnail: i.thumbValue,
                        value: i.value()
                    }
                }),
                title: {
                    type: 'tkfTitleTextBox',
                    cssClass: 'tkf-input-title-text',
                    deleted: false,
                    value: article.title()
                },
                editableContent: ko.toJS(article.content),
                categoryId: article.categoryId,
                state: article.state,
                articleTypeId: article.articleTypeId,
                date: article.date(),
                Id: article.id,
                Uid: article.uid,
                SiteId: article.siteId
            }

            if (article.ratings && article.ratings()) {
                o.ratings = article.ratings().map(function (r) {
                    return {
                        name: r.name(),
                        rating: r.rating(),
                        text: r.text(),
                        type: 'Rating'
                    };
                });
                o.ratings.push({
                    name: article.summary().name(),
                    rating: article.summary().rating(),
                    text: article.summary().text(),
                    type: 'Summary'
                });
            }

            return o;
        }
    }

    return EditorModel;
});