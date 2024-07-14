using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Responses.Responses_User;
using Domain.Responses.Shared;
using MediatR;
using UseCases.Common.Models;

namespace UseCases.UC_User.Queries.GetUsersByEmail;

public class
    GetUsersByEmailHandler(IUnitOfWork unitOfWork, CurrentUser currentUser) : IRequestHandler<GetUsersByEmailQuery, Result<PaginatedResponse<PublicUserDetailResponse>>>
{
    public async Task<Result<PaginatedResponse<PublicUserDetailResponse>>> Handle(GetUsersByEmailQuery request,
        CancellationToken cancellationToken)
    {
        if (!currentUser.IsLoggedIn()) return Result.Unauthorized();
        return await unitOfWork.UserRepository
            .SearchUserByEmail(
                emailTerm: request.SearchTerm,
                pageSize: request.PageSize,
                pageNumber: request.PageNumber,
                cancellationToken: cancellationToken
            );
    }
         
}