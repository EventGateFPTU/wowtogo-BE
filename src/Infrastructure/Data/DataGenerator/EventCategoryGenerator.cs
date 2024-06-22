using Bogus;
using Domain.Models;

namespace Infrastructure.Data.DataGenerator;
public static class EventCategoryGenerator
{
    public static EventCategory[] GenerateEventCategories(Event[] events, Category[] categories)
    {
        EventCategory[] eventCategories = new Faker<EventCategory>()
        .UseSeed(1)
        .UseDateTimeReference(DateTime.UtcNow)
        .RuleFor(ec => ec.Id, f => f.Random.Guid())
        .RuleFor(ec => ec.EventId, f => f.PickRandom(events).Id)
        .RuleFor(ec => ec.CategoryId, f => f.PickRandom(categories).Id)
        .RuleFor(ec => ec.CreatedAt, f => f.Date.Past())
        .RuleFor(ec => ec.UpdatedAt, f => f.Date.Past())
        .Generate(200)
        .ToArray();
        return eventCategories.DistinctBy(ec => new { ec.EventId, ec.CategoryId }).ToArray();
    }
}