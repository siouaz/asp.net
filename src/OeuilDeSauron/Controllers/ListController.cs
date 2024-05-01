using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OeuilDeSauron.Domain.Queries;

namespace OeuilDeSauron.Controllers;

[Authorize]
[Route("api/lists")]
[ApiController]
public class ListController : ControllerBase
{
    private readonly IListQueries _listQueries;

    public ListController(IListQueries listQueries) =>
        _listQueries = listQueries;

    [HttpGet]
    public async Task<IActionResult> GetLists() =>
        Ok(await _listQueries.GetAllAsync());
}
