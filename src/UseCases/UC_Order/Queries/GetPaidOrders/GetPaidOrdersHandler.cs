using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_Order;
using MediatR;

namespace UseCases.UC_Order.Queries.GetPaidOrders;
public class GetPaidOrdersHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPaidOrdersQuery, Result<GetPaidOrdersResponse>>
{
    public async Task<Result<GetPaidOrdersResponse>> Handle(GetPaidOrdersQuery request, CancellationToken cancellationToken)
    {
        User? user = await unitOfWork.UserRepository.FindAsync(u => u.Id.Equals(request.UserId), cancellationToken: cancellationToken);
        if (user is null) return Result.NotFound("User is not found");
        IEnumerable<OrderResponse> orders = await unitOfWork.OrderRepository.GetPaidOrdersAsync(userId: request.UserId,
                                                                                                    pageNumber: request.PageNumber,
                                                                                                    pageSize: request.PageSize,
                                                                                                    cancellationToken: cancellationToken);
        if (!orders.Any()) return Result.NotFound("No paid orders are found");
        return Result.Success(new GetPaidOrdersResponse(orders, request.PageNumber, request.PageSize), "Get paid orders successfully");
    }
}