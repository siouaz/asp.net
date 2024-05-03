using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;

namespace OeuilDeSauron.Data.Identity;

/// <summary>
/// Application user.
/// </summary>
public class User : IdentityUser, IAggregateRoot
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    /// <summary>
    /// Gets user full name.
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";

    /// <summary>
    /// Gets or sets user phone number.
    /// </summary>
    public string Telephone { get; set; }

    /// <summary>
    /// Gets or sets user secondary phone number.
    /// </summary>
    public string Telephone2 { get; private set; }

    /// <summary>
    /// Gets or sets user ternary phone number.
    /// </summary>
    public string Telephone3 { get; private set; }

    /// <summary>
    /// Gets or sets user birth date.
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Gets or sets user birth place.
    /// </summary>
    public string BirthPlace { get; set; }

    private List<UserRole> _roles = new();

    /// <summary>
    /// Gets user roles.
    /// </summary>
    public virtual IReadOnlyCollection<UserRole> Roles => _roles?.AsReadOnly();

    public bool IsLockedOut => LockoutEnd.HasValue && LockoutEnd.Value > DateTimeOffset.UtcNow;

    /// <summary>
    /// Lock the user
    /// </summary>
    public void Lock() => LockoutEnd = DateTimeOffset.UtcNow.Date.AddYears(100);

    /// <summary>
    /// Unlock the user
    /// </summary>
    public void Unlock() => LockoutEnd = null;

    public void SetProfile(string firstName, string lastName, string email, string userName, DateTime birthDate,
        string birthPlace,
        string telephone)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        BirthDate = birthDate;
        BirthPlace = birthPlace;
        UserName = userName;
        Telephone = telephone;
    }

    public void SetRoles(IList<UserRole> roles)
    {
        _roles.Clear();
        _roles.AddRange(roles);
    }
}
