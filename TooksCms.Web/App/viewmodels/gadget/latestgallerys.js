define('viewmodels/gadget/latestgallerys', ['knockout', 'durandal/app', 'plugins/router', 'datacontext', 'moment', 'utils'], function (ko, app, router, datacontext, moment, utils) {
    var _model = ko.observable();

    function ViewModel(data) {
        this.gallerys = data,
        this.goToGallery = function (gallery) {
            router.navigate('/Gallery/View/' + gallery.galleryId);
        }
        this.getImagePath = function (gallery) {
            return '/Uploads/Images/Galleries/' + gallery.galleryUid + '/' + gallery.imageThumbnail;
        }
    }

    return {
        activate: function () {
            datacontext.galleries.getLatestInfo().done(function (data) {
                var vm = new ViewModel(data);
                $.extend(vm, utils.formatter);

                _model(vm);
            });
        },
        model: _model
    }
})