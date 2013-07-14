'use strict'
window.Models = window.Models || {};
window.Models.SimulatedGameSession = (function (_, $, api, gameRepository) {
    var _allBots,
        STATUS = { 
            stopped: 1,
            running: 2,
            stopping:3
        };

    // Private Funcs

    function init() {
        var initDefered;
        if (!initDefered) {
            initDefered = api.get('Players').done(function (response) {
                _allBots = response;
            })
        }

        return initDefered;
    }

    function getBotId() {
        var index = Math.floor((Math.random() * _allBots.length));
        return _allBots[index];
    }

    function createPlayer() {
        var firstname = ['Billy', 'Bob', 'Boris', 'Bernard', 'Beatrix'],
            lastname = ['Smith', 'Miller', 'Baker', 'Tailor', 'Cartwright'],
            firstnameIndex = Math.floor((Math.random() * firstname.length)),
            lastnameIndex = Math.floor((Math.random() * lastname.length)),
            data = {
                PlayerName: firstname[firstnameIndex] + ' ' + lastname[lastnameIndex]
            };
        return api.post('Players', data);
    }

    function runGame(session) {

        init().done(function () {
            $.when(createPlayer(), getBotId())
                .done(function (playerId, bot) {
                    var options = {
                        ruleSet: 1
                    };
                    gameRepository.create(playerId[0], bot.PlayerId, options)
                        .done(function (game) {
                            session.currentGame = game;
                            processRound(session);
                        });
                });
        });
    }

    function processRound(session) {
        var selection = Math.floor((Math.random() * 3) + 1);

        session.currentGame.executeMove(session.currentGame.player1Id, selection).done(function (response) {

            if (!session.currentGame.hasFinished()) {
                processRound(session);
            }
            else if (session.status === STATUS.running) {
                runGame(session);
            }
            else {
                if (session._onStopDef) {
                    session._onStopDef.resolve();
                }
                session.status = STATUS.stopped;
            }
        });
    }

    // Constructor

    function Session() {
        $.extend(this, { status: STATUS.stopped});
        
        if (_.isUndefined(this.stop)) {
            
            Session.prototype.stop = function () {
                this._onStopDef = $.Deferred();
                if (this.status === STATUS.running) {
                    this.status = STATUS.stopping;
                }
                else if (this.status === STATUS.stopped) {
                    this._onStopDef.resolve();
                }

                return this._onStopDef.promise();
            }
        }

        if (_.isUndefined(this.start)) {
            Session.prototype.start = function () {
                if (this.status === STATUS.stopping) {
                    this.status = STATUS.running;
                } else if (this.status === STATUS.stopped) {
                    this.status = STATUS.running;
                    runGame(this);
                }
            }
        }
        
    }



    function create() {
        var session = new Session();
        return session;
    }


    return {
        create: create
    }
})(_, $, Api, Models.Game);
