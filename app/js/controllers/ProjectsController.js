angular.module('issueTrackingSystem.projects', [])
    .controller('ProjectsController',[
        '$scope',
        '$routeParams',
        'projectsService',
        function($scope, $routeParams, projectsService){
            projectsService.getAllProjects().then(function(receivedProjects){
                $scope.allProjects = receivedProjects;
            });

            if($routeParams.id != undefined) {
                projectsService.getProjectById($routeParams.id).then(function(receivedProject){
                    $scope.projectById = receivedProject;
                });
            }
        }
    ]);
