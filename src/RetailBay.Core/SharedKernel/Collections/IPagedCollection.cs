using System.Collections.Generic;

namespace RetailBay.Core.SharedKernel.Collections
{
    /// <summary>
    /// PagedCollection contract.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Collections.Generic.IEnumerable{T}" />
    public interface IPagedCollection<T> : IEnumerable<T>
    {
        /// <summary>
        /// Gets the total item count.
        /// </summary>
        /// <value>
        /// The total item count.
        /// </value>
        int TotalItemCount { get; }

        /// <summary>
        /// Gets the number of available pages.
        /// </summary>
        /// <value>
        /// The number of available pages.
        /// </value>
        int PageCount { get; }

        /// <summary>
        /// Gets the current page number.
        /// </summary>
        /// <value>
        /// The current page number.
        /// </value>
        int PageNumber { get; }

        /// <summary>
        /// Gets the max size of the page.
        /// </summary>
        /// <value>
        /// The max size of the page.
        /// </value>
        int PageSize { get; }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        IEnumerable<T> Items { get; }
    }
}
