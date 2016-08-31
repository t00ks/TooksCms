define('dataservices/stats', ['jquery', 'tk/ajax', 'durandal/system'], function ($, ajax, system) {
    "use strict";

    return {
        loggedInUsers: function () {
            return $.Deferred(function (def) {
                ajax.post("/home/loggedinusers/").done(function (data) {
                    def.resolve(data);
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        },
        registerPageVisit: function (stat) {
            return $.Deferred(function (def) {
                ajax.put("/api/stat/", stat).done(function (data) {
                    def.resolve(data);
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        }
    }
});