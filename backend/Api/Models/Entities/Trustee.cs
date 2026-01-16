namespace Api.Models.Entities;

public class Trustee
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string RoleTitle { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public string? PhotoUrl { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
}

