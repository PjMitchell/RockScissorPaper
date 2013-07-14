'use strict'
window.GameSimulator = (function ($, api, gameRepository, gameSession) {

    // Vars
    var _allBots,
        _session = gameSession.create(3, runGame);
        

    // Init

    function init() {
        var initDefered;
        if (!initDefered) {
            initDefered = api.get('Players').done(function (response) {
                _allBots = response;
            })
        }

        return initDefered;
    }

    // Public functions

    function start() {
        _session.start();
    }

    function stop() {
        _session.stop();
    }

    // Private helpers

    function runGame() {
        init().done(function () {
            $.when(createPlayer(), getBotId()).done(function (playerId, bot) {
                var options = {
                    ruleSet: 1
                };
                gameRepository.create(playerId[0], bot.PlayerId, options)
                    .done(function (game) {
                        processRound(game);
                    });
            });
        });
    }

    function createPlayer() {
        var firstname = ['Billy', 'Bob', 'Boris', 'Bernard', 'Beatrix'],
            lastname = ['Smith', 'Miller', 'Baker', 'Tailor', 'Cartwright'],
            firstnameIndex = Math.floor((Math.random() * firstname.length)),
            lastnameIndex = Math.floor((Math.random() * lastname.length)),
            data = {
                PlayerName: firstname[firstnameIndex] + ' ' + lastname[lastnameIndex]
            };
        return api.post('Players', data);
    }

    function getBotId() {
        var index = Math.floor((Math.random() * _allBots.length));
        return _allBots[index];
    }

    function processRound(game) {
        //checks if game over then makes move
        var selection = Math.floor((Math.random() * 3) + 1);
            
        game.executeMove(session.game.player1Id, selection).done(function (response) {
            
            if (! session.game.hasFinished()) {
                processRound(game);
            }
            else {
                _session.checkpoint(runGame);
            }
        });
    }
    
    // Definition
    return {
        start: start,
        stop: stop
    }
})($, Api, Models.Game, Models.SimulatedGameSession);