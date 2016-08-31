define('dataservices/account', ['jquery', 'tk/ajax', 'durandal/system'], function ($, ajax, system) {
    "use strict";

    return {
        getGuestStatus: function (term) {
            return $.Deferred(function (def) {
                ajax.get("/api/account/guest").done(function (data) {
                    def.resolve(data);
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        }
    }
});