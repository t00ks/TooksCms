define('dataservices/wedding', ['jquery', 'tk/ajax', 'durandal/system'], function ($, ajax, system) {
    "use strict";

    return {
        loadHotels: function () {
            return $.Deferred(function (def) {
                ajax.get("/api/wedding/hotels").done(function (data) {
                    if (system.debug()) {
                        console.log(data);
                    }
                    def.resolve(data);
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        },
        loadGuests: function () {
            return $.Deferred(function (def) {
                ajax.get("/api/weddingadmin/guests").done(function (data) {
                    if (system.debug()) {
                        console.log(data);
                    }
                    def.resolve(data);
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        },
        loadGroups: function () {
            return $.Deferred(function (def) {
                ajax.get("/api/weddingadmin/groups").done(function (data) {
                    if (system.debug()) {
                        console.log(data);
                    }
                    def.resolve(data);
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        },
        loadFood: function() {
            return $.Deferred(function (def) {
                ajax.get("/api/weddingadmin/food").done(function (data) {
                    if (system.debug()) {
                        console.log(data);
                    }
                    def.resolve(data);
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        },
        saveGuest: function (guest) {
            return $.Deferred(function (def) {
                ajax.post("/api/wedding/guests", guest).done(function (data) {
                    if (system.debug()) {
                        console.log(data);
                    }
                    def.resolve(data);
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        },
        saveFood: function (food) {
            return $.Deferred(function (def) {
                ajax.post("/api/wedding/food", food).done(function (data) {
                    if (system.debug()) {
                        console.log(data);
                    }
                    def.resolve(data);
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        },
        rsvp: function (rsvp) {
            return $.Deferred(function (def) {
                ajax.put("/api/wedding/rsvp", rsvp).done(function (data) {
                    if (system.debug()) {
                        console.log(data);
                    }
                    def.resolve(data);
                }).fail(function () {
                    def.reject();
                });
            }).promise();
        },
        checkCode: function (code) {
            return $.Deferred(function (def) {
                ajax.get("/api/wedding/login?code=" + encodeURIComponent(code)).done(function (data) {
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