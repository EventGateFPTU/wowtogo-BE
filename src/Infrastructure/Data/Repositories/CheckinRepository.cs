using Domain.Interfaces.Data.IRepositories;
using Domain.Models;
using Domain.Responses.Responses_Checkin;
using Domain.Responses.Shared;
using Microsoft.EntityFrameworkCore;
using UseCases.Mapper.Mapper_Checkin;

namespace Infrastructure.Data.Repositories;

public class CheckinRepository(WowToGoDBContext dbContext) : RepositoryBase<Checkin>(dbContext), ICheckinRepository
{
    public async Task<PaginatedResponse<GetCheckinDetailResponse>> GetCheckinsByEventAsync(Guid eventId, int pageNumber, int pageSize, bool trackChanges = false, CancellationToken cancellationToken = default)
    {
        IQueryable<Checkin> query = trackChanges ? _dbSet : _dbSet.AsNoTracking();
        query = query.Include(c => c.Show)
                    .Include(c => c.Ticket).ThenInclude(t => t.TicketType)
                    .Include(c => c.Ticket).ThenInclude(t => t.Attendee)
                    .Where(c => c.Show.EventId.Equals(eventId));
        int count = query.Count();
        IEnumerable<GetCheckinDetailResponse> result = await query
        .Skip(pageSize * (pageNumber - 1))
        .Take(pageSize)
        .Select(c => c.MapToGetCheckinDetailResponse())
        .ToListAsync(cancellationToken);
        return new PaginatedResponse<GetCheckinDetailResponse>(
            Data: result,
            PageNumber: pageNumber,
            PageSize: pageSize,
            Count: count
        );
    }
}