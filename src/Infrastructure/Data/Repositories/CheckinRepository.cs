using Domain.Interfaces.Data.IRepositories;
using Domain.Models;

namespace Infrastructure.Data.Repositories;

public class CheckinRepository(WowToGoDBContext dbContext) : RepositoryBase<Checkin>(dbContext), ICheckinRepository
{
    
}