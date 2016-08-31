define('dataservices/gadgets', ['jquery', 'tk/ajax', 'durandal/system'], function ($, ajax, system) {
    "use strict";

    return {
        loadForArea: function (area) {
            return $.Deferred(function (def) {
                ajax.get("/api/gadget/" + area).done(function (data) {
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