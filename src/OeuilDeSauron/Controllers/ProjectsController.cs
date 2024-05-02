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
using OeuilDeSauron.Domain.Queries.Projects;
using OeuilDeSauron.Domain.Commands.Project;




namespace OeuilDeSauron.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly MonitoringContext _context;
        private readonly IMediator _mediator;

        public ProjectsController(MonitoringContext context,IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var query = new GetAllProjectsQuery();
            var projects = await _mediator.Send(query);
            return Ok(projects);
        }

        // POST: api/Projects
        [HttpPost]
        public async Task<IActionResult> PostProject([FromBody] Project project)
        {
            var command = new AddProjectCommand(project);
            var projectCreated = await _mediator.Send(command);
            return Ok(projectCreated);
        }

        // GET: api/Projects/5
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

        // PUT: api/Projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(string id, [FromBody] Project project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }
            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(string id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
