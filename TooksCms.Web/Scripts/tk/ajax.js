/*
 * TooksCms jquery ajax wrapper
 * 
 * 
 * Author: Timothy Crittenden
 */

define('tk/ajax', ['jquery', 'tk/ajaxevents'], function ($, events) {
    var _settings = {
        cache: false,
        async: true,
        dataType: 'json',
        beforeSend: events ? events.beforeSend : null,
        complete: events ? events.complete : null,
        error: events ? events.error : null,
        sucess: events ? events.sucess : null,
        contentType: 'application/json; charset=utf-8',
    },
    _call = function (url, data, type) {
        var settings = {
            type: type,
            url: url,
            data: data != null ? data = JSON.stringify(data) : data
        };

        $.extend(_settings, settings);

        return $.ajax(_settings);
    };

    return {
        get: function (url) {
            return _call(url, null, 'GET');
        },
        put: function (url, data) {
            return _call(url, data, 'PUT');
        },
        post: function (url, data) {
            return _call(url, data, 'POST');
        },
        del: function (url, data) {
            return _call(url, data, 'DELETE');
        }
    }
});