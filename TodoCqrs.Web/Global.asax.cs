using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SimpleInjector;
using SimpleInjector.Extensions;
using SimpleInjector.Integration.WebApi;
using TodoCqrs.Web.Bus;
using TodoCqrs.Web.CommandHandlers;
using TodoCqrs.Web.Data;
using TodoCqrs.Web.Decorators;
using TodoCqrs.Web.Infrastructure;
using TodoCqrs.Web.QueryHandlers;

namespace TodoCqrs.Web
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            InitializeSimpleInjector();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public static void InitializeSimpleInjector()
        {
            var container = new Container();

            InitializeContainer(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static void InitializeContainer(Container container)
        {
            container.Register<IQueryBus, QueryBus>();

            container.RegisterManyForOpenGeneric(typeof(IQueryHandler<,>), typeof(IQueryHandler<,>).Assembly);

            container.Register<ICommandBus, CommandBus>();

            container.RegisterManyForOpenGeneric(typeof(ICommandHandler<>), typeof(ICommandHandler<>).Assembly);
            container.RegisterDecorator(typeof (ICommandHandler<>), typeof (CommitUnitOfWorkDecorator<>));
            container.RegisterDecorator(typeof (ICommandHandler<>), typeof (PostCommitEventDecorator<>));

            container.RegisterWebApiRequest<PostCommitEvent>();
            container.Register<IPostCommitEvent>(container.GetInstance<PostCommitEvent>);
            
            container.RegisterWebApiRequest<TodoDbContext>();
            container.Register<IRepository, DbContextRepository>();

            container.Register<IUnitOfWork, DbContextUnitOfWork>();
        }
    }
}