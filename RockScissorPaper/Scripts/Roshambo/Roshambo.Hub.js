'use strict'

window.Roshambo = window.Roshambo || {};

window.Roshambo.Hub = (function ($) {
    function liveStatus() {
        // Reference the auto-generated proxy for the hub. 
        var hub = $.connection.roshamboHub,
            $peopleConnectedField = $('#peoplePlayingField'),
            $botWinsField = $('#botWins'),
            $humanWinsField = $('#humanWins'),
            $logBox = $('#liveStatusLog');
        // Create a function that the hub can call back to display changes in number of players.
        hub.client.updatePeopleConnected = function (peopleConnected) {
            // Add the message to the page. 

            $peopleConnectedField.html('Games Running: ' + peopleConnected);
        };
        hub.client.refreshView = function (view) {
            $peopleConnectedField.html('Games Running: ' + view.NumberOfPeopleConnected)
            $botWinsField.html('Bots: ' + view.BotWins)
            $humanWinsField.html('Humans: ' + view.HumanWins)
        }
        hub.client.newGameReport = function (message) {
            $logBox.prepend('<p>' + message + '</p>')
            $logBox.find("p").slice(10).remove();
        }

        $.connection.hub.start().done(function () {
            hub.server.getInfo();
        });
    }

    return { liveStatus: liveStatus }

})($);