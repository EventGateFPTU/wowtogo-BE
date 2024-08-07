using Bogus;
using Domain.Enums;
using Domain.Models;

namespace Infrastructure.Data.DataGenerator;
public static class EventGenerator
{
    public static Event[] GenerateEvents(Organizer[] organizers)
        => new Faker<Event>()
        .UseSeed(1)
        .UseDateTimeReference(DateTime.UtcNow)
        .RuleFor(e => e.Id, f => f.Random.Guid())
        .RuleFor(e => e.Title, f => f.Lorem.Sentence())
        .RuleFor(e => e.Description, f => f.Lorem.Paragraph())
        .RuleFor(e => e.Location, f => f.Address.City())
        .RuleFor(e => e.Status, f => f.Random.Enum<EventStatusEnum>())
        .RuleFor(e => e.BackgroundImageUrl, f => f.Image.PicsumUrl())
        .RuleFor(e => e.BannerImageUrl, f => f.Image.PicsumUrl())
        .RuleFor(e => e.OrganizerId, f => f.PickRandom(organizers).Id)
        .RuleFor(e => e.MaxTickets, f => f.Random.Number(100, 1000))
        .RuleFor(e => e.CreatedAt, f => f.Date.Past())
        .RuleFor(e => e.UpdatedAt, f => f.Date.Past())
        .Generate(5)
        .ToArray();
}