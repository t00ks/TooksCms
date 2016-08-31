define('tk/modal', ['jquery'], function ($) {

    return {
        show: function (options) {

            function getAnim(opts) {
                switch (opts.anim) {
                    case 'move':
                        return 'mfp-move-horizontal';
                    default:
                        return 'mfp-fade';
                }
            }

            var opts = {
                type: 'inline',
                anim: 'fade',
                callbacks: undefined,
                closeOnOverlayClick: false,
                typeOptions: {},
                contentClass: undefined
            };

            $.extend(opts, options);

            var items = {
                type: opts.type
            };

            switch (opts.type) {
                case 'inline':
                    items.src = opts.src;
                    break;
                case 'html':
                    items.html = opts.html;
                    break;
            }

            $.magnificPopup.open({
                items: items,
                typeOptions: opts.typeOptions,
                mainClass: getAnim(opts),
                removalDelay: 300,
                closeOnBgClick: opts.closeOnOverlayClick,
                callbacks: opts.callbacks,
                contentClass: opts.contentClass
            });

        },
        close: function () {
            if ($.magnificPopup.instance) {
                $.magnificPopup.close();
            }
        }
    };
});