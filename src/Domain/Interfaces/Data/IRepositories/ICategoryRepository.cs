using Domain.Models;
using Domain.Responses.Responses_Category;
using Domain.Responses.Shared;

namespace Domain.Interfaces.Data.IRepositories;
public interface ICategoryRepository : IRepositoryBase<Category>
{
    Task<PaginatedResponse<CategoryDB>> GetCategoriesAsync(string? searchTerm = null, int pageNumber = 1, int pageSize = 10, bool trackChanges = false, CancellationToken cancellationToken = default);
    Task<IEnumerable<Category>> GetCategoriesAsync(Guid[] ids,CancellationToken cancellationToken = default);
}