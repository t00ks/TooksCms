define('viewmodels/admin/lookup', ['knockout', 'durandal/app', 'datacontext', 'moment', 'utils', 'editor', 'area', 'toastr'],
    function (ko, app, datacontext, moment, utils, editor, area, toastr) {
        var _model = ko.observable();

        function CuntVM(cunt, isNew) {
            var self = this;

            this.name = ko.observable(isNew ? '' : cunt.name);
            this.isO3166 = ko.observable(isNew ? '' : cunt.isO3166);
            this.imageName = ko.observable(isNew ? '' : cunt.imageName);
            this.isNew = ko.observable(isNew ? true : cunt.isNew);
            this.isDirty = ko.observable(isNew ? true : cunt.isDirty);
            this.isDeleted = ko.observable(isNew ? false : cunt.isDeleted);

            this.id = isNew ? -1 : cunt.id;
            this.uid = ko.observable(cunt.uid);

            this.onItemChange = function () {
                this.IsDirty(true);
            };

            this.remove = function () {
                self.isDeleted(true);
            }

            this.name.subscribe(this.onItemChange, this);
            this.isO3166.subscribe(this.onItemChange, this);
            this.imageName.subscribe(this.onItemChange, this);
        }

        function CatVM(cat, isNew) {
            var self = this;

            this.categoryName = ko.observable(isNew ? '' : cat.categoryName);
            this.categoryDescription = ko.observable(isNew ? '' : cat.categoryDescription);
            this.parentCategoryId = ko.observable(isNew ? undefined : cat.parentCategoryId ? cat.parentCategoryId.toString() : undefined);
            this.isNew = ko.observable(isNew ? true : cat.isNew);
            this.isDirty = ko.observable(isNew ? true : cat.isDirty);
            this.isDeleted = ko.observable(isNew ? false : cat.isDeleted);

            this.id = isNew ? -1 : cat.id;
            this.uid = ko.observable(cat.uid);

            this.onItemChange = function () {
                self.isDirty(true);
            };

            this.remove = function () {
                self.isDeleted(true);
            }

            this.categoryDescription.subscribe(this.onItemChange, this);
            this.categoryName.subscribe(this.onItemChange, this);
            this.parentCategoryId.subscribe(this.onItemChange, this);
        }

        function ViewModel(cats, cunts, flags, parentCats) {
            var self = this;

            this.category = ko.observableArray();
            this.country = ko.observableArray();

            for (var i = 0; i < cats.length; i++) {
                this.category.push(new CatVM(cats[i]));
            }

            for (var i = 0; i < cunts.length; i++) {
                this.country.push(new CuntVM(cunts[i]));
            }

            this.parentCategories = parentCats;
            this.flags = flags

            this.getClass = function (data) {
                if (data.isNew()) {
                    return ' new';
                }
                if (data.isDirty()) {
                    return ' old';
                }
                return '';
            };

            this.toggle = function (type) {
                return function () {
                    switch (type) {
                        case 'cats':

                            $('#categories').show();
                            $('#countries').hide();

                            break;

                        case 'cunts':

                            $('#countries').show();
                            $('#categories').hide();

                            break;
                    }
                }
            }

            this.addCategory = function () {
                this.category.splice(0, 0, new CatVM({ Uid: "NEWIDCA" + this.category().length }, true));
            }
            this.saveCategory = function () {
                var jsCats = self.category().map(function (c) {
                    return {
                        id: c.id,
                        uid: c.uid(),
                        categoryName: c.categoryName(),
                        categoryDescription: c.categoryDescription(),
                        parentCategoryId: c.parentCategoryId(),
                        isNew: c.isNew(),
                        isDirty: c.isDirty(),
                        isDeleted: c.isDeleted()
                    };
                });
                datacontext.admin.lookup.updateCategories(jsCats).done(function () {
                    toastr.success('Categories Saved');

                    self.category().forEach(function (c) {
                        c.isNew(false);
                        c.isDeleted(false);
                        c.isDirty(false);
                    });
                });
            }
            this.addCountry = function () {
                this.country.splice(0, 0, new CuntVM({ Uid: "NEWIDCO" + this.country().length }, true));
            }
            this.saveCountry = function () {
                var jsCunts = self.country().map(function (c) {
                    return {
                        name: c.name(),
                        isO3166: c.isO3166(),
                        imageName: c.imageName(),
                        isNew: c.isNew(),
                        isDirty: c.isDirty(),
                        isDeleted: c.isDeleted()
                    };
                });
                datacontext.admin.lookup.updateCountries(jsCunts).done(function () {
                    toastr.success('Categories Saved');

                    self.country().forEach(function (c) {
                        c.isNew(false);
                        c.isDeleted(false);
                        c.isDirty(false);
                    });
                });
            }
        }

        return {
            activate: function (id) {

                var categories, countries, flags, parentcategories, calls = [];

                calls.push(datacontext.lists.get("parentcategories").done(function (data) {
                    parentcategories = data;
                }));

                calls.push(datacontext.lists.get("flags").done(function (data) {
                    flags = data;
                }));

                calls.push(datacontext.admin.lookup.loadCategories().done(function (data) {
                    categories = data;
                }));

                calls.push(datacontext.admin.lookup.loadCountries().done(function (data) {
                    countries = data;
                }));

                $.when.apply($, calls).done(function () {
                    var vm = new ViewModel(categories, countries, flags, parentcategories);
                    _model(vm);
                });

                app.isAdmin(true);
            },
            compositionComplete: function () {

            },
            model: _model
        };
    });