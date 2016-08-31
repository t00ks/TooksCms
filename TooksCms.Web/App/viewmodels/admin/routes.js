define('viewmodels/admin/routes', ['knockout', 'durandal/app', 'datacontext', 'moment', 'utils', 'editor', 'area'],
    function (ko, app, datacontext, moment, utils, editor, area) {

        var _model = ko.observable();

        function RouteVM(data) {
            this.staticRoute = ko.observable(data.staticRoute);
            this.area = ko.observable(data.area);
            this.action = ko.observable(data.action);
            this.id = ko.observable(data.id);
            this.description = ko.observable(data.description);
        }

        function ViewModel(routes, galleries, articleTypes) {
            var self = this;

            this.routes = ko.observableArray();

            for (var i = 0; i < routes.length; i++) {
                this.routes.push(new RouteVM(routes[i]));
            }

            this.galleries = galleries;
            this.articleTypes = articleTypes

            this.articles = ko.observableArray();

            this.selectedArticleType = ko.observable(undefined).extend({ rateLimit: 500 });;
            this.selectedArticleType.subscribe(function (newValue) {
                datacontext.lists.getArticles(newValue).done(function (data) {
                    self.articles.removeAll();
                    self.articles(data);
                })
            });

            this.remove = function (route) {
                datacontext.admin.routes.remove({
                    staticRoute: route.staticRoute(),
                    area: route.area(),
                    action: route.action(),
                    id: route.id(),
                    description: route.description(),
                }).done(function () {
                    self.routes.remove(route);
                });
            }

            this.addArticle = function () {
                var typeId = $('#dd-articleType').val();
                var id = $('#dd-article').val();
                var route = $('#tb-new-route-text').val();

                datacontext.admin.routes.addArticle(typeId, id, route).done(function (data) {
                    self.routes.push(new RouteVM(data));
                });
            }

            this.addGallery = function () {
                var id = $('#dd-gallery').val();
                var route = $('#tb-new-gallery-route-text').val();

                datacontext.admin.routes.addGallery(id, route).done(function (data) {
                    self.routes.push(new RouteVM(data));
                });
            }
        }

        return {
            activate: function (id) {

                var calls = [],
                    routes, galleries, articleTypes;

                calls.push(datacontext.admin.routes.load().done(function (data) {
                    routes = data;
                }));

                calls.push(datacontext.lists.get("galleries").done(function (data) {
                    galleries = data;
                }));

                calls.push(datacontext.lists.get("articletypes").done(function (data) {
                    articleTypes = data;
                }));

                $.when.apply($, calls).done(function () {
                    var vm = new ViewModel(routes, galleries, articleTypes);

                    _model(vm);
                })

                app.isAdmin(true);
            },
            compositionComplete: function () {

            },
            model: _model
        };
    });