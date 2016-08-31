define('viewmodels/fileupload', ['durandal/app', 'area', 'fileupload'], function (app, area, fileupload) {
    var _model = ko.observable();

    return {
        activate: function (model) {
            _model(model);
        },
        preview: function () {
            fileupload.preview('files');
        },
        uploadfiles: function () {
            fileupload.upload(_model());
        }
    };
});