define('viewmodels/admin/comment', ['knockout', 'durandal/app', 'datacontext', 'moment', 'utils', 'editor', 'area', 'toastr'],
    function (ko, app, datacontext, moment, utils, editor, area, toastr) {
        var _model = ko.observable();

        function ViewModel(comments) {
            var self = this;

            this.comments = ko.observableArray(comments);

            this.articleLink = function (comment) {
                return {
                    href: '/article/view/' + comment.articleId
                }
            }

            this.remove = function (comment) {
                datacontext.admin.comments.remove(comment.id);
                self.comments.remove(comment);
            }
        }

        return {
            activate: function (id) {

                var comments, calls = [];

                calls.push(datacontext.admin.comments.load().done(function (data) {
                    comments = data;
                }));

                $.when.apply($, calls).done(function () {
                    var vm = new ViewModel(comments);
                    _model(vm);
                });

                app.isAdmin(true);
            },
            compositionComplete: function () {

            },
            model: _model
        };
    });