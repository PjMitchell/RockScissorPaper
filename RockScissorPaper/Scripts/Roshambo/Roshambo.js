"use strict";

window.Roshambo = (function ($, api) {

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
            $this.html('<img src="/Content/Images/Selections/' + text + '.png")" height="25"/>');
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
                data = '/Content/Images/Selections/Rock.png';
                break;
            case 2:
                data = '/Content/Images/Selections/Scissor.png';
                break;
            case 3:
                data = '/Content/Images/Selections/Paper.png';
                break;
            case 0:
                data = '/Content/Images/Selections/Ready.png';
                break;
        }

        $divToUpdate.attr('src',data);
    }

    function processSelection(input) {
        var inputModel = { PlayerId: _playerId, GameId: _gameId, Selection: input },
            $buttonbox = $('#playerOptions');
        api.put('Games', _gameId, inputModel)
            .done(function (data) {
                processResult(data);
            });
        $buttonbox
            .children('button')
            .attr('disabled', 'disabled')
            .html('<img src="/Content/Images/ajax-loader.gif"/>');
        

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
            flashRoundResult(data);
            setTimeout(function () { processSelection(1) }, 3000);
        }
        else if (data.Status === 6) {

        }
        else {
            flashRoundResult(data);
        }
        
    }


    function flashRoundResult(data) {
        var  $p1Selection = $('#p1Selection'),
             $p2Selection = $('#p2Selection');
        $p1Selection.css('opacity', '1');
        $p2Selection.css('opacity', '1');
        $p1Selection.animate({ "right": "-40px" }, "slow");
        $p2Selection.animate({ "left": "-40px" }, "slow");

        if(data.PlayerOne.RoundOutcome ===2)
        {
            $p1Selection.animate({ "right": "-40px" }, "slow").animate({ "right": "0px" }, "slow");
            $p2Selection.animate({ "left": "-40px" }, "slow").animate({ "left": "0px" }, "slow");
        }
        else if (data.PlayerOne.RoundOutcome ===1)
        {
            $p1Selection.animate({ "right": "-30px" }, "slow").animate({ "opacity": "0.15" }, "slow").animate({ "right": "0px" }, "slow");
            $p2Selection.animate({ "left": "-60px" }, 1200).animate({ "left": "0px" }, "slow" );
        }
        else if (data.PlayerTwo.RoundOutcome ===1)
        {
            $p1Selection.animate({ "right": "-60px" }, 1200).animate({ "right": "0px" }, "slow");
            $p2Selection.animate({ "left": "-30px" }, "slow").animate({ "opacity": "0.15" }, "slow" ).animate({ "left": "0px" }, "slow" );
        }
       
        restoreButtons();
    }

    return {
        init: init
    }
})($, Api);



