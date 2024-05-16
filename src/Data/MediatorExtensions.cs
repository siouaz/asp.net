using System.Linq;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace OeuilDeSauron.Data;

/// <summary>
/// Mediator database context extensions.
/// </summary>
public static class MediatorExtensions
{
    /// <summary>
    /// Dispatches domain events through the mediator.
    /// </summary>
    /// <typeparam name="TContext">Database context type.</typeparam>
    /// <param name="mediator"><see cref="IMediator" /> instance.</param>
    /// <param name="context">Database context.</param>
    public static async Task DispatchEvents<TContext>(this IMediator mediator, TContext context)
        where TContext : DbContext
    {
        var entities = context?.ChangeTracker
            .Entries<Entity>()
            .Where(e => e.Entity.Events is not null && e.Entity.Events.Any())
            .ToList();

        var events = entities
            .SelectMany(e => e.Entity.Events)
            .ToList();

        // Clear domain events
        entities
            .ToList()
            .ForEach(e => e.Entity.ClearEvents());

        // Publish events
        var tasks = events
            .Select(async @event => await mediator.Publish(@event));

        await Task.WhenAll(tasks);
    }
}
