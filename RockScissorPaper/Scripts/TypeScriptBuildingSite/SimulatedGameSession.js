var Roshambo;
(function (Roshambo) {
    (function (Models) {
        /// <reference path="../typings/jquery/jquery.d.ts" />
        /// <reference path="../Lib/Api.ts" />
        /// <reference path="Game.ts"/>
        (function (SimulatedGameSession) {
            (function (GameStatus) {
                GameStatus[GameStatus["stopped"] = 1] = "stopped";
                GameStatus[GameStatus["running"] = 2] = "running";
                GameStatus[GameStatus["stopping"] = 3] = "stopping";
            })(SimulatedGameSession.GameStatus || (SimulatedGameSession.GameStatus = {}));
            var GameStatus = SimulatedGameSession.GameStatus;
            var _allBots, api, gamerepository = new Models.Game(), firstnameList = ['Susan', 'Jessica', 'Anna', 'Debra', 'Rita', 'Pepper', 'Kim', 'Vicky', 'Pam', 'Dan', 'Jason', 'Jeff', 'Eric', 'Scott', 'Dave', 'Chad', 'Steve', 'Jermaine', 'Liam', 'Richard', 'Jenny'], lastnameList = ['Clark', 'Ballmer', 'Wilson', 'Guthrie', 'Jackson', 'Lewis', 'Brown', 'Bailey', 'Cook', 'Bell', 'Cooper', 'Howard', 'Morris', 'Phillips', 'Parker', 'Ellis', 'Jordan', 'Tucker', 'Sims', 'Fox', 'Greene', 'Banks', 'Fuller', 'Brewer', 'Cannon', 'Hogan', 'Phelps', 'Fischer', 'Kemp'];

            function init() {
                var initDefered;
                if (!initDefered) {
                    initDefered = api.get('Players').done(function (response) {
                        _allBots = response;
                    });
                }

                return initDefered;
            }

            function getBotId() {
                var index = Math.floor((Math.random() * _allBots.length));
                return _allBots[index];
            }

            function createPlayer() {
                var firstnameIndex = Math.floor((Math.random() * firstnameList.length)), lastnameIndex = Math.floor((Math.random() * lastnameList.length)), data = {
                    PlayerName: firstnameList[firstnameIndex] + ' ' + lastnameList[lastnameIndex]
                };
                return api.post('Players', data);
            }

            function runGame(session) {
                init().done(function () {
                    $.when(createPlayer(), getBotId()).done(function (playerId, bot) {
                        var options = {
                            ruleSet: 1
                        };
                        this.gameRepository.create(playerId[0], bot.PlayerId, options).done(function (game) {
                            session.CurrentGame = game;
                            processRound(session);
                        });
                    });
                });
            }

            function processRound(session) {
                var selection = Math.floor((Math.random() * 3) + 1);

                session.CurrentGame.executeMove(session.CurrentGame.Player1Id, selection).done(function (response) {
                    if (!session.CurrentGame.hasFinished()) {
                        processRound(session);
                    } else if (session.Status === 2 /* running */) {
                        runGame(session);
                    } else {
                        session.Close();
                    }
                });
            }

            var Session = (function () {
                function Session() {
                }
                Session.prototype.Stop = function () {
                    this._onStopDef = $.Deferred();
                    if (this.Status === 2 /* running */) {
                        this.Status = 3 /* stopping */;
                    } else if (this.Status === 1 /* stopped */) {
                        this._onStopDef.resolve();
                    }
                    return this._onStopDef.resolve();
                };

                Session.prototype.Start = function () {
                    if (this.Status === 3 /* stopping */) {
                        this.Status = 2 /* running */;
                    } else if (this.Status === 1 /* stopped */) {
                        this.Status = 2 /* running */;
                        runGame(this);
                    }
                };

                Session.prototype.Close = function () {
                    if (this._onStopDef) {
                        this._onStopDef.resolve();
                    }
                    this.Status = 1 /* stopped */;
                };
                return Session;
            })();
            SimulatedGameSession.Session = Session;
        })(Models.SimulatedGameSession || (Models.SimulatedGameSession = {}));
        var SimulatedGameSession = Models.SimulatedGameSession;
    })(Roshambo.Models || (Roshambo.Models = {}));
    var Models = Roshambo.Models;
})(Roshambo || (Roshambo = {}));
//# sourceMappingURL=SimulatedGameSession.js.map
