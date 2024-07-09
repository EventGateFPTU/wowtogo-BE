using Ardalis.Result;
using Domain.Models;
using Domain.Responses.Responses_Category;
using MediatR;

namespace UseCases.UC_Category.Commands.CreateCategory
{
	public record CreateCategoryCommand(
				string Name
			) : IRequest<Result<CategoryDB>>;
}
