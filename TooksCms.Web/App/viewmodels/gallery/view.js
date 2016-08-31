define('viewmodels/gallery/view', ['knockout', 'durandal/app', 'datacontext', 'moment', 'utils', 'jquery', 'area', 'controls', 'stats'],
    function (ko, app, datacontext, moment, utils, $, area, controls, stats) {

        var _model = ko.observable(),
            _composed = true;

        function ViewModel(gallery) {
            var self = this;

            this.gallery = gallery;
            this.tags = ko.observableArray();

            this.updateTags = function (tags) {
                for (var i = 0; i < tags.length; i++) {
                    self.tags().push(tags[i]);
                }
                self.tags.notifySubscribers(self.tags());
            }
        }

        function _doBindings() {
            $("img.lazy").lazyload({ effect: "fadeIn" });
            controls.lightbox('a.gallery-image-a');
            setTimeout(function () { $(window).resize(); }, 150);
        }

        return {
            activate: function (id) {
                app.isLoading(true);
                _composed = false;

                datacontext.galleries.getGallery(id).done(function (data) {
                    var vm = new ViewModel(data);
                    app.pageTitle(data.title);
                    $.extend(vm, utils.formatter);
                    _model(vm);

                    if (_composed) {
                        _doBindings();
                    }
                    app.isLoading(false);

                    datacontext.tags.getGalleryTags(id).done(function (data) {
                        _model().updateTags(data);
                    });
                });

                app.isAdmin(false);
                app.loadGadgets(area.galleryView);
                stats.register(area.galleryView, id);
            },
            compositionComplete: function () {
                if (!app.isLoading()) {
                    _doBindings();
                }
                _composed = true;
            },
            model: _model
        };
    });