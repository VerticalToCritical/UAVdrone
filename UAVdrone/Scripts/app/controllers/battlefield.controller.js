'use strict';
angular.module('drone.battlefield').controller('battlefieldController', function ($scope, battlefield, droneService) {
    $scope.battlefield = battlefield;

    $scope.setupBattleField = function (formvalid) {
        if (formvalid) {
            droneService.setupBattleField($scope.battlefield).then(function (data) {
                $scope.battlefield.Width = data.Width;
                $scope.battlefield.Height = data.Height;
            });
        } else {

        }
    }
});