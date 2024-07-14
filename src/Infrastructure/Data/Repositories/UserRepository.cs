using Domain.Interfaces.Data.IRepositories;
using Domain.Models;
using Domain.Responses.Responses_User;
using Domain.Responses.Shared;
using Microsoft.EntityFrameworkCore;
using UseCases.Mapper.Mapper_User;

namespace Infrastructure.Data.Repositories;
public class UserRepository(WowToGoDBContext context) : RepositoryBase<User>(context), IUserRepository
{
    public async Task<User?> GetUserBySubject(string subject, CancellationToken cancellationToken = default)
        => await _dbSet.AsNoTracking()
            .Where(u => u.Subject.Equals(subject))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

    public async Task<PaginatedResponse<PublicUserDetailResponse>> SearchUserByEmail(string emailTerm, int pageNumber = 1, int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsNoTracking();
        query = query.Where(u => EF.Functions.ILike(u.Email, $"%{emailTerm}%"))
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        var count = query.Count();
        var data = await query
            .Select(u => u.MapToPublicUserDetailResponse())
            .ToListAsync(cancellationToken: cancellationToken);

        return new PaginatedResponse<PublicUserDetailResponse>(
            Data: data,
            PageNumber: pageNumber,
            PageSize: pageSize,
            Count: count
        );
    }
}