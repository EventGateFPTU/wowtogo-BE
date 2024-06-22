using Domain.Models;
using Domain.Responses.Responses_Show;
using Domain.Responses.Responses_TicketType;

namespace Domain.Interfaces.Data.IRepositories;
public interface IShowRepository : IRepositoryBase<Show>
{
    Task<GetShowDetailResponse?> GetShowDetailAsync(Guid showId, bool trackChanges = false, CancellationToken cancellationToken = default);
    Task<IEnumerable<GetShowDetailResponse>> GetShowsOfEventAsync(Guid eventId, int pageNumber = 1, int pageSize = 10, bool trackChanges = false, CancellationToken cancellationToken = default);
}