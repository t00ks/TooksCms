requirejs.config({
    paths: {
        'text': '../Scripts/lib/text',
        'durandal': '../Scripts/lib/durandal',
        'plugins': '../Scripts/lib/durandal/plugins',
        'transitions': '../Scripts/lib/durandal/transitions',
        'tk': '../Scripts/tk'//,
        //'i18next': '../Scripts/lib/i18next/i18next.amd.withJQuery-1.7.3'
    },
    enforceDefine: true // required so we can detect load failure in IE
});

define('jquery', function () { return jQuery; });
define('knockout', function () { return ko });
define('modernizr', function () { return Modernizr });
define('moment', function () { return moment; });
define('toastr', function () { return toastr });
define('syntaxhighlighter', function () { return SyntaxHighlighter });
define('amcharts', function () { return AmCharts; });
//define('globalize', function () { return Globalize; });
//define('jstz', function () { return jstz; });

define(['durandal/system', 'durandal/app', 'durandal/viewLocator', 'plugins/router', 'durandal/binder', 'jquery', 'config', 'appcache', 'utils', 'tk/menu', 'login', 'dataservices', 'snapshot'],
    function (system, app, viewLocator, router, binder, $, config, cache, utils, menu, login, dataservices, snapshot) {

        app.isAdmin = ko.observable();

        //>>excludeStart("build", true);
        system.debug(true);
        //>>excludeEnd("build");

        app.title = 'Digital Ectoplasm';

        app.configurePlugins({
            router: true,
            dialog: true,
            widget: true
        });

        utils.logger.init(config.isTest);

        require(['extensions/appextensions']);

        menu.init();
        login.init();
        snapshot.init();

        ////init globalize
        //$.get("/Cldr/main/en/ca-gregorian.json", Globalize.load);
        //$.get("/Cldr/main/en/numbers.json", Globalize.load);
        //$.get("/Cldr/supplemental/likelySubtags.json", Globalize.load);
        //$.get("/Cldr/supplemental/timeData.json", Globalize.load);
        //$.get("/Cldr/supplemental/weekData.json", Globalize.load);

        //var i18NOptions = {
        //    detectFromHeaders: false,
        //    lng: window.navigator.userLanguage || window.navigator.language || 'en-GB',
        //    fallbackLng: 'en',
        //    ns: 'app',
        //    resGetPath: '/App/locales/__lng__/__ns__',
        //    useCookie: false,
        //    useLocalStorage: false,
        //    localStorageExpirationTime: 86400000 // in ms, default 1 week
        //};

        //Globalize.locale("en");
        var calls = [];
        calls.push(dataservices.admin.routes.load().done(function (data) {
            app.routes = data;
        }));

        calls.push(dataservices.account.getGuestStatus().done(function (data) {
            app.guest = data;
        }));

        $.when.apply($, calls).done(function() {
            app.start().then(function () {
                //Replace 'viewmodels' in the moduleId with 'views' to locate the view.
                //Look for partial views in a 'views' folder in the root.
                viewLocator.useConvention();

                //i18n.init(i18NOptions, function () {
                //Show the app by setting the root view model for our application with a transition.
                app.setRoot('viewmodels/shell');
                //});
            });
        });
    });