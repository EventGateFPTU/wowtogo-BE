using Domain.Interfaces.Data.IRepositories;
using Domain.Models;
using Domain.Responses.Responses_Ticket;
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
            .ThenInclude(tt => tt.Show)
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
            .ThenInclude(tt => tt.Show)
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
            .Include(t => t.TicketType)
            .ThenInclude(tt => tt.Show)
            .Where(t => t.Code == code && t.TicketType.ShowId == showId)
            .SingleOrDefaultAsync(cancellationToken);
    }
}