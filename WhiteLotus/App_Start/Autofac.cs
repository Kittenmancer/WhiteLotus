using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using NHibernate;

namespace WhiteLotus
{
    public class AutofacConfig
    {
        public static IContainer RegistgerComponents()
        {
            var builder = new ContainerBuilder();

            // NHibernate components
            builder.Register(c => NHibernateConfig.ConfigureNHibernate()).As<ISessionFactory>().SingleInstance();
            builder.Register(c => c.Resolve<ISessionFactory>().OpenSession()).InstancePerRequest();

            // Controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Property injection on filter attributes
            builder.RegisterFilterProvider();

            // NHibernate Entity Binder
            builder.Register(c => new NHibernateEntityBinder(c.Resolve<ISession>())).InstancePerRequest();


            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            ModelBinders.Binders.DefaultBinder = new RequestDelegatedBinder<NHibernateEntityBinder>(container);

            return container;
        }
    }
}