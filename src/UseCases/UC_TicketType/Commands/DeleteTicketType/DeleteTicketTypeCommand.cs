using Ardalis.Result;
using MediatR;

namespace UseCases.UC_TicketType.Commands.DeleteTicketType;
public record DeleteTicketTypeCommand(Guid TicketTypeId) : IRequest<Result>;