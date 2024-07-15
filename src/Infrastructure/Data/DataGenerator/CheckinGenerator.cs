using Bogus;
using Domain.Enums;
using Domain.Models;

namespace Infrastructure.Data.DataGenerator;
public static class CheckinGenerator
{
    public static Checkin[] GenerateCheckins(Show[] shows, Ticket[] tickets)
        => new Faker<Checkin>()
            .UseSeed(1)
            .UseDateTimeReference(DateTime.UtcNow)
            .RuleFor(c => c.Id, f => f.Random.Guid())
            .RuleFor(c => c.ShowId, f => f.PickRandom(shows).Id)
            .RuleFor(c => c.TicketId, f => f.PickRandom(tickets).Id)
            .RuleFor(t => t.UsedInFormat, f => f.PickRandom<UsedInFormatEnum>())
            .RuleFor(t => t.UsedAt, f => f.Date.Past())
            .RuleFor(t => t.CreatedAt, f => f.Date.Past())
            .RuleFor(t => t.UpdatedAt, f => f.Random.Bool() ? f.Date.Past() : null!)
            .Generate(200)
            .ToArray()
            .DistinctBy(t => new { t.ShowId, t.TicketId }).ToArray();
}