using Bogus;
using Domain.Models;

namespace Infrastructure.Data.DataGenerator;
public static class ShowGenerator
{
    public static Show[] GenerateShows(Event[] events)
        => new Faker<Show>()
            .UseSeed(1)
            .UseDateTimeReference(DateTime.UtcNow)
            .RuleFor(s => s.Id, f => f.Random.Guid())
            .RuleFor(s => s.Title, f => f.Lorem.Word())
            .RuleFor(s => s.EventId, f => f.PickRandom(events).Id)
            .RuleFor(s => s.StartsAt, f => f.Date.Past())
            .RuleFor(s => s.EndsAt, f => f.Date.Future())
            .RuleFor(s => s.CreatedAt, f => f.Date.Past())
            .RuleFor(s => s.UpdatedAt, f => f.Date.Past())
            .Generate(20)
            .ToArray();
}