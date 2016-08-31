define('models/stats', function () {
    "use strict";

    var PageVisit = function (data) {
        this.id = data.id;
        this.areaType = data.areaType;
        this.itemId = data.itemId;
        this.searchTerm = data.searchTerm;
        this.referer = data.referer;
        this.url = data.url;
        this.linkType = data.linkType;
        this.previousId = data.previousId;
        this.userAgent = data.userAgent;
        this.userLanguages = data.userLanguages;
        this.browserVersion = data.browserVersion;
        this.browserName = data.browserName;
        this.ipAddress = data.ipAddress;
        this.dateTime = data.dateTime;
    };

    var BrowserStat = function (data) {
        this.browser = data.browser;
        this.count = data.count;
    }

    var ClickStat = function (data) {
        this.date = data.date;
        this.unique = data.unique;
        this.clicks = data.clicks;
    }

    return {
        browserStat: BrowserStat,
        pageVisit: PageVisit,
        clickStat: ClickStat
    };
});