using Domain.Models;
using Domain.Responses.Responses_Staff;
using Domain.Responses.Shared;

namespace Domain.Interfaces.Data.IRepositories;
public interface IStaffRepository : IRepositoryBase<Staff>
{
    Task<PaginatedResponse<StaffResponse>> GetEventStaffsAsync(Guid eventId,
        int pageNumber = 1,
        int pageSize = 10,
        bool trackChanges = false,
        CancellationToken cancellationToken = default);
}