using Domain.Models;

namespace Domain.Interfaces.Data.IRepositories;
public interface ITicketTypeRepository : IRepositoryBase<TicketType>
{
    Task<Event?> GetEventFromTicketTypeIdAsync(Guid ticketTypeId, CancellationToken cancellationToken = default);
}