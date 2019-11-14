using System.Collections.Generic;

namespace RetailBay.Common.QueryParameters
{
    public class SortingParameters : ISortingParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SortingParameters"/> class.
        /// </summary>
        public SortingParameters()
        {
            this.Sorters = new List<ISortingPair>();
        }

        /// <summary>
        /// Gets the sort pairs.
        /// </summary>
        public IList<ISortingPair> Sorters { get; private set; }

        /// <summary>
        /// Adds a new sorting pair.
        /// </summary>
        /// <param name="orderBy">The order by.</param>
        /// <param name="isAscending">if set to <c>true</c> [is ascending].</param>
        public void Add(string orderBy, bool isAscending)
        {
            Sorters.Add(new SortingPair(orderBy, isAscending));
        }

        /// <summary>
        /// Adds a new sorting pair.
        /// </summary>
        /// <param name="sortingPair">The sorting pair.</param>
        public void Add(ISortingPair sortingPair)
        {
            Sorters.Add(sortingPair);
        }
    }
}
