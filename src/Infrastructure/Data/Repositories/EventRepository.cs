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

    public async Task<PaginatedResponse<EventDB>> GetFeaturedEventsAsync(int pageNumber = 1, int pageSize = 10,
        string? searchTerm = null, bool trackChanges = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Event> eventQuery = _dbSet;
        eventQuery = trackChanges ? eventQuery : eventQuery.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            eventQuery = eventQuery.Where(e => e.Title.Contains(searchTerm));
        }
        eventQuery = eventQuery
            .Include(e => e.Organizer)
            .Include(e => e.Shows).ThenInclude(s => s.TicketTypeShow).ThenInclude(tts => tts.TicketType).ThenInclude(tt => tt.Orders)
            .Where(e => e.Status == EventStatusEnum.Published);
        int count = eventQuery.Count();
        IEnumerable<EventDB> result = await eventQuery
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
        return new PaginatedResponse<EventDB>(
            Data: result,
            PageSize: pageSize,
            PageNumber: pageNumber,
            Count: count
        );
    }

    public async Task<PaginatedResponse<EventDB>> SearchEventsAsync(IEnumerable<Guid> categoryIds, int pageNumber = 1, int pageSize = 10, string? searchTerm = null,
        string? location = null, DateTime? date = null, bool trackChanges = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Event> eventQuery = _dbSet;
        if (trackChanges)
        {
            eventQuery = eventQuery.AsNoTracking();
        }
        eventQuery = eventQuery
            .Include(e => e.Organizer)
            .Where(e => e.Status == EventStatusEnum.Published);

        var ids = categoryIds as Guid[] ?? categoryIds.ToArray();
        if (ids.Length != 0)
        {
            eventQuery = eventQuery.Where(x => x.EventCategories.Any(y => ids.Contains(y.CategoryId)));
        }

        if (!string.IsNullOrEmpty(searchTerm))
        {
            var pattern = $"%{searchTerm.Trim()}%";
            eventQuery = eventQuery.Where(x => EF.Functions.ILike(x.Title, pattern));
        }
        
        if (!string.IsNullOrEmpty(location))
        {
            var pattern = $"%{location.Trim()}%";
            eventQuery = eventQuery.Where(x => EF.Functions.ILike(x.Location, pattern));
        }

        if (date is not null)
        {
            var dateOnly = date.Value.Date;
            eventQuery = eventQuery.Where(x => x.Shows.Any(y => y.StartsAt <= dateOnly && dateOnly <= y.EndsAt));
        }
        
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