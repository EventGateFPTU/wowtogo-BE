using Domain.Interfaces.Data.IRepositories;
using Domain.Models;
using Domain.Responses.Responses_Organizer;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;
public class OrganizerRepository(WowToGoDBContext dbContext) : RepositoryBase<Organizer>(dbContext), IOrganizerRepository
{
	private IOrganizerRepository _organizerRepositoryImplementation;

	public async Task<IEnumerable<OrganizerDB>> GetAllOrganizerAsync(int pageNumber, int pageSize, string? searchTerm, CancellationToken cancellationToken)
	{
		IQueryable<Organizer> organizersQuery = _dbSet;
		organizersQuery = organizersQuery.AsNoTracking();
		if (!string.IsNullOrWhiteSpace(searchTerm))
		{
			organizersQuery = organizersQuery.Where(o => o.OrganizationName.Contains(searchTerm));
		}
		return await organizersQuery
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.Select(o => new OrganizerDB(o.Id, o.OrganizationName, o.Description, o.ImageUrl))
			.ToListAsync(cancellationToken);
	}
}