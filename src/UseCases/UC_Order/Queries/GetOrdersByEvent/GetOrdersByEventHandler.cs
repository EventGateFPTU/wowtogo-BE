using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Responses.Responses_Order;
using Domain.Responses.Shared;
using MediatR;

namespace UseCases.UC_Order.Queries.GetOrdersByEvent;
public class GetOrdersByEventHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetOrdersByEventQuery, Result<PaginatedResponse<OrderResponse>>>
{
    public async Task<Result<PaginatedResponse<OrderResponse>>> Handle(GetOrdersByEventQuery request, CancellationToken cancellationToken)
    {
        PaginatedResponse<OrderResponse> result = await unitOfWork.OrderRepository.GetOrdersByEventAsync(request.Id, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
        return Result.Success(result, "Get Order Successfully");
    }
}