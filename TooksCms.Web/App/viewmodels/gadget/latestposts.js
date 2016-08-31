define('viewmodels/gadget/latestposts', ['knockout', 'durandal/app', 'plugins/router', 'datacontext', 'moment', 'utils'], function (ko, app, router, datacontext, moment, utils) {
    var _model = ko.observable();

    function ViewModel(data) {
        this.articles = data,
        this.goToArticle =  function (article) {
            router.navigate('/article/' + utils.getViewPath(article.typeName) + '/' + article.articleId);
        }
        this.getImagePath = function (article) {
            if (article.hasImages) {
                return '/Uploads/Images/' + utils.getImageFolder(article.typeName) + '/' + article.articleUid + '/' + article.imageThumbnail
            } else {
                if (!article.categoryImage) {
                    return '/Content/images/bulletin-backgrounds/dig-ec-placeholder.jpg';
                } else {
                    return '/Content/images/bulletin-backgrounds/' + article.categoryImage;
                }
            }
        }
    }

    return {
        activate: function () {
            datacontext.articles.getLatestInfo().done(function (data) {
                var vm = new ViewModel(data);
                $.extend(vm, utils.formatter);

                _model(vm);
            });
        },
        model: _model
    }
})