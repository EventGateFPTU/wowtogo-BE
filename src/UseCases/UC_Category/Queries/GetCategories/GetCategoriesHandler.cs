using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Responses.Responses_Category;
using Domain.Responses.Shared;
using MediatR;

namespace UseCases.UC_Category.Queries.GetCategories;
public class GetCategoriesHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCategoriesQuery, Result<PaginatedResponse<CategoryDB>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<PaginatedResponse<CategoryDB>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        PaginatedResponse<CategoryDB> gettingCategories = await _unitOfWork.CategoryRepository.GetCategoriesAsync(request.SearchTerm, request.PageNumber, request.PageSize, false, cancellationToken);
        return Result.Success(gettingCategories, "Get Category Successfully");
    }
}