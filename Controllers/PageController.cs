using Microsoft.AspNetCore.Mvc;
using WikiBackend.Dtos;

namespace WikiBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PageController : ControllerBase
{
    private readonly PageService _service;

    public PageController(PageService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<PageDto>> Get([FromQuery] string path)
    {
        var page = await _service.GetPageByPath(path);
        if (page == null) return NotFound();

        var dto = new PageDto
        {
            Title = page.Title,
            Path = page.Path,
            Content = page.Content,
            Category = page.Category,
            Subcategory = page.Subcategory,
            CreatedAt = page.CreatedAt,
            UpdatedAt = page.UpdatedAt
        };

        return Ok(dto);
    }

}