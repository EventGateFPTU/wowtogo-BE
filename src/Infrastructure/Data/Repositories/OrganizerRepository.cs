using Domain.Interfaces.Data.IRepositories;
using Domain.Models;
using Domain.Responses.Responses_Organizer;
using Domain.Responses.Shared;
using Microsoft.EntityFrameworkCore;
using UseCases.Mapper.Mapper_Organizer;

namespace Infrastructure.Data.Repositories;
public class OrganizerRepository(WowToGoDBContext dbContext) : RepositoryBase<Organizer>(dbContext), IOrganizerRepository
{
	public async Task<PaginatedResponse<OrganizerDB>> GetAllOrganizerAsync(int pageNumber, int pageSize, string? searchTerm, bool trackChanges, CancellationToken cancellationToken)
	{
		IQueryable<Organizer> organizersQuery = _dbSet;
		organizersQuery = trackChanges ? organizersQuery : organizersQuery.AsNoTracking();
		if (searchTerm is not null)
			organizersQuery = organizersQuery.Where(o => o.OrganizationName.Trim().ToLower().Contains(searchTerm.Trim().ToLower()));
		int count = organizersQuery.Count();
		IEnumerable<OrganizerDB> result = await organizersQuery
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.Select(o => o.MapOrganizerDB())
			.ToListAsync(cancellationToken);
		return new PaginatedResponse<OrganizerDB>(
			Data: result,
			PageNumber: pageNumber,
			PageSize: pageSize,
			Count: count);
	}

	public async Task<OrganizerDB?> GetCurrentOrganizationAsync(Guid userId, CancellationToken cancellationToken)
	{
		var organizer = await _dbSet.FirstOrDefaultAsync(x => x.User.Id == userId, cancellationToken);
		return organizer?.MapOrganizerDB();
	}
}