using Api.Data;
using Api.Models.Dtos;
using Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class PublicService
{
    private readonly AppDbContext _context;

    public PublicService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TrusteeDto>> GetActiveTrusteesAsync()
    {
        return await _context.Trustees
            .Where(t => t.IsActive)
            .OrderBy(t => t.DisplayOrder)
            .Select(t => new TrusteeDto
            {
                Id = t.Id,
                FullName = t.FullName,
                RoleTitle = t.RoleTitle,
                Bio = t.Bio,
                PhotoUrl = t.PhotoUrl,
                DisplayOrder = t.DisplayOrder,
                IsActive = t.IsActive
            })
            .ToListAsync();
    }

    public async Task<List<InitiativeDto>> GetActiveInitiativesAsync()
    {
        return await _context.Initiatives
            .Where(i => i.IsActive)
            .OrderBy(i => i.DisplayOrder)
            .Select(i => new InitiativeDto
            {
                Id = i.Id,
                Title = i.Title,
                ShortDescription = i.ShortDescription,
                Status = i.Status,
                DisplayOrder = i.DisplayOrder,
                IsActive = i.IsActive
            })
            .ToListAsync();
    }

    public async Task<Dictionary<string, string>> GetContentAsync(string[] keys)
    {
        var normalizedKeys = keys
            .Where(k => !string.IsNullOrWhiteSpace(k))
            .Select(k => k.Trim())
            .ToList();

        var content = await _context.SiteContents
            .Where(x => normalizedKeys.Contains(x.Key))
            .ToListAsync();

        return content.ToDictionary(x => x.Key, x => x.Value);
    }

    public async Task<Guid> CreateContactAsync(ContactSubmissionCreateDto dto)
    {
        var submission = new ContactSubmission
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            Message = dto.Message,
            SourcePage = dto.SourcePage,
            IsRead = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.ContactSubmissions.Add(submission);
        await _context.SaveChangesAsync();

        return submission.Id;
    }
}

