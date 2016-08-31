define('dataservices/twitter', ['jquery', 'tk/ajax', 'durandal/system'], function ($, ajax, system) {
    "use strict";

    return {
        fetchTimeline: function () {
            return $.Deferred(function (def) {
                ajax.get("/api/gadget/twitter").done(function (data) {
                    if (system.debug()) {
                        console.log($.parseJSON(data));
                    }
                    def.resolve($.parseJSON(data));
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        }
    }
});