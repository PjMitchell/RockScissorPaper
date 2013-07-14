'use strict'
window.GameSimulator = (function (_, $, gameSession, logger) {

    // Vars
    var _sessions = [];
        

    // Public functions

    function start(instances) {
        while (instances < _sessions.length) {
            var item = _sessions.pop();
            item.stop();
        }
        while (instances > _sessions.length) {
            var session = gameSession.create();
            _sessions.push(session);
            session.start();
        }
    }

    function stop() {
        var defArray = []
        log("Games Stopping");

        _.each(_sessions, function(session) {
            defArray.push( session.stop());
        });
        $.when.apply(this, defArray)
            .done(function () {
                log("All games Stopped");
            });
        _sessions = [];
    }

    function log(message) {
        if (logger) {
            logger.log(message);
        }
    }
    
    // Definition
    return {
        start: start,
        stop: stop
    }
})(_, $, Models.SimulatedGameSession, ConsoleLogger);