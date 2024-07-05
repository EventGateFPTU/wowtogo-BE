using Domain.Interfaces.Data.IRepositories;
using Domain.Models;
using Domain.Responses.Responses_Show;
using Domain.Responses.Responses_TicketType;
using Microsoft.EntityFrameworkCore;
using UseCases.Mapper.Mapper_Show;
using UseCases.Mapper.Mapper_TicketType;

namespace Infrastructure.Data.Repositories;
public class ShowRepository(WowToGoDBContext dbContext) : RepositoryBase<Show>(dbContext), IShowRepository
{
    public async Task<GetShowDetailResponse?> GetShowDetailAsync(Guid showId, bool trackChanges = false, CancellationToken cancellationToken = default)
    {
        IQueryable<Show> query = _dbSet;
        if (!trackChanges) query = query.AsNoTracking();
        return await query
            .Include(s => s.Event)
            .Where(s => s.Id == showId)
            .Select(s => s.MapToGetShowDetailResponse())
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<Show?> GetShowIncludingEventAsync(Guid showId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Include(s => s.Event)
            .SingleOrDefaultAsync(s => s.Id == showId, cancellationToken);
    }

    public async Task<IEnumerable<GetShowDetailResponse>> GetShowsOfEventAsync(Guid eventId, int pageNumber = 1, int pageSize = 10, bool trackChanges = false, CancellationToken cancellationToken = default)
    {
        IQueryable<Show> query = _dbSet;
        if (!trackChanges) query = query.AsNoTracking();
        return await query
            .Include(s => s.Event)
            .Include(s => s.TicketTypeShow)
            .ThenInclude(tts => tts.TicketType)
            .Where(s => s.Event.Id == eventId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(s => s.MapToGetShowDetailResponse())
            .ToListAsync(cancellationToken);
    }
}