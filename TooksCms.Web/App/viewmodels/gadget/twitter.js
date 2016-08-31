define('viewmodels/gadget/twitter', ['knockout', 'durandal/app', 'plugins/router', 'dataservices', 'moment', 'utils'], function (ko, app, router, dataservices, moment, utils) {
    var _model = ko.observable();

    function ViewModel(data) {
        this.feed = data;
        this.formatDate = function (tweet) {
            return utils.formatter.formatDateDayMonth(moment(tweet.created_at));
        }
        this.linkifyText = function (text) {
            return utils.linkifyText(text);
        }
    }

    return {
        activate: function () {
            dataservices.twitter.fetchTimeline().done(function (data) {
                var vm = new ViewModel(data);
                _model(vm);
            });
        },
        model: _model
    }
})