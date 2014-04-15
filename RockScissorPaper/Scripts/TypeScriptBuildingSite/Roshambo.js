/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../Lib/Api.ts" />
/// <reference path="Logger.ts"/>
var Roshambo;
(function (_Roshambo) {
    //export class RoshamboInputCommand {
    //    PlayerId: number
    //    GameId: number
    //    Selection: number
    //    }
    var Roshambo = (function () {
        function Roshambo() {
            this.api = new Core.Api();
        }
        Roshambo.prototype.init = function (gameId, playerId) {
            $('#playerOptions').on('click', 'button', function (e) {
                var $button = $(this), selection = $button.attr('data-selection');
                this.processSelection(selection);
            });
            this.gameId = gameId;
            this.playerId = playerId;
        };

        Roshambo.prototype.processSelection = function (input) {
            var inputModel = { PlayerId: this.playerId, GameId: this.gameId, Selection: input }, $buttonbox = $('#playerOptions');

            this.api.put('Games', this.gameId, inputModel).done(function (data) {
                this.processResult(data);
            });
            $buttonbox.children('button').attr('disabled', 'disabled').html('<img src="/Content/Images/ajax-loader.gif"/>');
        };

        Roshambo.prototype.processResult = function (data) {
            var $banner = $('#gameBannerMessage'), $p1Selection = $('#p1Selection'), $p2Selection = $('#p2Selection'), $p1Score = $('#p1Score'), $p2Score = $('#p2Score'), $userMessage = $('#userMessage'), $gameRounds = $('#GameRounds');

            $banner.html(data.BannerMessage);
            this.setCurrentSelection($p1Selection, data.PlayerOne.CurrentSelection);
            this.setCurrentSelection($p2Selection, data.PlayerTwo.CurrentSelection);
            $p1Score.html(data.PlayerOne.CurrentScore);
            $p2Score.html(data.PlayerTwo.CurrentScore);
            if (data.PlayerOne.IsViewer) {
                $userMessage.html(data.PlayerOne.PlayerMessage);
            } else if (data.PlayerTwo.IsViewer) {
                $userMessage.html(data.PlayerTwo.PlayerMessage);
            }
            $gameRounds.html(data.RoundMessage);
            if (data.Status === 5) {
                this.removeButtons();
                this.flashRoundResult(data);
                setTimeout(function () {
                    this.processSelection(1);
                }, 3000);
            } else if (data.Status === 6) {
            } else {
                this.flashRoundResult(data);
            }
        };

        Roshambo.prototype.setCurrentSelection = function ($divToUpdate, data) {
            var output;
            switch (data) {
                case 1:
                    output = '/Content/Images/Selections/Rock.png';
                    break;
                case 2:
                    output = '/Content/Images/Selections/Scissor.png';
                    break;
                case 3:
                    output = '/Content/Images/Selections/Paper.png';
                    break;
                case 0:
                    output = '/Content/Images/Selections/Ready.png';
                    break;
            }

            $divToUpdate.attr('src', output);
        };

        Roshambo.prototype.removeButtons = function () {
            var $buttonbox = $('#playerOptions');
            $buttonbox.children('button').each(function () {
                var $this = $(this);
                $this.remove();
            });
        };

        Roshambo.prototype.flashRoundResult = function (data) {
            var $p1Selection = $('#p1Selection'), $p2Selection = $('#p2Selection');
            $p1Selection.css('opacity', '1');
            $p2Selection.css('opacity', '1');
            $p1Selection.animate({ "right": "-40px" }, "slow");
            $p2Selection.animate({ "left": "-40px" }, "slow");

            if (data.PlayerOne.RoundOutcome === 2) {
                $p1Selection.animate({ "right": "-40px" }, "slow").animate({ "right": "0px" }, "slow");
                $p2Selection.animate({ "left": "-40px" }, "slow").animate({ "left": "0px" }, "slow");
            } else if (data.PlayerOne.RoundOutcome === 1) {
                $p1Selection.animate({ "right": "-30px" }, "slow").animate({ "opacity": "0.15" }, "slow").animate({ "right": "0px" }, "slow");
                $p2Selection.animate({ "left": "-60px" }, 1200).animate({ "left": "0px" }, "slow");
            } else if (data.PlayerTwo.RoundOutcome === 1) {
                $p1Selection.animate({ "right": "-60px" }, 1200).animate({ "right": "0px" }, "slow");
                $p2Selection.animate({ "left": "-30px" }, "slow").animate({ "opacity": "0.15" }, "slow").animate({ "left": "0px" }, "slow");
            }

            this.restoreButtons();
        };

        Roshambo.prototype.restoreButtons = function () {
            var $buttonbox = $('#playerOptions');
            $buttonbox.children('button').each(function () {
                var $this = $(this), text = $this.attr('data-selectionName');
                $this.removeAttr('disabled');
                $this.html('<img src="/Content/Images/Selections/' + text + '.png")" height="25"/>');
            });
        };
        return Roshambo;
    })();
    _Roshambo.Roshambo = Roshambo;
})(Roshambo || (Roshambo = {}));
//# sourceMappingURL=Roshambo.js.map
