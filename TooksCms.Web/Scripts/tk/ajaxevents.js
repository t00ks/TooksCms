/*
 * TookCms project custom ajax events class
 * 
 * 
 * Author: Timothy Crittenden
 */

define('tk/ajaxevents', ['jquery'], function ($) {

    return {
        beforeSend: function (xhr) {
        },
        complete: function (data) {

        },
        error: function (jqXHR, textStatus, errorThrown) {
            if (console) {
                console.log(textStatus);
                console.log(errorThrown);
            }
        }
    }
});