'use strict';
angular.module('drone.commandResult').controller('droneResultController', function ($scope, $location, results, droneService) {
    $scope.results = results;
    $scope.setupDrone = function () {
        droneService.resetCommandResults();
        $location.path('/setup');
    }
});