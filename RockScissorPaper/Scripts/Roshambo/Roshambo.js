"use strict";

window.Roshambo = (function ($) {

    var _gameId,
        _playerId;

    function init(gameId, playerId) {
        $('#playerSelections').on('click', 'button', function (e) {
            var $button = $(this),
                selection = $button.attr('data-selection');
            processSelection(selection);
        });
        _gameId = gameId;
        _playerId = playerId;
    }
    
    function setCurrentSelection($divToUpdate, data) {
        switch (data) {
            case 1:
                data = 'Rock';
                break;
            case 2:
                data = 'Scissor';
                break;
            case 3:
                data = 'Paper';
                break;
            case 0:
                data = 'Ready';
                break;
        }

        $divToUpdate.html(data);
    }

    function processSelection(input) {
        var inputModel = { playerId: _playerId, selection: input };

        $.ajax({
            dataType: 'json',
            url: '/api/Games/' + _gameId,
            data: inputModel,
            type: 'PUT',
            success: function (data) {
                var $banner = $('#gameBannerMessage'),
                    $p1Selection = $('#p1Selection'),
                    $p2Selection = $('#p2Selection'),
                    $p1Score = $('#p1Score'),
                    $p2Score = $('#p2Score'),
                    $userMessage = $('#userMessage'),
                    $gameRounds = $('#GameRounds');

                $banner.html(data.BannerMessage);
                setCurrentSelection($p1Selection, data.PlayerOne.CurrentSelection);
                setCurrentSelection($p2Selection, data.PlayerTwo.CurrentSelection);
                $p1Score.html(data.PlayerOne.CurrentScore);
                $p2Score.html(data.PlayerTwo.CurrentScore);
                if (data.PlayerOne.IsViewer) {
                    $userMessage.html(data.PlayerOne.PlayerMessage);
                } else if (data.PlayerTwo.IsViewer) {
                    $userMessage.html(data.PlayerTwo.PlayerMessage);
                }
                $gameRounds.html(data.RoundMessage);
            }

        });
        
    }

    return {
        init: init
    }
})($);



