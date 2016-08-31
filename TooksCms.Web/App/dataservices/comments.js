define('dataservices/comments', ['jquery', 'tk/ajax', 'durandal/system'], function ($, ajax, system) {
    "use strict";

    return {
        loadComments: function (type, id) {
            return $.Deferred(function (def) {
                ajax.get("/api/comment/" + type + "/" + id).done(function (data) {
                    if (system.debug()) {
                        console.log(data);
                    }
                    def.resolve(data);
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        },
        saveComment: function (comment) {
            return $.Deferred(function (def) {
                ajax.put("/api/comment/", comment).done(function (data) {
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