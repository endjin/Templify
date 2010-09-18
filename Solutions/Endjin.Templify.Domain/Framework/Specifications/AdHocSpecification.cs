namespace Endjin.Templify.Domain.Framework.Specifications
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;

    #endregion

    public class AdHocSpecification<T> : QuerySpecification<T>
    {
        private readonly LambdaExpression expression;

        public AdHocSpecification(LambdaExpression expression)
        {
            this.expression = expression;
        }

        public override Expression<Func<T, bool>> MatchingCriteria
        {
            get { return this.expression as Expression<Func<T, bool>>; }
        }
    }
}