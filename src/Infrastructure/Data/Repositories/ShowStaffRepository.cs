using Domain.Interfaces.Data.IRepositories;
using Domain.Models;

namespace Infrastructure.Data.Repositories;

public class ShowStaffRepository(WowToGoDBContext dbContext) : RepositoryBase<ShowStaff>(dbContext), IShowStaffRepository
{
    
}