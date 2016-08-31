define('login', ['jquery', 'tk/ajax', 'tk/modal', 'toastr', 'tk/menu', 'durandal/app'], function ($, ajax, modal, toastr, menu, app) {


    function _bind() {

        $('.main-menu .sign-in').on('click', function () {
            modal.show({
                type: 'inline',
                src: '#login-form'
            });
        });


        $('#signin-form').submit(function (e) {
            e.preventDefault();
            //do some verification

            var data = {
                username: $('#menu-login-username').val(),
                password: $('#menu-login-password').val(),
                persist: $('#menu-login-persist').prop('checked')
            }

            ajax.post($(this).attr('action'), data).done(function (data) {

                if (data) {
                    modal.close();
                    menu.refresh();
                    app.isAuthenticated(true);
                    toastr.success("Login Successful");
                }
                else {
                    toastr.error("Login Failed: Incorrect user name or password");
                }
            });
        });

    }


    return {
        init: function () {

            _bind();

        }
    }
});