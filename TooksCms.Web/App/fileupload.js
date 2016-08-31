define('fileupload', ['jquery', 'durandal/app'], function ($, app) {

    function _show() {
        var self = this;
        $.magnificPopup.open({
            items: {
                src: '#fileupload',
                type: 'inline'
            },
            callbacks: {
                open: function () {
                    //self.init();
                }
            },
            mainClass: 'mfp-move-horizontal',
            removalDelay: 300
        });
        //TODO:: Overlay Close
    }

    function _preview(id) {
        var files = document.getElementById(id).files;
        var preview = $('#uploadPreview')[0];
        for (var i = 0; i < files.length; i++) {
            var file = files[i];
            var imageType = /image.*/;

            if (!file.type.match(imageType)) {
                continue;
            }

            console.log(file);

            var container = $('<div class="image"></div>');

            var img = document.createElement("img");
            img.file = file;

            container.append(img)

            container.append($('<div class="progress"><div class="bar"></div></div>'))

            preview.appendChild(container[0]);

            var reader = new FileReader();
            reader.onload = (function (aImg) { return function (e) { aImg.src = e.target.result; }; })(img);
            reader.readAsDataURL(file);
        }
    }

    function _upload(model) {
        var imgs = $(".image:not(.complete)"),
            articleid = model.uid,
            imagecount = model.images().length;

        for (var i = 0; i < imgs.length; i++) {
            imagecount++;
            _uploadFile(imgs[i], $('img', imgs[i])[0].file, articleid, imagecount, model.addImage);
        }
    }

    function _uploadFile(img, file, articleid, imagecount, callback) {

        var reader = new FileReader(),
            bar = $('.bar', img),
            xhr = new XMLHttpRequest();

        xhr.upload.addEventListener("progress", function (e) {
            if (e.lengthComputable) {
                var percentage = Math.round((e.loaded * 100) / e.total);
                bar.css('width', percentage + '%');
            }
        }, false);

        xhr.upload.addEventListener("load", function (e) {
            bar.css('width', '100%');
        }, false);

        xhr.open("POST", "/FileUpload/PostFile/" + file.name, true);

        xhr.setRequestHeader('X-File-Articleid', articleid);
        xhr.setRequestHeader('X-File-Imagecount', imagecount);

        xhr.overrideMimeType('text/plain; charset=x-user-defined-binary');

        xhr.onreadystatechange = function() {
            if (xhr.readyState == 4 && xhr.status == 200) {
                if (xhr.responseText) {
                    var response = JSON.parse(xhr.responseText);
                    if (response.error) {
                        app.error("Error Uploading File:" + file.name);
                    } else {
                        $(img).addClass('complete');
                        app.trigger('fileupload:uploadcomplete');
                        callback(response);
                    }
                }
            }
        }

        reader.onload = function (evt) {
            xhr.sendAsBinary(evt.target.result);
        };
        reader.readAsBinaryString(file);
    }

    return {
        show: _show,
        preview: _preview,
        upload: _upload
    }
});
