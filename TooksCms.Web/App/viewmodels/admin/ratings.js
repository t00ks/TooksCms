define('viewmodels/admin/ratings', ['knockout', 'durandal/app', 'datacontext', 'moment', 'utils', 'editor', 'area'],
    function (ko, app, datacontext, moment, utils, editor, area) {
        var _model = ko.observable();

        function _initDroppable() {
            setTimeout(function () {
                $('.droppable').sortable({
                    connectWith: '.droppable',
                    handle: '.handle',
                    cursor: 'move',
                    helper: 'clone',
                    placeholder: 'gadget-placeHolder',
                    forcePlaceholderSize: true,
                    revert: 300,
                    opacity: 0.4,
                    dropOnEmpty: true
                });
            }, 25);
        }


        function RatingLinkJS(koModel) {
            var self = this;

            this.id = koModel.id;
            this.uid = koModel.uid;

            this.ratings = [];
            this.categoryName = koModel.categoryName();
            this.categoryId = koModel.categoryId();
            this.articleTypeName = koModel.articleTypeName();
            this.articleTypeId = koModel.articleTypeId();

            this.isDirty = koModel.isDirty();
            this.isNew = koModel.isNew();
            this.isDeleted = koModel.isDeleted();

            koModel.ratings().forEach(function (r) {
                self.ratings.push({
                    id: r.id,
                    uid: r.uid,
                    name: r.name(),
                    isDirty: r.isDirty(),
                    isNew: r.isNew(),
                    isDeleted: r.isDeleted()
                });
            });
        }

        function RatingVM(data) {
            this.id = data.id;
            this.uid = data.uid;

            this.name = ko.observable(data.name);
            this.isDirty = ko.observable(data.isDirty);
            this.isNew = ko.observable(data.isNew);
            this.isDeleted = ko.observable(data.isDeleted);
        }

        function RatingLinkVM(data) {
            var self = this;

            this.id = data.id;
            this.uid = data.uid;

            this.ratings = ko.observableArray();
            this.categoryName = ko.observable(data.categoryName);
            this.categoryId = ko.observable(data.categoryId);
            this.articleTypeName = ko.observable(data.articleTypeName);
            this.articleTypeId = ko.observable(data.articleTypeId);

            this.isDirty = ko.observable(data.isDirty);
            this.isNew = ko.observable(data.isNew);
            this.isDeleted = ko.observable(data.isDeleted);

            data.ratings.forEach(function (r) {
                self.ratings.push(new RatingVM(r));
            });

            this.save = function () {
                var c = $('#ratings-' + self.uid);
                c.children('div').each(function (index, elem) {
                    var id = $(elem).attr('data-id');
                    var uid = $(elem).attr('data-uid');
                    var name = $(elem).attr('data-name');
                    if (!self.ratings().some(function (r) {
                        return r.uid === uid;
                    })) {
                        self.ratings.push(new RatingVM({
                            id: id,
                            uid: uid,
                            name: name,
                            isDirty: false,
                            isNew: false,
                            isDeleted: false
                        }));
                        $(elem).remove();
                    }
                });
                datacontext.admin.ratingLinks.update(new RatingLinkJS(self)).done(function () {
                    app.success("Saved");
                });
            }
        }

        function ViewModel(ratings, ratingLinks, categories, articleTypes) {
            var self = this;

            this.ratings = ko.observableArray();
            this.ratingLinks = ko.observableArray();

            ratings.forEach(function (r) {
                self.ratings.push(new RatingVM(r));
            });

            ratingLinks.forEach(function (r) {
                self.ratingLinks.push(new RatingLinkVM(r));
            });

            this.categories = categories;
            this.articleTypes = articleTypes;

            this.newLink = function () {
                var articleTypeId = $('#dd-articleType').val();
                var categoryId = $('#dd-category').val();

                datacontext.admin.ratingLinks.add({ categoryId: categoryId, articleTypeId: articleTypeId }).done(function (data) {
                    self.ratingLinks.push(new RatingLinkVM(data));
                    _initDroppable();
                });
            };

            this.addRating = function () {
                var name = $('#tb-new-rating-text').val();

                datacontext.admin.ratings.add(name).done(function (data) {
                    self.ratings.push(new RatingVM(data));
                    _initDroppable();
                });
            };
        }

        return {
            activate: function (id) {
                var calls = [],
                    ratings, ratingLinks, categories, articleTypes;

                calls.push(datacontext.admin.ratings.load().done(function (data) {
                    ratings = data;
                }));

                calls.push(datacontext.admin.ratingLinks.load().done(function (data) {
                    ratingLinks = data;
                }));

                calls.push(datacontext.lists.get("categories").done(function (data) {
                    categories = data;
                }));

                calls.push(datacontext.lists.get("articletypes").done(function (data) {
                    articleTypes = data;
                }));

                $.when.apply($, calls).done(function () {
                    var vm = new ViewModel(ratings, ratingLinks, categories, articleTypes);

                    _model(vm);
                });

                app.isAdmin(true);
            },
            compositionComplete: function () {

                _initDroppable();

            },
            model: _model
        };
    });