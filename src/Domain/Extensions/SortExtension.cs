using System.Linq;
using System.Reflection;

using Microsoft.Extensions.Logging;

using OeuilDeSauron.Data.Pagination;

namespace OeuilDeSauron.Domain.Extensions;

/// <summary>
/// Sort extensions.
/// </summary>
public static class SortExtensions
{
    public static IQueryable<TEntity> Sort<TEntity>(this IQueryable<TEntity> query, SortOptions options, ILogger logger)
    {
        if (!string.IsNullOrWhiteSpace(options.Sort))
        {
            var concernedField = options.Sort;
            var type = typeof(TEntity);
            if (options.Sort.Contains("."))
            {
                var properties = options.Sort.Split(".");
                foreach (var prop in properties)
                {
                    if (properties.LastOrDefault() == prop)
                    {
                        concernedField = prop;
                    }
                    else
                    {
                        type = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).PropertyType;
                    }
                }
            }
            var sortPropertyInfo = type.GetProperty(concernedField, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (sortPropertyInfo is not null)
            {
                query = options.SortDirection == SortDirection.Asc
                     ? query.OrderByDynamic(x => $"x.{options.Sort}")
                     : query.OrderByDescendingDynamic(x => $"x.{options.Sort}");
            }
            else
            {
                // Log
                logger.LogError($"Le champ de tri {options.Sort} n'est pas une propriété de l'entité {typeof(TEntity).Name}");
                query = query.OrderByDynamic(x => $"x.Id");
            }
        }
        else
        {
            query = query.OrderByDynamic(x => $"x.Id");
        }
        return query;
    }
}
