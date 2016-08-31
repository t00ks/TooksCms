define('dataservices/tags', ['jquery', 'tk/ajax', 'durandal/system'], function ($, ajax, system) {
    "use strict";

    return {
        getTags: function (type, id) {
            return $.Deferred(function (def) {
                ajax.get("/api/tags/" + type + '/' + id).done(function (data) {
                    if (system.debug()) {
                        console.log(data);
                    }
                    def.resolve(data);
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        },
        getRankedTags: function () {
            return $.Deferred(function (def) {
                ajax.get("/api/gadget/tags").done(function (data) {
                    if (system.debug()) {
                        console.log(data);
                    }
                    def.resolve(data);
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        }
    }
});