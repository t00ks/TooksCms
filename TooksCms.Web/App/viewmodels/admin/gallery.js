define('viewmodels/admin/gallery', ['knockout', 'durandal/app', 'datacontext', 'moment', 'utils', 'editor', 'area', 'models/ko/galleryeditor', 'fileupload', 'controls'],
    function (ko, app, datacontext, moment, utils, editor, area, EditorModel, fileupload, controls) {
        var _model = ko.observable();

        function ViewModel(cats, gals) {
            var self = this;

            this.categories = cats;
            this.galleries = ko.observable(gals);

            this.selectedCategory = ko.observable();

            this.gallery = ko.observable();

            this.add = function () {
                if (self.selectedCategory()) {
                    datacontext.galleries.add(self.selectedCategory()).done(function (data) {
                        self.gallery(new EditorModel(data));
                        self.editing(true);
                    });
                } else {
                    app.error("Category Must Be Selected");
                }
            }

            this.list = function () {
                datacontext.galleries.getList().done(function (data) {
                    self.galleries(data);

                    self.editing(false);

                    setTimeout(function () {
                        $("img.lazy").lazyload({ effect: "fadeIn" });
                        $(window).resize();
                        controls.lightbox('a.gallery-image-a');
                    }, 100);
                })
            }

            this.edit = function (gal) {
                datacontext.galleries.getGallery(gal.id).done(function (data) {
                    self.gallery(new EditorModel(data));

                    self.editing(true);

                    setTimeout(function () {
                        $("img.lazy").lazyload({ effect: "fadeIn" });
                        $(window).resize();
                        controls.lightbox('a.gallery-image-a');
                    }, 100);
                });
            }

            this.editing = ko.observable(false);
        }

        return {
            activate: function (id) {
                var calls = [],
                    categories, galleries;

                calls.push(datacontext.lists.get("categories").done(function (data) {
                    categories = data;
                }));

                calls.push(datacontext.galleries.getList().done(function (data) {
                    galleries = data;
                }));

                $.when.apply($, calls).done(function () {
                    var vm = new ViewModel(categories, galleries);

                    $.extend(vm, utils.formatter);

                    _model(vm);
                });

                app.isAdmin(true);
            },
            compositionComplete: function () {
                function _do() {
                    setTimeout(function () {
                        $("img.lazy").lazyload({ effect: "fadeIn" });
                        $(window).resize();
                    }, 50);
                }

                app.off('fileupload:uploadcomplete', _do)
                app.on('fileupload:uploadcomplete', _do)

                _do();
            },
            model: _model,
            save: function () {
                datacontext.galleries.save(_model().gallery().parseModel()).done(function () {
                    app.success("Saved");
                    _model().list();
                });
            },
            upload: function () {
                fileupload.show();
            }
        };
    });