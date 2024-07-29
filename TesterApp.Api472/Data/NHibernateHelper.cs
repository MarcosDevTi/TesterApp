using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace TesterApp.Api472.Data
{
    public static class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    _sessionFactory = CreateSessionFactory();
                }
                return _sessionFactory;
            }
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString(c => c
                        .FromConnectionStringWithKey("Server=localhost;Database=TesterApp;Trusted_Connection=True;TrustServerCertificate=True;"))
                    .Dialect<CustomMsSqlDialect>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<CustomerMap>())
                .ExposeConfiguration(cfg =>
                {
                    cfg.LinqToHqlGeneratorsRegistry<ExtendedLinqToHqlGeneratorsRegistry>();
                    new SchemaExport(cfg).Create(false, false);
                })
                .BuildSessionFactory();
        }
    }
}