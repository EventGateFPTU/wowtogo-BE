using Domain.Models;

namespace Domain.Interfaces.Data.IRepositories;
public interface IUserRepository : IRepositoryBase<User>
{
    Task<IEnumerable<User>> GetUserList(bool trackChanges = false, CancellationToken cancellationToken = default);
}