namespace Endjin.Templify.Domain.Contracts.Framework.Specifications
{
    #region Using Directives

    using System.Linq;

    #endregion

    /// <summary>
    /// Defines a contract for the behaviour of a LINQ Specification design pattern.
    /// </summary>
    /// <typeparam name="T">
    /// Type to be used for Input / Output
    /// </typeparam>
    public interface ILinqSpecification<T>
    {
        /// <summary>
        /// satisfying elements from.
        /// </summary>
        /// <param name="candidates">
        /// The candidates.
        /// </param>
        /// <returns>
        /// A list of satisfying elements.
        /// </returns>
        IQueryable<T> SatisfyingElementsFrom(IQueryable<T> candidates);
    }
}