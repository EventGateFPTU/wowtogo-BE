using Domain.Models;
using Domain.Responses.Responses_Event;

namespace Domain.Interfaces.Data.IRepositories;
public interface IEventRepository : IRepositoryBase<Event>
{
    Task<GetEventResponse?> GetEventAsync(Guid eventId, 
                                                    bool trackChanges = false,
                                                    CancellationToken cancellationToken = default);
    
}