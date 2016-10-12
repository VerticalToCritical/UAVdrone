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
    .otherwise({ redirectTo: '/' });
});