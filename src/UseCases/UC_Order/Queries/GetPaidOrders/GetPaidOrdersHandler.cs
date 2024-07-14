using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Responses.Responses_Order;
using Domain.Responses.Shared;
using MediatR;

namespace UseCases.UC_Order.Queries.GetPaidOrders
{
    public class GetPaidOrdersHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPaidOrdersQuery, Result<PaginatedResponse<PaidOrderDB>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Result<PaginatedResponse<PaidOrderDB>>> Handle(GetPaidOrdersQuery request, CancellationToken cancellationToken)
        {
            PaginatedResponse<PaidOrderDB> gettingPaidOrders = await _unitOfWork.OrderRepository.GetPaidOrdersAsync(request.UserId, request.PageNumber, request.PageSize, false, cancellationToken);
            return Result.Success(gettingPaidOrders, "Get Paid Order Successfully");
        }
    }
}
