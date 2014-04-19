var Roshambo;
(function (Roshambo) {
    (function (Models) {
        /// <reference path="../typings/jquery/jquery.d.ts" />
        /// <reference path="../typings/underscore/underscore.d.ts" />
        /// <reference path="../Lib/Api.ts" />
        /// <reference path="../Logger.ts"/>
        /// <reference path="SimulatedGameSession.ts"/>
        (function (GameSimulator) {
            var Session = Models.SimulatedGameSession.Session;
            var _sessions = [], logger = new Logging.DivConsoleLogger();
            function start(instances) {
                while (instances < _sessions.length) {
                    var item = _sessions.pop();
                    item.stop();
                }
                while (instances > _sessions.length) {
                    var session = new Session();
                    _sessions.push(session);
                    session.Start();
                }
            }
            GameSimulator.start = start;

            function stop() {
                var defArray = [], $stopdeferred = $.Deferred();
                log("Games Stopping");

                _.each(_sessions, function (session) {
                    defArray.push(session.Stop());
                });

                $.when.apply($, defArray).done(function () {
                    log("All games Stopped");
                    $stopdeferred.resolve();
                });
                _sessions = [];

                return $stopdeferred.promise();
            }
            GameSimulator.stop = stop;

            function log(message) {
                if (logger) {
                    logger.log(message);
                }
            }
        })(Models.GameSimulator || (Models.GameSimulator = {}));
        var GameSimulator = Models.GameSimulator;
    })(Roshambo.Models || (Roshambo.Models = {}));
    var Models = Roshambo.Models;
})(Roshambo || (Roshambo = {}));
//# sourceMappingURL=GamesSimulatorService.js.map
