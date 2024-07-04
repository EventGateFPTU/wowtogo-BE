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
            .Include(tt => tt.TicketTypeShows)
            .ThenInclude(tts => tts.Show)
            .ThenInclude(s => s.Event)
            .Where(tt => tt.Id.Equals(ticketTypeId))
            .SelectMany(tt => tt.TicketTypeShows)
            .Select(tts => tts.Show)
            .Select(s => s.Event)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<TicketType?> GetTicketIncludingEventAsync(Guid ticketId, CancellationToken cancellationToken = default)
        => await _dbSet.AsNoTracking()
            .Include(tt => tt.TicketTypeShows)
            .ThenInclude(tts => tts.Show)
            .ThenInclude(s => s.Event)
            .Where(tt => tt.Id.Equals(ticketId))
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<IEnumerable<GetTicketTypeDetailsResponse>> GetTicketTypesOfShowAsync(Guid showId, int pageSize, int pageNumber, bool trackChanges = false, CancellationToken cancellationToken = default)
    {
        IQueryable<TicketType> query = _dbSet;
        if (!trackChanges) query = query.AsNoTracking();
        return await query
            .Include(tt => tt.TicketTypeShows)
            .ThenInclude(tts => tts.Show)
            .Where(tt => tt.TicketTypeShows.Any(tts => tts.ShowId.Equals(showId)))
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(tt => tt.MapToGetTicketTypeDetailsResponse())
            .ToListAsync();
    }
}