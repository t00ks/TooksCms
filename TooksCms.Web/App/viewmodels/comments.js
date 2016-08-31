define('viewmodels/comments', ['knockout', 'jquery', 'datacontext', 'utils', 'durandal/app', 'tk/ajax'], function (ko, $, datacontext, utils, app, ajax) {
    var _model = ko.observable(),
        _hasCaptcha = false,
        _activationData,
        _commentOpen = ko.observable(false);

    function NewCommentModel(auth, parent) {
        var self = this;

        this.parentId = ko.observable(parent ? parent.id : undefined);
        this.title = ko.observable(parent ? parent.title : "");

        this.guest = ko.observable(auth ? undefined : app.guest);
        this.isLoggedIn = ko.observable(auth);

        this.name = ko.observable("");
        this.email = ko.observable("");
        this.website = ko.observable("");

        this.comment = ko.observable("");

        this.isLoading = ko.observable(false);

        this.submit = function () {
            var challange, response;
            self.isLoading(true);

            if (!_validate()) {
                return false;
            }

            if (_hasCaptcha) {
                challange = Recaptcha.get_challenge();
                response = Recaptcha.get_response();
            }

            ajax.post('/Gadget/CheckCaptcha', { challange: challange, response: response }).done(function (data) {
                if (data.Passed) {
                    datacontext.comments.saveComment(_parseModel()).done(function () {
                        app.trigger('comment:saved');
                        self.isLoading(true);
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
                    self.isLoading(false);
                }
                if (_hasCaptcha) {
                    Recaptcha.reload();
                }
            });
        }

        this.signout = function () {
            app.signOut(true);
        }

        function _parseModel() {
            return {
                id: undefined,
                uid: undefined,
                articleId: _activationData.id,
                name: self.name(),
                email: self.email(),
                website: self.website(),
                title: self.title(),
                comment: self.comment(),
                parentId: self.parentId()
            }
        }

        function _validate() {
            var valid = true;
            if (self.title().trim().length == 0) {
                valid = false;
                $('#comment-form-title').addClass('has-error');
            } else {
                $('#comment-form-title').removeClass('has-error');
            }
            if (!self.isLoggedIn() && !self.guest()) {
                if (self.name().trim().length == 0) {
                    valid = false;
                    $('#comment-form-name').addClass('has-error');
                } else {
                    $('#comment-form-name').removeClass('has-error');
                }
                if (self.email().trim().length == 0) {
                    valid = false;
                    $('#comment-form-email').addClass('has-error');
                } else {
                    $('#comment-form-email').removeClass('has-error');
                }
            }
            if (self.comment().trim().length == 0) {
                valid = false;
                $('#comment-form-textarea').addClass('has-error');
            } else {
                $('#comment-form-textarea').removeClass('has-error');
            }
            return valid;
        }
    }

    function CommentModel(comment) {
        var self = this,
            _opening = false;

        $.extend(this, comment);
        $.extend(this, utils.formatter);

        this.replyComment = ko.observable();
        this.children = this.children.map(function (c) { return new CommentModel(c); });


        this.reply = function (comment) {
            if (self.replyComment() === undefined) {
                _opening = true;

                app.trigger('comment:opened');

                self.replyComment(new NewCommentModel(app.isAuthenticated(), comment));
                if (!app.isAuthenticated() && app.guest.isNew) {
                    Recaptcha.create("6LcMWsUSAAAAAIx6A-UmzPLUlpU3iUVsI4qV1EMf", 'recaptchaHost', {
                        theme: "red",
                        callback: Recaptcha.focus_response_field
                    });
                    _hasCaptcha = true;
                } else {
                    _hasCaptcha = false;
                }

                _opening = false;
            }
        }

        function _clearReply() {
            if (!_opening) {
                self.replyComment(undefined);
            }
        }

        this.linkifyText = function (text) {
            return utils.linkifyText(text);
        }

        app.off('comment:opened', _clearReply);
        app.on('comment:opened', _clearReply);
    }

    function ViewModel(data) {
        var self = this,
            _opening = false;

        this.comments = ko.observable(data);

        this.newComment = ko.observable();

        this.linkifyText = function (text) {
            return utils.linkifyText(text);
        }

        this.addComment = function () {
            if (self.newComment() === undefined) {
                _opening = true;

                app.trigger('comment:opened');

                self.newComment(new NewCommentModel(app.isAuthenticated()));
                if (!app.isAuthenticated() && app.guest.isNew) {
                    Recaptcha.create("6LcMWsUSAAAAAIx6A-UmzPLUlpU3iUVsI4qV1EMf", 'recaptchaHost', {
                        theme: "red",
                        callback: Recaptcha.focus_response_field
                    });
                    _hasCaptcha = true;
                } else {
                    _hasCaptcha = false;
                }

                _opening = false;
            }
        }

        app.off('comment:saved', _reload);
        app.on('comment:saved', _reload);

        function _reload() {
            self.newComment(undefined);
            self.comments().forEach(function (c) { c.replyComment(undefined); });

            datacontext.comments.getComments(_activationData.type, _activationData.id).done(function (data) {
                self.comments(data.map(function (c) { return new CommentModel(c); }));
            });
        }

        function _clearReply() {
            if (!_opening) {
                self.newComment(undefined);
            }
        }

        app.off('comment:opened', _clearReply);
        app.on('comment:opened', _clearReply);
    }

    return {
        activate: function (data) {
            _activationData = data;

            datacontext.comments.getComments(data.type, data.id).done(function (data) {
                var vm = new ViewModel(data.map(function (c) { return new CommentModel(c); }));

                $.extend(vm, utils.formatter);
                _model(vm);
            });
        },
        attached: function () {
        },
        model: _model
    };
});