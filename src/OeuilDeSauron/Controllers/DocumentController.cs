using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using siwar.Domain;
using siwar.Infrastructure.Files;

namespace siwar.Controllers;

/// <summary>
/// Document controller.
/// </summary>
[Authorize]
[ApiController]
[Route("api/documents")]
public class DocumentController : ControllerBase
{
    private readonly IFileManager _fileManager;

    public DocumentController(IFileManager fileManager)
    {
        _fileManager = fileManager;
    }

    [HttpGet("folders")]
    public async Task<IActionResult> GetFoldersAsync()
    {
        var folders = await _fileManager.GetFoldersAsync();

        return Ok(folders);
    }

    [HttpGet("{*folder}")]
    public async Task<IActionResult> GetFilesAsync(string folder)
    {
        var files = await _fileManager.GetFilesAsync(folder);

        return Ok(files);
    }

    [Authorize(Roles = Roles.GlobalAdministratorRole)]
    [HttpPost("{*folder}")]
    public async Task<IActionResult> UploadFilesAsync(string folder, IList<IFormFile> files)
    {
        foreach (var file in files)
        {
            await _fileManager.UploadFileAsync(Infrastructure.Files.File.FromFormFile(folder, file), file.OpenReadStream());
        }

        return Ok();
    }

    [HttpGet("download/{*path}")]
    public async Task<IActionResult> DownloadFileAsync(string path)
    {
        var file = await _fileManager.GetFileAsync(path);

        if (file is null)
        {
            return NotFound();
        }

        var stream = await _fileManager.DownloadFileAsync(path);

        return File(stream, file.ContentType, file.OriginalName);
    }

    [Authorize(Roles = Roles.GlobalAdministratorRole)]
    [HttpDelete("{*path}")]
    public async Task<IActionResult> DeleteFileAsync(string path)
    {
        await _fileManager.DeleteFileAsync(path);

        return NoContent();
    }
}
