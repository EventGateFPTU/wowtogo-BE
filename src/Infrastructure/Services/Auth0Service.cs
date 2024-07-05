using Domain.Interfaces.Data;
using Domain.Interfaces.Services;
using Domain.Models;
using UseCases.Common.Contracts;

namespace Infrastructure.Services;

public class Auth0Service(
    ICurrentUserService currentUserService,
    IUnitOfWork unitOfWork) : IAuth0Service
{
    public async Task<User?> SyncUserProfileAsync(string jwt, Guid? updateWithId = null)
    {
        var newUserData = await currentUserService.GetUser();
        if (newUserData is null) return null;
        
        var user = newUserData.Map();
        user.Id = updateWithId ?? Guid.NewGuid(); // Crete or Update
        user.UpdatedAt = DateTimeOffset.UtcNow;
        unitOfWork.UserRepository.Add(user);
        var result = await unitOfWork.SaveChangesAsync();
        return result ? await unitOfWork.UserRepository.GetUserBySubject(newUserData.Sub) : null;
    }
}