using Microsoft.AspNetCore.Identity;

namespace siwar.Data.Identity
{
    /// <summary>
    /// User structure relation.
    /// </summary>
    public class UserRole: IdentityUserRole<string>
    {
        public User User { get; private set; }

        public Role Role { get; private set; }
    }
}
