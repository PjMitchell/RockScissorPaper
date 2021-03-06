﻿/// <reference path="../typings/jquery/jquery.d.ts" />
var Core;
(function (Core) {
    var Api = (function () {
        function Api() {
        }
        Api.prototype.callAjax = function (type, path, data) {
            return $.ajax({
                dataType: 'json',
                type: type,
                data: data,
                url: path
            });
        };

        Api.prototype.get = function (route, id) {
            id = id || '';
            var path = '/api/' + route + '/' + id;
            return this.callAjax('GET', path, null);
        };

        Api.prototype.put = function (route, id, data) {
            var path = '/api/' + route + '/' + id;
            return this.callAjax('PUT', path, data);
        };

        Api.prototype.post = function (route, data) {
            var path = '/api/' + route;
            return this.callAjax('POST', path, data);
        };
        return Api;
    })();
    Core.Api = Api;
})(Core || (Core = {}));
//# sourceMappingURL=Api.js.map
