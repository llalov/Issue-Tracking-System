'use strict';

angular.module('issueTrackingSystem.home', [])
    .controller('HomeController', [
        '$scope',
        '$location',
        'authService',
        'notifyService',
        'dashboardService',
        'issuesService',
        function($scope, $location, authService, notifyService, dashboardService) {
            $scope.login = function(user) {
                authService.loginUser(user)
                    .then(function success() {
                        notifyService.showInfo('Login successful');
                        $location.path('/');
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

            dashboardService.getMyIssues().then(function(receivedIssues) {
                 $scope.myIssues = receivedIssues;
             });
        }
    ]);