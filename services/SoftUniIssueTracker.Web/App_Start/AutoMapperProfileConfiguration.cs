using AutoMapper;
using SIT.Models;
using SIT.Web.BindingModels.Comment;
using SIT.Web.BindingModels.Issue;
using SIT.Web.BindingModels.Project;
using SIT.Web.ViewModels.Comment;
using SIT.Web.ViewModels.Issue;
using SIT.Web.ViewModels.Label;
using SIT.Web.ViewModels.Priority;
using SIT.Web.ViewModels.Project;
using SIT.Web.ViewModels.Status;
using SIT.Web.ViewModels.User;

namespace SIT.Web
{
    public class AutoMapperProfileConfiguration : Profile
    {
        protected override void Configure()
        {
            CreateMap<Project, ProjectViewModel>();
            CreateMap<Issue, IssueViewModel>();
            CreateMap<ProjectBindingModel, Project>();
            CreateMap<IssueBindingModel, Issue>().ForMember(x => x.IssueLabels, opt => opt.Ignore());
            CreateMap<User, UserViewModel>();
            CreateMap<Priority, PriorityViewModel>();
            CreateMap<Status, StatusViewModel>();
            CreateMap<ProjectPriority, PriorityViewModel>().ForMember(dest => dest.Name,
               opts => opts.MapFrom(src => src.Priority.Name)).ForMember(dest => dest.Id,
               opts => opts.MapFrom(src => src.Priority.Id));
            CreateMap<ProjectLabel, LabelViewModel>().ForMember(dest => dest.Name,
              opts => opts.MapFrom(src => src.Label.Name)).ForMember(dest => dest.Id,
              opts => opts.MapFrom(src => src.Label.Id));
            CreateMap<IssueLabel, LabelViewModel>().ForMember(dest => dest.Name,
             opts => opts.MapFrom(src => src.Label.Name)).ForMember(dest => dest.Id,
             opts => opts.MapFrom(src => src.Label.Id));
            CreateMap<CommentBindingModel, Comment>();
            CreateMap<Comment, CommentViewModel>();
            CreateMap<Project, ProjectIssueViewModel>();
            CreateMap<Label, LabelViewModel>();
        }
    }
}
