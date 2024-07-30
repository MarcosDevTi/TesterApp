using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate.Dialect.Function;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using NHibernate.Tool.hbm2ddl;

namespace TesterApp.Api472.Data
{
    public class SessionFactoryBuilder
    {
        private string _connectionStringKey;
        private Type _mappingsAssemblyType;
        private List<(MethodInfo MethodInfo, ISQLFunction Function)> _generators = new List<(MethodInfo MethodInfo, ISQLFunction Function)>();

        public SessionFactoryBuilder WithConnectionString(string connectionStringKey)
        {
            _connectionStringKey = connectionStringKey;
            return this;
        }

        public SessionFactoryBuilder WithMappingsFromAssemblyOf<T>()
        {
            _mappingsAssemblyType = typeof(T);
            return this;
        }

        public SessionFactoryBuilder WithGenerators(List<(MethodInfo MethodInfo, ISQLFunction Function)> generators)
        {
            _generators = generators;
            return this;
        }

        public ISessionFactory Build()
        {
            var fluentConfig = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString(c => c
                        .FromConnectionStringWithKey(_connectionStringKey)));

            if (_generators.Any())
            {
                foreach (var generator in _generators)
                {
                    CustomMsSqlDialect.AddCustomFunction(generator.MethodInfo.Name, generator.Function);
                    CustomLinqToHqlGeneratorsRegistry.AddCustomGenerators(generator.MethodInfo);
                }

                fluentConfig = fluentConfig
                    .Database(MsSqlConfiguration.MsSql2012
                        .Dialect<CustomMsSqlDialect>())
                    .Mappings(m => m.FluentMappings.AddFromAssembly(_mappingsAssemblyType.Assembly))
                    .ExposeConfiguration(cfg =>
                    {
                        cfg.LinqToHqlGeneratorsRegistry<CustomLinqToHqlGeneratorsRegistry>();
                        new SchemaExport(cfg).Create(false, false);
                    });
            }
            else
            {
                fluentConfig = fluentConfig
                    .Mappings(m => m.FluentMappings.AddFromAssembly(_mappingsAssemblyType.Assembly))
                    .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false, false));
            }

            return fluentConfig.BuildSessionFactory();
        }
    }
}