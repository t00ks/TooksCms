define('viewmodels/admin/gadgets', ['knockout', 'durandal/app', 'datacontext', 'moment', 'utils', 'editor', 'area'],
    function (ko, app, datacontext, moment, utils, editor, area) {

        var _model = ko.observable();

        function ViewModel(gadgets, gadgetList, roles, articleTypes) {
            var self = this;

            this.gadgets = ko.observableArray(gadgets);

            this.roles = roles;
            this.gadgetList = gadgetList;
            this.articleTypes = articleTypes;

            this.remove = function (gadget) {
                datacontext.admin.gadgets.remove({
                    gadgetId: gadget.gadgetId,
                    name: gadget.name,
                    description: gadget.description,
                    view: gadget.view,
                    defaultColumn: gadget.defaultColumn,
                    roleName: gadget.roleName,
                    areaType: gadget.areaType,
                }).done(function () {
                    self.gadgets.remove(gadget);
                });
            }

            this.add = function () {
                var gadgetId = $('#dd-gadgets').val();
                var areaType = $('#dd-areaType').val();
                var roleId = $('#dd-roles').val();
                if (gadgetId > 0 && areaType > 0 && roleId > 0) {
                    datacontext.admin.gadgets.add(gadgetId, areaType, roleId).done(function (data) {
                        app.success("Gadget Link Added");
                        self.gadgets.push(data);
                    }).fail(function () {
                        app.error("Gadget link already exists");
                    });
                } else {
                    app.error("All values must be selected");
                }
            }
        }

        return {
            activate: function (id) {
                if (toastr) { toastr = require('toastr'); }

                var calls = [],
                    gadgets, roles, gadgetList, articleTypes;

                calls.push(datacontext.admin.gadgets.load().done(function (data) {
                    gadgets = data;
                }));

                calls.push(datacontext.lists.get("gadgets").done(function (data) {
                    gadgetList = data;
                }));

                calls.push(datacontext.lists.get("roles").done(function (data) {
                    roles = data;
                }));

                calls.push(datacontext.lists.get("areatypes").done(function (data) {
                    articleTypes = data;
                }));

                $.when.apply($, calls).done(function () {
                    var vm = new ViewModel(gadgets, gadgetList, roles, articleTypes);

                    _model(vm);
                });

                app.isAdmin(true);

            },
            compositionComplete: function () {

            },
            model: _model
        };
    });