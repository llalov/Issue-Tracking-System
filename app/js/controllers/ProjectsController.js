angular.module('issueTrackingSystem.projects', [])
    .controller('ProjectsController',[
        '$scope',
        '$routeParams',
        'projectsService',
        'pageNumber',
        'authService',
        function($scope, $routeParams, projectsService, pageNumber, authService){
            $scope.projectsParams = {
                pageSize: 6,
                pageNumber: pageNumber
            };

            if(authService.isLoggedIn()){
                projectsService.getAllProjects($scope.projectsParams.pageSize, $scope.projectsParams.pageNumber)
                    .then(function(receivedProjects){
                        $scope.allProjects = receivedProjects;
                    });
            }

            $scope.reloadProjects = function() {
                projectsService.getAllProjects($scope.projectsParams.pageSize, $scope.projectsParams.pageNumber)
                    .then(function(receivedProjects){
                        $scope.allProjects = receivedProjects;
                    });
            };

            if($routeParams.id != undefined) {
                projectsService.getProjectById($routeParams.id).then(function(receivedProject){
                    $scope.projectById = receivedProject;
                });
            }
        }
    ]);
