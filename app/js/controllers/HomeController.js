angular.module('issueTrackingSystem.home', [
    'issueTrackingSystem.authentication'
])
    .controller('HomeController', [
        '$scope',
        '$location',
        'authService',
        'notifyService',
        function($scope, $location, authService, notifyService) {


            $scope.registerUser = function(user) {
                authService.register(user)
                    .then(function(registeredUser) {
                        console.log(registeredUser);
                    })
            };
        }
    ]);