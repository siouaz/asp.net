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
using Hangfire;
using OeuilDeSauron.Domain.Queries.CheckHealthQueries;
using OeuilDeSauron.Domain.Jobs;
using OeuilDeSauron.Domain.Models.Project;
using Azure.Core;
using AutoMapper;




namespace OeuilDeSauron.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly HealthCheckJob _healthCheckJob;
        private readonly IMapper _mapper;

        public ProjectsController(IMediator mediator, HealthCheckJob healthCheckJob,IMapper mapper)
        {
            _mediator = mediator;
            _healthCheckJob = healthCheckJob;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var query = new GetAllProjectsQuery();
            var projects = await _mediator.Send(query);
            return Ok(projects);
        }



        [HttpPost]
        public async Task<IActionResult> PostProject([FromBody] ProjectRequest projectRequest)
        {
            var project = _mapper.Map<ProjectRequest, Project>(projectRequest);
            var command = new AddProjectCommand(project);
            var projectCreated = await _mediator.Send(command);

            var query = new GetApiHealthQuery(project.Id);

            if (project.IsActive)
            {
                RecurringJob.AddOrUpdate($"HealthCheckJob_{project.Id}", () => _healthCheckJob.RunAsync(project.Id), Cron.MinuteInterval(project.DurationInMinute));
            }


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
        public async Task<IActionResult> PutProject(string id, [FromBody] ProjectRequest ProjectRequest)
        {
            var project = _mapper.Map<ProjectRequest, Project>(ProjectRequest);
            var command = new UpdateProjectCommand(id,project);
            await _mediator.Send(command);
            if (project.IsActive)
            {
                RecurringJob.AddOrUpdate($"HealthCheckJob_{project.Id}", () => _healthCheckJob.RunAsync(project.Id), Cron.MinuteInterval(project.DurationInMinute));
            }
            else
            {
                RecurringJob.RemoveIfExists($"HealthCheckJob_{project.Id}");
            }
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(string id)
        {
            RecurringJob.RemoveIfExists($"HealthCheckJob_{id}");
            var command = new DeleteProjectCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
