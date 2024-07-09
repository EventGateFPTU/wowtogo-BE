using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Responses.Responses_Category;
using MediatR;

namespace UseCases.UC_Category.Queries.GetCategories;
public class GetCategoriesHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCategoriesQuery, Result<GetCategoriesResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<GetCategoriesResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<CategoryDB> categoryDBs = await _unitOfWork.CategoryRepository.GetCategoriesAsync(request.SearchTerm, request.PageNumber, request.PageSize, false, cancellationToken);
        if (!categoryDBs.Any()) return Result.NotFound("Categories are not found");
        GetCategoriesResponse result = new(request.PageNumber, request.PageSize, categoryDBs);
        return Result.Success(result, "Categories are found successfully");
    }
}