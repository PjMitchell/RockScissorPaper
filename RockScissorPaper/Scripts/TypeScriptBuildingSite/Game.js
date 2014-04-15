/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../Lib/Api.ts" />
/// <reference path="Logger.ts"/>
var Roshambo;
(function (Roshambo) {
    var GameOptions = (function () {
        function GameOptions() {
        }
        return GameOptions;
    })();
    Roshambo.GameOptions = GameOptions;

    var CreateGameCommand = (function () {
        function CreateGameCommand() {
        }
        return CreateGameCommand;
    })();

    var Game = (function () {
        function Game() {
            this.DEFAULT_RULESETID = 1;
            this.Api = new Core.Api();
            this.logger = new Logging.DivConsoleLogger();
        }
        Game.prototype.hasFinished = function () {
            return this.Status === 5 || this.Status === 6;
        };

        Game.prototype.create = function (player1Id, player2Id, options) {
            var createGameCommand = new CreateGameCommand();
            var options = options || new GameOptions();
            createGameCommand.PlayerOneId = player1Id;
            createGameCommand.PlayerTwoId = player2Id;
            createGameCommand.RuleId = options.ruleSet || this.DEFAULT_RULESETID;

            return this.Api.post('Games', createGameCommand).then(function (response) {
                this.log('Game ' + response + ' created between player ' + createGameCommand.PlayerOneId + ' and player ' + createGameCommand.PlayerTwoId);
                var game = new Game();
                game.GameId = response;
                game.Player1Id = createGameCommand.PlayerOneId;
                game.Player2Id = createGameCommand.PlayerTwoId;
                game.RuleSet = createGameCommand.RuleId;
                return game;
            });
        };

        Game.prototype.executeMove = function (playerid, selection) {
            var inputModel = {
                PlayerId: playerid,
                GameId: this.GameId,
                Selection: selection
            };
            var selectionText = this.getSelectionText(selection);

            this.log('player ' + playerid + ' selects ' + selectionText);

            return this.Api.put('Games', this.GameId, inputModel).then(function (response) {
                this.Status = response.Status;
                if (this.hasFinished()) {
                    this.log('Game ' + this.GameId + ' Complete');
                }
            });
        };

        Game.prototype.getSelectionText = function (selectionInt) {
            switch (selectionInt) {
                case 1:
                    return "Rock";
                case 2:
                    return "Scissor";
                case 3:
                    return "Paper";
            }
        };

        Game.prototype.log = function (message) {
            if (this.logger) {
                this.logger.log(message);
            }
        };
        return Game;
    })();
    Roshambo.Game = Game;
})(Roshambo || (Roshambo = {}));
//# sourceMappingURL=Game.js.map
