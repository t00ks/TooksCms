define('models/tag', function () {
    "use strict";

    var Tag = function (data) {
        this.id = data.id;
        this.uid = data.uid;
        this.name = data.name;
    };

    var RankedTag = function (data) {
        this.rank = data.rank;
        this.tag = new Tag(data.tag);
    }

    return {
        tag: Tag,
        rankedTag: RankedTag
    };
});