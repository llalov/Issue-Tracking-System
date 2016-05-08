'use strict';

angular.module('issueTrackingSystem.issues', [])
    .controller('IssuesController',[
        '$scope',
        '$routeParams',
        '$location',
        'issuesService',
        'notifyService',
        function ($scope, $routeParams, $location, issuesService, notifyService) {

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
                $scope.TextCom = '';
                issuesService.addIssueComment(issueId, commentText,
                    function success(){
                        notifyService.showInfo('Comment added');
                        $scope.reloadIssueComments();
                    },
                    function error(err){
                        notifyService.showError('Unsuccessful', err);
                    }
                )
            }
        }
    ]);
