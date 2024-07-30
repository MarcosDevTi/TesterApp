using FluentNHibernate.Conventions;
using NHibernate.Hql.Ast;
using NHibernate.Linq.Functions;
using NHibernate.Linq.Visitors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;

namespace TesterApp.Api472.Data
{
    public class CustomLinqToHqlGeneratorsRegistry : DefaultLinqToHqlGeneratorsRegistry
    {
        private static IList<MethodInfo> _methodInfos = new List<MethodInfo>();
        public CustomLinqToHqlGeneratorsRegistry()
        {
            this.RegisterCustomGenerators();
        }

        private void RegisterCustomGenerators()
        {
            foreach (var methodInfo in _methodInfos)
            {
                this.RegisterGenerator(methodInfo, new SimpleHqlGenerator(methodInfo));
            }
        }

        public static void AddCustomGenerators(MethodInfo methodInfo)
        {
            _methodInfos.Add(methodInfo);
        }
    }

    public class SimpleHqlGenerator : BaseHqlGeneratorForMethod
    {
        private readonly string _methodName;

        public SimpleHqlGenerator(MethodInfo method)
        {
            this._methodName = method.Name;
            this.SupportedMethods = new[] { method };
        }

        public override HqlTreeNode BuildHql(MethodInfo method, Expression targetObject, ReadOnlyCollection<Expression> arguments, HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
        {
            return treeBuilder.MethodCall(_methodName,
                visitor.Visit(targetObject).AsExpression(),
                visitor.Visit(arguments[0]).AsExpression());
        }
    }
}