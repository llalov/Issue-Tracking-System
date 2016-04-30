'use strict';

angular.module('issueTrackingSystem.home', [])
    .controller('HomeController', [
        '$scope',
        '$location',
        'authService',
        'notifyService',
        'dashboardService',
        function($scope, $location, authService, notifyService, dashboardService) {

            dashboardService.getMyIssues().then(function(receivedIssues) {
                $scope.myIssues = receivedIssues;
                $location.path('/');
            });

            $scope.login = function(user) {
                authService.loginUser(user)
                    .then(function success() {
                        notifyService.showInfo('Login successful');
                        dashboardService.getMyIssues().then(function(receivedIssues) {
                            $scope.myIssues = receivedIssues;
                            $location.path('/');
                        });
                    }, function error(err) {
                        notifyService.showError('Login failed', err);
                    });
            };
            $scope.register = function (userData) {
                authService.registerUser(userData,
                    function success(){
                        notifyService.showInfo('Registration successful');
                        $location.path('/');
                    },
                    function error(err) {
                        notifyService.showError('Registration failed', err);
                    })
            };
        }
    ]);