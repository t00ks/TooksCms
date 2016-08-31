define('viewmodels/sitemap', ['durandal/app', 'area', 'stats', 'tk/menu'], function (app, area, stats, menu) {
    return {
        activate: function () {
            app.loadGadgets(area.contact);
            app.isAdmin(false);
        },
        compositionComplete: function () {

        },
        deactivate: function () {
        }
    };
});