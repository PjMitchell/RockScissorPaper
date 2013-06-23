"use strict";

window.Roshambo = (function ($) {

    var _gameId,
        _playerId;

    function init(gameId, playerId) {
        $('#playerOptions').on('click', 'button', function (e) {
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
        var inputModel = { PlayerId: _playerId, GameId: _gameId, Selection: input },
            $buttonbox = $('#playerOptions');
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
                if (data.FinalRoundResult) {
                    $buttonbox.children('button').each(function () {
                        var $this = $(this);
                        $this.remove();
                    });
                    setTimeout(function () { processSelection(1) }, 3000);

                }
                else {
                    $buttonbox.children('button').each(function () {
                        var $this = $(this),
                             text = $this.attr('data-selectionName');
                        $this.removeAttr('disabled', 'disabled');
                        $this.html(text);
                    });
                }
                
            }
            
        });
        $buttonbox.children('button').attr('disabled', 'disabled').html('<img src="/Content/Images/ajax-loader.gif"/>');
        
    }

    function liveStatus() {
        // Reference the auto-generated proxy for the hub. 
        var hub = $.connection.roshamboHub,
            peopleConnectedField =$('#peoplePlayingField'),
            botWinsField = $('#botWins'),
            humanWinsField  =$('#humanWins');
        // Create a function that the hub can call back to display changes in number of players.
        hub.client.updatePeopleConnected = function (peopleConnected) {
            // Add the message to the page. 
               
            peopleConnectedField.html('People Playing :'+peopleConnected);
        };
        hub.client.refreshView = function (view) {
            peopleConnectedField.html('People Playing :'+ view.NumberOfPeopleConnected)
            botWinsField.html('Bots :'+ view.BotWins)
            humanWinsField.html('Humans :'+ view.HumanWins)
        }
        $.connection.hub.start().done(function () {
            hub.server.getInfo();
        });
    }

    return {
        init: init,
        liveStatus: liveStatus
    }
})($);



