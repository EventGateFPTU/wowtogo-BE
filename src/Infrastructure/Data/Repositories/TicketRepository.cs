using Domain.Interfaces.Data.IRepositories;
using Domain.Models;
using Domain.Responses.Responses_Ticket;
using Domain.Responses.Shared;
using Microsoft.EntityFrameworkCore;
using UseCases.Mapper.Mapper_Ticket;

namespace Infrastructure.Data.Repositories;
public class TicketRepository(WowToGoDBContext context) : RepositoryBase<Ticket>(context), ITicketRepository
{
    public async Task<GetTicketDetailsResponse?> GetTicketDetail(Guid ticketId, bool trackChanges = false, CancellationToken cancellationToken = default)
    {
        IQueryable<Ticket> query = _dbSet;
        if (!trackChanges) query = query.AsNoTracking();
        return await query
            .Include(t => t.TicketType)
            .ThenInclude(tt => tt.TicketTypeShows)
            .ThenInclude(tts => tts.Show)
            .ThenInclude(s => s.Event)
            .Where(t => t.Id == ticketId)
            .Select(t => t.MapToGetTicketDetailsResponse())
            .SingleOrDefaultAsync(cancellationToken);
    }
    public async Task<CreateTicketResponse?> GetCreatedTicketDetail(Guid ticketId, bool trackChanges = false, CancellationToken cancellationToken = default)
    {
        IQueryable<Ticket> query = _dbSet;
        if (!trackChanges) query = query.AsNoTracking();
        return await query
            .Include(t => t.TicketType)
            .ThenInclude(tt => tt.TicketTypeShows)
            .ThenInclude(tts => tts.Show)
            .ThenInclude(s => s.Event)
            .Where(t => t.Id == ticketId)
            .Select(t => t.MapToCreateTicketResponse())
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<Ticket?> GetTicketDetailByCode(string code, Guid showId, bool trackChanges = false, CancellationToken cancellationToken = default)
    {
        IQueryable<Ticket> query = _dbSet;
        if (!trackChanges) query = query.AsNoTracking();
        return await query
            .Include(t => t.Attendee)
            .Include(t => t.TicketType)
            .ThenInclude(tt => tt.TicketTypeShows)
            .ThenInclude(tts => tts.Show)
            .Where(t => t.Code == code && t.TicketType.TicketTypeShows.Any(tts => tts.ShowId == showId))
            .SingleOrDefaultAsync(cancellationToken);
    }
    public async Task<Ticket?> GetTicketDetailById(Guid id, bool trackChanges = false, CancellationToken cancellationToken = default)
    {
        IQueryable<Ticket> query = _dbSet;
        if (!trackChanges) query = query.AsNoTracking();
        return await query
            .Include(t => t.Attendee)
            .Include(t => t.TicketType)
            .ThenInclude(tt => tt.TicketTypeShows)
            .ThenInclude(tts => tts.Show)
            .Where(t => t.Id.Equals(id))
            .SingleOrDefaultAsync(cancellationToken);
    }



    public async Task<PaginatedResponse<GetTicketDetailsResponse>> GetTicketsOfUser(Guid userId, int pageNumber, int pageSize, bool trackChanges = false, CancellationToken cancellationToken = default)
    {
        IQueryable<Ticket> query = trackChanges ? _dbSet : _dbSet.AsNoTracking();
        query = query.Include(t => t.Attendee)
            .Where(t => t.Attendee.UserId.Equals(userId));
        int count = query.Count();
        IEnumerable<GetTicketDetailsResponse> result = await query.Skip(pageSize * (pageNumber - 1))
                                                                    .Take(pageSize)
                                                                    .Select(t => t.MapToGetTicketDetailsResponse())
                                                                    .ToListAsync();
        return new PaginatedResponse<GetTicketDetailsResponse>(
            Data: result,
            PageNumber: pageNumber,
            PageSize: pageSize,
            Count: count
        );
    }
}