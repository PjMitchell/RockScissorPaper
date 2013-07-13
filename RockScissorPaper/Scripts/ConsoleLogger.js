'use strict'
window.ConsoleLogger = (function ($) {

    function log(message) {
        console.log(message);
    }

    return {
        log : log
        //things to return
    }
})($);