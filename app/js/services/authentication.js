angular.module('issueTrackingSystem.users.authentication', [])
    .factory('authentication', [
        '$http',
        '$q',
        'BASE_URL',
        function($http, $q, BASE_URL) {
            function registerUser(user) {
                var deferred = $q.defer();

                $http.post(BASE_URL + 'api/Account/Register', user)
                    .then(function(response) {
                        deferred.resolve(response.data);
                    }, function (error) {

                    });

                return deferred.promise;
            }

            function loginUser(user){
                var deferred = $q.defer();
                var loginData = 'Username=' + user.email +
                                '&Password=' + user.password +
                                '&grant_type=password';

                $http.post(BASE_URL + 'api/Token', loginData)
                    .then(function(response) {
                        sessionStorage['accessToken'] = response.data.access_token;
                        console.log(sessionStorage['accessToken']);
                        deferred.resolve(response.data)
                    }, function(err) {
                        deferred.reject(err.data);
                    });

                return deferred.promise;
            }

            function logout() {
                sessionStorage['accessToken'] = '';
            }

            return {
                registerUser: registerUser,
                loginUser: loginUser,
                logout: logout
            }
    }]);