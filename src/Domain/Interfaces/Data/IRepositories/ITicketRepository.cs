using Domain.Models;
using Domain.Responses.Responses_Ticket;

namespace Domain.Interfaces.Data.IRepositories;
public interface ITicketRepository : IRepositoryBase<Ticket>
{
    Task<GetTicketDetailResponse?> GetTicketDetail(Guid ticketId, bool trackChanges = false, CancellationToken cancellationToken = default);
}