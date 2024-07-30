using NHibernate.Hql.Ast;
using NHibernate.Linq.Functions;
using NHibernate.Linq.Visitors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using NHibernate.Cfg;
using NHibernate.Linq;

namespace TesterApp.Api472.Data
{
    public class CustomHqlGeneratorBuilder
    {
        private readonly IDictionary<MethodInfo, string> methodsToNames = new Dictionary<MethodInfo, string>();

        public CustomHqlGeneratorBuilder AddDayMethod()
        {
            var methodInfo = ReflectionHelper.GetMethodDefinition<DateTime>(d => d.AddDays(0));
            methodsToNames[methodInfo] = "AddDays";
            return this;
        }

        public CustomHqlGeneratorBuilder AddMonthMethod()
        {
            var methodInfo = ReflectionHelper.GetMethodDefinition<DateTime>(d => d.AddMonths(0));
            methodsToNames[methodInfo] = "AddMonths";
            return this;
        }

        public CustomHqlGeneratorBuilder AddYearMethod()
        {
            var methodInfo = ReflectionHelper.GetMethodDefinition<DateTime>(d => d.AddYears(0));
            methodsToNames[methodInfo] = "AddYears";
            return this;
        }

        public void RegisterAll(Configuration cfg)
        {
            foreach (var methodToName in methodsToNames)
            {
                cfg.LinqToHqlGeneratorsRegistry<CustomLinqToHqlGeneratorsRegistry>();
        //        CustomLinqToHqlGeneratorsRegistry.RegisterGenerator(methodToName.Key, new SimpleHqlGenerator(methodToName.Value));
            }
        }

        private class SimpleHqlGenerator : BaseHqlGeneratorForMethod
        {
            private readonly string methodName;

            public SimpleHqlGenerator(string methodName)
            {
                this.methodName = methodName;
                SupportedMethods = new[] { ReflectionHelper.GetMethodDefinition<DateTime>(d => d.AddDays(0)) };
            }

            public override HqlTreeNode BuildHql(MethodInfo method, Expression targetObject, ReadOnlyCollection<Expression> arguments, HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
            {
                return treeBuilder.MethodCall(methodName,
                    visitor.Visit(targetObject).AsExpression(),
                    visitor.Visit(arguments[0]).AsExpression());
            }
        }
    }

}