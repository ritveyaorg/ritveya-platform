using Api.Models.Dtos;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/public")]
public class PublicController : ControllerBase
{
    private readonly PublicService _publicService;

    public PublicController(PublicService publicService)
    {
        _publicService = publicService;
    }

    [HttpGet("trustees")]
    public async Task<ActionResult<List<TrusteeDto>>> GetTrustees()
    {
        var trustees = await _publicService.GetActiveTrusteesAsync();
        return Ok(trustees);
    }

    [HttpGet("initiatives")]
    public async Task<ActionResult<List<InitiativeDto>>> GetInitiatives()
    {
        var initiatives = await _publicService.GetActiveInitiativesAsync();
        return Ok(initiatives);
    }

    [HttpGet("content")]
    public async Task<IActionResult> GetContent([FromQuery] string? keys)
    {
        var keyList = (keys ?? string.Empty)
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var content = await _publicService.GetContentAsync(keyList);
        return Ok(new { content });
    }

    [HttpPost("contact")]
    public async Task<ActionResult<object>> CreateContact([FromBody] ContactSubmissionCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Additional validation: Name and Message are required
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            return BadRequest(new { error = "Name is required" });
        }

        if (string.IsNullOrWhiteSpace(dto.Message))
        {
            return BadRequest(new { error = "Message is required" });
        }

        // Email format validation if provided
        if (!string.IsNullOrWhiteSpace(dto.Email))
        {
            var emailRegex = new System.Text.RegularExpressions.Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(dto.Email))
            {
                return BadRequest(new { error = "Invalid email format" });
            }
        }

        var id = await _publicService.CreateContactAsync(dto);
        return CreatedAtAction(nameof(CreateContact), new { id }, new { id });
    }
}

