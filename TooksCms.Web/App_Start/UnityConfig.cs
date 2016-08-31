using Microsoft.Practices.Unity;
using System.Web.Http;
using TooksCms.Core.Interfaces.Repository;
using TooksCms.DAL;
using Unity.WebApi;

namespace TooksCms.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

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
            container.RegisterType<IWeddingRepository, WeddingRepository>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}