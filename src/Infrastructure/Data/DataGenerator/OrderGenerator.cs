using Bogus;
using Domain.Models;

namespace Infrastructure.Data.DataGenerator;
public static class OrderGenerator
{
    public static Order[] GenerateOrders(User[] users)
        => new Faker<Order>()
        .UseSeed(1)
        .UseDateTimeReference(DateTime.UtcNow)
        .RuleFor(x => x.Id, f => f.Random.Guid())
        .RuleFor(x => x.CustomerId, f => f.PickRandom(users).Id)
        .RuleFor(x => x.Currency, f => f.Finance.Currency().Code)
        .RuleFor(x => x.TicketsIssued, f => f.Random.Bool())
        .Generate(200)
        .ToArray();
}