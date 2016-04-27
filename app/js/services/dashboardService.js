app.module('issueTrackingSystem.dashboard', [])
    .factory('dashboardService',[
        '$http',
        '$q',
        'BASE_URL',
        'authService',
        function($http, $q, BASE_URL, authService) {
            return {
                getMyIssues : function () {
                    var deferred = $q.defer();
                    var headers = authService.getAuthHeaders();
                    var pageNumber = 1;
                    var pageSize = 5;

                    var request = {
                        method: 'GET',
                        url: BASE_URL + 'issues/me/?orderBy=DueDate desc, IssueKey&pageSize='+ pageSize + '&pageNumber='+ pageNumber,
                        headers: {
                            'Authorization': headers
                        }
                    };

                    $http(request).then(function(receivedIssues) {
                        deferred.resolve(receivedIssues.data);
                    }, function (error) {
                        deferred.reject(error.data);
                    });

                    return  deferred.promise();
                }
            }

        }
    ]);
