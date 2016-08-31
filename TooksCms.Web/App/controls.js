define('controls', ['jquery', 'htmlStripper', 'durandal/app'], function ($, htmlStripper, app) {
    var _model = null;

    var _richEditor = (function () {
        var model = function () {
            this.cssClass = "rich-content";
            this.type = "editablediv";
            this.value = "";
        };

        return {
            initNew: function (position) {
                var _new = $('#editor-part-' + position)

                $('div.editable', _new).prop('contentEditable', true);
                $('a[data-type="editablediv"]', _new).click(function (event) {
                    var pos = $(this).attr('data-position');
                    _editorMenu.editableDiv(event, pos);
                    return false;
                });
            },
            init: function (first) {
                var c = $('.editable')
                for (var i = 0; i < c.length; i++) {
                    if (c[i].className.indexOf("rich-content") != -1) {
                        c[i].contentEditable = true;
                    }
                }
                $('a[data-type="editablediv"]').click(function (event) {
                    var pos = $(this).attr('data-position');
                    _editorMenu.editableDiv(event, pos);
                    return false;
                });
            }
        }
    })();

    var _codeSnippet = (function () {
        var model = function () {
            this.cssClass = "brush: c#";
            this.type = "codesnippet";
            this.value = "";
        }

        return {
            initNew: function (position) {
                var _new = $('#editor-part-' + position)

                $('div[data-type="codesnippet"]', _new).prop('contentEditable', true);
                $('a[data-type="codesnippet"]', _new).click(function (event) {
                    var pos = $(this).attr('data-position');
                    _editorMenu.codeSnippet(event, pos);
                    return false;
                });
            },
            init: function () {
                var c = $('div[data-type="codesnippet"]')
                for (var i = 0; i < c.length; i++) {
                    c[i].contentEditable = true;
                }
                $('a[data-type="codesnippet"]').click(function (event) {
                    var pos = $(this).attr('data-position');
                    _editorMenu.codeSnippet(event, pos);
                    return false;
                });
            }
        }
    })();

    var _singleImage = (function () {
        var model = function () {
            this.cssClass = "single-image";
            this.type = "singleimage";
            this.value = "";
        };

        function _setDroppable() {
            $('.single-image-droppable').droppable({
                drop: function (event, ui) {
                    $(this).append(ui.draggable);
                    $(this).droppable("option", "disabled", true);

                    $('.ui-icon-zoomin', ui.draggable).css('display', 'none');
                    $('.ui-icon-zoomout', ui.draggable).css('display', 'none');

                    var name = $(ui.draggable).attr('data-name');
                    var position = $(this).attr('data-id');
                    $.each(tk.article.koModel.images(), function () {
                        if (this.Value() == name) {
                            this.Position(position);
                            this.Size('x');
                        }
                    });
                }
            });
        }

        return {
            initNew: function (position) {
                var _new = $('#editor-part-' + position)

                $('a[data-type="singleimage"]', _new).click(function (event) {
                    var pos = $(this).attr('data-position');
                    _editorMenu.singleImage(event, pos);
                    return false;
                });
                _setDroppable();
            },
            init: function () {
                $('a[data-type="singleimage"]').click(function (event) {
                    var pos = $(this).attr('data-position');
                    _editorMenu.singleImage(event, pos);
                    return false;
                });
                _setDroppable();
            }
        }
    })();

    var _embededVideo = (function () {
        var model = function () {
            this.cssClass = "video youtube";
            this.type = "embededvideo";
            this.value = "";
        };

        return {
            initNew: function (position) {
                var _new = $('#editor-part-' + position)

                var div = $('div[data-type="embededvideo"]', _new);
                div.prop('contentEditable', true);
                div.bind("paste", function () {
                    htmlStripper.unformat($(this));
                });

                $('a[data-type="embededvideo"]', _new).click(function (event) {
                    var pos = $(this).attr('data-position');
                    _editorMenu.embededVideo(event, pos);
                    return false;
                });
            },
            init: function () {
                var c = $('div[data-type="embededvideo"]')
                for (var i = 0; i < c.length; i++) {
                    c[i].contentEditable = true;
                    $(c[i]).bind("paste", function () {
                        htmlStripper.unformat($(this));
                    });
                }
                $('a[data-type="embededvideo"]').click(function (event) {
                    var pos = $(this).attr('data-position');
                    _editorMenu.embededVideo(event, pos);
                    return false;
                });
            }
        };
    })();

    var _editor = (function () {
        var _gallery = null,
            _galleryId = null,
            _dragging = false;

        function _setImageClick(selector) {
            $(selector).unbind('click').click(function (event) {
                var $item = $(this),
                $target = $(event.target);
                if ($target.is("a.i-control.grow") || $target.is("i.fa-search-plus")) {
                    _growImage($item);
                } else if ($target.is("a.i-control.move") || $target.is("i.fa-arrows")) {
                    _moveImage($item);
                } else if ($target.is("a.i-control.shrink") || $target.is("i.fa-search-minus")) {
                    _shrinkImage($item);
                } else if ($target.is("a.i-control.cancel") || $target.is("i.fa-ban")) {
                    _cancelMove($item);
                }
                return false;
            });
        }

        function _cancelMove($item) {
            $('.editor-pic-host').removeClass('move-active').unbind('click');
            $item.removeClass('move-active');
            $('.image-widget .image-drop').animate({ height: '0px' }, 200, 'linear', function () {
                $(this).hide()
            }).unbind('click');;
        }

        function _moveImage($item) {
            $('.move-active').removeClass('move-active');
            $item.addClass('move-active');

            if (!$item.attr('data-gal')) {
                $('.image-widget .image-drop').show().animate({ height: '128px' }, 200, 'linear', function () {
                    $(this).find('.fa-sign-in').fadeIn(200);
                }).unbind('click').click(function () {
                    doClick($item, 'gal');
                });
            }
            $('.editor-pic-host:not(:has(.editor-image)):not(.locked)').addClass('move-active').click(function () {
                doClick($item, $(this).attr('data-id'));
            });

            function doClick($item, position) {
                $('.editor-pic-host').removeClass('move-active').unbind('click');
                $item.removeClass('move-active');

                //Set Attribute in koModel
                var name = $item.attr('data-name');

                _model.images().forEach(function (i) {
                    if (i.value() == name) {
                        i.position(position);
                    }
                });

                setTimeout(function () {
                    _setImageClick($('.editor-image'));
                }, 20);

                $('.image-widget .image-drop').animate({ height: '0px' }, 200, 'linear', function () {
                    $(this).hide()
                });
            }
        }

        function _growImage($item) {
            $item.draggable({ disabled: true });
            var $parent = $($item[0].parentNode);
            var $sibling = $('#pic-' + $parent.attr('data-sibling'));

            if ($sibling.find('.editor-image').length == 0) {

                var newParentHeight = (_getRatio($('img', $item)) * 450) + 16;

                _sizeChange($parent, 466, newParentHeight, 0, true);

                $('img', $item).animate({ width: 450 }, 200, 'linear', function () {
                    $('.i-control.grow', $item).hide();
                    $('.i-control.shrink', $item).show();
                });
                var name = $item.attr('data-name');


                _model.images().forEach(function (i) {
                    if (i.value() == name) {
                        i.size('l');
                    }
                });
            } else {
                app.error("Unable to comply, a large image may not have a sibling");
            }
        }

        function _sizeChange($parent, pWidth, pHeight, sWidth, notDroppable) {
            $parent.animate({ width: pWidth, height: pHeight }, 222);
            $('.editor-text-host').animate({ minHeight: pHeight }, 222);

            var $sibling = $('#pic-' + $parent.attr('data-sibling'));

            if (!notDroppable) { $sibling.css('min-width', ''); }
            else { $sibling.css('min-width', '0'); }

            var $siblingParent = $($sibling[0].parentNode);
            $sibling.animate({ width: sWidth }, 200, function () { if (!notDroppable) { $sibling.css('width', '').removeClass('locked'); } else { $sibling.addClass('locked'); } });
            $siblingParent.animate({ width: sWidth }, 200, function () { if (!notDroppable) { $siblingParent.css('width', '').removeClass('locked'); } else { $siblingParent.addClass('locked'); } });
        }

        function _shrinkImage($item) {
            $item.draggable({ disabled: false });
            var $parent = $($item[0].parentNode);

            var newParentHeight = (_getRatio($('img', $item)) * 165) + 16;

            _sizeChange($parent, 181, newParentHeight, 40, false);

            $('img', $item).animate({ width: 165 }, 200, 'linear', function () {
                $('.i-control.grow', $item).show();
                $('.i-control.shrink', $item).hide();
            });
            var name = $item.attr('data-name');
            _model.images().forEach(function (i) {
                if (i.value() == name) {
                    i.size('s');
                }
            });
        }

        function _getRatio(img) {
            return img.height() / img.width();
        }

        return {
            init: function () {
                htmlStripper.setvalues();
                _setImageClick(".editor-image");

                function _do() {
                    _setImageClick(".editor-image");
                }

                app.off('fileupload:uploadcomplete', _do)
                app.on('fileupload:uploadcomplete', _do)
            }
        }
    })();

    var _editorMenu = (function() {

        function _show(event, menu) {
            $('body').append(menu);
            menu.css({ top: (event.pageY + 5), left: (event.pageX - 120), display: 'none' });
            menu.mouseleave(function () {
                $(this).remove();
            });
            menu.show();
        }

        function _base(content) {
            var menu = $('<div class="context-menu"></div>');
            var ul = $('<ul></ul>').append(content);
            menu.append(ul);
            return menu;
        }

        function _removeItem(position, type) {
            var item = $('<li class="remove-item context-item" data-fn="delete">Remove<span class="fa fa-trash"></span></li>');
            return item;
        }

        return {
            editableDiv: function (event, position) {
                var content = $('<div></div>')
                    .append($('<li data-fn="edit" class="context-item">Edit Html</li>'))
                    .append($('<li data-fn="strip" class="context-item">Strip Html</li>'))
                    .append(_removeItem(position, 'article'));

                var menu = _base($(content.html()));
                $('.context-item', menu).click(function () {
                    var fn = $(this).attr('data-fn');
                    switch (fn) {
                        case "edit":
                            var part = $('#editor-part-' + position);
                            var html = $('div[data-type="editablediv"]', part).html();

                            var popup = $('<div id="html-' + position + '" class="html-editor"><textarea></textarea><div style="clear:both"></div><div class="controls">' +
                                            '<input type="button" value="OK" class="tk-input button ok" style="float:right;" />' +
                                            '<input type="button" value="Cancel" class="tk-input button cancel" style="float:right;" /></div><div style="clear:both"></div></div>');

                            $('textarea', popup).html(html);

                            $('.ok', popup).click(function () {
                                $('div[data-type="editablediv"]', part).html($('textarea', '#html-' + position).val());
                                $('div[data-type="editablediv"]', part).blur();
                                $.magnificPopup.close();
                            });
                            $('.cancel', popup).click(function () { $.magnificPopup.close(); });

                            $('#html-editor-inner').html(popup);

                            $.magnificPopup.open({
                                items: {
                                    src: '#html-editor',
                                    type: 'inline'
                                },
                                mainClass: 'mfp-fade',
                                removalDelay: 300
                            });
                            break;
                        case "strip":
                            var part = $('#editor-part-' + position);
                            htmlStripper.unformat($('div[data-type="editablediv"]', part));
                            break;
                        case "delete":
                            _model.removeItem(position);
                            break;
                    }
                });
                _show(event, menu);
            },
            codeSnippet: function (event, position) {
                var content = $('<div></div>')
                    .append($('<li data-class="cpp" class="context-item">C++</li>'))
                    .append($('<li data-class="c#" class="context-item">C#</li>'))
                    .append($('<li data-class="css" class="context-item">CSS</li>'))
                    .append($('<li data-class="java" class="context-item">Java</li>'))
                    .append($('<li data-class="js" class="context-item">JavaScript</li>'))
                    .append($('<li data-class="php" class="context-item">PHP</li>'))
                    .append($('<li data-class="text" class="context-item">Plain Text</li>'))
                    .append($('<li data-class="py" class="context-item">Python</li>'))
                    .append($('<li data-class="sql" class="context-item">SQL</li>'))
                    .append($('<li data-class="vb" class="context-item">VB</li>'))
                    .append($('<li data-class="xml" class="context-item">XML/HTML</li>'))
                    .append($('<li data-fn="edit" class="context-item">Edit Html</li>'))
                    .append(_removeItem(position, 'codesnippet'));

                var menu = _base($(content.html()));
                $('.context-item', menu).click(function () {
                    var fn = $(this).attr('data-fn');
                    if (fn === 'delete') {
                        _model.removeItem(position);
                        return;
                    }

                    if (fn == 'edit') {
                        var part = $('#editor-part-' + position);
                        var html = $('div[data-type="codesnippet"]', part).html();

                        var popup = $('<div id="html-' + position + '" class="html-editor"><textarea></textarea><div style="clear:both"></div><div class="controls">' +
                                        '<input type="button" value="OK" class="tk-input button ok" style="float:right;" />' +
                                        '<input type="button" value="Cancel" class="tk-input button cancel" style="float:right;" /></div><div style="clear:both"></div></div>');

                        $('textarea', popup).html(html);

                        $('.ok', popup).click(function () {
                            $('div[data-type="codesnippet"]', part).html($('textarea', '#html-' + position).val());
                            $('div[data-type="codesnippet"]', part).blur();
                            $.magnificPopup.close();
                        });
                        $('.cancel', popup).click(function () { $.magnificPopup.close(); });

                        $('#html-editor-inner').html(popup);

                        $.magnificPopup.open({
                            items: {
                                src: '#html-editor',
                                type: 'inline'
                            },
                            mainClass: 'mfp-fade',
                            removalDelay: 300
                        });

                        return;
                    }

                    var part = $('#editor-part-' + position),
                        brush = 'brush: ' + $(this).attr('data-class'),
                        text = $(this).html();
                    $('.watermark', part).html(text);

                    _model.content()[position].cssClass(brush);
                });
                _show(event, menu);
            },
            singleImage: function (event, position) {
                var menu = _base(_removeItem(position, 'singleimage'));
                $('.context-item', menu).click(function () {
                    var fn = $(this).attr('data-fn');
                    if (fn === 'delete') {
                        _model.removeItem(position);
                        return;
                    }
                });
                _show(event, menu);
            },
            embededVideo: function (event, position) {
                var content = $('<div></div>')
                    .append($('<li data-class="youtube" class="context-item">YouTube</li>'))
                    .append($('<li data-class="vimeo" class="context-item">Vimeo</li>'))
                    .append(_removeItem(position, 'embededvideo'));

                var menu = _base($(content.html()));
                $('.context-item', menu).click(function () {
                    var fn = $(this).attr('data-fn');
                    if (fn === 'delete') {
                        _model.removeItem(position);
                        return;
                    }

                    var part = $('#editor-part-' + position),
                        cssclass = 'video ' + $(this).attr('data-class'),
                        text = $(this).html();

                    _model.content()[position].cssClass(cssclass);
                });
                _show(event, menu);
            }
        };
    })();

    function _lightbox(delegate) {
        $('#scrollable').magnificPopup({
            type: 'image',
            delegate: delegate,
            key: 'viewImages',
            mainClass: 'mfp-move-horizontal',
            removalDelay: 300,
            gallery: {
                enabled: true, // set to true to enable gallery

                preload: [0, 2], // read about this option in next Lazy-loading section

                navigateByImgClick: true,

                arrowMarkup: '<button title="%title%" type="button" class="mfp-arrow mfp-arrow-%dir%"></button>', // markup of an arrow button

                tPrev: 'Previous (Left arrow key)', // title for left button
                tNext: 'Next (Right arrow key)', // title for right button
                tCounter: '<span class="mfp-counter">%curr% of %total%</span>' // markup of counter
            },
            callbacks: {
                beforeOpen: function () {
                    // just a hack that adds mfp-anim class to markup 
                    this.st.image.markup = this.st.image.markup.replace('mfp-figure', 'mfp-figure mfp-with-anim');
                }
            },
        });
    }

    return {
        init: function (model) {
            _model = model;

            _richEditor.init();
            _codeSnippet.init();
            _singleImage.init();
            _embededVideo.init();
            _editor.init();
        },
        richEditor: _richEditor,
        codeSnippet: _codeSnippet,
        singleImage: _singleImage,
        embededVideo: _embededVideo,
        editor: _editor,
        lightbox: _lightbox
    }

});