using Domain.Models;
using Domain.Responses.Responses_User;
using Domain.Responses.Shared;

namespace Domain.Interfaces.Data.IRepositories;
public interface IUserRepository : IRepositoryBase<User>
{
    Task<User?> GetUserBySubject(string subject, CancellationToken cancellationToken = default);

    Task<PaginatedResponse<PublicUserDetailResponse>> SearchUserByEmail(
        string emailTerm,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default
    );
}