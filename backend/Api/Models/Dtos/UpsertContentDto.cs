using System.ComponentModel.DataAnnotations;

namespace Api.Models.Dtos;

public class UpsertContentDto
{
    [Required(ErrorMessage = "Value is required")]
    public string Value { get; set; } = string.Empty;
}

