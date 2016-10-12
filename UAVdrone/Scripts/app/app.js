'use strict';
angular.module('app', ['ngRoute', 'drone.battlefield', 'drone.service', 'drone.commandResult', 'drone.setupDrone']).config(function ($routeProvider) {
    $routeProvider.when('/', {
        templateUrl: '/Scripts/app/templates/setupBattlefield.html',
        controller: 'battlefieldController',
        resolve: {
            battlefield: function (droneService) {
                return droneService.getBattleField();
            }
        }
    })
        .when('/setup',
        {
            templateUrl: '/Scripts/app/templates/setupDrones.html',
            controller: 'setupDroneController',
            resolve: {
                battlefield: function (droneService) {
                    return droneService.getBattleField();
                },
                compass: function(droneService) {
                    return droneService.getAllCompass();
                },
                commands: function(droneService) {
                    return droneService.getAllComands();
                }
            }
        })
    .otherwise({ redirectTo: '/' });
});