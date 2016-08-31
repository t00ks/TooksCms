define('viewmodels/wedding/index', ['durandal/app', 'plugins/router', 'area', 'stats', 'tk/menu', 'jquery'], function (app, router, area, stats, menu, $) {

    var childRouter = router.createChildRouter()
        .makeRelative({
            moduleId: 'viewmodels/wedding',
            fromParent: true
        }).map([
            { route: '', moduleId: 'login', title: 'Wedding' },
            { route: 'content', moduleId: 'content', title: 'Wedding | Welcome' }
        ]).buildNavigationModel();

    function _parallax() {
        $('.leaf.one').css('top', -Math.floor($(this).scrollTop() / 11));
        $('.leaf.two').css('top', -Math.floor($(this).scrollTop() / 8));
        $('.leaf.three').css('top', -Math.floor($(this).scrollTop() / 5));
    }

    return {
        activate: function () {
            app.isAdmin(true);
            menu.hide();
            $('.background-image').hide();
            $('body').addClass('wedding-colour');
            $(window).on('scroll', _parallax);
        },
        deactivate: function () {
            menu.show();
            $('.background-image').show();
            $('body').removeClass('wedding-colour');
            $(window).off('scroll', _parallax);
        },
        router: childRouter
    };
});