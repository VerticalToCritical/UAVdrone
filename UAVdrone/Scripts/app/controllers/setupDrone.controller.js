'use strict';
angular.module('drone.setupDrone')
    .controller('setupDroneController',
        function ($scope, $location, battlefield, compass, commands, droneService) {
            $scope.drones = [];
            $scope.tobeAdd = {
                xcoordinate: 0,
                ycoordinate: 0,
                direction: '',
                commands: ''
            };

            $scope.battlefield = battlefield;
            $scope.compass = compass;

            $scope.addDrone = function () {
                if ($scope.tobeAdd.xcoordinate < 0 || $scope.tobeAdd.xcoordinate > battlefield.Width) {
                    toastr.error('Drone X Coordinate must be in the range of 0 and ' + battlefield.Width,
                        'Please Note');
                    return;
                }

                if ($scope.tobeAdd.ycoordinate < 0 || $scope.tobeAdd.ycoordinate > battlefield.Height) {
                    toastr.error('Drone Y Coordinate must be in the range of 0 and ' + battlefield.Height,
                        'Please Note');
                    return;
                }

                if (!$scope.tobeAdd.direction) {
                    toastr.error('Please select drone facing position',
                        'Please Note');
                    return;
                }

                $scope.drones.push($scope.tobeAdd);
                $scope.reset();
                toastr.success('New drone added');
            }

            $scope.reset = function () {
                $scope.tobeAdd = {
                    xcoordinate: 0,
                    ycoordinate: 0,
                    direction: '',
                    commands: []
                };
            }

            $scope.deleteDrone = function(drone) {
                var idx = $scope.drones.indexOf(drone);
                if (idx > -1) {
                    $scope.drones.splice(idx, 1);
                    toastr.success('Drone removed');
                }                
            }

            $scope.runCommands = function () {
                droneService.runCommand($scope.drones).then(function (data){
                    $location.path('/result');
                })
                .catch(function() {
                        toastr.error('Run drone commands failed');
                    });
            }
        });