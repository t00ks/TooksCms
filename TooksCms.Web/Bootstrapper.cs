using System.Web.Mvc;
using Microsoft.Practices.Unity;
using TooksCms.Core.Interfaces.Repository;
using TooksCms.DAL;
using Unity.Mvc4;

namespace TooksCms.Web
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ILookupRepository, LookupRepository>();
            container.RegisterType<IBulletinRepository, BulletinRepository>();
            container.RegisterType<IArticleRepository, ArticleRepository>();
            container.RegisterType<IAccountRepository, AccountRepository>();
            container.RegisterType<IConfigRepository, ConfigRepository>();
            container.RegisterType<IEventRepository, EventRepository>();
            container.RegisterType<IStatsRepository, StatsRepository>();
            container.RegisterType<IGalleryRepository, GalleryRepository>();
            container.RegisterType<ISecurityRepository, SecurityRepository>();
            container.RegisterType<IContactRepository, ContactRepository>();
            container.RegisterType<ISnapshotRepository, SnapshotRepository>();
        }
    }
}