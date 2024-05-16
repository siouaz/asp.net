using System.Security.Claims;

namespace OeuilDeSauron.Domain.Interfaces
{
    public interface ICurrentUserService
    {
        ClaimsPrincipal User { get; }

        string UserId { get; }
    }
}
