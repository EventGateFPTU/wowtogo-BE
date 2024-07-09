using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Category.Commands.CreateCategory
{
	public record CreateCategoryCommand(
				string name
			) : IRequest<Result>;
}
