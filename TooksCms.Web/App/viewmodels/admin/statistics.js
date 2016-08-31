define('viewmodels/admin/statistics', ['knockout', 'durandal/app', 'datacontext', 'moment', 'utils', 'editor', 'area', 'amcharts'],
    function (ko, app, datacontext, moment, utils, editor, area, amcharts) {
        var _model = ko.observable();

        function _drawCharts(browsers, clicks) {
            amcharts.makeChart('browser-graph_svg',
            {
                "type": "pie",
                "pathToImages": "http://cdn.amcharts.com/lib/3/images/",
                "balloonText": "[[title]]<br><span style='font-size:14px'><b>[[value]]</b> ([[percents]]%)</span>",
                "innerRadius": "30%",
                "marginBottom": 0,
                "marginTop": 50,
                "pullOutEffect": "easeOutSine",
                "startEffect": "easeOutSine",
                "titleField": "browser",
                "valueField": "count",
                "allLabels": [],
                "balloon": {},
                "titles": [],
                "dataProvider": browsers
            });

            amcharts.makeChart('visit-graph_svg',
            {
                "type": "serial",
                "pathToImages": "http://cdn.amcharts.com/lib/3/images/",
                "categoryField": "date",
                "startDuration": 1,
                "categoryAxis": {
                    "gridPosition": "start"
                },
                "trendLines": [],
                "graphs": [
                    {
                        "balloonText": "[[title]] of [[category]]:[[value]]",
                        "bullet": "round",
                        "id": "AmGraph-1",
                        "title": "Unique Visits",
                        "valueField": "unique"
                    },
                    {
                        "balloonText": "[[title]] of [[category]]:[[value]]",
                        "bullet": "square",
                        "id": "AmGraph-2",
                        "title": "Clicks",
                        "valueField": "clicks"
                    }
                ],
                "guides": [],
                "valueAxes": [
                    {
                        "id": "ValueAxis-1",
                        "title": "Count"
                    }
                ],
                "allLabels": [],
                "balloon": {},
                "legend": {
                    "useGraphSettings": true
                },
                "titles": [
                    {
                        "id": "Title-1",
                        "size": 15,
                        "text": "Clicks & Visits"
                    }
                ],
                "dataProvider": clicks
            });
        }

        function ViewModel(pageVisit) {
            var self = this;

            this.pageVisit = pageVisit;
        }

        return {
            activate: function (id) {
                var calls = [], pageVists, browsers, clicks;

                calls.push(datacontext.admin.stats.loadBrowserStats().done(function (data) {
                    browsers = data;
                }));

                calls.push(datacontext.admin.stats.loadPageVisits().done(function (data) {
                    var vm = new ViewModel(data);

                    _model(vm);
                }));

                calls.push(datacontext.admin.stats.loadClicks().done(function (data) {
                    clicks = data;
                }));

                $.when.apply($, calls).done(function () {
                    _drawCharts(browsers, clicks);
                });

                app.isAdmin(true);
            },
            compositionComplete: function () {

            },
            model: _model
        };
    });