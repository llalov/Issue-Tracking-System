angular.module('issueTrackingSystem.projects', [])
    .controller('ProjectsController',[
        '$scope',
        '$routeParams',
        'projectsService',
        'pageNumber',
        function($scope, $routeParams, projectsService, pageNumber){
            projectsService.getAllProjects().then(function(receivedProjects){
                var pageNumber = pageNumber;
                $scope.allProjects = receivedProjects;
            });

            if($routeParams.id != undefined) {
                projectsService.getProjectById($routeParams.id).then(function(receivedProject){
                    $scope.projectById = receivedProject;
                });
            }
        }
    ]);
