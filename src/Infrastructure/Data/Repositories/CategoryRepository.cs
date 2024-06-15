using Domain.Interfaces.Data.IRepositories;
using Domain.Models;

namespace Infrastructure.Data.Repositories;
public class CategoryRepository(WowToGoDBContext dbContext) : RepositoryBase<Category>(dbContext), ICategoryRepository
{
    
}