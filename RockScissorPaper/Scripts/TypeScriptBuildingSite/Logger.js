/// <reference path="../typings/jquery/jquery.d.ts" />
var Logging;
(function (Logging) {
    var DivConsoleLogger = (function () {
        function DivConsoleLogger() {
            this.$console = $('#console');
        }
        DivConsoleLogger.prototype.log = function (message) {
            this.$console.prepend('<p>' + message + '</p>');
            this.$console.find("p").slice(100).remove();
        };
        return DivConsoleLogger;
    })();
    Logging.DivConsoleLogger = DivConsoleLogger;
})(Logging || (Logging = {}));
//# sourceMappingURL=Logger.js.map
