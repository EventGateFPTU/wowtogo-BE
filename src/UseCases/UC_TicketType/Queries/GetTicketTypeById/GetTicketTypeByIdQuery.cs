using Ardalis.Result;
using Domain.Responses.Responses_TicketType;
using MediatR;

namespace UseCases.UC_TicketType.Queries.GetTicketTypeById;

public record GetTicketTypeByIdQuery(Guid TicketTypeId) : IRequest<Result<GetTicketTypeByIdResponse>>;