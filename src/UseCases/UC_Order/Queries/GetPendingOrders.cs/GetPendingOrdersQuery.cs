using Ardalis.Result;
using Domain.Responses.Responses_Order;
using MediatR;

namespace UseCases.UC_Order.Queries.GetPendingOrders.cs;
public record GetPendingOrdersQuery(Guid UserId, int PageNumber = 1, int PageSize = 10) : IRequest<Result<GetPendingOrdersResponse>>;