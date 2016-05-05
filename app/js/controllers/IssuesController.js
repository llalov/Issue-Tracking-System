'use strict';

angular.module('issueTrackingSystem.issues', [])
    .controller('IssuesController',[
        '$scope',
        '$routeParams',
        'issuesService',
        'notifyService',
        function ($scope, $routeParams, issuesService, notifyService) {

            issuesService.getIssue($routeParams.id).then(function(receivedIssue) {
                $scope.issueById = receivedIssue;
            });

            issuesService.getIssueComments($routeParams.id).then(function (receivedComments) {
                    $scope.issueComments = receivedComments;
            });

            $scope.reloadIssueComments = function() {
                issuesService.getIssueComments($routeParams.id).then(function (receivedComments) {
                    $scope.issueComments = receivedComments;
                });
            };

            $scope.addIssueComment = function(issueId, commentText) {
                issuesService.addIssueComment(issueId, commentText);
            }
        }
    ]);
