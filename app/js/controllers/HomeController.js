angular.module('issueTrackingSystem.home', [
    'issueTrackingSystem.authentication'
])
    .controller('HomeController', [
        '$scope',
        '$location',
        'authService',
        'notifyService',
        function($scope, $location, authService, notifyService) {

            $scope.getIssues = function() {

            }

        }
    ]);