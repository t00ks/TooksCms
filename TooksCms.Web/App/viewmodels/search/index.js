define('viewmodels/search/index', ['knockout', 'durandal/app', 'datacontext', 'moment', 'utils', 'area', 'controls', 'stats'], function (ko, app, datacontext, moment, utils, area, controls, stats) {
    var _model = ko.observable();

    function ViewModel(results, term) {
        this.results = results;
        this.searchTerm = ko.observable(term);

        this.getArticleUrl = function (article) {
            return '/article/' + utils.getViewPath(article.typeName) + '/' + article.articleId;
        };

        this.getArticleImage = function (article) {
            if (article.hasImages) {
                return '/Uploads/Images/' + utils.getImageFolder(article.typeName) + '/' + article.articleUid + '/' + article.imageThumbnail
            } else {
                if (article.categoryImage == '') {
                    return '/Content/images/bulletin-backgrounds/dig-ec-placeholder.jpg';
                } else {
                    return '/Content/images/bulletin-backgrounds/' + article.categoryImage;
                }
            }
        };
    }

    return {
        activate: function (term) {
            datacontext.search.get(term).done(function (data) {
                var vm = new ViewModel(data, term);
                $.extend(vm, utils.formatter);

                _model(vm);
            });

            app.isAdmin(false);
            app.loadGadgets(area.search);
            stats.registerSearch(area.search, term);
        },
        compositionComplete: function () {

        },
        deactivate: function () {

        },
        model: _model
    };
});