using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using siwar.Domain.Models;
using siwar.Data;
using siwar.Models;




namespace OeuilDeSauron.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly MonitoringContext _context;

        public ProjectsController(MonitoringContext context)
        {
            _context = context;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var projects = _context.Projects.ToListAsync();
            return Ok(projects);
        }

        // POST: api/Projects
        [HttpPost]
        public async Task<IActionResult> PostProject([FromBody] Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        // PUT: api/Projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, [FromBody] Project project)
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
        public async Task<IActionResult> DeleteProject(int id)
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
