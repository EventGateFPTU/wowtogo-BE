using Domain.Enums;
using Domain.Interfaces.Data.IRepositories;
using Domain.Models;
using Domain.Responses.Responses_Event;
using Domain.Responses.Shared;
using Microsoft.EntityFrameworkCore;
using UseCases.Mapper.Mapper_Category;
using UseCases.Mapper.Mapper_Event;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Data.Repositories;
public class EventRepository(WowToGoDBContext context) : RepositoryBase<Event>(context), IEventRepository
{
    public async Task<PaginatedResponse<EventDB>> GetAllEventAsync(int pageNumber = 1, int pageSize = 10, string? searchTerm = null, bool trackChanges = false, CancellationToken cancellationToken = default)
    {
        IQueryable<Event> eventQuery = _dbSet;
        eventQuery = trackChanges ? eventQuery : eventQuery.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            eventQuery = eventQuery.Where(e => e.Title.Contains(searchTerm));
        }
        eventQuery = eventQuery
            .Include(e => e.Organizer)
            .Where(e => e.Status == EventStatusEnum.Published);
        int count = eventQuery.Count();
        IEnumerable<EventDB> result = await eventQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(c => c.MapEventDB())
            .ToListAsync(cancellationToken);
        return new PaginatedResponse<EventDB>(
            Data: result,
            PageNumber: pageNumber,
            PageSize: pageSize,
            Count: count
        );
    }

    public async Task<IEnumerable<EventDB>> GetFeaturedEventsAsync(int pageNumber = 1, int pageSize = 10,
        string? searchTerm = null, bool trackChanges = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Event> eventQuery = _dbSet;
        eventQuery = trackChanges ? eventQuery : eventQuery.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            eventQuery = eventQuery.Where(e => e.Title.Contains(searchTerm));
        }
        return await eventQuery
            .Include(e => e.Organizer)
            .Include(e => e.Shows).ThenInclude(s => s.TicketTypeShow).ThenInclude(tts => tts.TicketType).ThenInclude(tt => tt.Orders)
            .Where(e => e.Status == EventStatusEnum.Published)
            .Select(e => new
            {
                Event = e.MapEventDB(),
                Rate = e.Shows.SelectMany(s => s.TicketTypeShow).Select(tts => tts.TicketType).Select(tt => tt.Orders).Count() / (DateTime.UtcNow - e.CreatedAt).Days,
            })
            .OrderByDescending(o => o.Rate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(c => c.Event)
            .ToListAsync(cancellationToken);
    }

    public async Task<GetEventResponse?> GetEventAsync(Guid eventId, bool trackChanges = false, CancellationToken cancellationToken = default)
    {
        IQueryable<Event> query = _dbSet;
        if (!trackChanges) query = query.AsNoTracking();
        return await query
            .Include(e => e.Organizer)
            .Include(e => e.EventCategories).ThenInclude(ec => ec.Category)
            .Where(e => e.Id.Equals(eventId))
            .Select(e => e.MapToGetEventResponse())
            .FirstOrDefaultAsync(cancellationToken);
    }

}