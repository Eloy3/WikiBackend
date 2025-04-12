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
        var envUrl = Environment.GetEnvironmentVariable("SUPABASE_URL");
        var envKey = Environment.GetEnvironmentVariable("SUPABASE_KEY");

        Console.WriteLine($"🔍 ENV SUPABASE_URL: {envUrl}");
        Console.WriteLine($"🔍 ENV SUPABASE_KEY present: {!string.IsNullOrEmpty(envKey)}");

        var url = envUrl ?? throw new InvalidOperationException("Missing Supabase:Url");
        var key = envKey ?? throw new InvalidOperationException("Missing Supabase:Key");

        var options = new SupabaseOptions
        {
            AutoConnectRealtime = false,
            Headers = new Dictionary<string, string>
            {
                { "apikey", key },
                { "Authorization", $"Bearer {key}" }
            }
        };

        _supabase = new Supabase.Client(url, key, options);
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
