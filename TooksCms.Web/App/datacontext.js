define('datacontext', ['dataservices', 'models', 'appcache', 'jquery', 'utils', 'durandal/app'], function (dataServices, models, cache, $, utils, app) {
    "use strict";

    function _fillArticleInfos(data) {
        var articles = [];
        for (var i = 0; i < data.length; i++) {
            articles.push(new models.ArticleInfo(data[i]));
        }
        return articles;
    }

    function _fillComments(data) {
        var comments = [];
        for (var i = 0; i < data.length; i++) {
            comments.push(new models.Comment(data[i]));
        }
        return comments;
    }

    return {
        bulletins: (function () {
            function _fillBulletins(data) {
                var bulletins = [];
                for (var i = 0; i < data.length; i++) {
                    switch (data[i].bulletinType.toLowerCase()) {
                        case 'news':
                            bulletins.push(new models.NewsBulletin(data[i]));
                            break;
                        case 'review':
                            bulletins.push(new models.ReviewBulletin(data[i]));
                            break;
                        case 'gallery':
                            bulletins.push(new models.GalleryBulletin(data[i]));
                            break;
                    }
                }
                return bulletins;
            }

            return {
                getLatest: function () {
                    return $.Deferred(function (def) {
                        dataServices.bulletins.loadLatest().done(function (data) {
                            var bulletins = _fillBulletins(data);

                            def.resolve(bulletins);
                        }).fail(function () {
                            def.reject();
                        })
                    }).promise();
                },
                fillBulletins: _fillBulletins
            }
        }()),

        articles: (function () {
            function _fillArticle(data) {
                switch (data.type.toLowerCase()) {
                    case 'news':
                        return new models.NewsArticle(data);
                        break;
                    case 'review':
                        return new models.ReviewArticle(data);
                        break;
                }
            }

            return {
                getArticle: function (id) {
                    return $.Deferred(function (def) {
                        dataServices.articles.loadArticle(id).done(function (data) {
                            var article = _fillArticle(data);


                            def.resolve(article);
                        }).fail(function () {
                            def.reject();
                        })
                    }).promise();
                },
                getLatestInfo: function () {
                    return $.Deferred(function (def) {
                        dataServices.articles.loadLatestInfo().done(function (data) {
                            var articles = _fillArticleInfos(data);


                            def.resolve(articles);
                        }).fail(function () {
                            def.reject();
                        })
                    }).promise();
                },
                getList: function (type) {
                    return $.Deferred(function (def) {
                        dataServices.articles.loadList(type).done(function (data) {
                            var articles = _fillArticleInfos(data);


                            def.resolve(articles);
                        }).fail(function () {
                            def.reject();
                        })
                    }).promise();
                },
                save: function (type, article) {
                    return $.Deferred(function (def) {
                        dataServices.articles.save(type, article).done(function () {
                            def.resolve();
                        }).fail(function () {
                            def.reject();
                        })
                    }).promise();
                },
                add: function (catid, typeid) {
                    return $.Deferred(function (def) {
                        dataServices.articles.add(catid, typeid).done(function (data) {
                            var article = _fillArticle(data);


                            def.resolve(article);
                        }).fail(function () {
                            def.reject();
                        })
                    }).promise();
                },
                remove: function (id) {
                    return dataServices.articles.remove(id);
                }
            }
        }()),

        tags: (function () {
            function _fillTags(data) {
                var tags = [];
                for (var i = 0; i < data.length; i++) {
                    tags.push(new models.Tag(data[i]));
                }
                return tags;
            }

            function _fillRankedTags(data) {
                var tags = [];
                for (var i = 0; i < data.length; i++) {
                    tags.push(new models.RankedTag(data[i]));
                }
                return tags;
            }

            return {
                getArticleTags: function (id) {
                    return $.Deferred(function (def) {
                        dataServices.tags.getTags('article', id).done(function (data) {
                            var tags = _fillTags(data);

                            def.resolve(tags);
                        }).fail(function () {
                            def.reject();
                        })
                    }).promise();
                },
                getGalleryTags: function (id) {
                    return $.Deferred(function (def) {
                        dataServices.tags.getTags('gallery', id).done(function (data) {
                            var tags = _fillTags(data);

                            def.resolve(tags);
                        }).fail(function () {
                            def.reject();
                        })
                    }).promise();
                },
                getRankedTags: function () {
                    return $.Deferred(function (def) {
                        dataServices.tags.getRankedTags().done(function (data) {
                            var tags = _fillRankedTags(data);

                            def.resolve(tags);
                        }).fail(function () {
                            def.reject();
                        })
                    }).promise();
                }
            };
        })(),

        galleries: (function () {

            function _fillGallery(data) {
                return new models.Gallery(data);
            }

            function _fillGalleryInfos(data) {
                var galleries = [];
                for (var i = 0; i < data.length; i++) {
                    galleries.push(new models.GalleryInfo(data[i]));
                }
                return galleries;
            }

            function _fillListItem(data) {
                var items = [];
                for (var i = 0; i < data.length; i++) {
                    items.push(new models.GalleryListItem(data[i]));
                }
                return items;
            }

            return {
                getGallery: function (id) {
                    return $.Deferred(function (def) {
                        dataServices.galleries.loadGallery(id).done(function (data) {
                            var gallery = _fillGallery(data);


                            def.resolve(gallery);
                        }).fail(function () {
                            def.reject();
                        })
                    }).promise();
                },
                getLatestInfo: function () {
                    return $.Deferred(function (def) {
                        dataServices.galleries.loadLatestInfo().done(function (data) {
                            var galleries = _fillGalleryInfos(data);


                            def.resolve(galleries);
                        }).fail(function () {
                            def.reject();
                        })
                    }).promise();
                },
                getList: function () {
                    return $.Deferred(function (def) {
                        dataServices.galleries.list().done(function (data) {
                            var items = _fillListItem(data);


                            def.resolve(items);
                        }).fail(function () {
                            def.reject();
                        })
                    }).promise();
                },
                save: function (gallery) {
                    return $.Deferred(function (def) {
                        dataServices.galleries.save(gallery).done(function () {
                            def.resolve();
                        }).fail(function () {
                            def.reject();
                        })
                    }).promise();
                },
                add: function (catid) {
                    return $.Deferred(function (def) {
                        dataServices.galleries.add(catid).done(function (data) {
                            var gallery = _fillGallery(data);

                            def.resolve(gallery);
                        }).fail(function () {
                            def.reject();
                        })
                    }).promise();
                }
            }
        }()),

        comments: (function () {

            return {
                getComments: function (type, id) {
                    return $.Deferred(function (def) {
                        dataServices.comments.loadComments(type, id).done(function (data) {
                            var comments = _fillComments(data);
                            def.resolve(comments);
                        }).fail(function () {
                            def.reject();
                        })
                    }).promise();
                },
                saveComment: function (comment) {
                    return $.Deferred(function (def) {
                        dataServices.comments.saveComment(comment).done(function (data) {
                            def.resolve();
                        }).fail(function () {
                            def.reject();
                        })
                    }).promise();

                }
            };
        })(),

        admin: (function () {

            function _fill(data, type) {
                var result = [];
                for (var i = 0; i < data.length; i++) {
                    result.push(new models[type](data[i]));
                }
                return result;
            }

            function _fillBrowserStats(data) {

            }

            return {
                contact: {
                    load: function () {
                        return $.Deferred(function (def) {
                            dataServices.admin.contact.load().done(function (data) {
                                var comments = _fill(data, "ContactForm");

                                def.resolve(comments);
                            }).fail(function () {
                                def.reject();
                            })
                        }).promise();
                    },
                    update: function (info) {
                        return $.Deferred(function (def) {
                            dataServices.admin.contact.update(info).done(function () {
                                def.resolve();
                            }).fail(function () {
                                def.reject();
                            })
                        }).promise();
                    }
                },
                lookup: {
                    loadCountries: function () {
                        return $.Deferred(function (def) {
                            dataServices.admin.lookup.loadCountries().done(function (data) {
                                var cunts = _fill(data, "Country");

                                def.resolve(cunts);
                            }).fail(function () {
                                def.reject();
                            })
                        }).promise();
                    },
                    updateCountries: function (cunts) {
                        return $.Deferred(function (def) {
                            dataServices.admin.lookup.updateCountries(cunts).done(function () {
                                def.resolve();
                            }).fail(function () {
                                def.reject();
                            })
                        }).promise();
                    },
                    loadCategories: function () {
                        return $.Deferred(function (def) {
                            dataServices.admin.lookup.loadCategories().done(function (data) {
                                var cats = _fill(data, "Category");

                                def.resolve(cats);
                            }).fail(function () {
                                def.reject();
                            })
                        }).promise();
                    },
                    updateCategories: function (cats) {
                        return $.Deferred(function (def) {
                            dataServices.admin.lookup.updateCategories(cats).done(function () {
                                def.resolve();
                            }).fail(function () {
                                def.reject();
                            })
                        }).promise();
                    }
                },
                routes: {
                    load: function () {
                        return $.Deferred(function (def) {
                            dataServices.admin.routes.load().done(function (data) {
                                var routes = _fill(data, "Route");

                                def.resolve(routes);
                            }).fail(function () {
                                def.reject();
                            })
                        }).promise();
                    },
                    remove: function (route) {
                        return dataServices.admin.routes.remove(route);
                    },
                    addArticle: function (typeId, id, route) {
                        return $.Deferred(function (def) {
                            dataServices.admin.routes.add('article', id, route, typeId).done(function (data) {

                                def.resolve(route);
                            }).fail(function () {
                                def.reject();
                            })
                        }).promise();
                    },
                    addGallery: function (id, route) {
                        return $.Deferred(function (def) {
                            dataServices.admin.routes.add('gallery', id, route).done(function (data) {
                                var route = new models.Route(data);

                                def.resolve(route);
                            }).fail(function () {
                                def.reject();
                            })
                        }).promise();
                    }
                },
                gadgets: {
                    load: function () {
                        return $.Deferred(function (def) {
                            dataServices.admin.gadgets.load().done(function (data) {
                                var gadgets = _fill(data, "Gadget");

                                def.resolve(gadgets);
                            }).fail(function () {
                                def.reject();
                            })
                        }).promise();
                    },
                    add: function (gadgetId, areaType, roleId) {
                        return $.Deferred(function (def) {
                            dataServices.admin.gadgets.add({
                                gadgetId: gadgetId,
                                areaType: areaType,
                                roleId: roleId
                            }).done(function (data) {
                                var gadget = new models.Gadget(data);

                                def.resolve(gadget);
                            }).fail(function () {
                                def.reject();
                            })
                        }).promise();
                    },
                    remove: function (gadget) {
                        return dataServices.admin.gadgets.remove(gadget);
                    }
                },
                ratings: {
                    load: function () {
                        return $.Deferred(function (def) {
                            dataServices.admin.ratings.load().done(function (data) {
                                var ratings = _fill(data, "Rating");

                                def.resolve(ratings);
                            }).fail(function () {
                                def.reject();
                            })
                        }).promise();
                    },
                    add: function (rating) {
                        return $.Deferred(function (def) {
                            dataServices.admin.ratings.add(rating).done(function (data) {
                                var rating = new models.Rating(data);

                                def.resolve(rating);
                            }).fail(function () {
                                def.reject();
                            })
                        }).promise();
                    }
                },
                ratingLinks: {
                    load: function () {
                        return $.Deferred(function (def) {
                            dataServices.admin.ratingLinks.load().done(function (data) {
                                var ratingLinks = _fill(data, "RatingLink");

                                def.resolve(ratingLinks);
                            }).fail(function () {
                                def.reject();
                            })
                        }).promise();
                    },
                    add: function (ratinglink) {
                        return $.Deferred(function (def) {
                            dataServices.admin.ratingLinks.add(ratinglink).done(function (data) {
                                var ratingLink = new models.RatingLink(data);

                                def.resolve(ratingLink);
                            }).fail(function () {
                                def.reject();
                            })
                        }).promise();
                    },
                    update: function (ratinglink) {
                        return dataServices.admin.ratingLinks.update(ratinglink);
                    }
                },
                stats: {
                    loadBrowserStats: function () {
                        return $.Deferred(function (def) {
                            dataServices.admin.stats.loadBrowserStats().done(function (data) {
                                var stats = _fill(data, "BrowserStat");

                                def.resolve(stats);
                            }).fail(function () {
                                def.reject();
                            });
                        }).promise();
                    },
                    loadPageVisits: function () {
                        return $.Deferred(function (def) {
                            dataServices.admin.stats.loadPageVisits().done(function (data) {
                                var stats = _fill(data, "PageVisit");

                                def.resolve(stats);
                            }).fail(function () {
                                def.reject();
                            });
                        }).promise();
                    },
                    loadClicks: function () {
                        return $.Deferred(function (def) {
                            dataServices.admin.stats.loadClicks().done(function (data) {
                                var stats = _fill(data, "ClickStat");

                                def.resolve(stats);
                            }).fail(function () {
                                def.reject();
                            });
                        }).promise();
                    }
                },
                tags: {
                    load: function (id, type) {
                        return $.Deferred(function (def) {
                            dataServices.admin.tags.load(id, type).done(function (data) {
                                var tags = {
                                    common: _fill(data.common, "Tag"),
                                    included: _fill(data.included, "Tag")
                                };
                                def.resolve(tags);
                            }).fail(function () {
                                def.reject();
                            })
                        }).promise();
                    },
                    addNew: function (name, tagReference) {
                        return $.Deferred(function (def) {
                            dataServices.admin.tags.addNew(name, tagReference).done(function (data) {
                                var tags = {
                                    common: _fill(data.common, "Tag"),
                                    included: _fill(data.included, "Tag")
                                };
                                def.resolve(tags);
                            }).fail(function () {
                                def.reject();
                            })
                        }).promise();
                    },
                    register: function (tagId, tagReference) {
                        return $.Deferred(function (def) {
                            dataServices.admin.tags.register(tagId, tagReference).done(function (data) {
                                var tags = {
                                    common: _fill(data.common, "Tag"),
                                    included: _fill(data.included, "Tag")
                                };
                                def.resolve(tags);
                            }).fail(function () {
                                def.reject();
                            })
                        }).promise();
                    },
                    remove: function (tagId, tagReference) {
                        return $.Deferred(function (def) {
                            dataServices.admin.tags.remove(tagId, tagReference).done(function (data) {
                                var tags = {
                                    common: _fill(data.common, "Tag"),
                                    included: _fill(data.included, "Tag")
                                };
                                def.resolve(tags);
                            }).fail(function () {
                                def.reject();
                            })
                        }).promise();
                    }
                },
                comments: {
                    load: function () {
                        return $.Deferred(function (def) {
                            dataServices.admin.comments.load().done(function (data) {
                                var comments = _fillComments(data);
                                def.resolve(comments);
                            }).fail(function () {
                                def.reject();
                            })
                        }).promise();

                    },
                    remove: function (commentId) {
                        return dataServices.admin.comments.remove(commentId);
                    }
                }
            };

        })(),

        lists: (function () {

            function _fillListItems(data) {
                var items = [];
                for (var i = 0; i < data.length; i++) {
                    items.push(new models.ListItem(data[i]));
                }
                return items;
            }

            function _fillImageListItems(data) {
                var items = [];
                for (var i = 0; i < data.length; i++) {
                    items.push(new models.ImageListItem(data[i]));
                }
                return items;
            }

            return {
                get: function (type) {
                    return $.Deferred(function (def) {
                        dataServices.lists.get(type).done(function (data) {
                            var items;
                            switch (type) {
                                case "flags":
                                    items = _fillImageListItems(data);
                                    break;
                                default:
                                    items = _fillListItems(data, true);
                                    break;
                            }

                            def.resolve(items);
                        }).fail(function () {
                            def.reject();
                        })
                    }).promise();
                },
                getArticles: function (typeId) {
                    return $.Deferred(function (def) {
                        dataServices.lists.getArticles(typeId).done(function (data) {
                            var items = _fillListItems(data);

                            def.resolve(items);
                        }).fail(function () {
                            def.reject();
                        })
                    }).promise();
                }
            };

        })(),

        stats: (function () {
            return {
                loggedInUsers: function () {
                    return dataServices.stats.loggedInUsers();
                },
                registerPageVisit: function (stat) {
                    return $.Deferred(function (def) {
                        dataServices.stats.registerPageVisit(stat).done(function (data) {
                            cache.session('previous_stat', data);
                            def.resolve();
                        }).fail(function () {
                            def.reject();
                        })
                    }).promise();
                },
                getPreviousStat: function () {
                    return cache.session('previous_stat') ? new models.PageVisit(cache.session('previous_stat')) : null;
                }
            };
        })(),

        search: (function () {

            return {
                get: function (term) {
                    return $.Deferred(function (def) {
                        dataServices.search.get(term).done(function (data) {
                            var articles = _fillArticleInfos(data);

                            def.resolve(articles);
                        }).fail(function () {
                            def.reject();
                        })
                    }).promise();
                }
            }
        })(),

        contact: (function () {
            return {
                submit: function (contactForm) {
                    return dataServices.contact.submit(contactForm);
                }
            }
        })(),

        snapshots: (function() {

            return {
                load: function () {
                    return dataServices.snapshots.load();
                },
                update: function (url, date, snapshot) {
                    return $.Deferred(function (def) {
                        dataServices.snapshots.update({
                            snapshotId: 0,
                            url: url,
                            date: date,
                            html: snapshot
                        }).done(function () {
                            def.resolve();
                        }).fail(function() {
                            def.reject();
                        });
                    });
                }
            }
        })()
    };
});