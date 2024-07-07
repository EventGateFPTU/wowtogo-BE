using Domain.Interfaces.Data.IRepositories;
using Domain.Models;
using Domain.Responses.Responses_Event;
using Microsoft.EntityFrameworkCore;
using UseCases.Mapper.Mapper_Category;
using UseCases.Mapper.Mapper_Event;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Data.Repositories;
public class EventRepository(WowToGoDBContext context) : RepositoryBase<Event>(context), IEventRepository
{
    public async Task<IEnumerable<EventDB>> GetAllEventAsync(int pageNumber = 1, int pageSize = 10, string? searchTerm = null, bool trackChanges = false, CancellationToken cancellationToken = default)
    {
        IQueryable<Event> eventQuery = _dbSet;
        eventQuery = trackChanges ? eventQuery : eventQuery.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            eventQuery = eventQuery.Where(e => e.Title.Contains(searchTerm));
        }
        return await eventQuery
            .Include(e => e.Organizer)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(c => c.MapEventDB())
            .ToListAsync(cancellationToken);
    }

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