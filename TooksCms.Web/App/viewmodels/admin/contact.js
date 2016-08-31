define('viewmodels/admin/contact', ['knockout', 'durandal/app', 'datacontext', 'moment', 'utils', 'editor', 'area', 'toastr'],
    function (ko, app, datacontext, moment, utils, editor, area, toastr) {

        if (!toastr) { toastr = require('toastr'); }

        var _model = ko.observable();

        function _update(data) {
            datacontext.admin.contact.update(data).done(function () {
                toastr.success('Saved');
            });
        }

        function ContactVM(data) {
            var self = this;

            this.id = ko.observable(data.id);
            this.title = ko.observable(data.title);
            this.name = ko.observable(data.name);
            this.email = ko.observable(data.email);
            this.comment = ko.observable(data.comment);
            this.read = ko.observable(data.read);
            this.public = ko.observable(data.public);

            this.public.subscribe(function (newValue) {
                _update({
                    checked: newValue,
                    type: 'public',
                    id: self.id()
                });
            });

            this.read.subscribe(function (newValue) {
                _update({
                    checked: newValue,
                    type: 'read',
                    id: self.id()
                });
            });
        }

        function ViewModel(data) {
            this.contactForms = ko.observableArray();

            for (var i = 0; i < data.length; i++) {
                this.contactForms.push(new ContactVM(data[i]));
            }
        }

        return {
            activate: function (id) {
                datacontext.admin.contact.load().done(function (data) {
                    var vm = new ViewModel(data);

                    _model(vm);
                });

                app.isAdmin(true);
            },
            compositionComplete: function () {

            },
            model: _model
        };
    });