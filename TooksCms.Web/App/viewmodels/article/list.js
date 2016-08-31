define('viewmodels/article/list', ['knockout', 'durandal/app', 'datacontext', 'moment', 'utils', 'jquery', 'area', 'stats'],
    function (ko, app, datacontext, moment, utils, $, area, stats) {

        var _model = ko.observable();

        function ViewModel(articles) {
            var self = this;

            this.articles = articles;

            this.getArticleUrl = function (article) {
                return '/article/' + utils.getViewPath(article.typeName) + '/' + article.articleId;
            };

            this.getArticleImage = function (article) {
                if (article.hasImages) {
                    return '/Uploads/Images/' + utils.getImageFolder(article.typeName) + '/' + article.articleUid + '/' + article.imageThumbnail
                } else {
                    if (!article.categoryImage) {
                        return '/Content/images/bulletin-backgrounds/dig-ec-placeholder.jpg';
                    } else {
                        return '/Content/images/bulletin-backgrounds/' + article.categoryImage;
                    }
                }
            };
        }

        return {
            activate: function (type) {
                datacontext.articles.getList(type).done(function (data) {
                    var vm = new ViewModel(data);
                    
                    switch (type.toLowerCase()) {
                        case 'news':
                            app.pageTitle('News');
                            break;
                        case 'review':
                            app.pageTitle('Reviews');
                            break;
                    }

                    $.extend(vm, utils.formatter);
                    _model(vm);
                });

                app.isAdmin(false);
                app.loadGadgets(area.articleList);
                stats.register(area.articleList);
            },
            model: _model
        };
    });