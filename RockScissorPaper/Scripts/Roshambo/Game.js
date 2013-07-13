'use strict'
window.Models = window.Models || {};
window.Models.Game = (function ($, api, logger) {

    var DEFAULT_RULESETID = 1;

    function Game(data) {
        $.extend(this, data, {
            status: 0
        });
        
        if (typeof (this.executeMove) == 'undefined') {
            Game.prototype.executeMove = function (playerid, selection) {
                var me = this,
                    inputModel = {
                    PlayerId: playerid,
                    GameId: me.gameId,
                    Selection: selection
                    };

                log('player ' + playerid + ' selects ' + selection);

                return api.put('Games', this.gameId, inputModel).then(function (response) {
                    me.status = response.Status;
                    if (me.hasFinished()) {
                        log('Game ' + me.gameId + ' Complete');
                    }
                });
            }
        }
        if (typeof (this.hasFinished) == 'undefined') {
            Game.prototype.hasFinished = function () {
                return this.status === 5 || this.status === 6;
            }
        }
    }

    function create (player1Id, player2Id, options) {
        var createGameCommand = {};

        options = options || {};
        createGameCommand.PlayerOneId = player1Id;
        createGameCommand.PlayerTwoId = player2Id;
        createGameCommand.RuleId = options.ruleSet || DEFAULT_RULESETID;

        return api.post('Games', createGameCommand).then(function (response) {
            log('Game ' + response + ' created between player ' + createGameCommand.PlayerOneId + ' and player ' + createGameCommand.PlayerTwoId);
            return new Game({
                gameId: response,
                player1Id: createGameCommand.PlayerOneId,
                player2Id: createGameCommand.PlayerTwoId,
                ruleSet: createGameCommand.RuleId
            });
        });
    }

    function log(message) {
        if (logger) {
            logger.log(message);
        }
    }


    // PUBLIC
    return {
        create: create
    }

})($, Api, ConsoleLogger);