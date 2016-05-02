'use strict';

angular.module('issueTrackingSystem.home.dashboardService', [])
    .factory('dashboardService',[
        '$http',
        '$q',
        'BASE_URL',
        'authService',
        function($http, $q, BASE_URL, authService) {
            return {
                getMyIssues : function (pageNumber) {
                    var deferred = $q.defer(),
                        headers = authService.getAuthHeaders(),
                        pageSize = 6;

                    var request = {
                        method: 'GET',
                        url: BASE_URL + 'issues/me/?orderBy=DueDate desc, IssueKey&pageSize='+ pageSize + '&pageNumber='+ pageNumber,
                        headers: headers
                    };

                    $http(request).then(function(receivedIssues) {
                        deferred.resolve(receivedIssues.data);
                    }, function (error) {
                        deferred.reject(error.data);
                    });
                    return  deferred.promise;
                }
            }

        }
    ]);
