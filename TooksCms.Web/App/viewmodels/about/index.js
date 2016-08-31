define('viewmodels/about/index', ['knockout', 'durandal/app', 'plugins/router', 'area', 'stats'], function (ko, app, router, area, stats) {

    var childRouter = router.createChildRouter()
        .makeRelative({
            moduleId: 'viewmodels/about',
            fromParent: true
        }).map([
            { route: '', moduleId: 'static', title: 'About', nav: true, view: 'about.html' },
            { route: 'prowork', moduleId: 'static', title: 'Professional Work', nav: true, view: 'prowork.html' },
            { route: 'personal', moduleId: 'static', title: 'Personal Projects', nav: true, view: 'personal.html' }
        ]).buildNavigationModel();

    return {
        activate: function () {
            app.loadGadgets(area.about);
            stats.register(area.about);
            app.isAdmin(false);
        },
        router: childRouter
    };
});