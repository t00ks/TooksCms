define('viewmodels/home/index', ['knockout', 'durandal/app', 'datacontext', 'moment', 'utils', 'area', 'controls', 'stats'], function (ko, app, datacontext, moment, utils, area, controls, stats) {
    var _model = ko.observable(),
        _loading = true,
        _composed = true;


    function ViewModel(bulletins) {
        this.bulletins = ko.observableArray(bulletins);

        this.getArticleImage = function (bulletin) {
            if (bulletin.image) {
                return bulletin.image.imagePath;
            } else {
                if (!bulletin.categoryImage) {
                    return '/Content/images/bulletin-backgrounds/dig-ec-placeholder.jpg';
                } else {
                    return '/Content/images/bulletin-backgrounds/' + bulletin.categoryImage;
                }
            }
        }

        this.getVideoAttributes = function (bulletin) {
            return {
                'class': bulletin.contentCss,
                id: bulletin.id,
                'data-url': utils.getVideoUrl(bulletin.contentCss, bulletin.content)
            };
        }

        this.getRatingAttributes = function (bulletin) {
            var cssClass = 'total ' + (bulletin.summary.rating % 1 != 0 || bulletin.summary.rating > 9 ? "decimal" : "");
            var id = "rating-total" + bulletin.id;

            return {
                'class': cssClass,
                id: id
            };
        }
    }

    function _bindInfinateScroll() {

        $("#page-content").infinitescroll({
            loading: {
                finishedMsg: "<div style='text-align:center'><em>Congratulations, you've reached the end of the internet.</em></div>",
                img: '/Content/Images/loader-transparent.gif',
                msgText: "<em>Loading the next set of posts...</em>"
            },
            state: {
                currPage: 0
            },
            nextSelector: '#hiddenNextSelector',
            navSelector: '#hiddenNextSelector',
            itemSelector: '.bulletin',
            debug: false,
            dataType: 'json',
            appendCallback: false,
            animate: false
        }, function (data) {

            var newBulletings = datacontext.bulletins.fillBulletins(data);
            newBulletings.forEach(function (b) { _model().bulletins().push(b); });
            _model().bulletins.notifySubscribers(_model().bulletins());

            utils.syntaxHighlight();
            utils.embededVideo();
        });
    }

    function _doBindings() {
        utils.syntaxHighlight();
        utils.embededVideo();

        controls.lightbox('a.gallery-image-a');
        _bindInfinateScroll();

        app.isLoading(false);
    }

    return {
        activate: function () {
            app.isLoading(true);
            _loading = true;
            _composed = false;

            datacontext.bulletins.getLatest().done(function (data) {
                var vm = new ViewModel(data);
                $.extend(vm, utils.formatter);

                _model(vm);

                if (_composed) {
                    _doBindings();
                }

                _loading = false;
            });

            app.isAdmin(false);
            app.loadGadgets(area.home);
            stats.register(area.home);
        },
        compositionComplete: function () {
            if (!_loading) {
                _doBindings();
            }
            _composed = true;
        },
        deactivate: function () {
            $("#page-content").infinitescroll('destroy');
        },
        model: _model
    };
});