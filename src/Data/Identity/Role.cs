using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;

namespace siwar.Data.Identity;

/// <summary>
/// Application role.
/// </summary>
public class Role : IdentityRole, IAggregateRoot
{
    /// <summary>
    /// Gets or sets role description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Updates a role.
    /// </summary>
    /// <param name="role">Updated role.</param>
    public void Update(Role role) =>
        Description = role.Description;

    /// <inheritdoc />
    public override bool Equals(object obj) =>
        obj is Role role &&
        Id == role.Id;
    
    /// <inheritdoc />
    public override int GetHashCode() =>
        EqualityComparer<string>.Default.GetHashCode(Id);
}
