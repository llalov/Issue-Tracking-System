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
                $routeProvider.otherwise({redirectTo: '/'});

            }
     ]);


