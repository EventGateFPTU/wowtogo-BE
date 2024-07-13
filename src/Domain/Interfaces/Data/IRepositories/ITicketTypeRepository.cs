using Domain.Models;
using Domain.Responses.Responses_TicketType;
using Domain.Responses.Shared;

namespace Domain.Interfaces.Data.IRepositories;
public interface ITicketTypeRepository : IRepositoryBase<TicketType>
{
    Task<Event?> GetEventFromTicketTypeIdAsync(Guid ticketTypeId, CancellationToken cancellationToken = default);
    Task<PaginatedResponse<GetTicketTypeDetailsResponse>> GetTicketTypesOfShowAsync(Guid showId, int pageSize, int pageNumber, bool trackChanges = false, CancellationToken cancellationToken = default);
    Task<TicketType?> GetTicketIncludingEventAsync(Guid ticketId, CancellationToken cancellationToken = default);
}