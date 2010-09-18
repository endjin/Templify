namespace Endjin.Templify.Domain.Framework.Specifications
{
    #region Using Directives

    using System;
    using System.Linq;
    using System.Linq.Expressions;

    #endregion

    public static class QuerySpecificationExtensions
    {
        public static QuerySpecification<T> And<T>(this QuerySpecification<T> specification1, QuerySpecification<T> specification2)
        {
            var adhocSpec1 = new AdHocSpecification<T>(specification1.MatchingCriteria);
            var adhocSpec2 = new AdHocSpecification<T>(specification2.MatchingCriteria);

            var invokedExpr = Expression.Invoke(adhocSpec2.MatchingCriteria, adhocSpec1.MatchingCriteria.Parameters.Cast<Expression>());
            var dynamicClause = Expression.Lambda<Func<T, bool>>(Expression.AndAlso(adhocSpec1.MatchingCriteria.Body, invokedExpr), adhocSpec1.MatchingCriteria.Parameters);

            return new AdHocSpecification<T>(dynamicClause);
        }

        public static QuerySpecification<T> Or<T>(this QuerySpecification<T> specification1, QuerySpecification<T> specification2)
        {
            var adhocSpec1 = new AdHocSpecification<T>(specification1.MatchingCriteria);
            var adhocSpec2 = new AdHocSpecification<T>(specification2.MatchingCriteria);

            var invokedExpr = Expression.Invoke(adhocSpec2.MatchingCriteria, adhocSpec1.MatchingCriteria.Parameters.Cast<Expression>());
            var dynamicClause = Expression.Lambda<Func<T, bool>>(Expression.OrElse(adhocSpec1.MatchingCriteria.Body, invokedExpr), adhocSpec1.MatchingCriteria.Parameters);

            return new AdHocSpecification<T>(dynamicClause);
        }
    }
}