'use strict';

angular.module('issueTrackingSystem.home', [])
    .controller('HomeController', [
        '$scope',
        '$location',
        'pageNumber',
        'authService',
        'notifyService',
        'dashboardService',
        function($scope, $location, pageNumber, authService, notifyService, dashboardService) {

            $scope.issueParams = {
                pageNumber: pageNumber
            };

            $scope.login = function(user) {
                authService.loginUser(user)
                    .then(function success() {
                        notifyService.showInfo('Login successful');
                        dashboardService.getMyIssues($scope.issueParams.pageNumber).then(function(receivedIssues) {
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

            if(authService.isLoggedIn()) {
                dashboardService.getMyIssues($scope.issueParams.pageNumber).then(function(receivedIssues) {
                    $scope.myIssues = receivedIssues;
                    $location.path('/');
                });
                $scope.reloadIssues = function() {
                    dashboardService.getMyIssues($scope.issueParams.pageNumber).then(function(receivedIssues){
                        $scope.myIssues = receivedIssues;
                    }, function(error) {
                        notifyService.showError('Cannot load issues', error);
                    })
                }
            }
        }
    ]);