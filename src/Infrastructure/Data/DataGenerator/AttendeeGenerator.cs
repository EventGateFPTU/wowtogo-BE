using Bogus;
using Domain.Models;

namespace Infrastructure.Data.DataGenerator;
public static class AttendeeGenerator
{
    public static Attendee[] GenerateAttendees(User[] users,Event[] events)
        => new Faker<Attendee>()
            .UseSeed(1)
            .UseDateTimeReference(DateTime.UtcNow)
            .RuleFor(a => a.Id, f => f.Random.Guid())
            .RuleFor(a => a.PhoneNumber, f => f.Person.Phone)
            .RuleFor(a => a.DateOfBirth, f => f.Person.DateOfBirth)
            .RuleFor(a => a.FirstName, f => f.Person.FirstName)
            .RuleFor(a => a.LastName, f => f.Person.LastName)
            .RuleFor(a => a.UserId, f => f.Random.Bool() ? f.PickRandom(users).Id : null)
            .RuleFor(a => a.EventId, f => f.PickRandom(events).Id)
            .RuleFor(a => a.CreatedAt, f => f.Date.Past())
            .RuleFor(a => a.UpdatedAt, f => f.Date.Past())
            .Generate(4)
            .ToArray();

}