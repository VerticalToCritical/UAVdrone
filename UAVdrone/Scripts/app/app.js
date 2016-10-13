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
                check: function ($location, droneService) {
                    droneService.getBattleField().then(function (data) {
                        if (data == undefined || data.Width == 0 || data.Height == 0) $location.path('/');
                    });
                },
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
        .when('/result', {
            templateUrl: '/Scripts/app/templates/viewResults.html',
            controller: 'droneResultController',
            resolve: {
                check:function($location,droneService){
                    droneService.getCommandResults().then(function (data) {
                        if (data.length === 0) $location.path('/setup');
                    });
                },
                results: function (droneService) {
                    return droneService.getCommandResults();
                }
            }
        })
    .otherwise({ redirectTo: '/' });
});