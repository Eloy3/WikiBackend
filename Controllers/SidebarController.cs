using Microsoft.AspNetCore.Mvc;
using Supabase;
using WikiBackend.Models;
using WikiBackend.Dtos;

namespace WikiBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SidebarController : ControllerBase
{
    private readonly Supabase.Client _supabase;

    public SidebarController(IConfiguration config)
    {
        
        var url = config["Supabase:Url"] ?? throw new InvalidOperationException("Missing Supabase:Url in configuration");
        var key = config["Supabase:Key"] ?? throw new InvalidOperationException("Missing Supabase:Key in configuration");

        Console.log($"Supabase URL: {url}");
        Console.log($"Supabase Key: {key}");
        
        _supabase = new Supabase.Client(url, key, new SupabaseOptions { AutoConnectRealtime = false });
        _supabase.InitializeAsync().Wait();
    }

    [HttpGet]
    public async Task<ActionResult<List<SidebarCategoryDto>>> GetSidebar()
    {
        var result = await _supabase.From<Page>().Get();

        var grouped = result.Models
            .GroupBy(p => p.Category)
            .Select(cat => new SidebarCategoryDto
            {
                Name = cat.Key,
                Subcategories = cat
                    .GroupBy(p => p.Subcategory)
                    .Select(subcat => new SidebarSubcategoryDto
                    {
                        Name = subcat.Key,
                        Path = $"/{cat.Key}/{subcat.Key}",
                        Pages = subcat.Select(p => new SidebarPageDto
                        {
                            Title = p.Title,
                            Path = p.Path
                        }).ToList()
                    }).ToList()
            }).ToList();

        return Ok(grouped);
    }
}
