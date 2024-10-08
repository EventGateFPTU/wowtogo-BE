using Domain.Models;
using Domain.Responses.Responses_Event;
using Domain.Responses.Shared;

namespace Domain.Interfaces.Data.IRepositories;
public interface IEventRepository : IRepositoryBase<Event>
{
    Task<GetEventResponse?> GetEventAsync(Guid eventId,
                                                    bool trackChanges = false,
                                                    CancellationToken cancellationToken = default);
    Task<PaginatedResponse<GetEventResponse>> GetAllEventAsync(int pageNumber = 1, int pageSize = 10, string? searchTerm = null, bool trackChanges = false, CancellationToken cancellationToken = default);

    Task<PaginatedResponse<GetEventResponse>> GetFeaturedEventsAsync(int pageNumber = 1, int pageSize = 10, string? searchTerm = null,
        bool trackChanges = false, CancellationToken cancellationToken = default);

    Task<PaginatedResponse<GetEventResponse>> SearchEventsAsync(IEnumerable<Guid> categoryIds, int pageNumber = 1,
        int pageSize = 10, string? searchTerm = null, string? location = null, DateTime? date = null,
        bool trackChanges = false, CancellationToken cancellationToken = default);

    Task<List<EventDB>> GetOrganizerEvents(Guid organizerId, CancellationToken cancellationToken = default);

    Task<PaginatedResponse<GetEventResponse>> GetEventsOfStaff(Guid staffId, int pageNumber, int pageSize, bool trackChanges = false, CancellationToken cancellationToken = default);
    Task<Event?> GetEventWithOrganizer(Guid eventId, bool trackChanges = false, CancellationToken cancellationToken = default);
}