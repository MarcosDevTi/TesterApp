using NHibernate;
using NHibernate.Dialect;
using NHibernate.Dialect.Function;
using System.Collections.Generic;

namespace TesterApp.Api472.Data
{
    public class CustomMsSqlDialect : MsSql2012Dialect
    {
        private static List<(string Name, ISQLFunction Function)> _customFunctions = new List<(string Name, ISQLFunction Function)>();

        public CustomMsSqlDialect()
        {
            this.RegisterCustomFunctions();
        }

        private void RegisterCustomFunctions()
        {
            foreach (var function in _customFunctions)
            {
                this.RegisterFunction(function.Name, function.Function);
            }
        }

        public static void AddCustomFunction(string name, ISQLFunction function)
        {
            _customFunctions.Add((name, function));
        }
    }
}