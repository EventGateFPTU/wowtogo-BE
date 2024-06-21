using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_Order;
using MediatR;

namespace UseCases.UC_Order.Queries.GetPendingOrders.cs;
public class GetPendingOrdersHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPendingOrdersQuery, Result<GetPendingOrdersResponse>>
{
    public async Task<Result<GetPendingOrdersResponse>> Handle(GetPendingOrdersQuery request, CancellationToken cancellationToken)
    {
        User? user = await unitOfWork.UserRepository.FindAsync(u => u.Id.Equals(request.UserId), cancellationToken: cancellationToken);
        if (user is null) return Result.NotFound("User is not found");
        IEnumerable<OrderResponse> orders = await unitOfWork.OrderRepository.GetPendingOrdersAsync(userId: request.UserId,
                                                                                                    pageNumber: request.PageNumber,
                                                                                                    pageSize: request.PageSize,
                                                                                                    cancellationToken: cancellationToken);
        if (!orders.Any()) return Result.NotFound("No pending orders is found");
        return Result.Success(new GetPendingOrdersResponse(orders, request.PageNumber, request.PageSize), "Get pending orders successfully");
    }
}