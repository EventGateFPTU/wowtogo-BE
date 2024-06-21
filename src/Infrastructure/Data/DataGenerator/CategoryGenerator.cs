using Bogus;
using Domain.Models;

namespace Infrastructure.Data.DataGenerator;
public static class CategoryGenerator
{
    public static Category[] GenerateCategories()
        => new Faker<Category>()
            .UseSeed(1)
            .UseDateTimeReference(DateTime.UtcNow)
            .RuleFor(c => c.Id, f => f.Random.Guid())
            .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0])
            .RuleFor(c => c.CreatedAt, f => f.Date.Past())
            .RuleFor(c => c.UpdatedAt, f => f.Date.Past())
            .Generate(50)
            .ToArray();
}