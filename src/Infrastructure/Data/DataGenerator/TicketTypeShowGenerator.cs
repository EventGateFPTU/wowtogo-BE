using Bogus;
using Domain.Models;

namespace Infrastructure.Data.DataGenerator;
public class TicketTypeShowGenerator
{
    public static TicketTypeShow[] GenerateTicketTypeShows(TicketType[] ticketTypes, Show[] shows)
        => new Faker<TicketTypeShow>()
            .UseSeed(1)
            .UseDateTimeReference(DateTime.Now)
            .RuleFor(tts => tts.Id, f => f.Random.Guid())
            .RuleFor(tts => tts.CreatedAt, f => f.Date.Past())
            .RuleFor(tts => tts.UpdatedAt, f => f.Date.Recent())
            .RuleFor(tts => tts.ShowId, f => f.PickRandom(shows).Id)
            .RuleFor(tts => tts.TicketTypeId, f => f.PickRandom(ticketTypes).Id)
            .Generate(1000)
            .DistinctBy(tts => new { tts.ShowId, tts.TicketTypeId }).ToArray();
}