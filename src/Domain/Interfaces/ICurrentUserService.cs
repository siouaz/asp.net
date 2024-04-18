using System.Security.Claims;

namespace siwar.Domain.Interfaces
{
    public interface ICurrentUserService
    {
        ClaimsPrincipal User { get; }

        string UserId { get; }
    }
}
