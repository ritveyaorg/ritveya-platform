using Api.Data;
using Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public static class SeedData
{
    public static async Task InitializeAsync(WebApplication app)
    {
        // Only seed in Development environment
        if (!app.Environment.IsDevelopment())
        {
            return;
        }

        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Check if database is empty (no trustees)
        if (await dbContext.Trustees.AnyAsync())
        {
            return; // Database already seeded
        }

        // Seed Trustees
        var trustees = new List<Trustee>
        {
            new Trustee
            {
                Id = Guid.NewGuid(),
                FullName = "Dr. Rajesh Kumar",
                RoleTitle = "Founder & Chairman",
                Bio = "Dr. Rajesh Kumar brings over 20 years of experience in social work and community development. He has been instrumental in establishing educational programs across rural areas.",
                PhotoUrl = null,
                DisplayOrder = 1,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Trustee
            {
                Id = Guid.NewGuid(),
                FullName = "Ms. Priya Sharma",
                RoleTitle = "Secretary & Program Director",
                Bio = "Ms. Priya Sharma specializes in skill development and vocational training programs. She has led multiple initiatives that have impacted over 5000 beneficiaries.",
                PhotoUrl = null,
                DisplayOrder = 2,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }
        };

        await dbContext.Trustees.AddRangeAsync(trustees);

        // Seed Initiatives
        var initiatives = new List<Initiative>
        {
            new Initiative
            {
                Id = Guid.NewGuid(),
                Title = "Digital Literacy Program",
                ShortDescription = "A comprehensive program to teach basic computer skills and digital literacy to underprivileged youth, enabling them to access online resources and improve their employability.",
                Status = "Active",
                DisplayOrder = 1,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Initiative
            {
                Id = Guid.NewGuid(),
                Title = "Scholarship Support Initiative",
                ShortDescription = "Providing financial assistance and mentorship to meritorious students from economically disadvantaged backgrounds to pursue higher education.",
                Status = "Planned",
                DisplayOrder = 2,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }
        };

        await dbContext.Initiatives.AddRangeAsync(initiatives);

        // Seed Site Content
        var siteContents = new List<SiteContent>
        {
            new SiteContent { Key = "home.heroTitle", Value = "Ritveya Lok Sansthan", UpdatedAt = DateTime.UtcNow },
            new SiteContent { Key = "home.heroSubtitle", Value = "Structured, transparent, impact-driven social work.", UpdatedAt = DateTime.UtcNow },
            new SiteContent { Key = "home.focus1", Value = "Education support & learning resources", UpdatedAt = DateTime.UtcNow },
            new SiteContent { Key = "home.focus2", Value = "Skill development & training", UpdatedAt = DateTime.UtcNow },
            new SiteContent { Key = "home.focus3", Value = "Community welfare initiatives", UpdatedAt = DateTime.UtcNow },
            new SiteContent { Key = "about.title", Value = "About Ritveya Lok Sansthan", UpdatedAt = DateTime.UtcNow },
            new SiteContent { Key = "about.p1", Value = "Ritveya Lok Sansthan is a public-benefit initiative focused on education support, skill development, and community welfare.", UpdatedAt = DateTime.UtcNow },
            new SiteContent { Key = "transparency.title", Value = "Transparency", UpdatedAt = DateTime.UtcNow },
            new SiteContent { Key = "transparency.p1", Value = "Registration is in process. We follow ethical operations and maintain proper documentation.", UpdatedAt = DateTime.UtcNow }
        };

        await dbContext.SiteContents.AddRangeAsync(siteContents);

        await dbContext.SaveChangesAsync();
    }
}

