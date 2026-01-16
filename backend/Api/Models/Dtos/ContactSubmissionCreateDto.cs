using System.ComponentModel.DataAnnotations;

namespace Api.Models.Dtos;

public class ContactSubmissionCreateDto
{
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(120, ErrorMessage = "Name cannot exceed 120 characters")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(150, ErrorMessage = "Email cannot exceed 150 characters")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string? Email { get; set; }

    [MaxLength(30, ErrorMessage = "Phone cannot exceed 30 characters")]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "Message is required")]
    [MaxLength(2000, ErrorMessage = "Message cannot exceed 2000 characters")]
    public string Message { get; set; } = string.Empty;

    [MaxLength(50, ErrorMessage = "SourcePage cannot exceed 50 characters")]
    public string? SourcePage { get; set; }
}

