using Domain.Interfaces.Data.IRepositories;
using Domain.Models;

namespace Infrastructure.Data.Repositories;
public class StaffRepository(WowToGoDBContext dBContext) : RepositoryBase<Staff>(dBContext), IStaffRepository
{
}