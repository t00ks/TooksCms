define('viewmodels/gallery/list', ['knockout', 'durandal/app', 'datacontext', 'moment', 'utils', 'jquery', 'area', 'stats'],
    function (ko, app, datacontext, moment, utils, $, area, stats) {

        var _model = ko.observable(),
            _composed = true;

        function ViewModel(galleries) {
            var self = this;

            this.galleries = galleries;

            this.getGalleryUrl = function (gal) {
                return '/Gallery/View/' + gal.id;
            }
        }

        function _doBindings() {
            $("img.lazy").lazyload({ effect: "fadeIn" });
            setTimeout(function () { $(window).resize(); }, 150);
        }

        return {
            activate: function () {
                app.isLoading(true);
                _composed = false;

                datacontext.galleries.getList().done(function (data) {
                    var vm = new ViewModel(data);
                    $.extend(vm, utils.formatter);
                    _model(vm);

                    if (_composed) {
                        _doBindings();
                    }
                    app.isLoading(false);
                });
                app.isAdmin(false);
                app.loadGadgets(area.galleryList);
                stats.register(area.galleryList);
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