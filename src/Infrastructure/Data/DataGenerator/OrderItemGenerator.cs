using Bogus;
using Domain.Models;

namespace Infrastructure.Data.DataGenerator;
public static class OrderItemGenerator
{
    public static OrderItem[] GenerateOrderItems(Order[] orders)
        => new Faker<OrderItem>()
        .UseSeed(1)
        .UseDateTimeReference(DateTime.UtcNow)
        .RuleFor(x => x.Id, f => f.Random.Guid())
        .RuleFor(x => x.OrderId, f => f.PickRandom(orders).Id)
        .RuleFor(x => x.Quantity, f => f.Random.Number(1, 100))
        .RuleFor(x => x.UnitPrice, f => f.Random.Float(1, 100))
        .RuleFor(x => x.Price, (f, u) => u.UnitPrice * u.Quantity)
        .Generate(1000)
        .ToArray();
}