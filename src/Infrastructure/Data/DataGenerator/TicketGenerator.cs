using Bogus;
using Domain.Models;

namespace Infrastructure.Data.DataGenerator;
public static class TicketGenerator
{
    public static Ticket[] GenerateTickets(Event[] events, Order[] orders)
        => new Faker<Ticket>()
        .UseSeed(1)
        .UseDateTimeReference(DateTime.UtcNow)
        .RuleFor(x => x.Id, f => f.Random.Guid())
        .RuleFor(x => x.Code, f => f.Random.AlphaNumeric(10))
        .RuleFor(x => x.UsedAt, f => f.Date.Past())
        .RuleFor(x => x.EventId, f => f.PickRandom(events).Id)
        .RuleFor(x => x.OrderId, f => f.PickRandom(orders).Id)
        .Generate(200)
        .ToArray();
}