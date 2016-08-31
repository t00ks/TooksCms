define('viewmodels/about', ['durandal/app', 'area', 'stats'], function (app, area, stats) {
    return {
        activate: function () {
            app.loadGadgets(area.about);
            stats.register(area.about);
            app.isAdmin(false);
        },
        //attached: function () {
        //},
        binding: function (view) {
            return { applyBindings: false };
        }
    };
});