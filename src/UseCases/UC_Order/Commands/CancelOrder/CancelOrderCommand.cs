using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Order.Commands.CancelOrder;
public record CancelOrderCommand(Guid OrderId) : IRequest<Result>;