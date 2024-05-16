using Microsoft.AspNetCore.Identity;

namespace OeuilDeSauron.Data.Identity
{
    /// <summary>
    /// User structure relation.
    /// </summary>
    public class UserRole
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public User User { get; private set; }
        public string RoleId { get; set; }
        public Role Role { get; private set; }
    }
}
