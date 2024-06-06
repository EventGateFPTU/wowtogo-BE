using Domain.Interfaces.Data.IRepositories;
using Domain.Models;

namespace Infrastructure.Data.Repositories;
public class OrderRepository(WowToGoDBContext context) : RepositoryBase<Order>(context), IOrderRepository
{
    
}