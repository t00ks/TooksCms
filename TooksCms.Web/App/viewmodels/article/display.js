define('viewmodels/article/display', ['knockout', 'durandal/app', 'datacontext', 'moment', 'utils', 'models/ko/articledisplay', 'editor', 'area', 'controls', 'stats'],
    function (ko, app, datacontext, moment, utils, DisplayModel, editor, area, controls, stats) {
        var _model = ko.observable(),
            _composed = false;

        function _doBindings() {
            utils.syntaxHighlight();
            utils.embededVideo();
            controls.lightbox('a.view-image');
        }

        return {
            activate: function (id) {
                app.isLoading(true);
                _composed = false;

                datacontext.articles.getArticle(id).done(function (data) {
                    var vm = new DisplayModel(data);

                    app.pageTitle(data.title);
                    editor.article(data);

                    $.extend(vm, utils.formatter);
                    _model(vm);

                    datacontext.tags.getArticleTags(id).done(function (data) {
                        _model().updateTags(data);
                    });

                    _model().isAuthenticated(app.isAuthenticated());

                    if (_composed) {
                        _doBindings();
                    }
                    app.isLoading(false);

                    app.loadGadgets(area.article);
                });

                app.isAdmin(false);
                app.checkAuthenticated();
                stats.register(area.article, id);
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