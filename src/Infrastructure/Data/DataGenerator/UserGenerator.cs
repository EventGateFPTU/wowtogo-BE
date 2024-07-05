using Bogus;
using Domain.Models;

namespace Infrastructure.Data.DataGenerator;
public static class UserGenerator
{
    private static string[] DevAccounts { get; } = new[]
    {
        "auth0|668514d53722eb41601e3d3f", // k4it0z11@wearehackerone.com
        "google-oauth2|101146750969927268793" // luucuong26@gmail.com
    };

    public static User[] GenerateUsers()
    {
        var result = new List<User>();
        foreach (var devAccount in DevAccounts)
        {
            result.Add(new Faker<User>()
                .UseDateTimeReference(DateTime.UtcNow)
                .RuleFor(x => x.Id, f => f.Random.Guid())
                .RuleFor(x => x.Email, f => f.Internet.Email())
                .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                .RuleFor(x => x.LastName, f => f.Name.LastName())
                .RuleFor(x => x.Subject, f => devAccount)
                .RuleFor(x => x.CreatedAt, f => f.Date.Past())
                .RuleFor(x => x.UpdatedAt, f => f.Date.Past())
                .Generate()
            );
        }

        return result.ToArray();
    }
}