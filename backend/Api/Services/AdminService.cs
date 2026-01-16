using Api.Data;
using Api.Models.Dtos;
using Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class AdminService
{
    private readonly AppDbContext _context;

    public AdminService(AppDbContext context)
    {
        _context = context;
    }

    // Trustee CRUD
    public async Task<List<TrusteeDto>> GetTrusteesAsync()
    {
        return await _context.Trustees
            .OrderBy(t => t.DisplayOrder)
            .ThenBy(t => t.FullName)
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

    public async Task<TrusteeDto?> GetTrusteeByIdAsync(Guid id)
    {
        var trustee = await _context.Trustees.FindAsync(id);
        if (trustee == null) return null;

        return new TrusteeDto
        {
            Id = trustee.Id,
            FullName = trustee.FullName,
            RoleTitle = trustee.RoleTitle,
            Bio = trustee.Bio,
            PhotoUrl = trustee.PhotoUrl,
            DisplayOrder = trustee.DisplayOrder,
            IsActive = trustee.IsActive
        };
    }

    public async Task<Guid> CreateTrusteeAsync(TrusteeDto dto)
    {
        var trustee = new Trustee
        {
            Id = Guid.NewGuid(),
            FullName = dto.FullName,
            RoleTitle = dto.RoleTitle,
            Bio = dto.Bio,
            PhotoUrl = dto.PhotoUrl,
            DisplayOrder = dto.DisplayOrder,
            IsActive = dto.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        _context.Trustees.Add(trustee);
        await _context.SaveChangesAsync();

        return trustee.Id;
    }

    public async Task<bool> UpdateTrusteeAsync(Guid id, TrusteeDto dto)
    {
        var trustee = await _context.Trustees.FindAsync(id);
        if (trustee == null) return false;

        trustee.FullName = dto.FullName;
        trustee.RoleTitle = dto.RoleTitle;
        trustee.Bio = dto.Bio;
        trustee.PhotoUrl = dto.PhotoUrl;
        trustee.DisplayOrder = dto.DisplayOrder;
        trustee.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteTrusteeAsync(Guid id)
    {
        var trustee = await _context.Trustees.FindAsync(id);
        if (trustee == null) return false;

        _context.Trustees.Remove(trustee);
        await _context.SaveChangesAsync();
        return true;
    }

    // Initiative CRUD
    public async Task<List<InitiativeDto>> GetInitiativesAsync()
    {
        return await _context.Initiatives
            .OrderBy(i => i.DisplayOrder)
            .ThenBy(i => i.Title)
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

    public async Task<InitiativeDto?> GetInitiativeByIdAsync(Guid id)
    {
        var initiative = await _context.Initiatives.FindAsync(id);
        if (initiative == null) return null;

        return new InitiativeDto
        {
            Id = initiative.Id,
            Title = initiative.Title,
            ShortDescription = initiative.ShortDescription,
            Status = initiative.Status,
            DisplayOrder = initiative.DisplayOrder,
            IsActive = initiative.IsActive
        };
    }

    public async Task<Guid> CreateInitiativeAsync(InitiativeDto dto)
    {
        var initiative = new Initiative
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            ShortDescription = dto.ShortDescription,
            Status = dto.Status,
            DisplayOrder = dto.DisplayOrder,
            IsActive = dto.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        _context.Initiatives.Add(initiative);
        await _context.SaveChangesAsync();

        return initiative.Id;
    }

    public async Task<bool> UpdateInitiativeAsync(Guid id, InitiativeDto dto)
    {
        var initiative = await _context.Initiatives.FindAsync(id);
        if (initiative == null) return false;

        initiative.Title = dto.Title;
        initiative.ShortDescription = dto.ShortDescription;
        initiative.Status = dto.Status;
        initiative.DisplayOrder = dto.DisplayOrder;
        initiative.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteInitiativeAsync(Guid id)
    {
        var initiative = await _context.Initiatives.FindAsync(id);
        if (initiative == null) return false;

        _context.Initiatives.Remove(initiative);
        await _context.SaveChangesAsync();
        return true;
    }

    // Content management
    public async Task<bool> UpsertContentAsync(string key, string value)
    {
        var content = await _context.SiteContents.FindAsync(key);
        
        if (content == null)
        {
            content = new SiteContent
            {
                Key = key,
                Value = value,
                UpdatedAt = DateTime.UtcNow
            };
            _context.SiteContents.Add(content);
        }
        else
        {
            content.Value = value;
            content.UpdatedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    // Contact submissions
    public async Task<List<ContactSubmissionDto>> GetContactSubmissionsAsync()
    {
        return await _context.ContactSubmissions
            .OrderByDescending(cs => cs.CreatedAt)
            .Select(cs => new ContactSubmissionDto
            {
                Id = cs.Id,
                Name = cs.Name,
                Email = cs.Email,
                Phone = cs.Phone,
                Message = cs.Message,
                SourcePage = cs.SourcePage,
                IsRead = cs.IsRead,
                CreatedAt = cs.CreatedAt
            })
            .ToListAsync();
    }
}

