using Microsoft.AspNetCore.Mvc;

namespace siwar.Controllers;

/// <summary>
/// App controller.
/// </summary>
[ApiExplorerSettings(IgnoreApi = true)]
public class AppController : Controller
{
    /// <summary>
    /// App entry action.
    /// </summary>
    public IActionResult Index() => View();

    [HttpGet("~/api/app/version")]
    public IActionResult Version() => Ok(typeof(AppController).Assembly.GetName().Version);
}
