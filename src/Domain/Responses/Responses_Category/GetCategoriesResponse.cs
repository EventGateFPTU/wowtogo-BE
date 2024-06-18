using Domain.Models;

namespace Domain.Responses.Responses_Category;
public record CategoryDB(Guid Id, string Name);
public record GetCategoriesResponse(int PageNumber, int PageSize, IEnumerable<CategoryDB> Categories);