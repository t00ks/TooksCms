define('appcache', ['modernizr', 'utils'], function (Modernizr, utils) {
    return (function () {
        var cachedData = {},
            enableSessionCache = true,
            enableLocalCache = true,
            getSessionData = function (key) {
                if (Modernizr.sessionstorage && enableSessionCache) {
                    return JSON.parse(sessionStorage.getItem(key));
                }
                return utils.hasProperties(cachedData[key]) ? cachedData[key] : false;
            },
            setSessionData = function (key, data) {
                if (Modernizr.sessionstorage && enableSessionCache) {
                    sessionStorage.setItem(key, JSON.stringify(data));
                } else {
                    cachedData[key] = data;
                }
            },
            getLocalData = function (key) {
                if (Modernizr.localstorage && enableLocalCache) {
                    return JSON.parse(localStorage.getItem(key));
                }
                return utils.hasProperties(cachedData[key]) ? cachedData[key] : false;
            },
            setLocalData = function (key, data) {
                if (Modernizr.localstorage && enableLocalCache) {
                    localStorage.setItem(key, JSON.stringify(data));
                } else {
                    cachedData[key] = data;
                }
            };

        return {
            session: function (key, data) {
                if (data === undefined) {
                    return getSessionData(key);
                }
                setSessionData(key, data);
            },
            local: function (key, data) {
                if (data === undefined) {
                    return getLocalData(key);
                }
                setLocalData(key, data);
            }
        };

    }());
});