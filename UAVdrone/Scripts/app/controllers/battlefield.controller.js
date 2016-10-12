'use strict';
angular.module('drone.battlefield').controller('battlefieldController', function ($scope, battlefield, droneService) {
    $scope.battlefield = battlefield;

    $scope.setupBattleField = function (formvalid) {
        if (formvalid) {
            droneService.setupBattleField($scope.battlefield).then(function (data) {
                $scope.battlefield.width = data.Width;
                $scope.battlefield.height = data.Height;
            });
        } else {

        }
    }
});