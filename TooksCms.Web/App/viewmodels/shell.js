define('viewmodels/shell', ['durandal/app', 'plugins/router', 'durandal/viewEngine', 'config', 'appcache', 'knockout', 'datacontext'],
function (app, router, viewEngine, config, cache, ko, datacontext) {

    var _test = ko.observable("Test"),
        _isAdmin = ko.observable(),
        _pageCss = ko.computed(function () {
            if (_isAdmin()) {
                return "col-md-12 admin";
            }
            return "col-md-8";
        }),
        _routes = null;

    return {
        activate: function () {

            router.makeRelative({ moduleId: 'viewmodels' }).map([
                {
                    route: '',
                    moduleId: 'home/index',
                    title: 'Latest Posts'
                },
                {
                    route: 'article/view/:id',
                    moduleId: 'article/display',
                    title: app.pageTitle
                },
                {
                    route: 'article/review/:id',
                    moduleId: 'article/display',
                    title: app.pageTitle
                },
                {
                    route: 'article/edit',
                    moduleId: 'article/editor',
                    title: 'Editor'
                },
                {
                    route: 'gallery/view/:id',
                    moduleId: 'gallery/view',
                    title: app.pageTitle
                },
                {
                    route: 'gallery/list',
                    moduleId: 'gallery/list',
                    title: 'Galleries'
                },
                {
                    route: 'article/list/:type',
                    moduleId: 'article/list',
                    title: app.pageTitle
                },
                {
                    route: 'search/:term',
                    moduleId: 'search/index',
                    title: app.pageTitle
                },
                {
                    route: 'about*about',
                    moduleId: 'about/index',
                    title: 'About'
                },
                {
                    route: 'contact',
                    moduleId: 'contact',
                    title: 'Contact'
                },
                {
                    route: 'wedding*wedding',
                    moduleId: 'wedding/index',
                    title: 'Wedding'
                },
                {
                    route: 'sitemap',
                    moduleId: 'sitemap',
                    title: 'Sitemap'
                },
                {
                    route: 'admin/article',
                    moduleId: 'admin/article',
                    title: 'Admin: Article'
                },
                {
                    route: 'admin/gallery',
                    moduleId: 'admin/gallery',
                    title: 'Admin: Gallery'
                },
                {
                    route: 'admin/contact',
                    moduleId: 'admin/contact',
                    title: 'Admin: Contact'
                },
                {
                    route: 'admin/gadgets',
                    moduleId: 'admin/gadgets',
                    title: 'Admin: Gadgets'
                },
                {
                    route: 'admin/gallery',
                    moduleId: 'admin/gallery',
                    title: 'Admin: Gallery'
                },
                {
                    route: 'admin/lookup',
                    moduleId: 'admin/lookup',
                    title: 'Admin: Lookup'
                },
                {
                    route: 'admin/ratings',
                    moduleId: 'admin/ratings',
                    title: 'Admin: Ratings'
                },
                {
                    route: 'admin/routes',
                    moduleId: 'admin/routes',
                    title: 'Admin: Routes'
                },
                {
                    route: 'admin/statistics',
                    moduleId: 'admin/statistics',
                    title: 'Admin: Statistics'
                },
                {
                    route: 'admin/comment',
                    moduleId: 'admin/comment',
                    title: 'Admin: Comments'
                },
                {
                    route: 'admin/wedding',
                    moduleId: 'admin/wedding',
                    title: 'Admin: Wedding'
                }
            ]);

            router.mapUnknownRoutes(function (instruction) {
                if (app.routes) {
                    var route = app.routes.filter(function (r) { return r.staticRoute.toLowerCase() == instruction.fragment.toLowerCase() })[0];

                    if (route) {
                        switch (route.area.toLowerCase()) {
                            case 'article':
                                instruction.config.moduleId = "article/display";
                                instruction.params = [route.id];
                                return;
                            case 'gallery':
                                instruction.config.moduleId = "gallery/view";
                                instruction.params = [route.id];
                                return;
                        }
                    }
                }
                instruction.config.moduleId = "404";
            });

            router.on('router:navigation:processing', function () {
                if ($('#flyout-container').hasClass('open')) {
                    setTimeout(function () {
                        $('#flyout-container').toggleClass('open');
                        $('body').toggleClass('locked');
                    }, 250);
                    $('#peek').toggleClass('open');
                }
            })

            ko.computed(function () {
                _isAdmin(app.isAdmin());
            });

            datacontext.stats.loggedInUsers().done(function (data) {
                $('#online-users').text(data);
            });

            $(window).on('scroll', function () {
                $('#peek .background-image').css('top', -Math.floor($(this).scrollTop() / 5));
            });

            return router.activate({ pushState: true, ignore: ['gallery-image-a', 'view-image'] });
        },
        router: router,
        attached: function () {
            $('#site-header .btn-search').on('click', function () {
                var term = $('#site-header .tb-search').val();
                if (term) {
                    router.navigate('/search/' + term)
                } else {
                    app.error("You cannot search for nothing!");
                }
            });
        },
        app: app,
        test: _test,
        pageCss: _pageCss,
        isLoading: app.isLoading
    }
});