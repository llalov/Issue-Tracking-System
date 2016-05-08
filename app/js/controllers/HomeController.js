'use strict';

angular.module('issueTrackingSystem.home', [])
    .controller('HomeController', [
        '$scope',
        '$location',
        'pageNumber',
        'authService',
        'notifyService',
        'dashboardService',
        'projectsService',
        function($scope, $location, pageNumber, authService, notifyService, dashboardService, projectsService) {
            $scope.issueParams = {
                pageSize: 6,
                pageNumber: pageNumber
            };
            $scope.projectParams = {
                pageSize: 6,
                pageNumber: pageNumber
            };

            var affiliatedProjects = [],
                projectIds = {};

            $scope.login = function(user) {
                authService.loginUser(user)
                    .then(function success() {
                        notifyService.showInfo('Login successful');
                        dashboardService.getMyIssues($scope.issueParams.pageSize, $scope.issueParams.pageNumber)
                            .then(function(receivedIssues) {
                                $scope.myIssues = receivedIssues;
                                $location.path('/');
                        });
                    }, function error(err) {
                        notifyService.showError('Login failed', err);
                    });
            };

            $scope.register = function (userData) {
                authService.registerUser(userData,
                    function success(){
                        notifyService.showInfo('Registration successful');
                        $location.path('/');
                    },
                    function error(err) {
                        notifyService.showError('Registration failed', err);
                    })
            };

            if(authService.isLoggedIn()) {
                authService.getUserInfo()
                    .then(function(receivedUserInfo){
                        $scope.userInfo = receivedUserInfo;
                        projectsService.getMyProjects(receivedUserInfo.Id, $scope.projectParams.pageSize, $scope.projectParams.pageNumber)
                            .then(function(receivedProjects){
                                var projectsLength = receivedProjects.Projects.length;
                                for (var i = 0; i < projectsLength; i++) {
                                    var project = receivedProjects.Projects[i];
                                    if(!projectIds[project.Id]) {
                                        affiliatedProjects.push(project);
                                        projectIds[project.Id] = project.Id;
                                    }
                                }
                                $scope.myProjects = receivedProjects;
                                $scope.myProjects.Projects = affiliatedProjects;
                                $location.path('/');
                            });

                    });

                dashboardService.getMyIssues($scope.issueParams.pageSize, $scope.issueParams.pageNumber)
                    .then(function(receivedIssues) {
                        $scope.myIssues = receivedIssues;
                        var issuesLength = receivedIssues.Issues.length;
                        for(var i = 0; i < issuesLength; i++) {
                            var issue = receivedIssues.Issues[i];
                            if(!projectIds[issue.Project.Id]){
                                affiliatedProjects.push(issue.Project);
                                projectIds[issue.Project.Id] = issue.Project.Id;
                            }
                        }
                        $location.path('/');
                    });

                $scope.reloadMyProjects = function() {
                    projectsService.getMyProjects($scope.userInfo.Id, $scope.projectParams.pageSize, $scope.projectParams.pageNumber)
                        .then(function(receivedProjects){
                            var projectsLength = receivedProjects.Projects.length;
                            for (var i = 0; i < projectsLength; i++) {
                                var project = receivedProjects.Projects[i];
                                if(!projectIds[project.Id]) {
                                    affiliatedProjects.push(project);
                                    projectIds[project.Id] = project.Id;
                                }
                            }

                            $scope.myProjects = receivedProjects;
                            $scope.affiliatedProjects = affiliatedProjects;
                            $location.path('/');
                        });
                };

                $scope.reloadMyIssues = function() {
                    dashboardService.getMyIssues($scope.issueParams.pageSize, $scope.issueParams.pageNumber)
                        .then(function(receivedIssues){
                            $scope.myIssues = receivedIssues;
                            var issuesLength = receivedIssues.Issues.length;
                            for(var i = 0; i < issuesLength; i++) {
                                var issue = receivedIssues.Issues[i];
                                if(!projectIds[issue.Project.Id]){
                                    affiliatedProjects.push(issue.Project);
                                    projectIds[issue.Project.Id] = issue.Project.Id;
                                }
                            }
                    }, function(error) {
                        notifyService.showError('Cannot load issues', error);
                    })
                };
            }
        }
    ]);