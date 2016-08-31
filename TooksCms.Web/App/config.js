define('config', ['jquery', 'modernizr'], function ($, modernizr) {
    "use strict";

    var _appRoot = "/",
        _apiRoot = '/',
        _getAppRoot = function (path) {
            if (path === undefined) {
                return _appRoot;
            }
            _appRoot = path;
            if (path.charAt(path.length - 1) !== '/') {
                _appRoot = _appRoot + '/';
            }
        },
        _isTest = true;

    return {
        appRoot: _getAppRoot,
        history: function () { return modernizr.history && true; },
        apiRoot: _apiRoot,
        isTest: _isTest
    }
});