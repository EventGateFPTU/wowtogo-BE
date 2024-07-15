using Domain.Interfaces.Data.IRepositories;
using Domain.Models;
using Domain.Responses.Responses_Category;
using Domain.Responses.Shared;
using Microsoft.EntityFrameworkCore;
using UseCases.Mapper.Mapper_Category;

namespace Infrastructure.Data.Repositories;
public class CategoryRepository(WowToGoDBContext dbContext) : RepositoryBase<Category>(dbContext), ICategoryRepository
{
    public async Task<PaginatedResponse<CategoryDB>> GetCategoriesAsync(string? searchTerm, int pageNumber = 1, int pageSize = 10, bool trackChanges = false, CancellationToken cancellationToken = default)
    {
        IQueryable<Category> categoriesQuery = _dbSet;
        categoriesQuery = trackChanges ? categoriesQuery : categoriesQuery.AsNoTracking();
        if (searchTerm is not null)
            categoriesQuery = categoriesQuery.Where(c => c.Name.Trim().ToLower().Contains(searchTerm.Trim().ToLower()));
        int count = categoriesQuery.Count();
        IEnumerable<CategoryDB> result = await categoriesQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(c => c.MapCategoryDB())
            .ToListAsync(cancellationToken);
        return new PaginatedResponse<CategoryDB>(
            Data: result,
            PageNumber: pageNumber,
            PageSize: pageSize,
            Count: count
        );
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync(Guid[] ids, CancellationToken cancellationToken = default)
    {
        if (ids.Length == 0) return [];
        return await _dbSet
            .AsNoTracking()
            .Where(c => ids.Contains(c.Id))
            .ToListAsync(cancellationToken);
    }
}