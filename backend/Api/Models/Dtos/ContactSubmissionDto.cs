namespace Api.Models.Dtos;

public class ContactSubmissionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? SourcePage { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
}

