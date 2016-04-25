angular.module('issueTrackingSystem.app',[
]).
    controller('AppController',[
        '$scope',
        '$location',
        'authService',
        'notifyService',
        function($scope,$location, authService, notifyService) {
            $scope.authService = authService;

            $scope.logout = function() {
                authService.logout();
                notifyService.showInfo('Logout successful');
                $location.path('/');
            };
        }]);
