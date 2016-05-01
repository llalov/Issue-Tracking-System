angular.module('issueTrackingSystem.projects.projectsService',[])
    .factory('projectsService', [
        '$http',
        '$q',
        'BASE_URL',
        'authService',
        function($http, $q, BASE_URL, authService){
            return {
                getAllProjects: function() {
                    var deferred = $q.defer(),
                        headers = authService.getAuthHeaders(),
                        request = {
                            method: 'GET',
                            url: BASE_URL + 'projects',
                            headers: headers
                        };
                    $http(request).then(function(receivedProjects){
                        deferred.resolve(receivedProjects.data);
                    }, function(error){
                        deferred.reject(error.data);
                    });

                    return deferred.promise;
                },
                getProjectById: function(id) {
                    var deferred = $q.defer(),
                        headers = authService.getAuthHeaders(),
                        request = {
                            method: 'GET',
                            url: BASE_URL + 'projects/'+ id,
                            headers: headers
                        };

                    $http(request).then(function(receivedProject) {
                        deferred.resolve(receivedProject);
                    }, function(error){
                        deferred.reject(error);
                    });

                    return deferred.promise;
                }
            }
        }
    ]);
