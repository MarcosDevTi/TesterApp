using NHibernate.Dialect.Function;
using NHibernate.Dialect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesterApp.Api472.Builders
{
    public class DialectFunctionBuilder
    {
        private MsSql2012Dialect dialect;

        public DialectFunctionBuilder(MsSql2012Dialect dialect)
        {
            this.dialect = dialect;
        }

        public DialectFunctionBuilder RegisterFunction(string name, ISQLFunction function)
        {
          //  dialect.RegisterFunction(name, function);
            return this;
        }

        public MsSql2012Dialect Build()
        {
            return dialect;
        }
    }
}