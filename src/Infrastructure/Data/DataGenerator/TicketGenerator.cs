using Bogus;
using Domain.Enums;
using Domain.Models;

namespace Infrastructure.Data.DataGenerator;
public static class TicketGenerator
{
    public static Ticket[] GenerateTickets(Attendee[] attendees,TicketType[] ticketTypes)
        => new Faker<Ticket>()
            .UseSeed(1)
            .UseDateTimeReference(DateTime.UtcNow)
            .RuleFor(t => t.Id, f => f.Random.Guid())
            .RuleFor(t => t.AttendeeId, f => f.PickRandom(attendees).Id)
            .RuleFor(t => t.TicketTypeId, f => f.PickRandom(ticketTypes).Id)
            .RuleFor(t => t.Code, f => f.Random.AlphaNumeric(10).ToUpper())
            .RuleFor(t => t.CreatedAt, f => f.Date.Past())
            .RuleFor(t => t.UpdatedAt, f => f.Date.Past())
            .Generate(20)
            .ToArray();
}