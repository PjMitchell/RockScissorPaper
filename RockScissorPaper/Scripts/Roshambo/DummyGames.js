'use strict'
window.DummyGame = (function ($, api, logger) {
    // Create player

    function createPlayer(name) {
        var data = {
            PlayerName: name
        };
        return api.post('Players', data);
        
    }

    function getBotId() {
        return api.get('Players');
    }

    function startDummyGame() {
        
        $.when(createPlayer('bob'), getBotId()).done(function (playerId, bot) {
            var gameCommand = {
                PlayerOneId: playerId[0],
                PlayerTwoId: bot[0].PlayerId,
                RuleId: 1
            };
            api.post('Games', gameCommand)
                .done(function (response) {
                    var moveCommand = {
                        Player: gameCommand.PlayerOneId,
                        GameId: response
                    };
                    if (logger) {
                        logger.log('Game Started');
                    }
                    makeMove(moveCommand);
                });
        });
    }

    function makeMove(command) {
        //checks if game over then makes move
        var selection = Math.floor((Math.random() * 3) + 1),
            inputModel = {
                PlayerId: command.Player,
                GameId: command.GameId,
                Selection: selection
            };
        if (logger) {
            logger.log('Player Selected ' + selection);
        }
        api.put('Games', command.GameId, inputModel).done(function (response) {
            if (response.Status !== 5 && response.Status !== 6) {
                
                makeMove(command);
            }
            else {
                if (logger) {
                    logger.log('Game is over');
                }
            }
        });
    }
    
    // Create Game need bot id player id Rule id
    return {
        startDummyGame: startDummyGame
    }
})($, Api ,ConsoleLogger);