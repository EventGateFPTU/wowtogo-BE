using Bogus;
using Domain.Models;

namespace Infrastructure.Data.DataGenerator;
public static class UserGenerator
{
    public static User[] GenerateUsers()
        => new Faker<User>()
        .UseSeed(1)
        .UseDateTimeReference(DateTime.UtcNow)
        .RuleFor(x => x.Id, f => f.Random.Guid())
        .RuleFor(x => x.Email, f => f.Internet.Email())
        .RuleFor(x => x.FirstName, f => f.Name.FirstName())
        .RuleFor(x => x.LastName, f => f.Name.LastName())
        .RuleFor(x => x.Password, f => f.Internet.Password())
        .Generate(50)
        .ToArray();

}