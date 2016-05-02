'use strict';

angular.module('issueTrackingSystem.issues', [])
    .controller('IssuesController',[
        '$scope',
        '$routeParams',
        'issuesService',
        function ($scope, $routeParams, issuesService) {

            issuesService.getIssue($routeParams.id).then(function(receivedIssue) {
                $scope.issueById = receivedIssue;
            });

            issuesService.getIssueComments($routeParams.id).then(function (receivedComments) {
                    $scope.issueComments = receivedComments;
            })

        }
    ]);
