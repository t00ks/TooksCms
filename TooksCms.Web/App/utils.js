define('utils', ['jquery', 'moment', 'config', 'toastr', 'syntaxhighlighter'], function ($, moment, config, toastr, sh) {
    "use strict";

    function _getYouTubeVideoIdFromUrl(url) {
        var start = url.indexOf("/v");
        if (start < 0) {
            start = url.indexOf("?v");
        }
        start = start + 3;
        var endQu = url.indexOf("?", start);
        var endAmp = url.indexOf("&", start);

        var end = endQu < 0 && endAmp < 0 ? (url.length) : endQu < 0 ? endAmp : endAmp < 0 ? endQu : endQu > endAmp ? endAmp : endQu;

        var id = url.substring(start, end);

        return id;
    }

    return {
        clearCache: function () {
            localStorage.clear(); sessionStorage.clear();
        },
        isOnline: function () {
            return navigator.onLine;
            //var xhr = new XMLHttpRequest();
            //var status;

            //// Open new request as a HEAD to the root hostname with a random param to bust the cache
            //xhr.open("HEAD", "//" + window.location.hostname + "/status/checkaccess?rand=" + Math.floor((1 + Math.random()) * 0x10000), false);

            //// Issue request and handle response
            //try {
            //    xhr.send();
            //    return (xhr.status >= 200 && xhr.status < 300 || xhr.status === 304);
            //} catch (error) {
            //    return false;
            //}
        },
        getVideoUrl: function (type, link) {
            if (type === 'video youtube') {
                return _getYouTubeVideoIdFromUrl(link);
            }
            return link;
        },
        syntaxHighlight: (function () {
            function _path() {
                var args = arguments,
                  result = [];

                for (var i = 0; i < args.length; i++)
                    result.push(args[i].replace('@', '/Scripts/lib/sh/'));

                return result
            }

            return function () {
                sh.config.stripBrs = true;
                sh.defaults['toolbar'] = false;
                sh.config.bloggerMode = true;
                sh.autoloader.apply(null, _path(
                    'cpp c                  @shBrushCpp.js',
                    'c# c-sharp csharp      @shBrushCSharp.js',
                    'css                    @shBrushCss.js',
                    'java                   @shBrushJava.js',
                    'js jscript javascript  @shBrushJScript.js',
                    'php                    @shBrushPhp.js',
                    'text plain             @shBrushPlain.js',
                    'py python              @shBrushPython.js',
                    'sql                    @shBrushSql.js',
                    'vb vbnet               @shBrushVb.js',
                    'xml xhtml xslt html    @shBrushXml.js'
                ));
                sh.all();
            }
        })(),
        embededVideo: (function () {
            var _vimeoId = '';

            window.embedVimeo = function (video) {
                document.getElementById(_vimeoId).innerHTML = unescape(video.html);
            }

            function YouTubePlayer(id, url) {
                var player,
                    done = false;

                function _onPlayerReady(event) {

                }

                function _onPlayerStateChange(event) {

                }

                function _stopVideo() {

                }

                player = new YT.Player(id, {
                    height: '390',
                    width: '640',
                    videoId: url,
                    events: {
                        'onReady': _onPlayerReady,
                        'onStateChange': _onPlayerStateChange
                    }
                });
            }

            return function () {
                var videoHosts = $('.video');
                var length = videoHosts.length;
                for (var i = 0; i < length; i++) {
                    var video = $(videoHosts[i]);
                    if (video.hasClass('youtube')) {
                        var id = video.attr('id');
                        var url = video.attr('data-url');

                        new YouTubePlayer(id, url);

                        //var params = { allowScriptAccess: "always", wmode: "opaque" };
                        //var atts = { id: "myytplayer" };
                        //var id = video.attr('id');
                        //var url = video.attr('data-url');
                        //swfobject.embedSWF("http://www.youtube.com/v/" + url + "?enablejsapi=1&playerapiid=ytplayer&version=3",
                        //   id, "640", "390", "8", null, null, params, atts);
                    } else if (video.hasClass('vimeo')) {
                        var videoUrl = video.attr('data-url');
                        var endpoint = 'http://www.vimeo.com/api/oembed.json';
                        var callback = 'embedVimeo';
                        var url = endpoint + '?url=' + encodeURIComponent(videoUrl) + '&callback=' + callback + '&width=640';
                        _vimeoId = video.attr('id');
                        var js = document.createElement('script');
                        js.setAttribute('type', 'text/javascript');
                        js.setAttribute('src', url);
                        document.getElementsByTagName('head').item(0).appendChild(js);
                    }
                }
            }
        })(),
        getViewPath: function (view) {
            return view == 'News' ? 'View' : view;
        },
        getImageFolder: function (type) {
            switch (type) {
                case 'Review':
                    return 'Review';
                case 'News':
                    return 'NewsArticle';
            }
        },
        linkifyText: function (text) {
            var encoded = $('<div/>').text(text).html();
            var exp = /(\b(https?|ftp|file):\/\/[-A-Z0-9+&@#\/%?=~_|!:,.;]*[-A-Z0-9+&@#\/%=~_|])/ig;
            return encoded.replace(exp, "<a href='$1'>$1</a>");
        },
        formatter: (function () {

            function _formatDate(date) {
                return moment(date).format('MMM DD YYYY');
            }

            function _formatDateDayMonth(date) {
                return moment(date).format('Do MMM');
            }

            function _formatTimeFromNow(date) {
                return moment(date).fromNow();
            }

            function _addThisAttributes(data) {

                var vpath = "";

                switch (data.type) {
                    case 'review':
                        vpath = 'Article/Review';
                        break;
                    case 'news':
                        vpath = 'Article/View';
                        break;
                    case 'gallery':
                        vpath = 'Gallery/View'
                        break;
                }

                var url = 'http://www.dig-ec.co.uk/Article/' + vpath + '/' + data.id;

                return {
                    'addthis:title': data.title,
                    'addthis:url': url
                }
            }

            return {
                formatDate: _formatDate,
                formatDateDayMonth: _formatDateDayMonth,
                addThisAttributes: _addThisAttributes,
                formatTimeFromNow: _formatTimeFromNow
            };
        })(),
        logger: (function () {
            var _debug = false;

            return {
                init: function (isDebug) {
                    toastr.options.positionClass = "toast-top-right";
                    toastr.options.timeOut = 2000;
                    toastr.options.showMethod = 'slideDown';

                    _debug = isDebug;
                },
                error: function (exception, consoleOnly) {
                    if (_debug) {
                        if (!consoleOnly) {
                            toastr.error(exception.message);
                        }
                        console.log(exception);
                    }
                },
                info: function (message) {
                    if (_debug) {
                        var t = toastr.info(message);
                        console.log(message);

                        setTimeout(function () {
                            toastr.clear(t);
                        }, 2500);
                    }
                }
            }
        })()
    };
});