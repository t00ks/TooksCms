define('dataservices/articles', ['jquery', 'tk/ajax', 'durandal/system'], function ($, ajax, system) {
    "use strict";

    return {
        loadArticle: function (id) {
            return $.Deferred(function (def) {
                ajax.get("/api/article/" + id).done(function (data) {
                    if (system.debug()) {
                        console.log(data);
                    }
                    def.resolve(data);
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        },
        loadLatestInfo: function () {
            return $.Deferred(function (def) {
                ajax.get("/api/article").done(function (data) {
                    if (system.debug()) {
                        console.log(data);
                    }
                    def.resolve(data);
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        },
        loadList: function (type) {
            return $.Deferred(function (def) {
                ajax.get("/api/article/list/" + type).done(function (data) {
                    if (system.debug()) {
                        console.log(data);
                    }
                    def.resolve(data);
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        },
        save: function (type, article) {
            return $.Deferred(function (def) {
                ajax.put("/api/article/" + type, article).done(function (data) {
                    if (system.debug()) {
                        console.log(data);
                    }
                    def.resolve(data);
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        },
        add: function (catid, typeid) {
            return $.Deferred(function (def) {
                ajax.get("/api/article/add/" + catid + "/" + typeid).done(function (data) {
                    if (system.debug()) {
                        console.log(data);
                    }
                    def.resolve(data);
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        },
        remove: function (id) {
            return $.Deferred(function (def) {
                ajax.del("/api/article/" + id).done(function () {
                    def.resolve();
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        }
    }
});