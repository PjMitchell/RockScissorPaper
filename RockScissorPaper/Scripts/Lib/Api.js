'use strict'
window.Api = (function ($) {

    function get(route, id) {
        id = id || '';
        var path = '/api/' + route + '/' + id;
        return callAjax('GET', path);
    }

    function put(route, id, data) {
        var path = '/api/' + route + '/' + id;
        return callAjax('PUT', path, data);
    }

    function post(route, data) {
        var path = '/api/' + route;
        return callAjax('POST', path, data);
    }

    function callAjax(type, path, data) {
        return $.ajax({
            dataType: 'json',
            type: type,
            data: data,
            url: path
        });
    }

    return {

        get: get,
        put: put,
        post: post
    }
})($);