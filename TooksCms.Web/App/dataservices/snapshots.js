define('dataservices/snapshots', ['jquery', 'tk/ajax', 'durandal/system'], function ($, ajax, system) {
    "use strict";

    return {
        load: function () {
            return $.Deferred(function (def) {
                ajax.get("/api/snapshots").done(function (data) {
                    def.resolve(data);
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        },
        update: function (snapshot) {
            return $.Deferred(function (def) {
                ajax.put("/api/snapshots", snapshot).done(function (data) {
                    def.resolve(data);
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        }
    }
});