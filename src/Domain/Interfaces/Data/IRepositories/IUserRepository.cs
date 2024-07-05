using Domain.Models;

namespace Domain.Interfaces.Data.IRepositories;
public interface IUserRepository : IRepositoryBase<User>
{
    Task<User?> GetUserBySubject(string subject, CancellationToken cancellationToken = default);
}