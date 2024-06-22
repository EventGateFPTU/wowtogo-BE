using Ardalis.Result;
using Domain.Responses.Responses_Show;
using MediatR;

namespace UseCases.UC_Show.Queries.GetShowDetails;
public record GetShowDetailsQuery(
    Guid ShowId
) : IRequest<Result<GetShowDetailResponse>>;