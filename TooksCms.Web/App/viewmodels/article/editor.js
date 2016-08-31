define('viewmodels/article/editor', ['knockout', 'durandal/app', 'datacontext', 'moment', 'utils', 'models/ko/articleeditor', 'editor', 'area', 'controls', 'fileupload'],
    function (ko, app, datacontext, moment, utils, EditorModel, editor, area, controls, fileupload) {
        var _model = ko.observable();

        return {
            activate: function (id) {
                var vm = new EditorModel(editor.article());

                _model(vm);
                app.loadGadgets(area.articleEdit);
                editor.articleKo(vm);
                editor.mode('edit');
                editor.type('article');
            },
            compositionComplete: function () {
                controls.init(_model());
            },
            deactivate: function () {
                editor.mode('view');
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