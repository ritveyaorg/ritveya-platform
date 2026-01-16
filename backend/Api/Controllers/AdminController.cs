using Api.Models.Dtos;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly AdminService _adminService;

    public AdminController(AdminService adminService)
    {
        _adminService = adminService;
    }

    // Trustee endpoints
    [HttpGet("trustees")]
    public async Task<ActionResult<List<TrusteeDto>>> GetTrustees()
    {
        var trustees = await _adminService.GetTrusteesAsync();
        return Ok(trustees);
    }

    [HttpGet("trustees/{id}")]
    public async Task<ActionResult<TrusteeDto>> GetTrustee(Guid id)
    {
        var trustee = await _adminService.GetTrusteeByIdAsync(id);
        if (trustee == null)
        {
            return NotFound();
        }
        return Ok(trustee);
    }

    [HttpPost("trustees")]
    public async Task<ActionResult<object>> CreateTrustee([FromBody] TrusteeDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var id = await _adminService.CreateTrusteeAsync(dto);
        return CreatedAtAction(nameof(GetTrustee), new { id }, new { id });
    }

    [HttpPut("trustees/{id}")]
    public async Task<IActionResult> UpdateTrustee(Guid id, [FromBody] TrusteeDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var success = await _adminService.UpdateTrusteeAsync(id, dto);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("trustees/{id}")]
    public async Task<IActionResult> DeleteTrustee(Guid id)
    {
        var success = await _adminService.DeleteTrusteeAsync(id);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    // Initiative endpoints
    [HttpGet("initiatives")]
    public async Task<ActionResult<List<InitiativeDto>>> GetInitiatives()
    {
        var initiatives = await _adminService.GetInitiativesAsync();
        return Ok(initiatives);
    }

    [HttpGet("initiatives/{id}")]
    public async Task<ActionResult<InitiativeDto>> GetInitiative(Guid id)
    {
        var initiative = await _adminService.GetInitiativeByIdAsync(id);
        if (initiative == null)
        {
            return NotFound();
        }
        return Ok(initiative);
    }

    [HttpPost("initiatives")]
    public async Task<ActionResult<object>> CreateInitiative([FromBody] InitiativeDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var id = await _adminService.CreateInitiativeAsync(dto);
        return CreatedAtAction(nameof(GetInitiative), new { id }, new { id });
    }

    [HttpPut("initiatives/{id}")]
    public async Task<IActionResult> UpdateInitiative(Guid id, [FromBody] InitiativeDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var success = await _adminService.UpdateInitiativeAsync(id, dto);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("initiatives/{id}")]
    public async Task<IActionResult> DeleteInitiative(Guid id)
    {
        var success = await _adminService.DeleteInitiativeAsync(id);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    // Content endpoint
    [HttpPut("content/{key}")]
    public async Task<IActionResult> UpsertContent(string key, [FromBody] UpsertContentDto dto)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return BadRequest(new { error = "Key is required" });
        }

        if (string.IsNullOrWhiteSpace(dto.Value))
        {
            return BadRequest(new { error = "Value is required" });
        }

        await _adminService.UpsertContentAsync(key, dto.Value);
        return NoContent();
    }

    // Contact submissions endpoint
    [HttpGet("contact-submissions")]
    public async Task<ActionResult<List<ContactSubmissionDto>>> GetContactSubmissions()
    {
        var submissions = await _adminService.GetContactSubmissionsAsync();
        return Ok(submissions);
    }
}

