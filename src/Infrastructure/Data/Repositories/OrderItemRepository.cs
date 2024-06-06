using Domain.Interfaces.Data.IRepositories;
using Domain.Models;

namespace Infrastructure.Data.Repositories;
public class OrderItemRepository(WowToGoDBContext context) : RepositoryBase<OrderItem>(context), IOrderItemRepository
{
    
}