/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../Lib/Api.ts" />
/// <reference path="Logger.ts"/>
module Roshambo {
    
    export class GameOptions {
        RuleSet: number;
    }

    class CreateGameCommand {
        PlayerOneId: number;
        PlayerTwoId: number;
        RuleId: number;
    }


    export class Game {
        private DEFAULT_RULESETID = 1;
        private Api = new Core.Api();
        private logger = new Logging.DivConsoleLogger();

        GameId: number;
        Player1Id: number;
        Player2Id: number;
        RuleSet: number;
        Status: number;

        hasFinished() {
            return this.Status === 5 || this.Status === 6;
        }

        create(player1Id, player2Id, options) {
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
                game.RuleSet = createGameCommand.RuleId
            return game;
            });
        }

        executeMove(playerid: number, selection: number) {
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
        }

        private getSelectionText(selectionInt: number) {
            switch (selectionInt) {
                case 1:
                    return "Rock";
                case 2:
                    return "Scissor";
                case 3:
                    return "Paper";
            }
        }

        private log(message: string) {
            if (this.logger) {
                this.logger.log(message);
            }
        }

    }
}