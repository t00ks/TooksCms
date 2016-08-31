define('htmlStripper', ['jquery'], function ($) {

    var _rchars = null,
        _hents = [],
        _tags = null,
        _rempty = null,
        _aattr = [],
        _dctags = null,
        _quotes = null,
        _btags = null,
        _text = '',
        _rtext = '';


    function _unformat($e, istimeout) {
        if (!istimeout) {
            setTimeout(function () { _unformat($e, true); }, 20);
        } else {
            $e.html(_cleanHTML($e.html()));
        }
    }

    function _cleanHTML(txt, allTags, styling) {

        if (styling === true) { _aattr['span'] = []; }
        else { _aattr['span'] = ['class']; }
        if (allTags === true) { _tags = ['br']; }
        else { _tags = ['br']; }

        _text = txt;
        _rtext = '';

        var i;
        //Replace all whitespace characters with spaces
        _text = _text.replace(/(\s|&nbsp;)+/g, ' ');
        //replace weird word characters
        for (i = 0; i < _rchars[0].length; i++)
            _text = _text.replace(new RegExp(_rchars[0][i], 'g'), _rchars[1][i]);

        //initialize flags
        //what the next character is expected to be
        var expected = '';
        //tag text
        var tag = '';
        //tag name
        var tagname = '';
        //what type of tag it is, start, end, or single
        var tagtype = 'start';
        //attribute text
        var attribute = '';
        //attribute name
        var attributen = '';
        //if the attribute has had an equals sign
        var attributeequals = false;
        //if attribute has quotes, and what they are
        var attributequotes = '';

        var c = '';
        var n = '';

        /*Parser format:
        The parser is divided into three parts:
        The first section is for when the current type of character is known
        The second is for when it is an unknown character in a tag
        The third is for anything outside of a tag
        */

        //editing pass
        for (i = 0; i < _text.length; i++) {
            //current character
            c = _getc(i);
            //next character
            n = _getc(i + 1);

            //***Section for when the current character is known

            //if the tagname is expected
            if (expected == 'tagname') {
                tagname += c.toLowerCase();
                //lookahead for end of tag name
                if (n == ' ' || n == '>' || n == '/') {
                    tag += tagname;
                    expected = 'tag';
                }
            }
                //if an attribute name is expected
            else if (expected == 'attributen') {
                attributen += c.toLowerCase();
                //lookahead for end of attribute name
                if (n == ' ' || n == '>' || n == '/' || n == '=') {
                    attribute += attributen;
                    //check to see if its an attribute without an assigned value
                    //determines whether there is anything but spaces between the attribute name and the next equals sign
                    if (_endOfAttr(i)) {
                        //if the attribute is allowed, add it to the output
                        if (_ae(attributen, _aattr[tagname]))
                            tag += attribute;

                        attribute = '';
                        attributen = '';
                        attributeequals = false;
                        attributequotes = '';
                    }
                    expected = 'tag';
                }
            }
                //if an attribute value is expected
            else if (expected == 'attributev') {
                attribute += c;

                //lookahead for end of value
                if ((c == attributequotes) || ((n == ' ' || n == '/' || n == '>') && !attributequotes)) {
                    //if the attribute is allowed, add it to the output
                    if (_ae(attributen, _aattr[tagname]))
                        tag += attribute;

                    attribute = '';
                    attributen = '';
                    attributeequals = false;
                    attributequotes = '';

                    expected = 'tag';
                }
            }

                //***Section for when the character is unknown but it is inside of a tag

            else if (expected == 'tag') {
                //if its a space
                if (c == ' ')
                    tag += c;
                    //if its a slash after the tagname, signalling a single tag.
                else if (c == '/' && tagname) {
                    tag += c;
                    tagtype = 'single';
                }
                    //if its a slash before the tagname, signalling its an end tag
                else if (c == '/') {
                    tag += c;
                    tagtype = 'end';
                }
                    //if its the end of a tag
                else if (c == '>') {
                    tag += c;
                    //if the tag is allowed, add it to the output
                    if (_ae(tagname, _tags))
                        _rtext += tag;

                    //if its a start tag
                    if (tagtype == 'start') {
                        //if the tag is supposed to have its contents deleted
                        if (_ae(tagname, _dctags)) {
                            //if there is an end tag, skip to it in order to delete the tags contents
                            if (-1 != (endpos = _text.indexOf('</' + tagname, i))) {
                                //have to make it one less because i gets incremented at the end of the loop
                                i = endpos - 1;
                            }
                            //if there isn't an end tag, then it was probably a non-compliant single tag
                        }
                    }

                    tag = '';
                    tagname = '';
                    tagtype = 'start';
                    expected = '';
                }
                    //if its an attribute name
                else if (tagname && !attributen) {
                    attributen += c.toLowerCase();
                    expected = 'attributen';
                    //lookahead for end of attribute name, in case its a one character attribute name
                    if (n == ' ' || n == '>' || n == '/' || n == '=') {
                        attribute += attributen;
                        //check to see if its an attribute without an assigned value
                        //determines whether there is anything but spaces between the attribute name and the next equals sign
                        if (_endOfAttr(i)) {
                            //if the attribute is allowed, add it to the output
                            if (_ae(attributen, attributen))
                                tag += attribute;

                            attribute = '';
                            attributen = '';
                            attributeequals = false;
                            attributequotes = '';
                        }
                        expected = 'tag';
                    }
                }
                    //if its a start quote for an attribute value
                else if (_ae(c, _quotes) && attributeequals) {
                    attribute += c;
                    attributequotes = c;
                    expected = 'attributev';
                }
                    //if its an attribute value
                else if (attributeequals) {
                    attribute += c;
                    expected = 'attributev';

                    //lookahead for end of value, in case its only one character
                    if ((c == attributequotes) || ((n == ' ' || n == '/' || n == '>') && !attributequotes)) {
                        //if the attribute is allowed, add it to the output
                        if (_ae(attributen, attributen))
                            tag += attribute;

                        attribute = '';
                        attributen = '';
                        attributeequals = false;
                        attributequotes = '';

                        expected = 'tag';
                    }
                }
                    //if its an attribute equals
                else if (c == '=' && attributen) {
                    attribute += c;
                    attributeequals = true;
                }
                    //if its the tagname
                else {
                    tagname += c.toLowerCase();
                    expected = 'tagname';

                    //lookahead for end of tag name, in case its a one character tag name
                    if (n == ' ' || n == '>' || n == '/') {
                        tag += tagname;
                        expected = 'tag';
                    }
                }
            }
                //if nothing is expected
            else {
                //if its the start of a tag
                if (c == '<') {
                    tag = c;
                    expected = 'tag';
                }
                    //anything else
                else
                    _rtext += _htmlentities(c);
            }
        }
        //beautifying regexs
        //remove duplicate spaces
        _rtext = _rtext.replace(/\s+/g, ' ');
        //remove unneeded spaces in tags
        _rtext = _rtext.replace(/\s>/g, '>');
        //remove empty tags
        //this loops until there is no change from running the regex
        var remptys = _rempty.join('|');
        var oo = _rtext;
        while ((_rtext = _rtext.replace(new RegExp("\\s?<(" + remptys + ")>\s*<\\/\\1>", 'gi'), '')) != oo)
            oo = _rtext;
        //make block tags regex string
        var btagss = _btags.join('|');
        //add newlines after block tags
        _rtext = _rtext.replace(new RegExp("\\s?</(" + btagss + ")>", 'gi'), "</$1>\n");
        //remove spaces before block tags
        _rtext = _rtext.replace(new RegExp("\\s<(" + btagss + ")", 'gi'), "<$1");

        //fix lists
        _rtext = _rtext.replace(/((<p.*>\s*(&middot;|&#9642;) .*<\/p.*>\n)+)/gi, "<ul>\n$1</ul>\n"); //make ul for dot lists
        _rtext = _rtext.replace(/((<p.*>\s*\_text+\S*\. .*<\/p.*>\n)+)/gi, "<ol>\n$1</ol>\n"); //make ol for numerical lists
        _rtext = _rtext.replace(/((<p.*>\s*[a-z]+\S*\. .*<\/p.*>\n)+)/gi, "<ol style=\"list-style-type: lower-latin;\">\n$1</ol>\n"); //make ol for latin lists
        _rtext = _rtext.replace(/<p(.*)>\s*(&middot;|&#9642;|\_text+(\S*)\.|[a-z]+\S*\.) (.*)<\/p(.*)>\n/gi, "\t<li$1>$3$4</li$5>\n"); //make li

        //extend outer lists around the nesting lists
        _rtext = _rtext.replace(/<\/(ul|ol|ol style="list-style-type: lower-latin;")>\n(<(?:ul|ol|ol style="list-style-type: lower-latin;")>[\s\S]*<\/(?:ul|ol|ol style="list-style-type: lower-latin;")>)\n(?!<(ul|ol|ol style="list-style-type: lower-latin;")>)/g, "</$1>\n$2\n<$1>\n</$1>\n");

        //nesting lists
        _rtext = _rtext.replace(/<\/li>\s+<\/ol>\s+<ul>([\s\S]*?)<\/ul>\s+<ol>/g, "\n<ul>$1</ul></li>"); //ul in ol
        _rtext = _rtext.replace(/<\/li>\s+<\/ol>\s+<ol style="list-style-type: lower-latin;">([\s\S]*?)<\/ol>\s+<ol>/g, "\n<ol style=\"list-style-type: lower-latin;\">$1</ol></li>"); //latin in ol
        _rtext = _rtext.replace(/<\/li>\s+<\/ul>\s+<ol>([\s\S]*?)<\/ol>\s+<ul>/g, "\n<ol>$1</ol></li>"); //ol in ul
        _rtext = _rtext.replace(/<\/li>\s+<\/ul>\s+<ol style="list-style-type: lower-latin;">([\s\S]*?)<\/ol>\s+<ul>/g, "\n<ol style=\"list-style-type: lower-latin;\">$1</ol></li>"); //latin in ul
        _rtext = _rtext.replace(/<\/li>\s+<\/ol>\s+<ol style="list-style-type: lower-latin;">([\s\S]*?)<\/ol>\s+<ol>/g, "\n<ol style=\"list-style-type: lower-latin;\">$1</ol></li>"); //ul in latin
        _rtext = _rtext.replace(/<\/li>\s+<\/ul>\s+<ol style="list-style-type: lower-latin;">([\s\S]*?)<\/ol>\s+<ul>/g, "\n<ol style=\"list-style-type: lower-latin;\">$1</ol></li>"); //ul in latin
        //remove empty tags. this is needed a second time to delete empty lists that were created to fix nesting, but weren't needed
        _rtext = _rtext.replace(new RegExp("\\s?<(" + remptys + ")>\s*<\\/\\1>", 'gi'), '');

        return _rtext;
    }


    //array equals
    //loops through all the elements of an array to see if any of them equal the test.
    function _ae(needle, haystack) {
        if (typeof (haystack) == 'object')
            for (var i = 0; i < haystack.length; i++)
                if (needle == haystack[i])
                    return true;

        return false;
    }

    //get character
    //return specified character from _text
    function _getc(i) {
        return _text.charAt(i);
    }

    //end of attr
    //determines if their is anything but spaces between the current character, and the next equals sign
    function _endOfAttr(i) {
        var between = _text.substring(i + 1, _text.indexOf('=', i + 1));
        if (between.replace(/\s+/g, ''))
            return true;
        else
            return false;
    }

    function _htmlentities(character) {
        if (_hents[character])
            return _hents[character];
        else
            return character;
    }

    return {
        unformat: _unformat,

        setvalues: function () {
            _rchars = [["Ã±", "Ã³", "Ã«", "Ã­", "Ã¬", "Ã®", 'â€ '], ["-", "-", "'", "'", '"', '"', ' ']];

            //html entities translation array

            _hents['Â°'] = '&iexcl;';
            _hents['Â¢'] = '&cent;';
            _hents['Â£'] = '&pound;';
            _hents['Â§'] = '&curren;';
            _hents['â€¢'] = '&yen;';
            _hents['Â¶'] = '&brvbar;';
            _hents['ÃŸ'] = '&sect;';
            _hents['Â®'] = '&uml;';
            _hents['Â©'] = '&copy;';
            _hents['â„¢'] = '&ordf;';
            _hents['Â´'] = '&laquo;';
            _hents['Â¨'] = '&not;';
            _hents['â‰ '] = '&shy;';
            _hents['Ã†'] = '&reg;';
            _hents['Ã˜'] = '&macr;';
            _hents['âˆž'] = '&deg;';
            _hents['Â±'] = '&plusmn;';
            _hents['â‰¤'] = '&sup2;';
            _hents['â‰¥'] = '&sup3;';
            _hents['Â¥'] = '&acute;';
            _hents['Âµ'] = '&micro;';
            _hents['âˆ‚'] = '&para;';
            _hents['âˆ‘'] = '&middot;';
            _hents['âˆ'] = '&cedil;';
            _hents['Ï€'] = '&sup1;';
            _hents['âˆ«'] = '&ordm;';
            _hents['Âª'] = '&raquo;';
            _hents['Âº'] = '&frac14;';
            _hents['Î©'] = '&frac12;';
            _hents['Ã¦'] = '&frac34;';
            _hents['Ã¸'] = '&iquest;';
            _hents['Â¿'] = '&Agrave;';
            _hents['Â¡'] = '&Aacute;';
            _hents['Â¬'] = '&Acirc;';
            _hents['âˆš'] = '&Atilde;';
            _hents['Æ’'] = '&Auml;';
            _hents['â‰ˆ'] = '&Aring;';
            _hents['âˆ†'] = '&AElig;';
            _hents['Â«'] = '&Ccedil;';
            _hents['Â»'] = '&Egrave;';
            _hents['â€¦'] = '&Eacute;';
            _hents['Â '] = '&Ecirc;';
            _hents['Ã€'] = '&Euml;';
            _hents['Ãƒ'] = '&Igrave;';
            _hents['Ã•'] = '&Iacute;';
            _hents['Å’'] = '&Icirc;';
            _hents['Å“'] = '&Iuml;';
            _hents['â€“'] = '&ETH;';
            _hents['â€”'] = '&Ntilde;';
            _hents['â€œ'] = '&Ograve;';
            _hents['â€'] = '&Oacute;';
            _hents['â€˜'] = '&Ocirc;';
            _hents['â€™'] = '&Otilde;';
            _hents['Ã·'] = '&Ouml;';
            _hents['â—Š'] = '&times;';
            _hents['Ã¿'] = '&Oslash;';
            _hents['Å¸'] = '&Ugrave;';
            _hents['â„'] = '&Uacute;';
            _hents['â‚¬'] = '&Ucirc;';
            _hents['â€¹'] = '&Uuml;';
            _hents['â€º'] = '&Yacute;';
            _hents['ï¬'] = '&THORN;';
            _hents['ï¬‚'] = '&szlig;';
            _hents['â€¡'] = '&agrave;';
            _hents['Â·'] = '&aacute;';
            _hents['â€š'] = '&acirc;';
            _hents['â€ž'] = '&atilde;';
            _hents['â€°'] = '&auml;';
            _hents['Ã‚'] = '&aring;';
            _hents['ÃŠ'] = '&aelig;';
            _hents['Ã'] = '&ccedil;';
            _hents['Ã‹'] = '&egrave;';
            _hents['Ãˆ'] = '&eacute;';
            _hents['Ã'] = '&ecirc;';
            _hents['ÃŽ'] = '&euml;';
            _hents['Ã'] = '&igrave;';
            _hents['ÃŒ'] = '&iacute;';
            _hents['Ã“'] = '&icirc;';
            _hents['Ã”'] = '&iuml;';
            _hents['ï£¿'] = '&eth;';
            _hents['Ã’'] = '&ntilde;';
            _hents['Ãš'] = '&ograve;';
            _hents['Ã›'] = '&oacute;';
            _hents['Ã™'] = '&ocirc;';
            _hents['Ä±'] = '&otilde;';
            _hents['Ë†'] = '&ouml;';
            _hents['Ëœ'] = '&divide;';
            _hents['Â¯'] = '&oslash;';
            _hents['Ë˜'] = '&ugrave;';
            _hents['Ë™'] = '&uacute;';
            _hents['Ëš'] = '&ucirc;';
            _hents['Â¸'] = '&uuml;';
            _hents['Ë'] = '&yacute;';
            _hents['Ë›'] = '&thorn;';
            _hents['Ë‡'] = '&yuml;';
            _hents['"'] = '&quot;';
            _hents['<'] = '&lt;';
            _hents['>'] = '&gt;';

            //allowed tags
            _tags = ['p', 'h1', 'h2', 'h3', 'h4', 'h5', 'h6', 'h7', 'h8', 'ul', 'ol', 'li', 'u', 'i', 'b', 'a', 'table', 'tr', 'th', 'td', 'img', 'em', 'strong', 'br'];
            _tags = [];

            //tags which should be removed when empty
            _rempty = ['p', 'h1', 'h2', 'h3', 'h4', 'h5', 'h6', 'h7', 'h8', 'ul', 'ol', 'li', 'u', 'i', 'b', 'a', 'table', 'tr', 'em', 'strong'];

            //allowed atributes for tags
            _aattr['a'] = ['href', 'name'];
            _aattr['table'] = ['border'];
            _aattr['th'] = ['colspan', 'rowspan'];
            _aattr['td'] = ['colspan', 'rowspan'];
            _aattr['img'] = ['src', 'width', 'height', 'alt'];
            _aattr['span'] = ['class'];

            //tags who's content should be deleted
            _dctags = ['head', 'meta', 'xml', 'style'];

            //Quote characters
            _quotes = ["'", '"'];

            //tags which are displayed as a block
            _btags = ['p', 'h1', 'h2', 'h3', 'h4', 'h5', 'h6', 'h7', 'h8', 'ul', 'ol', 'li', 'table', 'tr', 'th', 'td', 'br'];
        }
    }
});