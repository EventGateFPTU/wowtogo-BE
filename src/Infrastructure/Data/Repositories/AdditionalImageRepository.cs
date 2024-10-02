using Domain.Interfaces.Data.IRepositories;
using Domain.Models;

namespace Infrastructure.Data.Repositories;

public class AdditionalImageRepository(WowToGoDBContext dbContext) : RepositoryBase<AdditionalImage>(dbContext), IAdditionalImageRepository
{
    
}