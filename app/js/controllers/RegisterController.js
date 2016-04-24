'use strict';

app.controller('RegisterController',
    function ($scope, $location, townsService, authService, notifyService) {
        townsService.getTowns(function(towns) {
            $scope.towns = towns;
        }, function(err) {
            notifyService.showError('Could not load towns', err);
        });

        $scope.register = function (userData) {
            authService.register(userData,
                function success(){
                    notifyService.showInfo('Registration successful');
                    $location.path('/');
                },
                function error(err) {
                    notifyService.showError('Registration failed', err);
                })
        }
    });
