using System.Collections.Generic;

using MediatR;

namespace siwar.Data;

/// <summary>
/// Base entity.
/// </summary>
public abstract class Entity
{
    private List<INotification> _events;
    /// <summary>
    /// Gets entity attached domain events.
    /// </summary>
    public IReadOnlyCollection<INotification> Events => _events?.AsReadOnly();

    /// <summary>
    /// Adds a domain event.
    /// </summary>
    /// 
    /// <param name="event">Domain event.</param>
    public void AddEvent(INotification @event)
    {
        _events ??= new List<INotification>();
        _events.Add(@event);
    }

    /// <summary>
    /// Removes a domain event.
    /// </summary>
    /// 
    /// <param name="event">Domain event.</param>
    public void RemoveEvent(INotification @event) => _events?.Remove(@event);

    /// <summary>
    /// Clears domain events.
    /// </summary>
    public void ClearEvents() => _events?.Clear();
}
