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

    function restoreButtons() {
        var $buttonbox = $('#playerOptions');
        $buttonbox.children('button').each(function () {
            var $this = $(this),
                 text = $this.attr('data-selectionName');
            $this.removeAttr('disabled', 'disabled');
            $this.html(text);
        });
    }

    function removeButtons() {
        var $buttonbox = $('#playerOptions');
        $buttonbox.children('button').each(function () {
            var $this = $(this);
            $this.remove();
        });
    }
    
    function setCurrentSelection($divToUpdate, data) {
        switch (data) {
            case 1:
                data = '<img src="/Content/Images/Selections/Rock.png"/>';
                break;
            case 2:
                data = '<img src="/Content/Images/Selections/Scissor.png"/>';
                break;
            case 3:
                data = '<img src="/Content/Images/Selections/Paper.png"/>';
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
            success: function (data) { processResult(data); } });
        $buttonbox.children('button').attr('disabled', 'disabled').html('<img src="/Content/Images/ajax-loader.gif"/>');
        
    }
    
    function processResult(data) {
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
        if (data.Status === 5) {
            removeButtons();
            setTimeout(function () { processSelection(1) }, 3000);
        }
        else {
            restoreButtons();
        }
        new function(){flashRoundResult(data)};
    }


    function flashRoundResult(data) {
        var $p1Box = $('#playerOne'),
            $p2Box = $('#playerTwo'),
            $centerBox = $('#displayCenter');

        if(data.PlayerOne.RoundOutcome ===2)
        {
            //alert("Draw");
        }
        else if (data.PlayerOne.RoundOutcome ===1)
        {
            //alert("Player One Lose!");
        }
        else if (data.PlayerTwo.RoundOutcome ===1)
        {
            //alert("Player Two Lose!");
        }
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



