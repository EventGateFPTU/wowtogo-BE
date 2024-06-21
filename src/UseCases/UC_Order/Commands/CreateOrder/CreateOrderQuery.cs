using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Order.Commands.CreateOrder;
public record CreateOrderQuery(Guid TicketTypeId, Guid UserId, string Currency, string PhoneNumber, DateTime DateOfBirth) : IRequest<Result>
{

}