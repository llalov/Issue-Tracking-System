using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Ninject;
using Ninject.Web.Common;
using SIT.Data.Interfaces;
using SIT.Data.Repositories;
using SIT.Web.Services;
using SIT.Web.Services.Interfaces;

namespace SIT.Web
{
    public class WebApiApplication : NinjectHttpApplication
    {
        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfileConfiguration());
            });

            kernel.Bind<ISoftUniIssueTrackerData>().To<SoftUniIssueTrackerData>();
            kernel.Bind<IProjectsService>().To<ProjectsService>();
            kernel.Bind<IIssuesService>().To<IssuesService>();
            kernel.Bind<ITransitionSchemeService>().To<TransitionSchemesService>();
            kernel.Bind<ILabelsService>().To<LabelsService>();
            kernel.Bind<IUsersService>().To<UsersService>();
            kernel.Bind<IMapper>().ToMethod((_) => mapperConfiguration.CreateMapper());
        }
    }
}
