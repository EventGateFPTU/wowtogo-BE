using Ardalis.Result;
using Domain.Responses.Responses_User;
using Domain.Responses.Shared;
using MediatR;

namespace UseCases.UC_User.Queries.GetUsersByEmail;

public record GetUsersByEmailQuery(
    string? SearchTerm,
    int PageNumber = 1,
    int PageSize = 5
    ) : IRequest<Result<PaginatedResponse<PublicUserDetailResponse>>>;