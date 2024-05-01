using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OeuilDeSauron.Data;
using OeuilDeSauron.Domain;
using OeuilDeSauron.Domain.Interfaces;
using OeuilDeSauron.Domain.Models;
using OeuilDeSauron.Domain.Services;
using OeuilDeSauron.Infrastructure.Files;
using Polly;

namespace OeuilDeSauron.Controllers;

/// <summary>
/// Document controller.
/// </summary>
//[Authorize]
[ApiController]
[Route("api/health-check")]
public class HealthCheckController : ControllerBase
{
    private readonly IMyHealthCheck _healthCheck;
    private readonly MonitoringContext _context;

    public HealthCheckController(IMyHealthCheck healthCheck, MonitoringContext context)
    {
        _healthCheck = healthCheck;
        _context = context;
    }

    [HttpGet("{projectId}")]
    public async Task<IActionResult> CheckHealth(string projectId)
    {
        var project = await _context.Projects.FindAsync(projectId);
        if (project == null)
        {
            return NotFound("Project not found.");
        }

        var request = new HealthCheckRequest
        {
            Name = project.Name,
            Url = project.HealthcheckUrl,
            Headers = project.Headers
        };

        var result = await _healthCheck.CheckHealthAsync(request);
        return Ok(result);
    }


}
