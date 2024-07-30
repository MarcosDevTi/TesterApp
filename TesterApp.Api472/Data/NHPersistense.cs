using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Dialect.Function;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Reflection;
using NHibernate.Util;
using System.Collections.Generic;
using NHibernate.Cfg;
using TesterApp.Api472.Builders;

namespace TesterApp.Api472.Data
{
    public class NHPersistense
    {
        private ISessionFactory _sessionFactory;
        private List<(MethodInfo MethodInfo, ISQLFunction Function)> _generators = new List<(MethodInfo MethodInfo, ISQLFunction Function)>();
        public ISessionFactory SessionFactory
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

        public void AddDialects(params (MethodInfo MethodInfo, ISQLFunction Function)[] dialects)
        {
            foreach (var dialect in dialects)
            {
                CustomMsSqlDialect.AddCustomFunction(dialect.MethodInfo.Name, dialect.Function);
                CustomLinqToHqlGeneratorsRegistry.AddCustomGenerators(dialect.MethodInfo);
            }
        }

        private  ISessionFactory CreateSessionFactory()
        {
            var fluentConfigure = Fluently.Configure();

            fluentConfigure.Database(MsSqlConfiguration.MsSql2012
                .ConnectionString(c => c
                    .FromConnectionStringWithKey("TesterAppDatabase")));

            var persistenceConfigurerBuilder = new PersistenceConfigurerBuilder().WithConnectionStringKey("TesterAppDatabase")
                .WithDialect<CustomMsSqlDialect>();

           // Action<Configuration> config = 

            return Fluently.Configure()
                .Database(persistenceConfigurerBuilder.Build())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<CustomerMap>())
                .ExposeConfiguration(cfg =>
                {
                    cfg.LinqToHqlGeneratorsRegistry<CustomLinqToHqlGeneratorsRegistry>();
                    new SchemaExport(cfg).Create(false, false);

                })
                .BuildSessionFactory();
        }
    }
}