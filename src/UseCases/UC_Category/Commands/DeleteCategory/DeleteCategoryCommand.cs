using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Category.Commands.DeleteCategory
{
	public record DeleteCategoryCommand(Guid CategoryId) : IRequest<Result>;
}
