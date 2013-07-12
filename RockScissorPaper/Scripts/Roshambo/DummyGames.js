'use strict'
window.Roshambo = (function ($) {
    // Create player

    function CreatePlayer(name) {
        return $.ajax({
            dataType: 'json',
            url: '/api/Players/',
            data: name,
            type: 'POST',
            async: false
        }).responseText;
    }

    function GetBotId() {
        $.ajax({
            dataType: 'json',
            url: '/api/Players/',
            data: name,
            type: 'GET',
            async: false
        }).responseText;
    }

    function StartDummyGame() {
        var command = {},
            output = {};
        command.PlayerOneId = CreatePlayer('bob');
        command.PlayerTwoId = GetBotId();
        command.RuleId = 1;
        output.Player = command.PlayerOneId

        return $.ajax({
            dataType: 'json',
            url: '/api/Games/',
            data: name,
            type: 'POST',
            success: function (data) {
                output.gameId;
                MakeMove(output);
            }
        });

    }

    function MakeMove(command) {
        //checks if game over then makes move
        var selection = Math.floor((Math.random() * 3) + 1),
            inputModel = { PlayerId: output.Player, GameId: command.gameId, Selection: selection };
          
        $.ajax({
            dataType: 'json',
            url: '/api/Games/' + _gameId,
            data: inputModel,
            type: 'PUT',
            success: function (data) {
                if (data.Status !== 5 && data.Status !== 6) {
                    MakeMove(command);
                }
            }
        });
    }

    


    // Create Game need bot id player id Rule id
    return {
        StartDummyGame: StartDummyGame
    }
})($);