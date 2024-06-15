using Bogus;
using Domain.Enums;
using Domain.Models;

namespace Infrastructure.Data.DataGenerator;
public static class ArticleGenerator
{
    public static Article[] GenerateArticles()
        => new Faker<Article>()
        .UseSeed(1)
        .UseDateTimeReference(DateTime.UtcNow)
        .RuleFor(x => x.Id, f => f.Random.Guid())
        .RuleFor(x => x.StartsAt, f => f.Date.Past())
        .RuleFor(x => x.EndsAt, f => f.Date.Future())
        .RuleFor(x => x.Title, f => f.Lorem.Sentence())
        .RuleFor(x => x.Description, f => f.Lorem.Paragraph())
        .RuleFor(x => x.Location, f => f.Address.City())
        .RuleFor(x => x.Status, f => f.Random.Enum<ArticleStatusEnum>())
        .Generate(200)
        .ToArray();
}