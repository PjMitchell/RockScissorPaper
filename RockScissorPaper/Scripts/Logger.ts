/// <reference path="typings/jquery/jquery.d.ts" />
module Logging {
        export interface Logger {
            log(message :string);
        }

        export class DivConsoleLogger {
            private $console

            constructor() {
                this.$console = $('#console');
            }

            log(message: string) {
                this.$console.prepend('<p>' + message + '</p>')
                this.$console.find("p").slice(100).remove();
            }
        }
} 