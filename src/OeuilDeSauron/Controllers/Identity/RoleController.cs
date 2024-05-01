using System;
using System.Linq;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MediatR;

using OeuilDeSauron.Domain.Identity;
using OeuilDeSauron.Domain.Queries;
using OeuilDeSauron.Domain.Models.Identity;
using OeuilDeSauron.Domain.Commands.Identity;

namespace OeuilDeSauron.Controllers.Identity
{
    /// <summary>
    /// Role controller.
    /// </summary>
    [Authorize(Policy = "UserPolicy")]
    [ApiController]
    [Route("api/roles")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleQueries _roleQueries;
        public RoleController(IRoleQueries roleQueries)
        {
            _roleQueries = roleQueries;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleQueries.GetAllAsync();
            return Ok(roles);
        }
    }
}
