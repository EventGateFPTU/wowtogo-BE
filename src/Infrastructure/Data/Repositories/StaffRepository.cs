using Domain.Enums;
using Domain.Interfaces.Data.IRepositories;
using Domain.Models;
using Domain.Responses.Responses_Staff;
using Domain.Responses.Shared;
using Microsoft.EntityFrameworkCore;
using UseCases.Mapper.Mapp_Staff;

namespace Infrastructure.Data.Repositories;
public class StaffRepository(WowToGoDBContext dBContext) : RepositoryBase<Staff>(dBContext), IStaffRepository
{
    public async Task<PaginatedResponse<StaffResponse>> GetEventStaffsAsync(Guid eventId, int pageNumber = 1, int pageSize = 10, bool trackChanges = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Staff> query = _dbSet;
        if (!trackChanges) query = query.AsNoTracking();
        query = query.Include(s => s.User).Where(o => o.EventId.Equals(eventId));
        int count = query.Count();
        IEnumerable<StaffResponse> result = await query.Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(o => o.MapToStaffResponse())
            .ToListAsync(cancellationToken);
        return new PaginatedResponse<StaffResponse>(
            Data: result,
            PageNumber: pageNumber,
            PageSize: pageSize,
            Count: count
        );
    }
}