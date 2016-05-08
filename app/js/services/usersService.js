'use strict';

angular.module('issueTrackingSystem.users', [])
    .factory('usersService', [
        '$http',
        '$q',
        'BASE_URL',
        'authService',
        function($http, $q, BASE_URL, authService) {
            return {
                getAllUsers: function() {
                    var deferred = $q.defer(),
                        headers = authService.getAuthHeaders(),
                        request = {
                            method: 'GET',
                            url: BASE_URL + 'users',
                            headers: headers
                        };

                    $http(request).then(function(receivedUsers){
                        deferred.resolve(receivedUsers.data);
                    }, function(error){
                        deferred.reject(error.data);
                    });

                    return deferred.promise;
                }
            }
        }]);
