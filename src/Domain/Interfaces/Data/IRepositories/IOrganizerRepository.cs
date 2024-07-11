using Domain.Models;
using Domain.Responses.Responses_Organizer;

namespace Domain.Interfaces.Data.IRepositories;
public interface IOrganizerRepository : IRepositoryBase<Organizer>
{
	Task<IEnumerable<OrganizerDB>> GetAllOrganizerAsync(int pageNumber, int pageSize, string? searchTerm, CancellationToken cancellationToken);
}