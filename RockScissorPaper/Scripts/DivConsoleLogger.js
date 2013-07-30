'use strict'
window.ConsoleLogger = (function ($) {

    var $console;

    function log(message) {
        $console.prepend('<p>'+message+'</p>')
        $console.find("p").slice(100).remove();
    }

    function init() {
        $console = $('#console');
    }

    return {
        log: log,
        init: init
    }
})($);