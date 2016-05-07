angular.module('issueTrackingSystem.projects.projectsService',[])
    .factory('projectsService', [
        '$http',
        '$q',
        'BASE_URL',
        'authService',
        function($http, $q, BASE_URL, authService){
            return {
                getAllProjects: function(pageSize, pageNumber) {
                    var deferred = $q.defer(),
                        headers = authService.getAuthHeaders(),
                        request = {
                            method: 'GET',
                            url: BASE_URL + 'projects?filter=&pageSize='+ pageSize +'&pageNumber='+ pageNumber,
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
                        deferred.resolve(receivedProject.data);
                    }, function(error){
                        deferred.reject(error.data);
                    });

                    return deferred.promise;
                },

                getMyProjects: function(userId, pageSize, pageNumber) {
                    var deferred = $q.defer(),
                        headers = authService.getAuthHeaders(),
                        request = {
                            method: 'GET',
                            url: BASE_URL + 'projects?filter=Lead.Id="'+ userId +'"&pageSize='+ pageSize +'&pageNumber='+ pageNumber,
                            headers: headers
                        };

                    $http(request).then(function(receivedProjects){
                        deferred.resolve(receivedProjects.data);
                    }, function(error){
                        deferred.reject(error.data);
                    });

                    return deferred.promise;
                }


            }
        }
    ]);
