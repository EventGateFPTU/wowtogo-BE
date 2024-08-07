using Bogus;
using Domain.Models;

namespace Infrastructure.Data.DataGenerator;
public static class OrganizerGenerator
{
    public static Organizer[] GenerateOrganizers(User[] users)
    {
        Organizer[] organizers = new Faker<Organizer>()
            .UseSeed(1)
            .UseDateTimeReference(DateTime.UtcNow)
            .RuleFor(o => o.Id, f => f.PickRandom(users).Id)
            .RuleFor(o => o.OrganizationName, f => f.Company.CompanyName())
            .RuleFor(o => o.Description ,f=> f.Lorem.Sentence())
            .RuleFor(o => o.CreatedAt, f => f.Date.Past())
            .RuleFor(o => o.ImageUrl, f => f.Image.PicsumUrl())
            .RuleFor(o => o.UpdatedAt, f => f.Date.Past())
            .Generate(2)
            .ToArray();
        return organizers.DistinctBy(o => o.Id).ToArray();
    }
}