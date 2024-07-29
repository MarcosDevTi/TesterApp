using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;
using NHibernate.Hql.Ast;
using NHibernate.Linq;
using NHibernate.Linq.Functions;
using NHibernate.Linq.Visitors;
using NHibernate.Util;

namespace TesterApp.Api472.Data
{
    public class AddDaysGenerator : BaseHqlGeneratorForMethod
    {
        public AddDaysGenerator()
        {
            SupportedMethods = new[] { ReflectionHelper.GetMethodDefinition<DateTime>(d => d.AddDays(0)) };
        }

        public override HqlTreeNode BuildHql(MethodInfo method, Expression targetObject, ReadOnlyCollection<Expression> arguments, HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
        {
            return treeBuilder.MethodCall("AddDays",
                visitor.Visit(targetObject).AsExpression(),
                visitor.Visit(arguments[0]).AsExpression());
        }
    }
}