/// <autosync enabled="false" />
/// <reference path="jquery-2.1.1.js" />
/// <reference path="jquery-migrate-1.2.1.js" />
/// <reference path="knockout-3.1.0.js" />
/// <reference path="lib/toastr.js" />
/// <reference path="lib/text.js" />
/// <reference path="almond-custom.js" />
/// <reference path="lib/r.js" />
/// <reference path="lib/require.js" />
/// <reference path="lib/require.intellisense.js" />

// configure intellisense for requireJS
(function () {
    "use strict";

    // Tell require.js where this projects scripts are located.

    requirejs.config({
        baseUrl: '~/App/',
        paths: {
            'text': '../Scripts/lib/text',
            'durandal': '../Scripts/lib/durandal',
            'plugins': '../Scripts/lib/durandal/plugins',
            'transitions': '../Scripts/lib/durandal/transitions',
            'tk': '../Scripts/tk'
        }
    });

    // Define 3rd party modules
    define('jquery', function () { return jQuery; });
    define('knockout', function () { return ko });
    define('modernizr', function () { return Modernizr });
    define('moment', function () { return moment; });
    define('toastr', function () { return toastr });
    define('globalize', function () { return Globalize; });
}());