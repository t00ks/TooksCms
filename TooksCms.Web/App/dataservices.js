define('dataservices', [
    'dataservices/bulletins',
    'dataservices/articles',
    'dataservices/tags',
    'dataservices/galleries',
    'dataservices/gadgets',
    'dataservices/twitter',
    'dataservices/comments',
    'dataservices/admin',
    'dataservices/lists',
    'dataservices/stats',
    'dataservices/search',
    'dataservices/account',
    'dataservices/contact',
    'dataservices/snapshots',
    'dataservices/wedding'
], function (bulletins, articles, tags, galleries, gadgets, twitter, comments, admin, lists, stats, search, account, contact, snapshots, wedding) {
    "use strict";
    return {
        bulletins: bulletins,
        articles: articles,
        tags: tags,
        galleries: galleries,
        gadgets: gadgets,
        twitter: twitter,
        comments: comments,
        admin: admin,
        lists: lists,
        stats: stats,
        search: search,
        account: account,
        contact: contact,
        snapshots: snapshots,
        wedding: wedding
    }
});