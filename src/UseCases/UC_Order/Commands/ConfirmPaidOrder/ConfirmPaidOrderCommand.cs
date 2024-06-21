using Ardalis.Result;
using Domain.Models;
using MediatR;

namespace UseCases.UC_Order.Commands.ConfirmPaidOrder;
public record ConfirmPaidOrderCommand(Guid OrderId) : IRequest<Result<Ticket>>
{

}