'use strict';
angular.module('drone.service').factory('droneService', function ($http, $q) {
    var self = this;
    self.battleField = {
        width: 0,
        height: 0
    };

    return {
        setupBattleField: function (width, height) {
            var deffered = $q.defer();

            $http.post("/api/Drone/SetupBattlefield", { Width: width, Height: height })
                .success(function (data) {
                    self.battleField = data;
                    deffered.resolve({ result: data });
                })
            .error(function (msg, code) {
                self.battleField = {};
                deffered.reject(msg);
            });
            return deffered.promise;
        },

        runCommand: function (drones) {
            var deffered = $q.defer();

            $http.post("/api/Drone/RunDroneCommands", { BattleField: self.battleField, Drones: drones })
                .success(function (data) {
                    deffered.resolve({ result: data });
                })
            .error(function (msg, code) {
                deffered.reject(msg);
            });
            return deffered.promise;
        },
        getBattleField: function () {
            return $q.when(self.battleField);
        }
    }
});