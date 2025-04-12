namespace WikiBackend.Dtos;

public class SidebarPageDto
{
    public string Title { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
}

public class SidebarSubcategoryDto
{
    public string Name { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public List<SidebarPageDto> Pages { get; set; } = new();
}

public class SidebarCategoryDto
{
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = "map"; // Optional default
    public List<SidebarSubcategoryDto> Subcategories { get; set; } = new();
}
