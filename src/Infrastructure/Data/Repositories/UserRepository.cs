using Domain.Interfaces.Data.IRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;
public class UserRepository(WowToGoDBContext context) : RepositoryBase<User>(context), IUserRepository
{
    public async Task<IEnumerable<User>> GetUserList(bool trackChanges = false, CancellationToken cancellationToken = default)
    {
        IQueryable<User> query = _dbSet.Include(user => user.Orders);
        return trackChanges
            ? await query.ToListAsync(cancellationToken)
            : await query.AsNoTracking().ToListAsync(cancellationToken);
    }
}