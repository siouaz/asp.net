using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using OeuilDeSauron.Domain.Models;
using OeuilDeSauron.Data;
using OeuilDeSauron.Models;
using MediatR;
using OeuilDeSauron.Domain.Queries.ProjectQueries;
using OeuilDeSauron.Domain.Commands.ProjectCommands;




namespace OeuilDeSauron.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var query = new GetAllProjectsQuery();
            var projects = await _mediator.Send(query);
            return Ok(projects);
        }

        [HttpPost]
        public async Task<IActionResult> PostProject([FromBody] Project project)
        {
            var command = new AddProjectCommand(project);
            var projectCreated = await _mediator.Send(command);
            return Ok(projectCreated);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(string id)
        {
            var query = new GetProjectQuery(id);

            var project = await _mediator.Send(query);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(string id, [FromBody] Project project)
        {
            var command = new UpdateProjectCommand(id,project);
            await _mediator.Send(command);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(string id)
        {
            var command = new DeleteProjectCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
