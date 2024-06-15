using Domain.Interfaces.Data.IRepositories;
using Domain.Models;

namespace Infrastructure.Data.Repositories;
public class TicketTypeRepository(WowToGoDBContext dbContext) : RepositoryBase<TicketType>(dbContext), ITicketTypeRepository
{
    
}