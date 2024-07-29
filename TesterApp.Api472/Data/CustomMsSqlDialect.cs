using NHibernate;
using NHibernate.Dialect;
using NHibernate.Dialect.Function;

namespace TesterApp.Api472.Data
{
    public class CustomMsSqlDialect : MsSql2012Dialect
    {
        public CustomMsSqlDialect()
        {
            RegisterFunction(
                "AddDays",
                new SQLFunctionTemplate(NHibernateUtil.DateTime, "DATEADD(day, ?2, ?1)")
            );
        }
    }
}