'use strict';

angular.module('issueTrackingSystem.login', [])
    .controller('LoginController',[
        '$scope',
        '$rootScope',
        '$location',
        'authService',
        'notifyService',
        function ($scope, $rootScope, $location, authService, notifyService) {
            $scope.login = function(user) {
                authService.loginUser(user)
                    .then(function success() {
                        notifyService.showInfo('Login successful');
                        $location.path('/dashboard');
                    }, function error(err) {
                        notifyService.showError('Login failed', err);
                    });
            };
        } ]);
