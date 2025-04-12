using Supabase.Postgrest.Attributes;
using System.Text.Json.Serialization;
using Supabase.Postgrest.Models;

namespace WikiBackend.Models;

[Table("page")]
public class Page : BaseModel
{
    [PrimaryKey("id", false)]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [Column("title")]
    public string Title { get; set; } = string.Empty;

    [Column("path")]
    public string Path { get; set; } = string.Empty;

    [Column("content")]
    public string Content { get; set; } = string.Empty;

    [Column("category")]
    public string Category { get; set; } = string.Empty;

    [Column("subcategory")]
    public string Subcategory { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
