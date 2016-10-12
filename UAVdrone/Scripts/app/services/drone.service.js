'use strict';
angular.module('drone.service').factory('droneService', function ($http, $q) {
    var self = this;
    self.battleField = {
        Width: 0,
        Height: 0
    };

    return {
        setupBattleField: function (item) {
            var deffered = $q.defer();
            
            $http.post("/api/Drone/SetupBattlefield", { Width : item.Width, Height: item.Height })
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
        },
        getAllCompass: function() {
            return $http.get('/api/Drone/GetCompassItems')
                .then(function(response) {
                    return response.data;
                });
        },
        getAllComands: function() {
            return $http.get('/api/Drone/GetCommands')
                .then(function (response) {
                    return response.data;
                });
        }
    }
});