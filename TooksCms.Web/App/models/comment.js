define('models/comment', function () {
    "use strict";

    var Comment = function (data) {
        this.name = data.name;
        this.website = data.website;
        this.email = data.email;
        this.title = data.title;
        this.comment = data.comment;
        this.autherId = data.autherId;
        this.isGuest = data.isGuest;
        this.date = data.date;
        this.parentId = data.parentId;
        this.children = [];
        this.id = data.id;
        this.uid = data.uid;
        this.articleId = data.articleId;

        for (var i = 0; i < data.children.length; i++) {
            this.children.push(new Comment(data.children[i]));
        }
    }

    return {
        comment: Comment
    };
});