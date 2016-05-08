angular.module('issueTrackingSystem.app',[
]).
    controller('AppController',[
        '$scope',
        '$rootScope',
        '$location',
        'authService',
        'notifyService',
        function($scope, $rootScope, $location, authService, notifyService) {
            $scope.authService = authService;

            if (authService.isLoggedIn()) {
                authService.isAdmin().then(function(receivedData) {
                    $scope.isAdmin = receivedData;
                });

                authService.getUserInfo().then(function(receivedInfo){
                    $scope.userInfo = receivedInfo;
                });
            }

            $scope.logout = function() {
                authService.logout();
                notifyService.showInfo('Logout successful');
                $location.path('/');
            };
        }]);
