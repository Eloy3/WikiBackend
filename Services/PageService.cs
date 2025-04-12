using Supabase;
using WikiBackend.Models;
using static Supabase.Postgrest.Constants;

public class PageService
{
    private readonly Client _supabase;

    public PageService(IConfiguration config)
    {
        var url = Environment.GetEnvironmentVariable("SUPABASE_URL") ?? config["Supabase:Url"] ?? throw new ArgumentNullException("Supabase URL cannot be null.");
        var key = Environment.GetEnvironmentVariable("SUPABASE_KEY") ?? config["Supabase:Key"];
        var options = new SupabaseOptions { AutoConnectRealtime = false };

        _supabase = new Client(url, key, options);
        _supabase.InitializeAsync().Wait();
    }
    public async Task<Page?> GetPageByPath(string path)
    {
        var result = await _supabase
            .From<Page>()
            .Filter("path", Operator.Equals, path)
            .Get();

        return result.Models.FirstOrDefault();
    }

}
