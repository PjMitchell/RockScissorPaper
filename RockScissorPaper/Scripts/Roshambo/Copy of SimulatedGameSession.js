'use strict'
window.Models = windows.Models || {};
window.Models.SimulatedGameSession = (function ($, logger) {


    function Session(instancesTotal, callback) {
        $.extend(this, instancesTotal, callback, { isStopping: true, currentInstances: 0 });
        
        if (typeof (this.start) == 'undefined') {
            Session.prototype.start = function () {
                var me = this;
                if (me.isStopping) {
                    me.isStopping = false;
                    while (me.currentInstances <= me.instancesTotal) {
                        me.callback();
                        me.currentInstances += 1;
                    }
                }
            }
        }

        if (typeof (this.stop) == 'undefined') {
            Session.prototype.stop = function () {
                this.isStopping = true;
            }
        }

        if (typeof (this.checkpoint) == 'undefined') {
            Session.prototype.checkpoint = function(callback) {
                if (this.isStopping) {
                    this.currentInstances -= 1;
                }
                else {
                    callback();
                }
            }
        }

    }

    function create(instances) {
        return new Session(instances);
    }


    return {
        create: create,
    }
})($, ConsoleLogger);
