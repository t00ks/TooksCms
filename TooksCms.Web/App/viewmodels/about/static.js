define(['jquery', 'knockout', 'viewmodels/about/index'], function ($, ko, parent) {
    return function Static() {
        var self = this;
        self.view = ko.computed(function () {
            var route = parent.router.activeInstruction();
            return route ? 'views/about/' + route.config.view : null;
        });
    };
});