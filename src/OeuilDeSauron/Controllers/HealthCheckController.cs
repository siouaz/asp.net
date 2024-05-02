using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OeuilDeSauron.Data;
using OeuilDeSauron.Domain;
using OeuilDeSauron.Domain.Interfaces;
using OeuilDeSauron.Domain.Models;
using OeuilDeSauron.Domain.Queries.CheckHealthQueries;
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
    private readonly IMediator _mediator;

    public HealthCheckController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{projectId}")]
    public async Task<IActionResult> CheckHealth(string projectId)
    {
       var query = new GetApiHealthQuery(projectId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }


}
