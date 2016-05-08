angular.module('issueTrackingSystem.projects', [])
    .controller('ProjectsController',[
        '$scope',
        '$routeParams',
        '$location',
        'projectsService',
        'issuesService',
        'notifyService',
        'pageNumber',
        'authService',
        'usersService',
        function($scope, $routeParams, $location, projectsService, issuesService, notifyService, pageNumber, authService, usersService){
            $scope.projectsParams = {
                pageSize: 6,
                pageNumber: pageNumber
            };

            if(authService.isLoggedIn()){
                projectsService.getAllProjects($scope.projectsParams.pageSize, $scope.projectsParams.pageNumber)
                    .then(function(receivedProjects){
                        $scope.allProjects = receivedProjects;
                    });

                $scope.reloadProjects = function() {
                    projectsService.getAllProjects($scope.projectsParams.pageSize, $scope.projectsParams.pageNumber)
                        .then(function(receivedProjects){
                            $scope.allProjects = receivedProjects;
                        });
                };

                $scope.addIssue = function(title, description, dueDate, projectId, assigneeId, priorityId, labelOne, labelTwo) {
                    issuesService.addIssue(title, description, dueDate, projectId, assigneeId, priorityId, labelOne, labelTwo,
                        function success(){
                            notifyService.showInfo('Issue added');
                            $location.path('/');
                        },
                        function error(err) {
                            notifyService.showError('Unsuccessful', err);
                        })
                };

                usersService.getAllUsers().then(function(receivedUsers) {
                    $scope.allUsers = receivedUsers;
                });

            }

            if($routeParams.id != undefined) {
                projectsService.getProjectById($routeParams.id).then(function(receivedProject){
                    $scope.projectById = receivedProject;
                });
            }
        }
    ]);
