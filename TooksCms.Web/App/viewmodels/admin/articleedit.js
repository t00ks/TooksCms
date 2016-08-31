define('viewmodels/admin/articleedit', ['knockout', 'durandal/app', 'datacontext', 'moment', 'utils', 'models/ko/articleeditor', 'editor', 'area', 'controls', 'fileupload'],
    function (ko, app, datacontext, moment, utils, EditorModel, editor, area, controls, fileupload) {
        var _model = ko.observable();

        return {
            viewUrl: 'views/article/editor.html',
            activate: function (vm) {
                _model(vm);
            },
            compositionComplete: function () {
                controls.init(_model());
            },
            deactivate: function () {

            },
            model: _model,
            save: function () {
                datacontext.articles.save(_model().type, _model().parseModel()).done(function () {

                    app.success("Saved");
                });
            },
            upload: function () {
                fileupload.show();
            }
        };
    });