'use strict';

angular.module('issueTrackingSystem.registration', [])
    .controller('RegisterController',[
        '$scope',
        '$location',
        'authService',
        'notifyService',
        function ($scope, $location, authService, notifyService) {
            $scope.register = function (userData) {
                authService.register(userData,
                    function success(){
                        notifyService.showInfo('Registration successful');
                        $location.path('/dashboard');
                    },
                    function error(err) {
                        notifyService.showError('Registration failed', err);
                    })
            };

        }]
    );
