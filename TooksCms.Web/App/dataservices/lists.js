define('dataservices/lists', ['jquery', 'tk/ajax', 'durandal/system'], function ($, ajax, system) {
    "use strict";

    return {
        get: function (type) {
            return $.Deferred(function (def) {
                ajax.get("/api/admin/list/" + type).done(function (data) {
                    if (system.debug()) {
                        console.log(data);
                    }
                    def.resolveWith(this, [data]);
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        },
        getArticles: function (typeId) {
            return $.Deferred(function (def) {
                ajax.get("/api/admin/list/articles/" + typeId).done(function (data) {
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