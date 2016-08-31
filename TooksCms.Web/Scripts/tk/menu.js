define('tk/menu', ['jquery', 'tk/ajax'], function ($, ajax) {
    var _clicklocked = false;

    function _bind() {
        $('#site-header .main-menu .submenu:not(.no-click)').on('click', function () {
            $(this).toggleClass('open');
            _clicklocked = !_clicklocked;
            $('#site-header .main-menu .submenu').unbindclickoutside();

            setTimeout(function () {
                $('#site-header .main-menu .submenu').clickoutside(function () {
                    $('#site-header .main-menu .submenu').removeClass('open');
                    _clicklocked = false;
                    $('#site-header .main-menu .submenu').unbindclickoutside();
                });
            }, 20);

        });
        $('#site-header .main-menu .submenu').on('mouseenter', function () {
            if (_clicklocked) {
                $('#site-header .main-menu .submenu').removeClass('open');
                $(this).addClass('open');
            }
        })
        $('.main-menu .menu').on('click', function () {
            if ($('#flyout-container').hasClass('open')) {
                setTimeout(function () {
                    $('#flyout-container').toggleClass('open');
                    $('body').toggleClass('locked');
                }, 250);
            } else {
                $('#flyout-container').toggleClass('open');
                setTimeout(function () {
                    $('body').toggleClass('locked');
                }, 250);
            }
            $('#peek').toggleClass('open');
        });

        $('.click-parent').on('click', function () {
            $(this).parent().find('.click-content').toggleClass('open');
        });
    }

    return {
        init: function () {
            _bind();
        },
        refresh: function () {
            ajax.post('/Home/Menu', null).done(function (data) {
                $('#menu-host').html(data.mainMenu);
                $('#menu-flyout-host').html(data.flyout);

                _bind();
            });
        },
        hide: function () {
            $('#peek').addClass('menu-hidden');
        },
        show: function () {
            $('#peek').removeClass('menu-hidden');
        }
    }
});