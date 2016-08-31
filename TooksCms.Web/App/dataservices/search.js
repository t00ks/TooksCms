define('dataservices/search', ['jquery', 'tk/ajax', 'durandal/system'], function ($, ajax, system) {
    "use strict";

    return {
        get: function (term) {
            return $.Deferred(function (def) {
                ajax.get("/api/search/" + term).done(function (data) {
                    def.resolve(data);
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        }
    }
});