define('viewmodels/admin/article', ['knockout', 'durandal/app', 'datacontext', 'moment', 'utils', 'editor', 'area', 'models/ko/articleeditor', 'tk/modal'],
    function (ko, app, datacontext, moment, utils, editor, area, EditorModel, modal) {
        var _model = ko.observable(),
            _deleteArticle;

        function ViewModel(cats, types) {
            var self = this;

            this.categories = cats;
            this.articleTypes = types;

            this.selectedType = ko.observable(undefined);
            this.selectedType.subscribe(function () {
                var type = $('#dd-articleType option:selected').text();
                datacontext.articles.getList(type).done(function (data) {
                    self.list(data);
                });
            });

            this.selectedCategory = ko.observable(undefined);

            this.article = ko.observable();
            this.list = ko.observableArray();

            this.edit = function (article) {
                datacontext.articles.getArticle(article.articleId).done(function (data) {
                    self.article(new EditorModel(data));

                    self.editing(true);
                });
            }

            this.remove = function (article) {
                _deleteArticle = article;

                modal.show({
                    type: 'inline',
                    src: '#confirmation-check'
                });
            }

            this.removeConfirm = function () {
                datacontext.articles.remove(_deleteArticle.articleId).done(function () {
                    app.success(_deleteArticle.title + " Removed");
                    self.list.remove(_deleteArticle);
                    _deleteArticle = null;
                }).fail(function () {
                    app.error("Remove Article Failed!")
                });
                modal.close();
            }

            this.cancelRemove = function () {
                modal.close();
                _deleteArticle = null;
            }

            this.cancel = function () {
                self.editing(false);
                self.article(undefined);
            }

            this.add = function () {
                if (self.selectedCategory() && self.selectedType()) {
                    datacontext.articles.add(self.selectedCategory(), self.selectedType()).done(function (data) {
                        self.article(new EditorModel(data));
                        self.editing(true);
                    });
                } else {
                    app.error("Category and Type Must Be Selected");
                }
            }

            this.editing = ko.observable(false);
        }

        return {
            activate: function (id) {
                var calls = [],
                    categories, articleTypes;

                calls.push(datacontext.lists.get("categories").done(function (data) {
                    categories = data;
                }));

                calls.push(datacontext.lists.get("articletypes").done(function (data) {
                    articleTypes = data;
                }));

                $.when.apply($, calls).done(function () {
                    var vm = new ViewModel(categories, articleTypes);

                    $.extend(vm, utils.formatter);

                    _model(vm);
                });

                app.isAdmin(true);
            },
            compositionComplete: function () {

            },
            model: _model
        };
    });