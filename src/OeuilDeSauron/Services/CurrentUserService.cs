using System.Collections.Generic;
using System.Security.Claims;

using Microsoft.AspNetCore.Http;

using siwar.Domain.Interfaces;

namespace siwar.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        // TODO, dictionary userId->login (like a subcontext)
        public IDictionary<string, IList<string>> UserLoginContext { get; private set; }

        /// <summary>
        /// Returns current user login context if any
        /// </summary>
        public IList<string> LoginsContext
        {
            get
            {
                if (UserId == null)
                {
                    return new List<string>();
                }
                return UserLoginContext[UserId];
            }
        }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserLoginContext = new Dictionary<string, IList<string>>();
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;

        public string UserId => User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
