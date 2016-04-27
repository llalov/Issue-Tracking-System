angular.module('issueTrackingSystem.home', [
    'issueTrackingSystem.authentication'
])
    .controller('HomeController', [
        '$scope',
        '$location',
        'authService',
        'notifyService',
        function($scope, $location, authService, notifyService) {
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
                authService.register(userData,
                    function success(){
                        notifyService.showInfo('Registration successful');
                        $location.path('/');
                    },
                    function error(err) {
                        notifyService.showError('Registration failed', err);
                    })
            };
            $scope.getIssues = function() {

            };

        }
    ]);