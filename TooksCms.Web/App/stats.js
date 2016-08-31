define('stats', ['datacontext'], function (datacontext) {

    return {
        register: function (area, itemId) {
            datacontext.stats.registerPageVisit({
                area: area,
                itemId: itemId,
                searchTerm: null,
                linkType: null,
                previous: datacontext.stats.getPreviousStat()
            });
        },
        registerSearch: function (area, term) {
            datacontext.stats.registerPageVisit({
                area: area,
                itemId: null,
                searchTerm: term,
                linkType: null,
                previous: datacontext.stats.getPreviousStat()
            });
        }
    }
});