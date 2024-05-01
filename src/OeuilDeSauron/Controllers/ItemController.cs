using System;
using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OeuilDeSauron.Data.Pagination;
using OeuilDeSauron.Domain;
using OeuilDeSauron.Domain.Commands;
using OeuilDeSauron.Domain.Models;
using OeuilDeSauron.Domain.Queries;

namespace OeuilDeSauron.Controllers;

[Authorize]
[ApiController]
[Route("api/items")]
public class ItemController : ControllerBase
{
    private readonly IItemQueries _itemQuery;
    private readonly IMediator _mediator;
    private readonly IResources _resources;

    public ItemController(IItemQueries itemQuery, IMediator mediator, IResources resources)
    {
        _itemQuery = itemQuery;
        _mediator = mediator;
        _resources = resources;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItemsAsync(int id, [FromQuery] SortOptions options)
    {
        return Ok(await _itemQuery.GetItemsByListAsync(id, options));
    }

    [HttpGet("by-id/{id}")]
    public async Task<IActionResult> GetItemByIdAsync(int id) =>
        Ok(await _itemQuery.GetItemByIdAsync(id));

    [HttpGet]
    public async Task<IActionResult> GetItemsAsync()
    {
        return Ok(await _itemQuery.GetAllAsync());
    }

    [HttpPost]
    public async Task<IActionResult> AddItemAsync([FromBody] ItemModel item)
    {
        try
        {
            var newItem = await _mediator.Send(new CreateOrUpdateItem(item));
            return Ok(newItem);
        }
        catch (Exception e)
        {
            return BadRequest(new { error = e.Message });
        }
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveItemAsync([FromQuery] int itemId)
    {
        try
        {
            var success = await _mediator.Send(new RemoveItem(itemId));
            if (success)
            {
                return Ok();
            }
            return BadRequest();
        }
        catch (Exception e)
        {
            if (e.InnerException.Message.Contains("FK"))
            {
                return BadRequest(new { error = _resources.DependentItem });
            }
            return BadRequest(new { error = e.Message });
        }
    }

    [HttpGet("by-parent/{parentId}")]
    public async Task<IActionResult> GetItemsByParentIdAsync(int parentId) =>
        Ok(await _itemQuery.GetItemsByParentIdAsync(parentId));
}
