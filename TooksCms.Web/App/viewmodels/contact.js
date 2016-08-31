define('viewmodels/contact', ['durandal/app', 'area', 'stats', 'knockout', 'datacontext', 'tk/ajax'], function (app, area, stats, ko, datacontext, ajax) {
    var _model = ko.observable();

    function _clear() {
        $('#contact-form-title').val('');
        $('#contact-form-name').val('');
        $('#contact-form-email').val('');
        $('#contact-form-comment').val('');
    }

    function ViewModel() {
        var self = this;

        this.title = ko.observable("");
        this.name = ko.observable("");
        this.email = ko.observable("");
        this.comment = ko.observable("");

        this.submit = function () {
            if (_validate()) {
                var challange = Recaptcha.get_challenge(),
                    response = Recaptcha.get_response();

                ajax.post('/Gadget/CheckCaptcha', { challange: challange, response: response }).done(function (data) {
                    if (data.Passed) {
                        datacontext.contact.submit({
                            title: self.title(),
                            name: self.name(),
                            email: self.email(),
                            comment: self.comment()
                        }).done(function () {
                            _clear();
                            app.success('Comment Submitted');
                        });
                    } else {
                        switch (data.Message) {
                            case "incorrect-captcha-sol":
                                app.error("Invalid Captcha");
                                break;
                            case "recaptcha-not-reachable":
                                app.error("Captcha server currently unavailable, please try again later");
                                break;
                        }
                    }
                    Recaptcha.reload();
                });
            }
            return false;
        }

        function _validate() {
            var valid = true;
            if (self.title().trim().length == 0) {
                valid = false;
                $('#contact-form-title').addClass('has-error');
            } else {
                $('#contact-form-title').removeClass('has-error');
            }
            if (self.name().trim().length == 0) {
                valid = false;
                $('#contact-form-name').addClass('has-error');
            } else {
                $('#contact-form-name').removeClass('has-error');
            }
            if (self.email().trim().length == 0) {
                valid = false;
                $('#contact-form-email').addClass('has-error');
            } else {
                $('#contact-form-email').removeClass('has-error');
            }
            if (self.comment().trim().length == 0) {
                valid = false;
                $('#contact-form-comment').addClass('has-error');
            } else {
                $('#contact-form-comment').removeClass('has-error');
            }
            return valid;
        }
    }

    return {
        activate: function () {
            app.loadGadgets(area.contact);
            stats.register(area.contact);
            _model(new ViewModel());
            app.isAdmin(false);
        },
        compositionComplete: function () {
            Recaptcha.create("6LcMWsUSAAAAAIx6A-UmzPLUlpU3iUVsI4qV1EMf", 'recaptchaHost', {
                theme: "red",
                callback: Recaptcha.focus_response_field
            });
        },
        model: _model
    };
});