using Domain.Enums;
using Domain.Interfaces.Data.IRepositories;
using Domain.Models;
using Domain.Responses.Responses_Staff;
using Microsoft.EntityFrameworkCore;
using UseCases.Mapper.Mapp_Staff;

namespace Infrastructure.Data.Repositories;
public class StaffRepository(WowToGoDBContext dBContext) : RepositoryBase<Staff>(dBContext), IStaffRepository
{
    public async Task<IEnumerable<StaffResponse>> GetEventStaffsAsync(Guid eventId, int pageNumber = 1, int pageSize = 10, bool trackChanges = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Staff> query = _dbSet;
        if (!trackChanges) query = query.AsNoTracking();
        return await query.Where(o => o.EventId.Equals(eventId))
            .Include(s => s.User)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(o => o.MapToStaffResponse())
            .ToListAsync(cancellationToken);
    }
}