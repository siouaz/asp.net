using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.WebUtilities;

using OeuilDeSauron.Data.Identity;

namespace OeuilDeSauron.Data.Applications;

/// <summary>
/// Consumer application.
/// </summary>
public sealed class Application
{
    /// <inheritdoc/>
    public string Code { get; }

    /// <inheritdoc/>
    public string Name { get; }

    /// <inheritdoc/>
    public string ApiKey { get; set; }

    public bool Deleted { get; set; }

    public DateTime Updated { get; set; }

    public string UpdatedById { get; set; }

    public User UpdatedBy { get; set; }

    public Application() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Application"/> class.
    /// </summary>
    public Application(string code, string name, string apiKey, DateTime updated, string currentUserId)
    {
        Code = code;
        Name = name;
        ApiKey = apiKey;
        Updated = updated;
        UpdatedById = currentUserId;
    }

    /// <summary>
    /// Updates an application.
    /// </summary>
    /// <param name="updatedById">User id</param>
    /// <param name="date">Date of update</param>
    public void Update(string updatedById, DateTime date)
    {
        Updated = date;
        UpdatedById = updatedById;
    }

    public void GenerateApiKey()
    {
        var key = new byte[32];
        using var generator = RandomNumberGenerator.Create();
        generator.GetBytes(key);
        ApiKey = WebEncoders.Base64UrlEncode(key);
    }
}
