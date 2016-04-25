angular.module('issueTrackingSystem.authentication', [])
    .factory('authService', [
        '$http',
        '$q',
        'BASE_URL',
        function($http, $q, BASE_URL) {
            return {
                registerUser: function(user) {
                    var deferred = $q.defer();

                    $http.post(BASE_URL + 'api/Account/Register', user)
                        .then(function(response) {
                            sessionStorage['currentUser'] = JSON.stringify(response.data);
                            deferred.resolve(response.data);
                        },
                        function(error) {
                            deferred.reject(error.data);
                        });
                    return deferred.promise;
                },
                loginUser: function(user) {
                    var deferred = $q.defer();

                    var loginData = 'Username=' + user.email +
                        '&Password=' + user.password +
                        '&grant_type=password';

                    $http.post(BASE_URL + 'api/Token', loginData)
                        .then(function(response) {
                            deferred.resolve(response.data);
                            sessionStorage['currentUser'] =JSON.stringify(response.data);

                        },
                        function(err) {
                            deferred.reject(err.data);
                        });

                    return deferred.promise;
                },
                logout : function() {
                    delete sessionStorage['currentUser'];
                },
                getCurrentUser : function() {
                    var userObject = sessionStorage['currentUser'];
                    if(userObject != undefined) {
                        return JSON.parse(sessionStorage['currentUser']);
                    }
                },
                isAnonymous : function() {
                    return sessionStorage['currentUser'] === undefined;
                },
                isLoggedIn : function() {
                    return sessionStorage['currentUser'] !== undefined;
                },
                isNormalUser : function() {
                    var currentUser = this.getCurrentUser();
                    return (currentUser != undefined) && (!currentUser.isAdmin);
                },
                isAdmin : function() {
                    var currentUser = this.getCurrentUser();
                    return (currentUser != undefined) && (!currentUser.isNormalUser);
                },
                getAuthHeaders : function() {
                    var headers = {};
                    var currentUser = this.getCurrentUser();
                    if (currentUser) {
                        headers['Authorization'] = 'Bearer ' + currentUser.access_token;
                    }
                    return headers;
                }
            }
        }
    ]);