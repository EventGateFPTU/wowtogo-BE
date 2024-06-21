using Bogus;
using Domain.Enums;
using Domain.Models;

namespace Infrastructure.Data.DataGenerator;
public static class OrderGenerator
{
    public static Order[] GenerateOrders(TicketType[] ticketTypes, User[] users)
        => new Faker<Order>()
            .UseSeed(1)
            .UseDateTimeReference(DateTime.UtcNow)
            .RuleFor(o => o.Id, f => f.Random.Guid())
            .RuleFor(o => o.TicketTypeId, f => f.PickRandom(ticketTypes).Id)
            .RuleFor(o => o.UserId, f => f.PickRandom(users).Id)
            .RuleFor(o => o.Status, f => f.Random.Enum<OrderStatusEnum>())
            .RuleFor(o => o.TotalPrice, (f, o) => ticketTypes.First(t => t.Id == o.TicketTypeId).Price)
            .RuleFor(o => o.Currency, f => f.Finance.Currency().Code)
            .Generate(200)
            .ToArray();
}