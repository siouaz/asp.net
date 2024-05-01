using System.Collections.Generic;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OeuilDeSauron.Data.Pagination
{
    public class SortOptions
    {
    /// <summary>
        /// Gets sort column, if any.
    /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// Gets sort direction.
        /// </summary>
        /// <remarks>
        /// <value><c>SortDirection.Asc</c></value>
        /// </remarks>
        [DefaultValue(SortDirection.Asc)]
        public SortDirection SortDirection { get; set; } = SortDirection.Asc;
    }

    public class PaginationOptions : SortOptions
    {
        /// <summary>
        /// Gets page size.
        /// </summary>
        /// <remarks>
        /// <value><c>10</c></value>
        /// </remarks>
        [DefaultValue(10)]
        [Range(1, int.MaxValue)]
        public int Take { get; set; } = 10;

        /// <summary>
        /// Gets items to skip.
        /// </summary>
        /// <remarks>
        /// <value><c>0</c></value>
        /// </remarks>
        [DefaultValue(0)]
        [Range(0, int.MaxValue)]
        public int Skip { get; set; } = 0;
    }

    /// <summary>
    /// Pagination options with search feature.
    /// </summary>
    /// <remarks>
    /// Used because we don't want to interfere with exposed APIs.
    /// </remarks>
    public class SearchablePaginationOptions : PaginationOptions
    {
        /// <summary>
        /// Gets or sets search term, if any.
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Whether take all data or use pagination.
        /// </summary>
        public bool TakeAll { get; set; } = false;
    }

    /// <summary>
    /// Pagination options with Filter feature.
    /// </summary>
    public class FilterPaginationOptions : SearchablePaginationOptions
    {
        public IEnumerable<PaginationFilter> Filters { get; set; }
    }

    /// <summary>
    /// Pagination filter.
    /// </summary>
    public class PaginationFilter
    {
        /// <summary>
        /// Gets or sets filter operator.
        /// </summary>
        public FilterOperator Operator { get; set; }

        /// <summary>
        /// Gets or sets filter field.
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Gets or sets filter value.
        /// </summary>
        public string Value { get; set; }

        public PaginationFilter()
        {
            Operator = FilterOperator.Eq;
        }
    }

    /// <summary>
    /// Filter operator.
    /// </summary>
    public enum FilterOperator
    {
        Eq,
        Contains,
        Startswith
    }
}
