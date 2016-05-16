using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using AutoMapper;
using SIT.Data.Interfaces;
using SIT.Models;
using SIT.Web.BindingModels.Project;
using SIT.Web.Services.Interfaces;
using SIT.Web.ViewModels.Label;
using SIT.Web.ViewModels.Priority;
using SIT.Web.ViewModels.Project;

namespace SIT.Web.Services
{
    public class ProjectsService : BaseService, IProjectsService
    {
        private ITransitionSchemeService transitionSchemeService;

        public ProjectsService(ISoftUniIssueTrackerData data, IMapper mapper, ITransitionSchemeService transitionSchemeService)
            : base(data, mapper)
        {
            this.transitionSchemeService = transitionSchemeService;
        }

        public ProjectViewModel Add(string userId, ProjectBindingModel model)
        {
            var user = this.data.UserRepository.GetById(userId);
            if (!user.isAdmin)
            {
                throw new InvalidOperationException(Constants.NotAdmin);
            }

            var project = mapper.Map<ProjectBindingModel, Project>(model);

            var leadUser = this.data.UserRepository.GetById(model.LeadId);
            if (leadUser == null)
            {
                throw new ArgumentException(Constants.UnexistingUserErrorMessage);
            }
            project.Lead = leadUser;

            AddTransitionScheme(model.TransitionSchemeId, project);
            if (model.Labels != null)
                AddLabels(model.Labels, project);
            AddPriorities(model.Priorities, project);

            this.data.ProjectRepository.Insert(project);
            this.data.Save();

            return GetMappedProject(project);
        }

        public ProjectViewModel Edit(string userId, int id, ProjectEditBindingModel model)
        {
            var project = this.data.ProjectRepository.GetById(id)
                .Include(p => p.ProjectLabels.Select(pl => pl.Label))
                .Include(p => p.ProjectPriorities.Select(pp => pp.Priority))
                .FirstOrDefault();
            if (project == null)
            {
                throw new ArgumentException(Constants.UnexistingProjectErrorMessage);
            }

            var user = this.data.UserRepository.GetById(userId);
            if (project.LeadId != user.Id && !user.isAdmin)
            {
                throw new InvalidOperationException(Constants.NotProjectLeaderOrAdmin);
            }

            if (project.LeadId != model.LeadId)
            {
                if (!user.isAdmin)
                {
                    throw new InvalidOperationException(Constants.NotAdmin);
                }
                var projectLead = this.data.UserRepository.GetById(model.LeadId);
                if (projectLead == null)
                {
                    throw new ArgumentException(Constants.UnexistingUserErrorMessage);
                }
                project.LeadId = model.LeadId;
            }
            project.Name = model.Name;
            project.Description = model.Description;

            var labels = this.data.ProjectLabelsRepository.Get().Where(pl => pl.ProjectId == project.Id);
            foreach (var projectLabel in labels)
            {
                this.data.ProjectLabelsRepository.Delete(projectLabel);
            }

            if (model.Labels != null)
                AddLabels(model.Labels, project);

            if (model.Priorities != null)
            {
                var priorities = this.data.ProjectPrioritiesRepository.Get().Where(pl => pl.ProjectId == project.Id);
                foreach (var priority in priorities)
                {
                    this.data.ProjectPrioritiesRepository.Delete(priority);
                }

                AddPriorities(model.Priorities, project);
            }
            AddTransitionScheme(model.TransitionSchemeId, project);

            this.data.Save();

            return GetMappedProject(project);
        }

        public IEnumerable<ProjectViewModel> Get()
        {
            var projects = this.data.ProjectRepository.Get()
                .Include(p => p.ProjectLabels.Select(pl => pl.Label))
                .Include(p => p.ProjectPriorities.Select(pp => pp.Priority))
                .OrderBy(p => p.Id)
                .ToList();

            return GetMappedProjects(projects);
        } 

