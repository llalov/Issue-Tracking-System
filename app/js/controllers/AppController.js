angular.module('issueTrackingSystem.app',[
]).
    controller('AppController',[
        '$scope',
        '$location',
        'authService',
        'notifyService',
        function($scope,$location, authService, notifyService) {
            $scope.authService = authService;

            authService.isAdmin().then(function(receivedData) {
                $scope.isAdmin = receivedData;
            });

            $scope.logout = function() {
                authService.logout();
                notifyService.showInfo('Logout successful');
                $location.path('/');
            };
        }]);
