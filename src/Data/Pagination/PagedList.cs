using System.Collections.Generic;

namespace siwar.Data.Pagination
{
    /// <summary>
    /// Represents a paged list of items.
    /// </summary>
    /// 
    /// <typeparam name="T">Page type.</typeparam>
    public class PagedList<T> where T : class
    {
        /// <summary>
        /// Gets or sets page size.
        /// </summary>
        public int Take { get; private set; }

        /// <summary>
        /// Gets or sets items to skip.
        /// </summary>
        public int Skip { get; private set; }

        /// <summary>
        /// Gets or sets items total count.
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// Gets or sets paged list items.
        /// </summary>
        public IList<T> Items { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}" /> class.
        /// </summary>
        public PagedList(int take, int skip, int totalCount, IList<T> items)
        {
            Take = take;
            Skip = skip;
            TotalCount = totalCount;
            Items = items;
        }
    }

    /// <summary>
    /// Paged list filter.
    /// </summary>
    public class Filter
    {
        public string Field { get; set; }

        public object Id { get; set; }

        public object Value { get; set; }

        /// <summary>
        /// In case there is a dependence
        /// </summary>
        public object Parents { get; set; }

        public Filter(string field, object id, object value, object parents)
        {
            Field = field;
            Id = id;
            Value = value;
            Parents = parents;
        }
    }
}
