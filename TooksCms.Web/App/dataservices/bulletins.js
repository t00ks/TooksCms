define('dataservices/bulletins', ['jquery', 'tk/ajax', 'durandal/system'], function ($, ajax, system) {
    "use strict";

    return {
        loadLatest: function () {
            return $.Deferred(function (def) {
                ajax.get("/api/bulletin/").done(function (data) {
                    if (system.debug()) {
                        console.log(data);
                    }
                    def.resolveWith(this, [data]);
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        },
        loadPage: function (page) {
            return $.Deferred(function (def) {
                ajax.get("/api/bulletin/?page=" + page).done(function (data) {
                    if (system.debug()) {
                        console.log(data);
                    }
                    def.resolveWith(this, [data]);
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        }
    }
});