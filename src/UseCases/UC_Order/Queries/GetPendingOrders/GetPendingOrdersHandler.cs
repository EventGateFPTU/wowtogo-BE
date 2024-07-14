using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Responses.Responses_Order;
using Domain.Responses.Shared;
using MediatR;

namespace UseCases.UC_Order.Queries.GetPendingOrders;
public class GetPendingOrdersHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPendingOrdersQuery, Result<PaginatedResponse<PendingOrderDB>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<PaginatedResponse<PendingOrderDB>>> Handle(GetPendingOrdersQuery request, CancellationToken cancellationToken)
    {
        PaginatedResponse<PendingOrderDB> gettingPendingOrders = await _unitOfWork.OrderRepository.GetPendingOrdersAsync(request.UserId, request.PageNumber, request.PageSize, false, cancellationToken);
        return Result.Success(gettingPendingOrders, "Get Pending Order Successfully");
    }
}