using Domain.Models;
using Domain.Responses.Responses_Event;

namespace Domain.Interfaces.Data.IRepositories;
public interface IEventRepository : IRepositoryBase<Event>
{
    Task<GetEventResponse?> GetEventAsync(Guid eventId, 
                                                    bool trackChanges = false,
                                                    CancellationToken cancellationToken = default);
    Task<Event?> GetEventDeleteAsync(Guid eventId, CancellationToken cancellationToken);
    Task<IEnumerable<EventDB>> GetAllEventAsync(int pageNumber = 1, int pageSize = 10, bool trackChanges = false, CancellationToken cancellationToken = default);
}