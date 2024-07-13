using Domain.Models;
using Domain.Responses.Responses_Event;
using Domain.Responses.Shared;

namespace Domain.Interfaces.Data.IRepositories;
public interface IEventRepository : IRepositoryBase<Event>
{
    Task<GetEventResponse?> GetEventAsync(Guid eventId,
                                                    bool trackChanges = false,
                                                    CancellationToken cancellationToken = default);
    Task<PaginatedResponse<EventDB>> GetAllEventAsync(int pageNumber = 1, int pageSize = 10, string? searchTerm = null, bool trackChanges = false, CancellationToken cancellationToken = default);

    Task<PaginatedResponse<EventDB>> GetFeaturedEventsAsync(int pageNumber = 1, int pageSize = 10, string? searchTerm = null,
        bool trackChanges = false, CancellationToken cancellationToken = default);
    
    Task<PaginatedResponse<EventDB>> SearchEventsAsync(IEnumerable<Guid> categoryIds, int pageNumber = 1,
        int pageSize = 10, string? searchTerm = null, string? location = null, DateTime? date = null,
        bool trackChanges = false, CancellationToken cancellationToken = default);
}