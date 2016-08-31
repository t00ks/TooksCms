define('models/route', function () {
    "use strict";

    var Route = function (data) {
        this.staticRoute = data.staticRoute;
        this.area = data.area;
        this.action = data.action;
        this.id = data.id;
        this.description = data.description;
    };

    return {
        route: Route
    };
});