define('editor', ['knockout', 'durandal/app'], function (ko, app) {

    var _article = ko.observable(),
        _articleKo = ko.observable(),
        _gallery = ko.observable(),
        _mode = ko.observable('view'),
        _type = ko.observable();

    _mode.subscribe(function (newValue) {
        app.trigger('editor:modechange', newValue);
    });

    return {
        mode: _mode,
        gallery: _gallery,
        article: _article,
        articleKo: _articleKo,
        type: _type
    }

});