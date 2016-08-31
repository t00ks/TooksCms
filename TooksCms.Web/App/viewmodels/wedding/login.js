define('viewmodels/wedding/login', ['jquery', 'dataservices', 'toastr', 'plugins/router', 'appcache'], function ($, dataservices, toastr, router, cache) {


    return {
        activate: function () {
        },
        deactivate: function () {
        },
        login: function () {
            dataservices.wedding.checkCode($('#loginCode').val()).done(function (data) {
                if (data.granted) {
                    cache.session('wedding_guests', data)
                    router.navigate('/wedding/content');
                } else {
                    toastr.error('Sorry you have entered an incorrect access code!');
                }
            })
        }
    };
});