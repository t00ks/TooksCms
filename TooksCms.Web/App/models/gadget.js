define('models/gadget', function () {
    "use strict";

    var Gadget = function (data) {
        this.gadgetId = data.gadgetId;
        this.name = data.name;
        this.description = data.description;
        this.view = data.view;
        this.defaultColumn = data.defaultColumn;
        this.roleName = data.roleName;
        this.areaType = data.areaType;
    }

    return {
        gadget: Gadget
    };
});