using Domain.Models;
using Domain.Responses.Responses_Show;
using Domain.Responses.Responses_TicketType;
using Domain.Responses.Shared;

namespace Domain.Interfaces.Data.IRepositories;
public interface IShowRepository : IRepositoryBase<Show>
{
    Task<GetShowDetailResponse?> GetShowDetailAsync(Guid showId, bool trackChanges = false, CancellationToken cancellationToken = default);
    Task<PaginatedResponse<GetShowDetailResponse>> GetShowsOfEventAsync(Guid eventId, int pageNumber = 1, int pageSize = 10, bool trackChanges = false, CancellationToken cancellationToken = default);
    Task<Show?> GetShowIncludingEventAsync(Guid showId, CancellationToken cancellationToken = default);
}