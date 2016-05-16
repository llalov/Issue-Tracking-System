using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIT.Data;
using SIT.Data.Interfaces;
using SIT.Models;
using SIT.Models.Interfaces;

namespace SIT.Data.Repositories
{
    public class SoftUniIssueTrackerData : IDisposable, ISoftUniIssueTrackerData
    {
        private SITDbContext context;
        private UserRepository userRepository;
        private EntityRepository<Project> projectRepository;
        private EntityRepository<ProjectLabel> projectLabelsRepository;
        private EntityRepository<IssueLabel> issueLabelsRepository;
        private EntityRepository<ProjectPriority> projectPrioritiesRepository;
        private EntityRepository<Comment> commentRepository;
        private EntityRepository<Issue> issueRepository;
        private EntityRepository<Label> labelRepository;
        private EntityRepository<Priority> priorityRepository;
        private EntityRepository<Status> statusRepository;
        private EntityRepository<StatusTransition> statusTransitionRepository;
        private EntityRepository<TransitionScheme> transitionSchemeRepository;

        public SoftUniIssueTrackerData(SITDbContext dbContext)
        {
            this.context = dbContext;
        }

        public UserRepository UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new UserRepository(this.context);
                }
                return this.userRepository;
            }
        }

        public EntityRepository<ProjectLabel> ProjectLabelsRepository => this.GetRepository(ref this.projectLabelsRepository);
        public EntityRepository<ProjectPriority> ProjectPrioritiesRepository => this.GetRepository(ref this.projectPrioritiesRepository);
        public EntityRepository<Project> ProjectRepository => this.GetRepository(ref this.projectRepository);
        public EntityRepository<Comment> CommentRepository => this.GetRepository(ref this.commentRepository);
        public EntityRepository<Issue> IssueRepository => this.GetRepository(ref this.issueRepository);
        public EntityRepository<IssueLabel> IssueLabelsRepository => this.GetRepository(ref this.issueLabelsRepository);
        public EntityRepository<Label> LabelRepository => this.GetRepository(ref this.labelRepository);
        public EntityRepository<Priority> PriorityRepository => this.GetRepository(ref this.priorityRepository);
        public EntityRepository<Status> StatusRepository => this.GetRepository(ref this.statusRepository);
        public EntityRepository<StatusTransition> StatusTransitionRepository => this.GetRepository(ref this.statusTransitionRepository);
        public EntityRepository<TransitionScheme> TransitionSchemeRepository => this.GetRepository(ref this.transitionSchemeRepository);

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private EntityRepository<TEntity> GetRepository<TEntity>(ref EntityRepository<TEntity> repository)
            where TEntity : class, IDentificatable
        {
            if (repository == null)
            {
                repository = new EntityRepository<TEntity>(this.context);
            }

            return repository;
        }
    }
}
