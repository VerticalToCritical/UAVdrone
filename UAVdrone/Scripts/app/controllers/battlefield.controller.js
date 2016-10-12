'use strict';
angular.module('drone.battlefield').controller('battlefieldController', function ($scope, $location, battlefield, droneService) {
    $scope.battlefield = battlefield;

    $scope.setupBattleField = function (formvalid) {
        if (formvalid) {
            droneService.setupBattleField($scope.battlefield).then(function (data) {
                $location.path('/setup');
            });
        } else {
            
        }
    }
});