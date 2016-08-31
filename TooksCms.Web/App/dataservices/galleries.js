define('dataservices/galleries', ['jquery', 'tk/ajax', 'durandal/system'], function ($, ajax, system) {
    "use strict";

    return {
        loadGallery: function (id) {
            return $.Deferred(function (def) {
                ajax.get("/api/gallery/" + id).done(function (data) {
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
                ajax.get("/api/gallery").done(function (data) {
                    if (system.debug()) {
                        console.log(data);
                    }
                    def.resolve(data);
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        },
        list: function () {
            return $.Deferred(function (def) {
                ajax.get("/api/gallery/list").done(function (data) {
                    if (system.debug()) {
                        console.log(data);
                    }
                    def.resolve(data);
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        },
        save: function (gallery) {
            return $.Deferred(function (def) {
                ajax.put("/api/gallery/save", gallery).done(function (data) {
                    if (system.debug()) {
                        console.log(data);
                    }
                    def.resolve(data);
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        },
        add: function (catid) {
            return $.Deferred(function (def) {
                ajax.get("/api/gallery/add/" + catid).done(function (data) {
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