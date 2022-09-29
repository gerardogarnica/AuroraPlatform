using System.Linq.Expressions;

namespace Aurora.Framework
{
    internal class ReplaceVisitor : ExpressionVisitor
    {
        private readonly Expression _from, _to;

        internal ReplaceVisitor(Expression from, Expression to)
        {
            _from = from;
            _to = to;
        }

        public override Expression Visit(Expression node)
        {
            return node == _from ? _to : base.Visit(node);
        }
    }
}