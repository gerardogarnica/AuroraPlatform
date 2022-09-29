using System.Linq.Expressions;

namespace Aurora.Framework
{
    public static class BinaryExpressions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> filter, Expression<Func<T, bool>> expressionToAdd)
        {
            var newFilter = new ReplaceVisitor(filter.Parameters[0], expressionToAdd.Parameters[0])
                .Visit(filter.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.And(newFilter, expressionToAdd.Body), expressionToAdd.Parameters);
        }
    }
}