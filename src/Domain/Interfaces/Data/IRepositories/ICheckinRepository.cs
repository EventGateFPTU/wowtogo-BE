using Domain.Models;
using Domain.Responses.Responses_Checkin;
using Domain.Responses.Shared;

namespace Domain.Interfaces.Data.IRepositories;

public interface ICheckinRepository : IRepositoryBase<Checkin>
{
    Task<PaginatedResponse<GetCheckinDetailResponse>> GetCheckinsByEventAsync(Guid eventId, int pageNumber, int pageSize, bool trackChanges = false, CancellationToken cancellationToken = default);
}