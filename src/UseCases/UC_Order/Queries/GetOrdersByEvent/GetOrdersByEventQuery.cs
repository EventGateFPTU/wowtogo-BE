using Ardalis.Result;
using Domain.Responses.Responses_Order;
using Domain.Responses.Shared;
using MediatR;

namespace UseCases.UC_Order.Queries.GetOrdersByEvent;
public record GetOrdersByEventQuery(
    Guid Id,
    int PageNumber,
    int PageSize
) : IRequest<Result<PaginatedResponse<OrderResponse>>>;