define('models/listItem', function () {
    "use strict";

    var ListItem = function (data, isNumeric) {
        this.selected = data ? data.selected : null;
        this.text = data ? data.text : null;
        this.value = data ? (isNumeric ? parseInt(data.value) : data.value) : null;
    };

    var ImageListItem = function (data, isNumeric) {
        ListItem.call(this, data, isNumeric);

        this.backgroundImage = data ? data.backgroundImage : null;
    }

    ImageListItem.prototype = new ListItem();
    ImageListItem.prototype.constructor = ImageListItem;

    return {
        listItem: ListItem,
        imageListItem: ImageListItem
    };
});