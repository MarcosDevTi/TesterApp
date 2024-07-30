using FluentNHibernate.Cfg.Db;
using NHibernate.Dialect;
using System;

namespace TesterApp.Api472.Builders
{
    public class PersistenceConfigurerBuilder
    {
        private string _connectionStringKey;
        private string _dialectQualifiedName;

        public PersistenceConfigurerBuilder WithConnectionStringKey(string key)
        {
            this._connectionStringKey = key;
            return this;
        }

        public PersistenceConfigurerBuilder WithDialect<T>() where T : Dialect
        {
            this._dialectQualifiedName = typeof(T).AssemblyQualifiedName;
            return this;
        }

        public IPersistenceConfigurer Build()
        {
            return MsSqlConfiguration.MsSql2012
                .ConnectionString(c => c.FromConnectionStringWithKey(this._connectionStringKey))
                .Dialect(this._dialectQualifiedName);
        }
    }
}