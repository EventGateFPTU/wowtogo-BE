using Domain.Interfaces.Data.IRepositories;
using Domain.Models;
using Domain.Responses.Responses_TicketType;
using Microsoft.EntityFrameworkCore;
using UseCases.Mapper.Mapper_TicketType;

namespace Infrastructure.Data.Repositories;
public class TicketTypeRepository(WowToGoDBContext dbContext) : RepositoryBase<TicketType>(dbContext), ITicketTypeRepository
{
    public async Task<Event?> GetEventFromTicketTypeIdAsync(Guid ticketTypeId, CancellationToken cancellationToken = default)
        => await _dbSet.AsNoTracking()
            .Where(tt => tt.Id.Equals(ticketTypeId))
            .Select(tt => tt.Show)
            .Select(s => s.Event)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<IEnumerable<GetTicketTypeDetailsResponse>> GetTicketTypesOfShowAsync(Guid showId, int pageSize, int pageNumber, bool trackChanges = false, CancellationToken cancellationToken = default)
    {
        IQueryable<TicketType> query = _dbSet;
        if (!trackChanges) query = query.AsNoTracking();
        return await query
            .Include(tt => tt.Show)
            .Where(tt => tt.ShowId.Equals(showId))
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(tt => tt.MapToGetTicketTypeDetailsResponse())
            .ToListAsync();
    }
}