define('snapshot', ['jquery', 'plugins/router', 'datacontext', 'moment'], function ($, router, datacontext, moment) {

    var snapshots = {},
        loading = true,
        timeout = null;

    function _loadSnapshots() {
        datacontext.snapshots.load().done(function (data) {
            snapshots = data || {};
            loading = false;
        });
    }

    function _takeSnapshot() {
        if (!snapshots[window.location.pathname] ||
           (snapshots[window.location.pathname] && moment(snapshots[window.location.pathname]).isBefore(moment().add(-1, 'd')))) {

            snapshots[window.location.pathname] = new Date();

            if (timeout) { clearTimeout(timeout); }
            timeout = setTimeout(function () {
                datacontext.snapshots.update(window.location.pathname, new Date(), $('html').html());
            }, 5000);
        }
    }

    return {
        init: function () {
            _loadSnapshots();

            router.on('router:navigation:complete', function () {
                if (!loading) {
                    _takeSnapshot();
                }
            });
        }
    }

});