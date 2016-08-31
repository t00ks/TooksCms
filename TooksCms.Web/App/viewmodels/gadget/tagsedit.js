define('viewmodels/gadget/tagsedit', ['knockout', 'durandal/app', 'plugins/router', 'datacontext', 'editor', 'utils'], function (ko, app, router, datacontext, editor, utils) {
    var _model = ko.observable();

    function ViewModel(data) {
        var self = this;

        this.common = ko.observable(data.common);
        this.included = ko.observable(data.included);

        this.name = ko.observable();

        this.add = function () {
            if (this.name()) {
                datacontext.admin.tags.addNew(self.name(), {
                    id: editor.article().id,
                    type: editor.type()
                }).done(function (data) {
                    self.common(data.common);
                    self.included(data.included);
                });
            } else {
                app.error('You Must Enter a Name');
            }
        }

        this.remove = function (tag) {
            datacontext.admin.tags.remove(tag.id, {
                id: editor.article().id,
                type: editor.type()
            }).done(function (data) {
                self.common(data.common);
                self.included(data.included);
            });
        }

        this.register = function (tag) {
            datacontext.admin.tags.register(tag.id, {
                id: editor.article().id,
                type: editor.type()
            }).done(function (data) {
                self.common(data.common);
                self.included(data.included);
            });
        }
    }

    return {
        activate: function () {
            datacontext.admin.tags.load(editor.article().id, editor.type()).done(function (data) {
                var vm = new ViewModel(data);
                _model(vm);
            });
        },
        model: _model
    }
})