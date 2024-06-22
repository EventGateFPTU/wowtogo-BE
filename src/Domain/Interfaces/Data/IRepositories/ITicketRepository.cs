using Domain.Models;
using Domain.Responses.Responses_Ticket;

namespace Domain.Interfaces.Data.IRepositories;
public interface ITicketRepository : IRepositoryBase<Ticket>
{
    Task<GetTicketDetailsResponse?> GetTicketDetail(Guid ticketId, bool trackChanges = false, CancellationToken cancellationToken = default);
    Task<CreateTicketResponse?> GetCreatedTicketDetail(Guid ticketId, bool trackChanges = false, CancellationToken cancellationToken = default);
    Task<Ticket?> GetTicketDetailByCode(string code, Guid showId, bool trackChanges = false, CancellationToken cancellationToken = default);
}