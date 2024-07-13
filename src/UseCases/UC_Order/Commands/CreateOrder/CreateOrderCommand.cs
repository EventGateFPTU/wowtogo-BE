using Ardalis.Result;
using Domain.Responses.Responses_Order;
using MediatR;

namespace UseCases.UC_Order.Commands.CreateOrder;
public record CreateOrderCommand(
    Guid TicketTypeId,
    string Currency,
    string PhoneNumber,
    DateTimeOffset DateOfBirth) : IRequest<Result<CreateOrderResponse>>;