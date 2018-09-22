using System;
using System.Collections;
using System.Collections.Generic;

namespace RetailBay.Core.SharedKernel.Collections
{
    /// <summary>
    /// PagedCollection implementation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ApplicationCore.SharedKernel.Collections.IPagedCollection{T}" />
    public class PagedCollection<T> : IPagedCollection<T>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedCollection{T}"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="totalItemCount">The total item count.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <exception cref="InvalidOperationException">
        /// Total item count should be equal to or greater than 0
        /// or
        /// Page number should be greater than 0
        /// or
        /// Page size should be greater than 0
        /// </exception>
        /// <exception cref="ArgumentNullException">items</exception>
        public PagedCollection(IEnumerable<T> items, int totalItemCount, int pageNumber, int pageSize)
        {
            if (totalItemCount < 0) throw new InvalidOperationException("Total item count should be equal to or greater than 0");
            if (pageNumber <= 0) throw new InvalidOperationException("Page number should be greater than 0");
            if (pageSize <= 0) throw new InvalidOperationException("Page size should be greater than 0");

            this.Items = items ?? throw new ArgumentNullException(nameof(items));
            this.TotalItemCount = totalItemCount;
            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the total item count.
        /// </summary>
        /// <value>
        /// The total item count.
        /// </value>
        public int TotalItemCount { get; }

        /// <summary>
        /// Gets the number of available pages.
        /// </summary>
        /// <value>
        /// The number of available pages.
        /// </value>
        public int PageCount { get => TotalItemCount / PageSize + 1; }

        /// <summary>
        /// Gets the current page number.
        /// </summary>
        /// <value>
        /// The current page number.
        /// </value>
        public int PageNumber { get; }

        /// <summary>
        /// Gets the max size of the page.
        /// </summary>
        /// <value>
        /// The max size of the page.
        /// </value>
        public int PageSize { get; }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public IEnumerable<T> Items { get; }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion Methods
    }
}
