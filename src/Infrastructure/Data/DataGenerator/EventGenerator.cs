using Bogus;
using Domain.Models;

namespace Infrastructure.Data.DataGenerator;
public static class EventGenerator
{
    public static Event[] GenerateEvents()
        => new Faker<Event>()
        .UseSeed(1)
        .UseDateTimeReference(DateTime.UtcNow)
        .RuleFor(x => x.Id, f => f.Random.Guid())
        .RuleFor(x => x.StartsAt, f => f.Date.Past())
        .RuleFor(x => x.EndsAt, f => f.Date.Future())
        .RuleFor(x => x.Title, f => f.Lorem.Sentence())
        .RuleFor(x => x.Description, f => f.Lorem.Paragraph())
        .RuleFor(x => x.Location, f => f.Address.City())
        .RuleFor(x => x.Status, f => f.Random.Number(0, 3))
        .Generate(200)
        .ToArray();
}