using Ardalis.Result;
using Domain.Responses.Responses_Order;
using Domain.Responses.Shared;
using MediatR;

namespace UseCases.UC_Order.Queries.GetPendingOrders;
public record GetPendingOrdersQuery(Guid UserId, int PageNumber = 1, int PageSize = 10) : IRequest<Result<PaginatedResponse<PendingOrderDB>>>;