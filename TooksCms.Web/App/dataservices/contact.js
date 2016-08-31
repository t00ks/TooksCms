define('dataservices/contact', ['jquery', 'tk/ajax', 'durandal/system'], function ($, ajax, system) {
    "use strict";

    return {
        submit: function (contactForm) {
            return $.Deferred(function (def) {
                ajax.put("/api/contactform/", contactForm).done(function () {
                    def.resolve();
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        }
    }
});