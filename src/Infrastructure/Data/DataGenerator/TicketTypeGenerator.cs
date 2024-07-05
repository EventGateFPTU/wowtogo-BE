using Bogus;
using Domain.Models;

namespace Infrastructure.Data.DataGenerator;
public static class TicketTypeGenerator
{
    public static TicketType[] GenerateTicketTypes(Show[] shows)
        => new Faker<TicketType>()
            .UseSeed(1)
            .UseDateTimeReference(DateTime.UtcNow)
            .RuleFor(tt => tt.Id, f => f.Random.Guid())
            .RuleFor(tt => tt.Name, f => f.Commerce.ProductName())
            .RuleFor(tt => tt.Description, f => f.Commerce.ProductDescription())
            .RuleFor(tt => tt.ImageUrl, f => f.Image.PicsumUrl())
            .RuleFor(tt => tt.Price, f => f.Random.Decimal(10, 100))
            .RuleFor(tt => tt.FromDate, f => f.Date.Past())
            .RuleFor(tt => tt.ToDate, f => f.Date.Future())
            .RuleFor(tt => tt.Amount, f => f.Random.Number(1, 100))
            .RuleFor(tt => tt.LeastAmountBuy, f => f.Random.Number(1, 100))
            .RuleFor(tt => tt.MostAmountBuy, f => f.Random.Number(100, 10000))
            .RuleFor(tt => tt.CreatedAt, f => f.Date.Past())
            .RuleFor(tt => tt.UpdatedAt, f => f.Date.Past())
            .Generate(20)
            .ToArray();
}