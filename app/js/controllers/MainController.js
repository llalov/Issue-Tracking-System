angular.module('issueTrackingSystem.main',[
]).
    controller('MainController',[
        '$scope',
        'identity',
        function($scope, identity){
            $scope.isAuthenticated = identity.isAuthenticated();
        }
    ]);
