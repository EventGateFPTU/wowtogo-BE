using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Order.Commands.CreateOrder;
public record CreateOrderQuery(Guid TicketTypeId, Guid UserId, string Currency, decimal TotalPrice, string PhoneNumber, DateTime DateOfBirth) : IRequest<Result>
{

}