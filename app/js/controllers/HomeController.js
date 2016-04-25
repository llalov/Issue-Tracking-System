angular.module('issueTrackingSystem.home', [
    'issueTrackingSystem.authentication'
])
    .config(['$routeProvider', function($routeProvider) {
        $routeProvider.when('/', {
            templateUrl: 'templates/home.html',
            controller: 'HomeController'
        })
    }])
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