using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
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

    public HealthCheckController(IMyHealthCheck healthCheck)
    {
        _healthCheck = healthCheck;
    }

    [HttpGet()]
    public async Task<IActionResult> CheckHealth([FromBody] HealthCheckRequest request)
    {
        var result = await _healthCheck.CheckHealthAsync(request);
        return Ok(result);
      
    }

    
}
