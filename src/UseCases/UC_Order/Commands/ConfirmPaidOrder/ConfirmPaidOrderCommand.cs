using Ardalis.Result;
using Domain.Models;
using Domain.Responses.Responses_Ticket;
using MediatR;

namespace UseCases.UC_Order.Commands.ConfirmPaidOrder;
public record ConfirmPaidOrderCommand(Guid OrderId) : IRequest<Result<CreateTicketResponse>>
{

}