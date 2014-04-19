/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../Lib/Api.ts" />
/// <reference path="Game.ts"/>
module Roshambo.Models.SimulatedGameSession{
    export enum GameStatus { stopped = 1, running = 2, stopping =3}
    var _allBots,
        api = new Core.Api(),
        gameRepository = new Game(),
        firstnameList = ['Susan', 'Jessica', 'Anna', 'Debra', 'Rita', 'Pepper', 'Kim', 'Vicky', 'Pam', 'Dan', 'Jason', 'Jeff', 'Eric', 'Scott', 'Dave', 'Chad', 'Steve', 'Jermaine', 'Liam', 'Richard', 'Jenny'],
        lastnameList = ['Clark', 'Ballmer', 'Wilson', 'Guthrie', 'Jackson', 'Lewis', 'Brown', 'Bailey', 'Cook', 'Bell', 'Cooper', 'Howard', 'Morris', 'Phillips', 'Parker', 'Ellis', 'Jordan', 'Tucker', 'Sims', 'Fox', 'Greene', 'Banks', 'Fuller', 'Brewer', 'Cannon', 'Hogan', 'Phelps', 'Fischer', 'Kemp'];

    function init() {
        var initDefered;
        if (!initDefered) {
            initDefered = api.get('Players').done(function (response) {
                _allBots = response;
            })
        }

        return initDefered;
    }
    
    function getBotId() :number {
        var index = Math.floor((Math.random() * _allBots.length));
        return _allBots[index];
    }

    function createPlayer() {
        var firstnameIndex = Math.floor((Math.random() * firstnameList.length)),
            lastnameIndex = Math.floor((Math.random() * lastnameList.length)),
            data = {
                PlayerName: firstnameList[firstnameIndex] + ' ' + lastnameList[lastnameIndex]
            };
        return api.post('Players', data);
    }

    function runGame(session :Session) {

    init().done(function () {
        $.when(createPlayer(), getBotId())
            .done(function (playerId, bot) {
                var options = {
                    ruleSet: 1
                };
                    gameRepository.create(playerId[0], bot.PlayerId, options)
                    .done(function (game) {
                        session.CurrentGame = game;
                        processRound(session);
                    });
            });
    });
    }

    function processRound(session : Session) {
        var selection = Math.floor((Math.random() * 3) + 1);

        session.CurrentGame.executeMove(session.CurrentGame.Player1Id, selection).done(function (response) {

            if (!session.CurrentGame.hasFinished()) {
                processRound(session);
            }
            else if (session.Status === GameStatus.running) {
                runGame(session);
            }
            else {
                session.Close();
                
            }
        });
    }

    export class Session {
        Status : GameStatus
        CurrentGame : Game
        private _onStopDef
        Stop(){
            this._onStopDef = $.Deferred();
            if(this.Status === GameStatus.running){
                this.Status = GameStatus.stopping;
            }
            else if (this.Status === GameStatus.stopped) {
                this._onStopDef.resolve();
            }
            return this._onStopDef;
        }

        Start() {
            if (this.Status === GameStatus.stopping) {
                this.Status = GameStatus.running;
            } else {
                this.Status = GameStatus.running;
                runGame(this);
            }
        }

        Close() {
            if (this._onStopDef) {
                    this._onStopDef.resolve();
                }
            this.Status = GameStatus.stopped;
        }
    }  
}


