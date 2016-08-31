define('models/contact', function () {
    "use strict";

    var ContactForm = function (data) {
        this.id = data.id;
        this.uid = data.uid;
        this.title = data.title;
        this.name = data.name;
        this.email = data.email;
        this.comment = data.comment;
        this.read = data.read;
        this.public = data.public;
    }

    return {
        contactForm: ContactForm
    };
});