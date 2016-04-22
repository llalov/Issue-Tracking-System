'use strict';

 angular.module('issueTrackingSystem', [
    'ngRoute',
    'issueTrackingSystem.home',
    'issueTrackingSystem.main',
    'issueTrackingSystem.users.identity'
    ])
     .config(['$routeProvider', function($routeProvider) {
         $routeProvider.otherwise({redirectTo: '/'});
      }])
     .constant('BASE_URL', 'http://softuni-issue-tracker.azurewebsites.net/');

