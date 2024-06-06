using Domain.Interfaces.Data.IRepositories;
using Domain.Models;

namespace Infrastructure.Data.Repositories;
public class TicketRepository(WowToGoDBContext context) : RepositoryBase<Ticket>(context), ITicketRepository
{
    
}