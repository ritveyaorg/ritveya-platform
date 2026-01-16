namespace Api.Models.Entities;

public class ContactSubmission
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? SourcePage { get; set; }
    public bool IsRead { get; set; } = false;
    public DateTime CreatedAt { get; set; }
}

