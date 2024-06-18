using Domain.Models;
using Domain.Responses.Responses_Category;

namespace UseCases.Mapper.Mapper_Category;
public static class CategoryDBMapper
{
    public static CategoryDB MapCategoryDB(this Category category)
        => new CategoryDB(category.Id, category.Name);
}