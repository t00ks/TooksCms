define('dataservices/admin', ['jquery', 'tk/ajax', 'durandal/system'], function ($, ajax, system) {
    "use strict";

    return {
        contact: {
            load: function () {
                return $.Deferred(function (def) {
                    ajax.get("/api/admin/contact/").done(function (data) {
                        if (system.debug()) {
                            console.log(data);
                        }
                        def.resolve(data);
                    }).fail(function () {
                        def.reject();
                    });
                }).promise();
            },
            update: function (info) {
                return $.Deferred(function (def) {
                    ajax.put("/api/admin/contact", info).done(function () {
                        def.resolve();
                    }).fail(function () {
                        def.reject();
                    });
                }).promise();
            }
        },
        lookup: {
            loadCountries: function () {
                return $.Deferred(function (def) {
                    ajax.get("/api/admin/country/").done(function (data) {
                        if (system.debug()) {
                            console.log(data);
                        }
                        def.resolveWith(this, [data]);
                    }).fail(function () {
                        def.reject();
                    });
                }).promise();
            },
            updateCountries: function (cunts) {
                return $.Deferred(function (def) {
                    ajax.put("/api/admin/country", cunts).done(function () {
                        def.resolve();
                    }).fail(function () {
                        def.reject();
                    });
                }).promise();
            },
            loadCategories: function () {
                return $.Deferred(function (def) {
                    ajax.get("/api/admin/category/").done(function (data) {
                        if (system.debug()) {
                            console.log(data);
                        }
                        def.resolveWith(this, [data]);
                    }).fail(function () {
                        def.reject();
                    });
                }).promise();
            },
            updateCategories: function (cats) {
                return $.Deferred(function (def) {
                    ajax.put("/api/admin/category", cats).done(function () {
                        def.resolve();
                    }).fail(function () {
                        def.reject();
                    });
                }).promise();
            }
        },
        routes: {
            load: function () {
                return $.Deferred(function (def) {
                    ajax.get("/api/admin/routes/").done(function (data) {
                        if (system.debug()) {
                            console.log(data);
                        }
                        def.resolve(data);
                    }).fail(function () {
                        def.reject();
                    });
                }).promise();
            },
            remove: function (route) {
                return $.Deferred(function (def) {
                    ajax.del("/api/admin/routes/", route).done(function () {
                        def.resolve();
                    }).fail(function () {
                        def.reject();
                    });
                }).promise();
            },
            add: function (type, id, route, typeId) {
                var url;
                switch (type) {
                    case 'article':
                        url = '/api/admin/routes/' + typeId + '/' + id;
                        break;
                    case 'gallery':
                        url = '/api/admin/routes/' + id;
                        break;
                }

                return $.Deferred(function (def) {
                    ajax.post(url, route).done(function (data) {
                        if (system.debug()) {
                            console.log(data);
                        }
                        def.resolve(data);
                    }).fail(function () {
                        def.reject();
                    });
                }).promise();
            }
        },
        gadgets: {
            load: function () {
                return $.Deferred(function (def) {
                    ajax.get("/api/admin/gadgets/").done(function (data) {
                        if (system.debug()) {
                            console.log(data);
                        }
                        def.resolve(data);
                    }).fail(function () {
                        def.reject();
                    });
                }).promise();
            },
            add: function (gadget) {
                return $.Deferred(function (def) {
                    ajax.post("/api/admin/gadgets/", gadget).done(function (data) {
                        if (system.debug()) {
                            console.log(data);
                        }
                        def.resolve(data);
                    }).fail(function () {
                        def.reject();
                    });
                }).promise();
            },
            remove: function (gadget) {
                return $.Deferred(function (def) {
                    ajax.del("/api/admin/gadgets/", gadget).done(function (data) {
                        def.resolve();
                    }).fail(function () {
                        def.reject();
                    });
                }).promise();
            }
        },
        ratings: {
            load: function () {
                return $.Deferred(function (def) {
                    ajax.get("/api/admin/rating/").done(function (data) {
                        if (system.debug()) {
                            console.log(data);
                        }
                        def.resolve(data);
                    }).fail(function () {
                        def.reject();
                    });
                }).promise();
            },
            add: function (rating) {
                return $.Deferred(function (def) {
                    ajax.post("/api/admin/rating/", rating).done(function (data) {
                        if (system.debug()) {
                            console.log(data);
                        }
                        def.resolve(data);
                    }).fail(function () {
                        def.reject();
                    });
                }).promise();
            },
        },
        ratingLinks: {
            load: function () {
                return $.Deferred(function (def) {
                    ajax.get("/api/admin/ratinglink/").done(function (data) {
                        if (system.debug()) {
                            console.log(data);
                        }
                        def.resolve(data);
                    }).fail(function () {
                        def.reject();
                    });
                }).promise();
            },
            add: function (ratinglink) {
                return $.Deferred(function (def) {
                    ajax.post("/api/admin/ratinglink/", ratinglink).done(function (data) {
                        if (system.debug()) {
                            console.log(data);
                        }
                        def.resolve(data);
                    }).fail(function () {
                        def.reject();
                    });
                }).promise();
            },
            update: function (ratinglink) {
                return $.Deferred(function (def) {
                    ajax.put("/api/admin/ratinglink/", ratinglink).done(function (data) {
                        if (system.debug()) {
                            console.log(data);
                        }
                        def.resolve(data);
                    }).fail(function () {
                        def.reject();
                    });
                }).promise();
            }
        },
        stats: {
            loadBrowserStats: function () {
                return $.Deferred(function (def) {
                    ajax.get("/api/admin/stats/browsers").done(function (data) {
                        if (system.debug()) {
                            console.log(data);
                        }
                        def.resolve(data);
                    }).fail(function () {
                        def.reject();
                    });
                }).promise();
            },
            loadPageVisits: function () {
                return $.Deferred(function (def) {
                    ajax.get("/api/admin/stats/pagevisits").done(function (data) {
                        if (system.debug()) {
                            console.log(data);
                        }
                        def.resolve(data);
                    }).fail(function () {
                        def.reject();
                    });
                }).promise();
            },
            loadClicks: function () {
                return $.Deferred(function (def) {
                    ajax.get("/api/admin/stats/clicks").done(function (data) {
                        if (system.debug()) {
                            console.log(data);
                        }
                        def.resolve(data);
                    }).fail(function () {
                        def.reject();
                    });
                }).promise();
            }
        },
        tags: {
            load: function (id, type) {
                return $.Deferred(function (def) {
                    ajax.get("/api/admin/tag/" + id + "/" + type).done(function (data) {
                        if (system.debug()) {
                            console.log(data);
                        }
                        def.resolve(data);
                    }).fail(function () {
                        def.reject();
                    });
                }).promise();
            },
            addNew: function (name, tagReference) {
                return $.Deferred(function (def) {
                    ajax.post("/api/admin/tag/" + name, tagReference).done(function (data) {
                        if (system.debug()) {
                            console.log(data);
                        }
                        def.resolve(data);
                    }).fail(function () {
                        def.reject();
                    });
                }).promise();
            },
            register: function (tagId, tagReference) {
                return $.Deferred(function (def) {
                    ajax.put("/api/admin/tag/" + tagId, tagReference).done(function (data) {
                        if (system.debug()) {
                            console.log(data);
                        }
                        def.resolve(data);
                    }).fail(function () {
                        def.reject();
                    });
                }).promise();
            },
            remove: function (tagId, tagReference) {
                return $.Deferred(function (def) {
                    ajax.del("/api/admin/tag/" + tagId, tagReference).done(function (data) {
                        if (system.debug()) {
                            console.log(data);
                        }
                        def.resolve(data);
                    }).fail(function () {
                        def.reject();
                    });
                }).promise();
            }
        },
        comments: {
            load: function () {
                return $.Deferred(function (def) {
                    ajax.get("/api/admin/comments/").done(function (data) {
                        if (system.debug()) {
                            console.log(data);
                        }
                        def.resolve(data);
                    }).fail(function () {
                        def.reject();
                    });
                }).promise();
            },
            remove: function (commentId) {
                return $.Deferred(function (def) {
                    ajax.del("/api/admin/comments/" + commentId).done(function () {
                        def.resolve();
                    }).fail(function () {
                        def.reject();
                    });
                }).promise();
            }
        }
    }
});