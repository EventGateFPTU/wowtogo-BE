using Domain.Interfaces.Data.IRepositories;
using Domain.Models;
using Domain.Responses.Responses_Category;
using Microsoft.EntityFrameworkCore;
using UseCases.Mapper.Mapper_Category;

namespace Infrastructure.Data.Repositories;
public class CategoryRepository(WowToGoDBContext dbContext) : RepositoryBase<Category>(dbContext), ICategoryRepository
{
    public async Task<IEnumerable<CategoryDB>> GetCategoriesAsync(int pageNumber = 1, int pageSize = 10, bool trackChanges = false, CancellationToken cancellationToken = default)
    {
        IQueryable<Category> categoriesQuery = _dbSet;
        categoriesQuery = trackChanges ? categoriesQuery : categoriesQuery.AsNoTracking();
        return await categoriesQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(c => c.MapCategoryDB())
            .ToListAsync(cancellationToken);
    }
}