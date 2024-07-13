using Ardalis.Result;
using Domain.Responses.Responses_TicketType;
using Domain.Responses.Shared;
using MediatR;

namespace UseCases.UC_TicketType.Queries.GetTicketTypesOfShow;
public record GetTicketTypesOfShowQuery(Guid ShowId, int PageSize, int PageNumber) : IRequest<Result<PaginatedResponse<GetTicketTypeDetailsResponse>>>;
