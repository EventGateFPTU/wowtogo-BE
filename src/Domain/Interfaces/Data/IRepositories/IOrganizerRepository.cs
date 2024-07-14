using Domain.Models;
using Domain.Responses.Responses_Organizer;
using Domain.Responses.Shared;

namespace Domain.Interfaces.Data.IRepositories;
public interface IOrganizerRepository : IRepositoryBase<Organizer>
{
    Task<PaginatedResponse<OrganizerDB>> GetAllOrganizerAsync(int pageNumber = 1, int pageSize = 10, string? searchTerm = null, bool trackChanges = false, CancellationToken cancellationToken = default);
}