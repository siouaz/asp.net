using Hangfire.Dashboard;

namespace OeuilDeSauron.Filters
{
    /// <summary>
    /// Hangfire authorization filter.
    /// </summary>
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        /// <inheritdoc />
        public bool Authorize(DashboardContext context)
            => context.GetHttpContext().User.Identity.IsAuthenticated;
    }
}
