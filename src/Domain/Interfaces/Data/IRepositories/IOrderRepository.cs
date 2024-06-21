using Domain.Models;
using Domain.Responses.Responses_Order;

namespace Domain.Interfaces.Data.IRepositories;
public interface IOrderRepository : IRepositoryBase<Order>
{
    Task<IEnumerable<OrderResponse>> GetPendingOrdersAsync(Guid userId,
                                                    int pageNumber = 1,
                                                    int pageSize = 10,
                                                    bool trackChanges = false,
                                                    CancellationToken cancellationToken = default);

}