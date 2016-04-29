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
                }
            }
        }
    ]);