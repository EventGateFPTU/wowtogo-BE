using Ardalis.Result;
using Domain.Responses.Responses_Order;
using MediatR;

namespace UseCases.UC_Order.Queries.GetPaidOrders;
public record GetPaidOrdersQuery(Guid UserId, int PageNumber = 1, int PageSize = 10) : IRequest<Result<GetPaidOrdersResponse>>;