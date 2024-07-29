using NHibernate.Linq.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesterApp.Api472.Data
{
    public class ExtendedLinqToHqlGeneratorsRegistry : DefaultLinqToHqlGeneratorsRegistry
    {
        public ExtendedLinqToHqlGeneratorsRegistry()
        {
            this.Merge(new AddDaysGenerator());
        }
    }
}