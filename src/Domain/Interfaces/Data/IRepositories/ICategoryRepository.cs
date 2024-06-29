using Domain.Models;
using Domain.Responses.Responses_Category;

namespace Domain.Interfaces.Data.IRepositories;
public interface ICategoryRepository : IRepositoryBase<Category>
{
    Task<IEnumerable<CategoryDB>> GetCategoriesAsync(int pageNumber = 1, int pageSize = 10, bool trackChanges = false, CancellationToken cancellationToken = default);
    Task<IEnumerable<Category>> GetCategoriesAsync(Guid[] ids,CancellationToken cancellationToken = default);
}