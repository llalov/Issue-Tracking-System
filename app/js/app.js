'use strict';

 angular.module('issueTrackingSystem', [
     'ngRoute',
     'ngResource',
     'ui.bootstrap.pagination',
     'issueTrackingSystem.home',
     'issueTrackingSystem.home.dashboardService',
     'issueTrackingSystem.app',
     'issueTrackingSystem.issues',
     'issueTrackingSystem.issues.issuesService',
     'issueTrackingSystem.notification',
     'issueTrackingSystem.authentication'
    ])
     .constant('BASE_URL', 'http://softuni-issue-tracker.azurewebsites.net/')
     .constant('pageSize', 5)
     .config([
         '$routeProvider',
            function($routeProvider) {
                $routeProvider.when('/',{
                    templateUrl: 'templates/home.html',
                    controller: 'HomeController'
                });
                $routeProvider.when('/issues/:id',{
                    templateUrl: 'templates/issue.html',
                    controller: 'IssuesController'
                });
                $routeProvider.when('/projects',{
                    templateUrl: 'templates/projects.html',
                    controller: 'ProjectsController'
                });
                $routeProvider.otherwise({redirectTo: '/'});

            }
     ])
     .run(function ($rootScope, $location, authService) {
         $rootScope.$on('$locationChangeStart', function () {
             if(($location.path().indexOf("/issues") != -1 ||
                 $location.path().indexOf("/projects") != -1) &&
                 !authService.isLoggedIn()) {
                 $location.path('/');
             }
         });
     });