using Bogus;
using Domain.Models;

namespace Infrastructure.Data.DataGenerator;
public static class StaffGenerator
{
    public static Staff[] GenerateStaff(User[] users, Event[] events)
    {
        // Staff[] staffs =
        return new Faker<Staff>()
        .UseSeed(1)
        .UseDateTimeReference(DateTime.UtcNow)
        .RuleFor(s => s.Id, f => Guid.NewGuid())
        .RuleFor(s => s.UserId, f => f.PickRandom(users).Id)
        .RuleFor(s => s.EventId, f => f.PickRandom(events).Id)
        .RuleFor(s => s.CreatedAt, f => f.Date.Past())
        .RuleFor(s => s.UpdatedAt, f => f.Date.Past())
        .Generate(2)
        .DistinctBy(s => new { s.Id, s.EventId })
        .ToArray();
        // return staffs.DistinctBy(s => new { s.Id, s.EventId }).ToArray();
    }
}