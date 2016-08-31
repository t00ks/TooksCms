define('extensions/appextensions', ['durandal/app', 'plugins/router', 'dataservices', 'knockout', 'toastr', 'tk/ajax'], function (app, router, dataservices, ko, toastr, ajax) {

    var _gadgets = ko.observable({
        col1: ko.observableArray(),
        col2: ko.observableArray()
    });

    function _parseNewGadgets(col, data) {
        data[col].forEach(function (g) {
            if (!_gadgets()[col]().some(function (g2) { return g2.id === g })) {
                _gadgets()[col].push({ modelId: "viewmodels/" + g.replace(".", "/"), id: g })
            }
        });

        var toRemove = [];
        _gadgets()[col]().forEach(function (g) {
            if (!data[col].some(function (g2) { return g2 === g.id })) {
                toRemove.push(g);
            }
        });

        if (toRemove.length > 0) {
            toRemove.forEach(function (g) { _gadgets()[col].remove(g); });
        }
    }

    app.isLoading = ko.observable();

    app.loadGadgets = function (area) {
        dataservices.gadgets.loadForArea(area).done(function (data) {
            _parseNewGadgets('col1', data);
            _parseNewGadgets('col2', data);
        });
    };

    app.gadgets = _gadgets;

    app.error = function (message) {
        toastr.error(message);
    }

    app.success = function (message) {
        toastr.success(message);
    }

    app.isAuthenticated = ko.observable(false);

    app.checkAuthenticated = function () {
        ajax.post('/Account/IsAuthenticated', null).done(function (data) {
            app.isAuthenticated(data.isAuthenticated);
        });
    }

    app.pageTitle = ko.observable();

    app.signOut = function (isGuest) {
        ajax.post("/Account/SignOut", { isGuest: isGuest }).done(function () {
            //TODO:: shouldn't have to reload location
            window.location.reload();
        });
    }

    window.signOut = app.signOut;
});
