using Ardalis.Result;
using MediatR;
using Net.payOS.Types;

namespace UseCases.UC_Order.Commands.CreateOrder;
public record CreateOrderCommand(
    Guid TicketTypeId,
    string PhoneNumber,
    string FirstName,
    string LastName,
    string Email,
    DateTimeOffset DateOfBirth) : IRequest<Result<CreatePaymentResult>>;