'use strict';

 angular.module('issueTrackingSystem', [
     'ngRoute',
     'ngResource',
     'issueTrackingSystem.authentication',
     'issueTrackingSystem.users',
     'angular-loading-bar',
     'ui.bootstrap.pagination',
     'issueTrackingSystem.notification',
     'issueTrackingSystem.app',
     'issueTrackingSystem.home',
     'issueTrackingSystem.home.dashboardService',
     'issueTrackingSystem.issues',
     'issueTrackingSystem.issues.issuesService',
     'issueTrackingSystem.projects',
     'issueTrackingSystem.projects.projectsService'
    ])
     .constant('BASE_URL', 'http://softuni-issue-tracker.azurewebsites.net/')
     .constant('pageNumber', 1)
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
                    templateUrl: 'templates/allProjects.html',
                    controller: 'ProjectsController'
                });
                $routeProvider.when('/project/:id',{
                    templateUrl: 'templates/project.html',
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