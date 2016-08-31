(function (jQuery) {
    jQuery.fn.clickoutside = function (callback) {
        var outside = 1, self = $(this);
        self.cb = callback;
        self.bind('click.clickoutside', function () {
            outside = 0;
        });
        $(document).bind('click.clickoutside', function () {
            outside && self.cb();
            outside = 1;
        });
        return $(this);
    }
    jQuery.fn.unbindclickoutside = function () {
        var self = $(this);
        self.unbind('.clickoutside');
        $(document).unbind('.clickoutside');
    }
})(jQuery);