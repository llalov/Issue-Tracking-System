'use strict';

angular.module('issueTrackingSystem.issues.issuesService', [])
    .factory('issuesService', [
        '$http',
        '$q',
        'authService',
        'BASE_URL',
        function($http, $q, authService, BASE_URL) {
            return {
                getIssueComments: function(id) {
                    var deferred = $q.defer(),
                        headers = authService.getAuthHeaders(),
                        request = {
                            method: 'GET',
                            url: BASE_URL + 'issues/'+id+'/comments',
                            headers: headers
                        };

                    $http(request).then(function(receivedComments){
                       deferred.resolve(receivedComments.data);
                    }, function(error) {
                        deferred.reject(error.data);
                    });


                    return deferred.promise;
                },

                getIssue: function(id) {
                    var deferred = $q.defer(),
                        headers = authService.getAuthHeaders(),
                        request = {
                            method: 'GET',
                            url: BASE_URL + 'issues/'+ id,
                            headers: headers
                        };

                    $http(request).then(function(receivedIssue) {
                        deferred.resolve(receivedIssue.data);
                    }, function (error) {
                        deferred.reject(error.data);
                    });

                    return deferred.promise;
                },

                addIssueComment: function(issueId, commentText) {
                    var deferred = $q.defer(),
                        request = {
                            method: 'POST',
                            url: BASE_URL+'issues/'+issueId+'/comments',
                            headers: authService.getAuthHeaders(),
                            data: {
                                Text: commentText
                            }
                        };

                    $http(request).then(function(success) {
                        deferred.resolve(success.data);
                    }, function(error) {
                        deferred.reject(error.data);
                    });

                    return deferred.promise;
                }

            }
        }
    ]);