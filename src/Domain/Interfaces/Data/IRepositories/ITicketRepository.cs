using Domain.Models;
using Domain.Responses.Responses_Ticket;
using Domain.Responses.Shared;

namespace Domain.Interfaces.Data.IRepositories;
public interface ITicketRepository : IRepositoryBase<Ticket>
{
    Task<GetTicketDetailsResponse?> GetTicketDetail(Guid ticketId, bool trackChanges = false, CancellationToken cancellationToken = default);
    Task<CreateTicketResponse?> GetCreatedTicketDetail(Guid ticketId, bool trackChanges = false, CancellationToken cancellationToken = default);
    Task<Ticket?> GetTicketDetailByCode(string code, Guid showId, bool trackChanges = false, CancellationToken cancellationToken = default);
    Task<Ticket?> GetTicketDetailById(Guid id, bool trackChanges = false, CancellationToken cancellationToken = default);
    Task<PaginatedResponse<GetTicketDetailsResponse>> GetTicketsOfUser(Guid userId, int pageNumber, int pageSize, bool trackChanges = false, CancellationToken cancellationToken = default);
}