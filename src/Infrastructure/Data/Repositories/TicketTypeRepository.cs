using Domain.Interfaces.Data.IRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;
public class TicketTypeRepository(WowToGoDBContext dbContext) : RepositoryBase<TicketType>(dbContext), ITicketTypeRepository
{
    public async Task<Event?> GetEventFromTicketTypeIdAsync(Guid ticketTypeId, CancellationToken cancellationToken = default)
        => await _dbSet.AsNoTracking()
            .Where(tt => tt.Id.Equals(ticketTypeId))
            .Select(tt => tt.Show)
            .Select(s => s.Event)
            .FirstOrDefaultAsync(cancellationToken);

}