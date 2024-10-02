using Bogus;
using Domain.Models;

namespace Infrastructure.Data.DataGenerator;

public class AdditionalImageGenerator
{
    public static AdditionalImage[] Generate(Event[] events)
        => [.. new Faker<AdditionalImage>()
            .UseSeed(1)
            .UseDateTimeReference(DateTime.UtcNow)
            .RuleFor(x => x.Id, f => f.Random.Guid())
            .RuleFor(x => x.EventId, f => f.PickRandom(events).Id)
            .RuleFor(x => x.Url, f => f.Image.PicsumUrl())
            .RuleFor(x=>x.SlotNumber, f => f.Random.Number(1, 5))
            .RuleFor(x => x.CreatedAt, f => f.Date.Past())
            .RuleFor(x => x.UpdatedAt, f => f.Random.Bool() ? f.Date.Past() : null!)
            .Generate(50)
            .DistinctBy(x => new { x.EventId, x.SlotNumber })];
}