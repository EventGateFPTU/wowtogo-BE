using Domain.Interfaces.Data.IRepositories;
using Domain.Models;
using Domain.Responses.Responses_Event;
using Microsoft.EntityFrameworkCore;
using UseCases.Mapper.Mapper_Event;

namespace Infrastructure.Data.Repositories;
public class EventRepository(WowToGoDBContext context) : RepositoryBase<Event>(context), IEventRepository
{
    public async Task<GetEventResponse?> GetEventAsync(Guid eventId, bool trackChanges = false, CancellationToken cancellationToken = default)
    {
        IQueryable<Event> query = _dbSet;
        if (!trackChanges) query = query.AsNoTracking();
        return await query
            .Include(e => e.Organizer)
            .Where(e => e.Id.Equals(eventId))
            .Select(e=> e.MapToGetEventResponse())
            .FirstOrDefaultAsync(cancellationToken);
    }
}