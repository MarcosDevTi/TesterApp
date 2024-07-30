using FluentNHibernate.Cfg.Db;
using NHibernate.Dialect;
using System;

namespace TesterApp.Api472.Builders
{
    public class PersistenceConfigurerBuilder
    {
        private string connectionStringKey;
        private string dialectQualifiedName;

        public PersistenceConfigurerBuilder WithConnectionStringKey(string key)
        {
            connectionStringKey = key;
            return this;
        }

        public PersistenceConfigurerBuilder WithDialect<T>() where T : Dialect
        {
            dialectQualifiedName = typeof(T).AssemblyQualifiedName;
            return this;
        }

        public IPersistenceConfigurer Build()
        {
            return MsSqlConfiguration.MsSql2012
                .ConnectionString(c => c.FromConnectionStringWithKey(connectionStringKey))
                .Dialect(dialectQualifiedName);
        }
    }
}