'use strict'
window.ConsoleLogger = (function ($) {

    var $console;

    function log(message) {
        $console.prepend('<p>'+message+'</p>')
        console.log(message);
    }

    function init() {
        $console = $('#console');
    }

    return {
        log: log,
        init: init
    }
})($);