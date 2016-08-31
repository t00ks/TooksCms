define('viewmodels/gadget/tagcloud', ['knockout', 'durandal/app', 'plugins/router', 'datacontext', 'moment', 'utils'], function (ko, app, router, datacontext, moment, utils) {
    var _model = ko.observable();

    function ViewModel(data) {
        var max = Math.max.apply(Math, data.map(function(t) { return t.rank })),
            min = Math.min.apply(Math, data.map(function (t) { return t.rank })),
            measure = (max - min) / 6;

        this.tags = ko.observableArray(data.sort(function (a, b) {
            if (a.tag.uid < b.tag.uid) {
                return 1;
            } else if (a.tag.uid > b.tag.uid) {
                return -1;
            }
            return 0;
        }));

        this.getRank = function (tag) {
            return ((tag.rank - min) / measure);
        }
    }

    return {
        activate: function () {
            datacontext.tags.getRankedTags().done(function (data) {
                var vm = new ViewModel(data);
                $.extend(vm, utils.formatter);

                _model(vm);
            });
        },
        model: _model
    }
})