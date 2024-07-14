using Domain.Models;
using Domain.Responses.Responses_Order;
using Domain.Responses.Shared;

namespace Domain.Interfaces.Data.IRepositories;
public interface IOrderRepository : IRepositoryBase<Order>
{
    Task<PaginatedResponse<PendingOrderDB>> GetPendingOrdersAsync(Guid userId,
                                                    int pageNumber = 1,
                                                    int pageSize = 10,
                                                    bool trackChanges = false,
                                                    CancellationToken cancellationToken = default);
    Task<PaginatedResponse<PaidOrderDB>> GetPaidOrdersAsync(Guid userId,
                                                    int pageNumber = 1,
                                                    int pageSize = 10,
                                                    bool trackChanges = false,
                                                    CancellationToken cancellationToken = default);
    Task<PaginatedResponse<OrderResponse>> GetOrdersByEventAsync(Guid eventId,
                                                                int pageNumber = 1,
                                                                int pageSize = 10,
                                                                bool trackChanges = false,
                                                                CancellationToken cancellationToken = default);

}