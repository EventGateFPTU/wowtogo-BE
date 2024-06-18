using Ardalis.Result;
using Domain.Responses.Responses_Category;
using MediatR;

namespace UseCases.UC_Category.Queries.GetCategories;
public record GetCategoriesQuery(int PageNumber = 1, int PageSize = 10) : IRequest<Result<GetCategoriesResponse>>;