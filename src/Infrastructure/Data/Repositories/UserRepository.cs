using Domain.Interfaces.Data.IRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;
public class UserRepository(WowToGoDBContext context) : RepositoryBase<User>(context), IUserRepository
{
    public async Task<User?> GetUserBySubject(string subject, CancellationToken cancellationToken = default)
        => await _dbSet.AsNoTracking()
            .Where(u => u.Subject.Equals(subject))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
}