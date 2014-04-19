/// <reference path="../typings/jquery/jquery.d.ts" />

module Core {
    export class Api {
       private callAjax(type :string, path :string , data : any) {
            return $.ajax({
                dataType: 'json',
                type: type,
                data: data,
                url: path
            });
       }

        get(route: string, id?: any) {
            id = id || '';
            var path = '/api/' + route + '/' + id;
            return this.callAjax('GET', path, null);
        }

        put(route :string, id: any, data: any) {
            var path = '/api/' + route + '/' + id;
            return this.callAjax('PUT', path, data);
        }

         post(route: string, data : any) {
            var path = '/api/' + route;
            return this.callAjax('POST', path, data);
        }
    }

}