        public ProjectWithPagesViewModel Get(int pageSize, int pageNumber, string filter)
        {
            var projectsQuery = this.data.ProjectRepository.Get()
                .Include(p => p.ProjectLabels.Select(pl => pl.Label))
                .Include(p => p.ProjectPriorities.Select(pp => pp.Priority))
                .OrderBy(p => p.Id)
                .AsQueryable();

            if (filter != null)
            {
                var specialFilterIndex = filter.IndexOf(Constants.SpecialFilterLabelPrefix, StringComparison.InvariantCulture);
                if (specialFilterIndex > -1)
                {
                    var labelName = filter.Substring(specialFilterIndex + Constants.SpecialFilterLabelPrefix.Length);
                    var label = this.data.LabelRepository.Get(l => l.Name == labelName).FirstOrDefault();
                    if (label == null)
                    {
                        throw new ArgumentException(Constants.UnexistingLabel);
                    }

                    projectsQuery = projectsQuery.Where(p => p.ProjectLabels.Any(pl => pl.LabelId == label.Id));
                }
                else
                {
                    projectsQuery = projectsQuery.Where(filter);
                }
            }

            var totalCount = projectsQuery.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);

            var projects = projectsQuery.Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();
            var mappedProjects = GetMappedProjects(projects);

            return new ProjectWithPagesViewModel()
            {
                TotalPages = totalPages,
                Projects = mappedProjects,
                TotalCount = totalCount
            };
        }

        public ProjectWithPagesViewModel GetByLabel(int pageSize, int pageNumber, string labelName)
        {
            return Get(pageSize, pageNumber, Constants.SpecialFilterLabelPrefix + labelName);
        }

        public ProjectViewModel GetById(int id)
        {
            var project = this.data.ProjectRepository.GetById(id)
                .Include(p => p.Lead)
                .Include(p => p.ProjectLabels.Select(pl => pl.Label))
                .Include(p => p.ProjectPriorities.Select(pp => pp.Priority))
                .FirstOrDefault();

            if (project == null)
            {
                throw new ArgumentException(Constants.UnexistingProjectErrorMessage);
            }

            return GetMappedProject(project);
        }

        private void AddLabels(IEnumerable<Label> labels, Project project)
        {
            foreach (var label in labels)
            {
                var labelEntity = this.data.LabelRepository.Get(l => l.Name == label.Name).FirstOrDefault();
                if (labelEntity == null)
                {
                    labelEntity = new Label()
                    {
                        Name = label.Name
                    };
                    this.data.LabelRepository.Insert(labelEntity);
                }

                var projectLabel = new ProjectLabel()
                {
                    Label = labelEntity,
                    Project = project
                };

                project.ProjectLabels.Add(projectLabel);
            }
        }

        private void AddPriorities(List<Priority> priorities, Project project)
        {
            foreach (var priority in priorities)
            {
                var priorityEntity = this.data.PriorityRepository.Get(l => l.Name == priority.Name).FirstOrDefault();
                if (priorityEntity == null)
                {
                    priorityEntity = new Priority()
                    {
                        Name = priority.Name
                    };
                    this.data.PriorityRepository.Insert(priorityEntity);
                }

                var projectPriority = new ProjectPriority()
                {
                    Priority = priorityEntity,
                    Project = project
                };

                project.ProjectPriorities.Add(projectPriority);
            }
        }

        private void AddTransitionScheme(int? transitionSchemeId, Project project)
        {
            if (transitionSchemeId != null)
            {
                var transitionSchemeEntity = this.data.TransitionSchemeRepository.GetById(transitionSchemeId.Value);
                if (transitionSchemeEntity == null)
                {
                    throw new ArgumentException(Constants.UnexistingTransitionSchemeErrorMessage);
                }
                project.TransitionSchemeId = transitionSchemeId.Value;
            }
            else
            {
                var transitionScheme = this.data.TransitionSchemeRepository.Get(t => t.IsDefault).FirstOrDefault();
                if (transitionScheme == null)
                {
                    transitionScheme = transitionSchemeService.AddDefaultTransitionScheme();
                }

                project.TransitionScheme = transitionScheme;
            }
        }

        private ProjectViewModel GetMappedProject(Project project)
        {
            return GetMappedProjects(new List<Project>() { project }).First();
        }

        private IEnumerable<ProjectViewModel> GetMappedProjects(IEnumerable<Project> projects)
        {
            var mappedProjects = new List<ProjectViewModel>();

            foreach (var project in projects)
            {
                var mappedProject = this.mapper.Map<Project, ProjectViewModel>(project);
                mappedProject.Labels =
                    this.mapper.Map<ICollection<ProjectLabel>, ICollection<LabelViewModel>>(project.ProjectLabels);
                mappedProject.Priorities =
                    this.mapper.Map<ICollection<ProjectPriority>, ICollection<PriorityViewModel>>(
                        project.ProjectPriorities);
                mappedProjects.Add(mappedProject);
            }

            return mappedProjects;
        }
    }
}
