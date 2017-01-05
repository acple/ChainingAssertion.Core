using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ChainingAssertion
{
    internal class ExpressionDumper : ExpressionVisitor
    {
        private readonly Dictionary<string, ReflectAccessor> members;

        private ExpressionDumper(IEnumerable<(ParameterExpression parameter, object target)> parameters)
        {
            this.members = parameters.ToDictionary(x => x.parameter.Name, x => new ReflectAccessor(x.target));
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            var parent = node.Expression.ToString();
            var name = node.Member.Name;
            var fullname = parent + "." + name;

            if (this.members.ContainsKey(fullname))
                return node;

            this.Visit(node.Expression);

            if (this.members.TryGetValue(parent, out var accessor))
                this.members.Add(fullname, new ReflectAccessor(accessor[name]));

            return node;
        }

        public static IReadOnlyDictionary<string, object> Dump(LambdaExpression expression, params object[] targets)
        {
            if (expression.Parameters.Count != targets.Length)
                throw new ArgumentException("parameter length doesn't match");

            var dumper = new ExpressionDumper(expression.Parameters.Zip(targets, ValueTuple.Create));
            dumper.Visit(expression.Body);
            return dumper.members.ToDictionary(x => x.Key, x => x.Value.Target);
        }
    }
}
