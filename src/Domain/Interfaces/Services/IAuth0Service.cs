using Domain.Models;

namespace Domain.Interfaces.Services;

public interface IAuth0Service
{
    Task<User?> SyncUserProfileAsync(string jwt, Guid? updateWithId = null);
}