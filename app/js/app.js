'use strict';

 angular.module('issueTrackingSystem', [
     'ngRoute',
     'ngResource',
     'ui.bootstrap.pagination',
     'issueTrackingSystem.home',
     'issueTrackingSystem.app',
     'issueTrackingSystem.registration',
     'issueTrackingSystem.login',
     'issueTrackingSystem.notification',
     'issueTrackingSystem.authentication'
    ])
     .constant('BASE_URL', 'http://softuni-issue-tracker.azurewebsites.net/')
     .constant('pageSize', 5)
     .config([
         '$routeProvider',
            function($routeProvider) {
                $routeProvider.when('/',{
                    templateUrl: 'templates/login.html',
                    controller: 'LoginController'
                });
                $routeProvider.when('/register',{
                    templateUrl: 'templates/register.html',
                    controller: 'RegisterController'
                });
                $routeProvider.when('/home',{
                    templateUrl: 'templates/home.html',
                    controller: 'HomeController'
                });
                $routeProvider.otherwise({redirectTo: '/'});

            }
     ])
     /*.run(function ($rootScope, $location, authService) {
         $rootScope.$on('$locationChangeStart', function (event) {
             if($location.path().indexOf("/home") != -1 && !authService.isLoggedIn()) {
                 $location.path('/');
             }
         });

         $rootScope.$on('$locationChangeStart', function (event) {
             if($location.path().indexOf("/") != -1 && authService.isLoggedIn()) {
                 $location.path('/home');
             }
         });
     });*/

