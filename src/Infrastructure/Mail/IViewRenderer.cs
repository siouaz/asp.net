using System.Threading;
using System.Threading.Tasks;

namespace OeuilDeSauron.Infrastructure.Mail
{
    /// <summary>
    /// View renderer.
    /// </summary>
    public interface IViewRenderer
    {
        /// <summary>
        /// Compiles a razor view.
        /// </summary>
        /// <param name="viewName">View name.</param>
        /// <param name="model">View model.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task<string> RenderAsync(string viewName, object model, CancellationToken cancellationToken = default);
    }
}
