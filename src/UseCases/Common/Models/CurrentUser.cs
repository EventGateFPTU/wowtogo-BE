using Domain.Models;

namespace UseCases.Common.Models;

public class CurrentUser
{
    public required User? User { get; set; }

    public bool IsLoggedIn() => User is not null;
}